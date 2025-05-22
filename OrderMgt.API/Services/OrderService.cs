using Newtonsoft.Json;
using OrderMgt.API.Interfaces;
using OrderMgt.Model.Entities;
using OrderMgt.Model.Models;

namespace OrderMgt.API.Services
{
    /// <summary>
    /// Provides functionality for processing, completing, and managing orders, including applying discounts and
    /// maintaining order history. This service acts as the primary interface for order-related operations within the
    /// system.
    /// </summary>
    /// <remarks>The <see cref="OrderService"/> class is responsible for handling the lifecycle of orders,
    /// including processing orders with applicable discounts, completing orders, and maintaining a record of completed
    /// orders in the order history. It interacts with various repositories to perform these operations and ensures that
    /// business rules, such as applying promotions based on customer segments, are enforced.  This class is designed to
    /// be used in scenarios where order management is required, such as e-commerce platforms or inventory systems. It
    /// relies on dependency injection to access the required repositories and logging functionality.</remarks>
    public class OrderService : IOrderService
    {
        protected readonly IOrderRepository _orderRepository;
        protected readonly IOrderHistoryRepository _orderHistoryRepository;
        protected readonly IPromotionRepository _promotionRepository;
        protected readonly ICustomerRepository _customerRepository;  
        protected readonly ILogger<OrderService> _logger;
        public OrderService(IOrderHistoryRepository orderHistoryRepository, IPromotionRepository promotionRepository,
            IOrderRepository orderRepository,ICustomerRepository customerRepository, ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _orderHistoryRepository = orderHistoryRepository;
            _promotionRepository = promotionRepository;
            _customerRepository = customerRepository;  
            _logger = logger;
        }

        /// <summary>
        /// Processes an order by applying discounts and updating the order details.
        /// </summary>
        /// <remarks>This method retrieves the specified order, applies any applicable discounts, and
        /// updates the order details in the repository.  If the order does not exist, the method returns a failure
        /// response.</remarks>
        /// <param name="orderID">The unique identifier of the order to process.</param>
        /// <returns>A <see cref="BaseResponseModel"/> containing the result of the operation.  If successful, the <see
        /// cref="BaseResponseModel.Data"/> property contains the updated order;  otherwise, <see
        /// cref="BaseResponseModel.Success"/> is <see langword="false"/> and  <see
        /// cref="BaseResponseModel.ErrorMessage"/> provides details about the failure.</returns>
        public async Task<BaseResponseModel> ProcessOrder(int orderID)
        {
            var order = _orderRepository.GetAsync(orderID).Result;

            if (order == null)
                return new BaseResponseModel() { Success = false, ErrorMessage = "Order not found!", Data = null };

            var result = await this.GenerateDiscount(order);
            if (result.Success && result.Data is not null)
            {
                order = (Order)result.Data;

                //update order details
                await _orderRepository.UpdateAsync(order);
            }

            return new BaseResponseModel { Success = true, Data = order };
        }

        /// <summary>
        /// Completes the specified order by marking it as completed, recording its history, and removing it from the
        /// active orders.
        /// </summary>
        /// <remarks>This method performs the following actions: <list type="bullet"> <item>Marks the
        /// order as completed and updates its status, completion date, and the user who completed it.</item>
        /// <item>Creates a historical record of the order in the order history repository.</item> <item>Removes the
        /// completed order from the active orders repository.</item> </list> If the specified order does not exist, the
        /// method returns a failure response with an error message.</remarks>
        /// <param name="orderID">The unique identifier of the order to complete.</param>
        /// <param name="UserID">The identifier of the user completing the order. This value is recorded for auditing purposes.</param>
        /// <returns>A <see cref="BaseResponseModel"/> indicating the success or failure of the operation.  If successful, <see
        /// cref="BaseResponseModel.Success"/> is <see langword="true"/>; otherwise, it is <see langword="false"/> with
        /// an appropriate error message.</returns>
        public async Task<BaseResponseModel> CompleteOrder(int orderID,string UserID)
        {
            var order = _orderRepository.GetAsync(orderID).Result;

            if (order == null)
                return new BaseResponseModel() { Success = false, ErrorMessage = "Order not found!", Data = null };

            //update order details
            order.OrderStatus = "Complete";
            order.CompletedBy = UserID;
            order.DateCompleted = DateTime.Now;

            var orderHistory = new OrderHistory()
            {
                OrderHistoryID = 0,
                TransactionDate = order.TransactionDate,
                OrderStatus = order.OrderStatus,
                DateCompleted = order.DateCompleted,
                CompletedBy = order.CompletedBy ?? "user",
                CreatedBy = order.CreatedBy,
                CustomerID = order.CustomerID,
                DateCreated = order.DateCreated,
                Details = order.Details,
                Discount = order.Discount,
                OrderID = order.OrderID,
                OrderNo = order.OrderNo,
                Total = order.Total,
            };
            await _orderHistoryRepository.AddAsync(orderHistory);//keep copy of order
            //insert accounting part into transactions

            //remove order from orders now that its complete
            await _orderRepository.RemoveAsync(order);

            return new BaseResponseModel { Success = true, Data = null };
        }

        /// <summary>
        /// Generates a discount for the specified order based on the customer's segment and order history.
        /// </summary>
        /// <remarks>This method retrieves the customer's details and order history to determine if a
        /// promotion is applicable  based on the customer's segment and the number of previous orders. If a promotion
        /// is found, the discount  is applied to the order's total. If no promotion is applicable, the order remains
        /// unchanged.</remarks>
        /// <param name="order">The order for which the discount is to be generated. The order must include a valid customer ID.</param>
        /// <returns>A <see cref="BaseResponseModel"/> containing the updated order with the applied discount, if applicable.  If
        /// no discount is applied, the response will indicate success with the original order.  If the customer is not
        /// found, the response will indicate failure with an appropriate error message.</returns>
        public async Task<BaseResponseModel> GenerateDiscount(Order order)
        {
            //get the customer details
            var customer = await _customerRepository.GetAsync(order.CustomerID);//use customers' segmentID to pull all promotions
            
            if (customer is null)
            {
                return new BaseResponseModel() { Success = false, ErrorMessage = "No customer found in the order to apply discount", Data = null };
            }

            //get customers' previous number of orders
            int numberOfPrevOrders = _orderHistoryRepository.Find(o => o.CustomerID == customer.CustomerID).Count();

            //check applicable promotion
            var promotion = _promotionRepository.Find(p => p.SegmentID == customer.SegmentID 
                                                       && p.MinOrders <= numberOfPrevOrders && p.MaxOrders >= numberOfPrevOrders).FirstOrDefault();
            order.OrderStatus = "Processed";

            if (promotion is null)
            {
                return new BaseResponseModel() { Success = true, ErrorMessage = "No promotion found in the order to apply discount", Data = order };
            }
            else
            {
                //apply discount on order
                var discountAmount = (order.Total * promotion.DiscountPercentage) / 100;
                order.Discount = discountAmount;                
                order.Total = order.Total - order.Discount;
            }

            return new BaseResponseModel() { Success = true, ErrorMessage = "", Data = order };
        }

       
    }
}

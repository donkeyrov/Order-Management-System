using Newtonsoft.Json;
using OrderMgt.API.Interfaces;
using OrderMgt.Model.Entities;
using OrderMgt.Model.Models;

namespace OrderMgt.API.Services
{
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

using Microsoft.AspNetCore.Mvc;
using OrderMgt.Model.Entities;
using OrderMgt.Model.Models;

namespace OrderMgt.API.Interfaces
{
    public interface IOrderService
    {        
        public Task<BaseResponseModel> GenerateDiscount(Order order);   
        public Task<BaseResponseModel> ProcessOrder(int orderID);  
        public Task<BaseResponseModel> CompleteOrder(int orderID,string userID);        
    }
}

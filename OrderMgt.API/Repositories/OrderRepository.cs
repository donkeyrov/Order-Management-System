﻿using OrderMgt.API.Data;
using OrderMgt.API.Interfaces;
using OrderMgt.Model.Entities;

namespace OrderMgt.API.Repositories
{
    public class OrderRepository: GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }
    }
}

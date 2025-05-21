using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderMgt.API.Interfaces;
using OrderMgt.API.Repositories;
using OrderMgt.Model.Entities;
using OrderMgt.Model.Models;
using Serialize.Linq.Serializers;
using System.Linq.Expressions;

namespace OrderMgt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        protected readonly IProductRepository repository;
        protected readonly ILogger<ProductController> logger;

        public ProductController(ProductRepository _productRepository, ILogger<ProductController> _logger)
        {
            repository = _productRepository;
            logger = _logger;
        }

        [HttpPost("AddAsync")]
        public async Task<IActionResult> AddAsync(Product product)
        {
            if (await repository.AddAsync(product))
            {
                return Ok(new BaseResponseModel { Success = true, Data = product });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false, ErrorMessage = "" });
            }
        }

        [HttpPost("AddRangeAsync")]
        public async Task<IActionResult> AddRangeAsync(IEnumerable<Product> products)
        {
            if (await repository.AddRangeAsync(products))
            {
                return Ok(new BaseResponseModel { Success = true, Data = products });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false, ErrorMessage = "" });
            }
        }

        [HttpGet("Find")]
        public IActionResult Find(string predicate)
        {
            var serializer = new ExpressionSerializer(new Serialize.Linq.Serializers.JsonSerializer());
            string serializedExpression = predicate.Replace('@', '#');

            Expression<Func<Product, bool>> result = (Expression<Func<Product, bool>>)serializer.DeserializeText(serializedExpression);

            var res = repository.Find(result);
            if (res.Count() > 0)
            {
                return Ok(new BaseResponseModel { Success = true, Data = res });
            }
            else
            {
                return Ok(new BaseResponseModel { Success = false, Data = null });
            }
        }

        [HttpGet("GetAsync")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var res = await repository.GetAsync(id);
            return Ok(new BaseResponseModel { Success = true, Data = res });
        }

        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var res = await repository.GetAllAsync();
            return Ok(new BaseResponseModel { Success = true, Data = res });
        }

        [HttpDelete("RemoveAsync")]
        public async Task<IActionResult> RemoveAsync(Product product)
        {
            if (await repository.RemoveAsync(product))
            {
                return Ok(new BaseResponseModel { Success = true });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }

        }

        [HttpDelete("RemoveRangeAsync")]
        public async Task<IActionResult> RemoveRangeAsync(IEnumerable<Product> products)
        {
            if (await repository.RemoveRangeAsync(products))
            {
                return Ok(new BaseResponseModel { Success = true });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }

        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(Product product)
        {
            if (await repository.UpdateAsync(product))
            {
                return Ok(new BaseResponseModel { Success = true, Data = product });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }

        [HttpPut("UpdateRangeAsync")]
        public async Task<IActionResult> UpdateRangeAsync(IEnumerable<Product> products)
        {
            if (await repository.UpdateRangeAsync(products))
            {
                return Ok(new BaseResponseModel { Success = true, Data = products });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }
    }
}

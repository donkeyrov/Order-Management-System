using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderMgt.API.Interfaces;
using OrderMgt.API.Repositories;
using OrderMgt.Model.Entities;
using OrderMgt.Model.Models;
using Serialize.Linq.Serializers;
using System.Linq.Expressions;
using System.Net.Http.Headers;

namespace OrderMgt.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        protected readonly IProductCategoryRepository repository;
        protected readonly ILogger<ProductCategoryController> logger;

        public ProductCategoryController(ProductCategoryRepository _productCategoryRepository, ILogger<ProductCategoryController> _logger)
        {
            repository = _productCategoryRepository;
            logger = _logger;
        }

        [HttpPost("AddAsync")]
        public async Task<IActionResult> AddAsync(ProductCategory productCategory)
        {
            if (await repository.AddAsync(productCategory))
            {
                return Ok(new BaseResponseModel { Success = true, Data = productCategory });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false, ErrorMessage = "" });
            }
        }

        [HttpPost("AddRangeAsync")]
        public async Task<IActionResult> AddRangeAsync(IEnumerable<ProductCategory> productCategories)
        {
            if (await repository.AddRangeAsync(productCategories))
            {
                return Ok(new BaseResponseModel { Success = true, Data = productCategories });
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

            Expression<Func<ProductCategory, bool>> result = (Expression<Func<ProductCategory, bool>>)serializer.DeserializeText(serializedExpression);

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
        public async Task<IActionResult> RemoveAsync(ProductCategory productCategory)
        {
            if (await repository.RemoveAsync(productCategory))
            {
                return Ok(new BaseResponseModel { Success = true });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }

        }

        [HttpDelete("RemoveRangeAsync")]
        public async Task<IActionResult> RemoveRangeAsync(IEnumerable<ProductCategory> productCategories)
        {
            if (await repository.RemoveRangeAsync(productCategories))
            {
                return Ok(new BaseResponseModel { Success = true });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }

        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(ProductCategory productCategory)
        {
            if (await repository.UpdateAsync(productCategory))
            {
                return Ok(new BaseResponseModel { Success = true, Data = productCategory });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }

        [HttpPut("UpdateRangeAsync")]
        public async Task<IActionResult> UpdateRangeAsync(IEnumerable<ProductCategory> productCategories)
        {
            if (await repository.UpdateRangeAsync(productCategories))
            {
                return Ok(new BaseResponseModel { Success = true, Data = productCategories });
            }
            else
            {
                return NotFound(new BaseResponseModel { Success = false });
            }
        }
    }
}

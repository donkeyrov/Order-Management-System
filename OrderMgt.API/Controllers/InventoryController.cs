using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderMgt.API.Interfaces;
using OrderMgt.API.Repositories;

namespace OrderMgt.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        protected readonly IInventoryRepository repository;
        protected readonly ILogger<InventoryController> logger;

        public InventoryController(InventoryRepository _inventoryRepository,ILogger<InventoryController> _logger)
        {
            repository = _inventoryRepository;
            logger = _logger;
        }
    }
}

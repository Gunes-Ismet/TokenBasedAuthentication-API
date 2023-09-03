using AuthServer.Core.DTO_s;
using AuthServer.Core.Entities;
using AuthServer.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.API.Controllers
{
    [Authorize]
    public class ProductController : CustomBaseController
    {
        private readonly IGenericService<Product, ProductDTO> _service;

        public ProductController(IGenericService<Product,ProductDTO> service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return ActionResultInstance(await _service.GetAllAsync());
        }
        [HttpPost]
        public async Task<IActionResult> SaveProduct(ProductDTO model)
        {
            return ActionResultInstance(await _service.AddAsync(model));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductDTO model) 
        {
            return ActionResultInstance(await _service.UpdateAsync(model,model.Id));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id) 
        { 
            return ActionResultInstance(await _service.RemoveAsync(id));
        }
    }
}

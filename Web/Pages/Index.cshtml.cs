using ECommerce.Application.DTOs;
using ECommerce.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public IndexModel(ILogger<IndexModel> logger, ICategoryService categoryService, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
        }

        public IEnumerable<ProductDto> Products { get; set; } = new List<ProductDto>();
        public IEnumerable<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
        public int? CategoryId { get; set; }

        public async Task OnGetAsync(int? categoryId)
        {
            CategoryId = categoryId;
            Categories = await _categoryService.GetAllCategoriesAsync();

            if (categoryId.HasValue)
            {
                Products = await _productService.GetProductsByCategoryAsync(categoryId.Value);
            }
            else
            {
                Products = await _productService.GetAllProductsAsync();
            }
        }
    }
}

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperShop.Data;
using SuperShop.Data.Entities;
using SuperShop.Helpers;


namespace SuperShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserHelper _userHelper;

        // removed datacontext here and added repository
        //private readonly DataContext _context;

        public ProductsController(
            IProductRepository productRepository,
            IUserHelper userHelper)
        {
            _productRepository = productRepository;
            _userHelper = userHelper;
        }

        // GET: Products
        public IActionResult Index()
        {

            // we organize by name here and not in repository because we know product will have a name
            return View(_productRepository.GetAll().OrderBy(p => p.Name));
        }

        // GET: Products/Details/5
        // int? means it can be null
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            // need id.Value because we used int? 
            var product = await _productRepository.GetByIdAsync(id.Value);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                // TODO: Modify to the user that is logged in 
                product.User = await _userHelper.GetUserByEmailAsync("rafaasfs@gmail.com");
                await _productRepository.CreateAsync(product);
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _productRepository.GetByIdAsync(id.Value);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Modify to the user that is logged in 
                    product.User = await _userHelper.GetUserByEmailAsync("rafaasfs@gmail.com");
                    await _productRepository.UpdateAsync(product);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _productRepository.ExistsAsync(product.Id))
                    {
                        return NotFound();

                    }
                    else
                    {
                        throw;
                    }
                }


                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _productRepository.GetByIdAsync(id.Value);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            await _productRepository.DeleteAsync(product);
            // we dont see to save here because the repository handles that for us
            return RedirectToAction(nameof(Index));
        }
    }
}
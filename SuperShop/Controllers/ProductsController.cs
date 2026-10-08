using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperShop.Data;
using SuperShop.Data.Entities;
using SuperShop.Helpers;
using SuperShop.Models;


namespace SuperShop.Controllers
{
    // only logged in users can access this controller
    [Authorize] 
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserHelper _userHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;



        // removed datacontext here and added repository
        //private readonly DataContext _context;

        public ProductsController(
            IProductRepository productRepository,
            IUserHelper userHelper,
            IImageHelper imageHelper,
            IConverterHelper converterHelper)
        {
            _productRepository = productRepository;
            _userHelper = userHelper;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
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
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                // colocar aqui as imagens
                var path = string.Empty;

                if(model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "products");

                }

                var product = _converterHelper.ToProduct(model, path, true);


                // TODO: Modify to the user that is logged in 
                product.User = await _userHelper.GetUserByEmailAsync("rafaasfs@gmail.com");
                await _productRepository.CreateAsync(product);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        // authorize - restrict access; trying to edit products sends to login page
        // GET: Products/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _productRepository.GetByIdAsync(id.Value);

            if (product == null)
                return NotFound();

            var model = _converterHelper.ToProductViewModel(product);

            return View(model);
        }



        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    var path = model.ImageUrl;

                    // if user uploaded a new image, we need to save it and update the path
                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        path = await _imageHelper.UploadImageAsync(model.ImageFile, "products");                   
                    }

                    var product = _converterHelper.ToProduct(model, path, false);

                    // TODO: Modify to the user that is logged in 
                    product.User = await _userHelper.GetUserByEmailAsync("rafaasfs@gmail.com");
                    await _productRepository.UpdateAsync(product);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _productRepository.ExistsAsync(model.Id))
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

            return View(model);
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
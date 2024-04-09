using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheMerchShop.DataAccess;

using TheMerchShop.Models;

namespace TheMerchShop.Controllers
{
    public class MerchController : Controller
    {
        private readonly MerchShopContext _context;

        public MerchController(MerchShopContext context)
        {
            _context = context;
        }

        public IActionResult Index() => RedirectToAction("List");

        public IActionResult List()
        {
            var merchlist = _context.Merch.ToList();
            return View(merchlist);
    
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Merch merchant = _context.Merch.FirstOrDefault(a => a.MerchID == id);
           
            if (merchant == null)
            {
                return NotFound();
            }

            MerchDetailModel vm = new MerchDetailModel();
            vm.Merch = merchant;
            vm.Vendors = _context.Vendors.ToList();

            ViewData["Vendors"] = vm.VendorNames();

            return (View(vm));
        }
    }



}

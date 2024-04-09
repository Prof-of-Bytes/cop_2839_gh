using Microsoft.AspNetCore.Mvc;
using TheMerchShop.DataAccess;

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
    }
}

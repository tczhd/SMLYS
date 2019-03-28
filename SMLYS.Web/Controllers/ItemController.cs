using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMLYS.ApplicationCore.DTOs.Items;
using SMLYS.ApplicationCore.Interfaces.Services.Items;
using SMLYS.Web.ViewModels.Base;
using SMLYS.Web.ViewModels.Items;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SMLYS.Web.Controllers
{
    [Authorize]
    [Route("Item")]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }


        // GET: /<controller>/
        [Route("{view=Index}")]
        public IActionResult Index(int id, string view)
        {
            if (view == "Item")
            {
                if (id == 0)
                {
                    ViewData["Title"] = $"New Item";
                    ViewData["FormType"] = $"New Item";
                }
                else
                {
                    ViewData["Title"] = $"Edit Item";
                    ViewData["FormType"] = $"Edit Item";

                    return View(view);
                }
            }
            else if (view == "Index")
            {
                ViewData["Title"] = $"Search Item";

                var data = _itemService.SearchItems();
                var viewData = data.Select(p => new ItemViewModel
                {
                    Cost = p.Cost,
                    Description = p.Description,
                    ItemId = p.ItemId,
                    ItemName = p.ItemName
                });
                return View(view, viewData);
            }

            return View(view);
        }

        [HttpPost]
        public IActionResult AddItem(ItemViewModel itemViewModel)
        {
            var result = new BaseResultViewModel();

            if (ModelState.IsValid)
            {
                var model = new ItemModel
                {
                    Cost = itemViewModel.Cost,
                    Description = itemViewModel.Description,
                    ItemName = itemViewModel.ItemName
                };

                var itemResult = _itemService.AddItem(model);
                result.Success = itemResult.Success;
                result.Message = itemResult.Message;
            }
            else {
                result.Message = "Invalid data.";
            }
    
            return View("_SharedResult", result);
        }

    }
}

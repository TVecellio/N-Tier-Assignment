using Domain.IItemRepository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace RazorCrudUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IItemRepository _repo;

        public IndexModel(IItemRepository repo)
        {
            _repo = repo;
        }


        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty]
        public IList<ItemModel> ItemModel { get; set; } = new List<ItemModel>();

        public void OnGet()
        {
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                ItemModel = (IList<ItemModel>)_repo.GetItems(SearchTerm);
            }
            else
            {
                ItemModel = (IList<ItemModel>)_repo.GetItems();
            }
        }
    }
}

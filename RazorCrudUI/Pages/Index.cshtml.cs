using Domain.IItemRepository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;

namespace RazorCrudUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IItemRepository _repo;

        public IndexModel(IItemRepository repo)
        {
            _repo = repo;
        }
        public  IList<ItemModel> ItemModel { get; set; } = default!;
        public void OnGet()
        {
            ItemModel = (IList<ItemModel>)_repo.GetItems();
        }
    }
}

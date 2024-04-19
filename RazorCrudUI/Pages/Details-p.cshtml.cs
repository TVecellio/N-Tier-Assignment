using Domain.IItemRepository;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UI.Pages
{
    [Authorize]
    public class Details_pModel : PageModel
    {
        private readonly IItemRepository _repo;
        public ItemModel Item { get; set; } = default!;
        public Details_pModel(IItemRepository repo)
        {
            _repo = repo;
        }
        public void OnGet(int? id)
        {
            

            if(id==null)
            {
                return;
            }
            var itemmodel = _repo.GetItemByID(id.Value);
            if(itemmodel!=null)
            {
                Item = itemmodel;
            }
        }
    }
}

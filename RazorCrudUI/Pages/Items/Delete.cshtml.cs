using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.IItemRepository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace RazorCrudUI.Pages.Items
{
    public class DeleteModel : PageModel
    {
        private readonly IItemRepository _repo;

        public DeleteModel(IItemRepository repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public ItemModel ItemModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemmodel = _repo.GetItemByID(id.Value);

            if (itemmodel == null)
            {
                return NotFound();
            }
            else
            {
                ItemModel = itemmodel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           _repo.deleteItem(id.Value);

            return RedirectToPage("./Index");
        }
    }
}

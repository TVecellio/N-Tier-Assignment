using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.IItemRepository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace RazorCrudUI.Pages.Items
{
    public class EditModel : PageModel
    {
        private readonly IItemRepository _repo;

        public EditModel(IItemRepository repo)
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
            ItemModel = itemmodel;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!_repo.updateItem(ItemModel))
                return NotFound();

            return RedirectToPage("./Index");
        }
    }
}

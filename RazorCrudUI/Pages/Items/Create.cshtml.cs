using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Models;
using Domain.IItemRepository;
using UI.Utilities;


namespace RazorCrudUI.Pages.Items
{
    public class CreateModel : PageModel
    {
        private readonly IItemRepository _repo;

        private readonly IWebHostEnvironment _env;

        public CreateModel(IItemRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
       

        [BindProperty]
        public ItemModel ItemModel { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if(HttpContext.Request.Form.Files.Count > 0)
            {
                ItemModel.PictureURL = FileHelper.UploadNewImage(_env, HttpContext.Request.Form.Files[0]);
            }

            _repo.insertItem(ItemModel);

            return RedirectToPage("./Index");
        }
    }
}

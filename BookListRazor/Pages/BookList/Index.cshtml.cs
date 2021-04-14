using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Book> Books { get; set; }

        public async Task OnGet()
        {
            Books = await _db.Book.ToListAsync();
        }

        // viewda "<button asp-page-handler="Delete" asp-route-id="@item.Id" >> OnPostDelete custom handlerýmýzý yazýcaz demek oluyor. asp-route-id="@item.Id" ile de custom tag helper ile id yollicam demek
        public async Task<IActionResult> OnPostDelete(int id)
        {
            Book bookToDelete = await _db.Book.FindAsync(id);
            if (bookToDelete == null)
            {
                return NotFound();
            }
            _db.Book.Remove(bookToDelete);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}

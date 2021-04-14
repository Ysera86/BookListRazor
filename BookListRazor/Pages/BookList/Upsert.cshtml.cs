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
    public class UpsertModel : PageModel // Create and Edit are so similar : so why not combine them?
    {
        private ApplicationDbContext _db;
        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            Book = new Book();
            if(id == null) // if null, it is a creation)
            {
                // create
                return Page();
            }
            // update
            Book = await _db.Book.FirstOrDefaultAsync(u => u.Id == id); // = Book = await _db.Book.FindAsync(id);
            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Book.Id == 0)
                {
                    _db.Book.Add(Book);
                }
                else
                {
                    _db.Book.Update(Book);
                }

                //var bookFromDb = await _db.Book.FindAsync(Book.Id); // View da  hidden field olarak Id lmalý yoksa urld olsa da id=0 gelior modelde olmadýðýndan
                //bookFromDb.Name = Book.Name;
                //bookFromDb.Author = Book.Author;
                //bookFromDb.ISBN = Book.ISBN;

                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }

        public void OnGet()
        {

        }

        // sayfa post oldu�unda (create new book ile) post gerekli, her zaman ba��na "On" ekleniyor. sayfadan Book nesnesi gelecek dbye kaydedilmek �zere, burda parametre olarak ekleyeblirdik ama s�n�fa prop olarak ekledik. Onun burada gelece�ini belirtmek i�in Book prop �zerine [BindProperty] ekliyoruz
        public async Task<IActionResult> OnPost() 
        {
            // Server-side validation
            if (ModelState.IsValid) //�uan isim bo� b�rak�l�p kitap eklemeye �al���nca buraya geliyor ve modele bak�p (book s�n�f�na) name required oldu�u i�in validation mesaj d�n�yor. kontrol� client taraf�nda yapmas�n� istiyoruz. Shared klas�r�nde _ValidationScriptsPartial.cshtml var . Buna referans vermemiz gerekli sadece Create.cshtml sayfa alt�nda @section ile dosya ismini script ekleyerek :  Client-side validation
            {
                await _db.Book.AddAsync(Book);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}

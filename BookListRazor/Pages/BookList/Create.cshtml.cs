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

        // sayfa post olduðunda (create new book ile) post gerekli, her zaman baþýna "On" ekleniyor. sayfadan Book nesnesi gelecek dbye kaydedilmek üzere, burda parametre olarak ekleyeblirdik ama sýnýfa prop olarak ekledik. Onun burada geleceðini belirtmek için Book prop üzerine [BindProperty] ekliyoruz
        public async Task<IActionResult> OnPost() 
        {
            // Server-side validation
            if (ModelState.IsValid) //Þuan isim boþ býrakýlýp kitap eklemeye çalýþýnca buraya geliyor ve modele bakýp (book sýnýfýna) name required olduðu için validation mesaj dönüyor. kontrolü client tarafýnda yapmasýný istiyoruz. Shared klasöründe _ValidationScriptsPartial.cshtml var . Buna referans vermemiz gerekli sadece Create.cshtml sayfa altýnda @section ile dosya ismini script ekleyerek :  Client-side validation
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

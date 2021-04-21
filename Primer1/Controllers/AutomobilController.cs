using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Primer1.Models;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;



namespace Primer1.Controllers
{
    [Authorize]
    public class AutomobilController : Controller
    {
        

        private readonly AutomobilContext db;

        public IStringLocalizer<Resource> localizer;

        public AutomobilController(IStringLocalizer<Resource> localizer1)
        {
            this.localizer = localizer1;
        }

        [AllowAnonymous]
        public IActionResult SetCulture(string culture, string sourceUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(
                    new RequestCulture(culture)
                    ),
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                }
                );

            return Redirect(sourceUrl);
        }

        public AutomobilController(AutomobilContext context)
        {
            db = context;
        }
        [AllowAnonymous]
        public FileContentResult CitajSliku(int? id)
        {
            if (id == null)
            {
                return null;
            }
            Automobil a = db.Automobili.Find(id);

            if (a == null)
            {
                return null;
            }

            return File(a.FajlSlike, a.TipFajla);
        }

        // GET: Automobil
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            
            return View(await db.Automobili.ToListAsync());
        }

        // GET: Automobil/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var automobil = await db.Automobili
                .FirstOrDefaultAsync(m => m.AutomobilId == id);
            if (automobil == null)
            {
                return NotFound();
            }

            return View(automobil);
        }

        // GET: Automobil/Create
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Automobil/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Create([Bind("AutomobilId,Marka,Model,Godiste,ZapreminaMotora,Snaga,Gorivo,Karoserija,FajlSlike,TipFajla,Opis,Cena,Kontakt")] Automobil automobil, IFormFile odabranaSlika)
        {
            if (odabranaSlika == null) { ModelState.AddModelError("FajlSlike", "Niste prosledili sliku"); }
            if (ModelState.IsValid)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    await odabranaSlika.CopyToAsync(ms);
                    automobil.FajlSlike = ms.ToArray();
                }
                automobil.TipFajla = odabranaSlika.ContentType;
                try
                {
                    db.Add(automobil);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    ViewBag.Greska = "Greska pri cuvanju podataka";
                }
            }
            return View(automobil);
        }

        // GET: Automobil/Edit/5
        [AllowAnonymous]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var automobil = await db.Automobili.FindAsync(id);
            if (automobil == null)
            {
                return NotFound();
            }
            return View(automobil);
        }

        // POST: Automobil/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AutomobilId,Marka,Model,Godiste,ZapreminaMotora,Snaga,Gorivo,Karoserija,FajlSlike,TipFajla,Opis,Cena,Kontakt")] Automobil automobil, IFormFile odabranaSlika)
        {

            if (id != automobil.AutomobilId)
            {
                return NotFound();
            }

            if (odabranaSlika == null) { ModelState.AddModelError("FajlSlike", "Niste prosledili sliku"); }

            if (ModelState.IsValid)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    await odabranaSlika.CopyToAsync(ms);
                    automobil.FajlSlike = ms.ToArray();
                }
                automobil.TipFajla = odabranaSlika.ContentType;
                try
                {
                    
                    db.Update(automobil);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutomobilExists(automobil.AutomobilId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(automobil);
        }

        // GET: Automobil/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var automobil = await db.Automobili
                .FirstOrDefaultAsync(m => m.AutomobilId == id);
            if (automobil == null)
            {
                return NotFound();
            }

            return View(automobil);
        }

        // POST: Automobil/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var automobil = await db.Automobili.FindAsync(id);
            db.Automobili.Remove(automobil);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutomobilExists(int id)
        {
            return db.Automobili.Any(e => e.AutomobilId == id);
        }
    }
}

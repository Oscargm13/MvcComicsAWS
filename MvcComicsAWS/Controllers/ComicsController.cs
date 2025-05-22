using Microsoft.AspNetCore.Mvc;
using MvcComicsAWS.Models;
using MvcComicsAWS.Repositories;

namespace MvcComicsAWS.Controllers
{
    public class ComicsController : Controller
    {
        private RepositoryComics repo;
        public ComicsController(RepositoryComics repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<Comic> comics = await this.repo.GetComicsAsync();
            return View(comics);
        }

        public async Task<IActionResult> Create(int idcomic)
        {
            Comic comic = await this.repo.FindComic(idcomic);
            return View(comic);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string nombre, string imagen)
        {
            await this.repo.InsertComic(nombre, imagen);
            return RedirectToAction("Index");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using MvcComicsAWS.Models;
using MvcComicsAWS.Repositories;
using MvcComicsAWS.Services;

namespace MvcComicsAWS.Controllers
{
    public class ComicsController : Controller
    {
        private RepositoryComics repo;
        private ServiceStorageS3 service;
        public ComicsController(RepositoryComics repo, ServiceStorageS3 service)
        {
            this.repo = repo;
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Comic> comics = await this.repo.GetComicsAsync();
            return View(comics);
        }

        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(string nombre, IFormFile imagen)
        {
            if (imagen != null)
            {
                using (Stream stream = imagen.OpenReadStream())
                {
                    await this.service.UploadFileAsync(imagen.FileName, stream);
                }

                string urlImagen = $"https://bucket-examen-oscar.s3.amazonaws.com/{imagen.FileName}";
                await this.repo.InsertComic(nombre, urlImagen);
            }

            return RedirectToAction("Index");
        }

        public IActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile
        (IFormFile file)
        {
            using (Stream stream = file.OpenReadStream())
            {
                await this.service.UploadFileAsync
                (file.FileName, stream);
            }
            return RedirectToAction("Index");
        }
    }
}

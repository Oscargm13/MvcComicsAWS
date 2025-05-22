using Microsoft.EntityFrameworkCore;
using MvcComicsAWS.Data;
using MvcComicsAWS.Models;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MvcComicsAWS.Repositories
{
    public class RepositoryComics
    {
        private ComicsContext context;
        public RepositoryComics(ComicsContext context)
        {
            this.context = context;
        }

        public async Task<List<Comic>> GetComicsAsync()
        {
            return await this.context.Comics.ToListAsync();
        }

        public async Task<Comic> FindComic(int idcomic)
        {
            return this.context.Comics.FirstOrDefault(c => c.IdComic == idcomic);
        }

        private async Task<int> GetMaxIdComicAsync()
        {
            return await this.context.Comics.MaxAsync(x => x.IdComic) + 1;
        }

        public async Task InsertComic(string nombre, string imagen)
        {
            Comic c = new Comic();
            c.IdComic = await this.GetMaxIdComicAsync();
            c.Nombre = nombre;
            c.Imagen = imagen;
            await this.context.Comics.AddAsync(c);
            await this.context.SaveChangesAsync();
        }
    }
}

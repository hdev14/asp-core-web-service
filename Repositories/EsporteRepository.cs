using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using web_service.database;
using web_service.Models;


namespace web_service.Repositories
{
    public class EsporteRepository
    {
        private readonly WebServiceContext context;

        public EsporteRepository(WebServiceContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateEsporteAsync(Esporte esporte)
        {
            context.Esportes.Add(esporte);

            if (await context.SaveChangesAsync() != 0)
                return true;

            return false;
        }

        public async Task<bool> UpdateEsporteAsync(int id, Esporte e)
        {
            var esporte = await this.FindEsporteAsync(id);

            if (esporte != null)
            {
                esporte.Nome = e.Nome;
                esporte.NumeroJogadores = e.NumeroJogadores;
                esporte.NumeroJogadoresTime = e.NumeroJogadoresTime;

                return (await context.SaveChangesAsync() != 0);
            }

            return false;
        }

        public async Task<bool> DeleteEsporteAsync(int id)
        {
            var esporte = await this.FindEsporteAsync(id);

            if (esporte != null)
            {
                context.Esportes.Remove(esporte);
                return (await context.SaveChangesAsync() != 0);
            }

            return false;
        }

        public async Task<Esporte> FindEsporteAsync(int id)
        {
            return await context.Esportes.FindAsync(id);
        }

        public async Task<List<Esporte>> GetEsportesAsync()
        {
            return await context.Esportes.ToListAsync();
        }
    }
}
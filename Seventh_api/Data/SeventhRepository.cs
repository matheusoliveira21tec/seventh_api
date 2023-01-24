using System;
using System.Collections.Generic;
using System.Linq;
using Seventh_api.Models;

namespace Seventh_api.Data
{
    public class SeventhRepository : ISeventhRepository
    {
        private readonly AppBdContext _context;

        public SeventhRepository(AppBdContext context)
        {
            _context = context;
        }

        public void CreateVideo(Guid servidorId, Video video)
        {
            video.IdServidor = servidorId;
            _context.Videos.Add(video);
        }

        public void CreateServidor(Servidor servidor)
        {
            _context.Servidores.Add(servidor);
        }

        public IEnumerable<Servidor> GetAllServidores()
        {
            return _context.Servidores.ToList();
        }

        public Video GetVideo(Guid servidorId, Guid videoId) => _context.Videos
            .Where(video => video.IdServidor == servidorId && video.ID == videoId).FirstOrDefault();
        public Servidor GetServidor(Guid servidorId) => _context.Servidores
          .Where(servidor => servidor.ID == servidorId).FirstOrDefault();

        public IEnumerable<Video> GetVideosDeServidor(Guid servidorId)
        {
            return _context.Videos
                .Where(video => video.IdServidor == servidorId);
        }

        public bool ServidorDisponivel(Guid servidorId)
        {
            Servidor servidor =  _context.Servidores.FirstOrDefault(s => s.ID == servidorId);
            bool disponivel = servidor.Disponivel ? true : false;
            return disponivel;
        }
        public bool ServidorExiste(Guid servidorId)
        {
            return _context.Servidores.Any(servidor => servidor.ID == servidorId);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void RemoveServidor(Servidor servidor)
        {
            _context.Remove(servidor);
        }
    }
}

using System.Collections.Generic;
using Seventh_api.Models;

namespace Seventh_api.Data;

public interface ISeventhRepository
{
    void SaveChanges();
    void RemoveServidor(Servidor servidor);
    IEnumerable<Servidor> GetAllServidores();
    void CreateServidor(Servidor servidor);
    bool ServidorDisponivel(Guid servidorId);
    bool ServidorExiste(Guid servidorId);
    IEnumerable<Video> GetVideosDeServidor(Guid servidorId);
    Video GetVideo(Guid servidorId, Guid videoId);
    Servidor GetServidor(Guid servidorId);
    void CreateVideo(Guid servidorId, Video video);
}

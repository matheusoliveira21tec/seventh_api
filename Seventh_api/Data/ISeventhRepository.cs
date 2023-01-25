using Seventh_api.Models;

namespace Seventh_api.Data;

public interface ISeventhRepository
{
    void SaveChanges();
    void RemoveServidor(Servidor servidor);
    void RemoveVideo(Guid servidorId, Guid videoId);
    IEnumerable<Servidor> GetAllServidores();
    IEnumerable<Video> GetAllVideos();
    IEnumerable<Video> GetVideos(Guid servidorId);
    void CreateServidor(Servidor servidor);
    bool ServidorDisponivel(Guid servidorId);
    bool ServidorExiste(Guid servidorId);
    bool VideoExiste(Guid servidorId, Guid videoId);
    Video GetVideo(Guid servidorId, Guid videoId);
    Servidor GetServidor(Guid servidorId);
    void CreateVideo(Guid servidorId, Video video);
   
}

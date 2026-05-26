using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PlanoriaCapstone.Models;


namespace PlanoriaCapstone.Bll.Interface
{
    public interface IArchivoService
    {
        Task<ArchivoSubido> SubirArchivoAsync(
            int idUsuario,
            int? idCurso,
            int cantidadFlashcards, 
            int cantidadPreguntas,
            IFormFile archivo);

        Task<IEnumerable<ArchivoSubido>> ObtenerArchivosUsuarioAsync(
            int idUsuario);

        Task<ArchivoSubido?> ObtenerArchivoPorIdAsync(
            int idArchivo);

        Task<bool> EliminarArchivoAsync(
            int idArchivo,
            int idUsuario);
    }
}

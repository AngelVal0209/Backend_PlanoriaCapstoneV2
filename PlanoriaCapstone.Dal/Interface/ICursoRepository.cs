using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Dal.Interface
{
    public interface ICursoRepository
    {
        // =====================================
        // CREAR
        // =====================================

        Task CrearAsync(Curso curso);

        // =====================================
        // GET ALL
        // =====================================

        Task<IEnumerable<Curso>>
            ObtenerTodosAsync();

        // =====================================
        // GET BY ID
        // =====================================

        Task<Curso?>
            ObtenerPorIdAsync(
                int idCurso);

        // =====================================
        // DELETE
        // =====================================

        Task EliminarAsync(
            Curso curso);

        // =====================================
        // SAVE CHANGES
        // =====================================

        Task GuardarCambiosAsync();
    }
}
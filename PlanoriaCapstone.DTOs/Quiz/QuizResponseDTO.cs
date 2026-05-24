using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Quiz
{
    public class QuizResponseDTO
    {
        public int IdQuiz { get; set; }

        public int IdAnalisis { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public List<PreguntaQuizDTO> Preguntas { get; set; }
            = new();
    }
}

namespace PlanoriaCapstone.DTOs.Auth.Requests
{
    public class RegisterRequestDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PreferredLanguage { get; set; } = "en";
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.DTOs.Archivo;
using System.Text;
using System.Text.Json;

namespace PlanoriaCapstone.Bll.Service
{
    public class GeminiService : IIAService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _model;
        private readonly ILogger<GeminiService> _logger;

<<<<<<< HEAD
        public GeminiService(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
=======
        public GeminiService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<GeminiService> logger)
>>>>>>> 80b1d727e3a30f8d8a54dd1c3b6744a7b30d6864
        {
            _httpClient = httpClientFactory.CreateClient();
            _logger = logger;

<<<<<<< HEAD
            _apiKey = configuration["Gemini:ApiKey"]
                ?? Environment.GetEnvironmentVariable("GEMINI_API_KEY")
                ?? throw new ArgumentNullException(
                    "Gemini API Key no configurada.");
=======
            _apiKey = !string.IsNullOrWhiteSpace(configuration["Gemini:ApiKey"])
                ? configuration["Gemini:ApiKey"]!
                : Environment.GetEnvironmentVariable("GEMINI_API_KEY")
                  ?? throw new ArgumentNullException(
                      "Gemini API Key no configurada.");

            _model = !string.IsNullOrWhiteSpace(configuration["Gemini:Model"])
                ? configuration["Gemini:Model"]!
                : "gemini-2.5-flash"; // Default to gemini-2.5-flash which is efficient and active
>>>>>>> 80b1d727e3a30f8d8a54dd1c3b6744a7b30d6864
        }

        public async Task<AnalisisDocumentoDto> AnalizarTextoAsync(
            string texto,
            int cantidadFlashcards,
            int cantidadPreguntas)
        {
<<<<<<< HEAD
            if (texto.Length > 15000)
            {
                texto = texto.Substring(0, 15000);
            }

            var url =
                "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent";

            var prompt = $@"
Eres un sistema educativo avanzado especializado en generación de material de estudio.
=======
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/{_model}:generateContent";

            var prompt = $@"
Eres un sistema educativo avanzado. Analiza el texto proporcionado y genera un resumen, temas clave, 5 flashcards y 5 preguntas de quiz en JSON válido. NO incluyas caracteres especiales ni marcas de código, solo JSON puro.

Formato JSON requerido:
{{
  ""resumen"": ""texto"",
  ""temasDetectados"": [""tema1"", ""tema2""],
  ""flashcards"": [
    {{ ""pregunta"": ""pregunta"", ""respuesta"": ""respuesta"" }}
  ],
  ""quizzes"": [
    {{ ""pregunta"": ""pregunta"", ""opciones"": [""a"", ""b"", ""c"", ""d""], ""respuestaCorrecta"": ""a"", ""explicacion"": ""texto"" }}
  ]
}}
>>>>>>> 80b1d727e3a30f8d8a54dd1c3b6744a7b30d6864

Analiza el siguiente texto y responde EXCLUSIVAMENTE en JSON válido.

REGLAS OBLIGATORIAS:
- NO uses markdown.
- NO uses bloques ```json.
- NO agregues explicaciones fuera del JSON.
- SOLO devuelve JSON válido.
- Genera EXACTAMENTE {cantidadFlashcards} flashcards.
- Genera EXACTAMENTE {cantidadPreguntas} preguntas quiz.
- Cada quiz debe tener EXACTAMENTE 4 opciones.
- La respuesta correcta debe coincidir exactamente con una opción.

FORMATO JSON:

{{
  ""Resumen"": ""string"",
  ""TemasDetectados"": [
    ""tema1"",
    ""tema2""
  ],
  ""Flashcards"": [
    {{
      ""Pregunta"": ""string"",
      ""Respuesta"": ""string""
    }}
  ],
  ""Quizzes"": [
    {{
      ""Pregunta"": ""string"",
      ""Opciones"": [
        ""A"",
        ""B"",
        ""C"",
        ""D""
      ],
      ""RespuestaCorrecta"": ""string"",
      ""Explicacion"": ""string""
    }}
  ]
}}

TEXTO:
{texto}
";

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new
                            {
                                text = prompt
                            }
                        }
                    }
                },
                generationConfig = new
                {
<<<<<<< HEAD
                    temperature = 0.4,
                    topK = 32,
                    topP = 1,
                    maxOutputTokens = 4096
=======
                    temperature = 0.2,
                    maxOutputTokens = 8192
>>>>>>> 80b1d727e3a30f8d8a54dd1c3b6744a7b30d6864
                }
            };

            var request = new HttpRequestMessage(
                HttpMethod.Post,
                url);

            request.Headers.Add("x-goog-api-key", _apiKey);

            request.Content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
<<<<<<< HEAD
                throw new Exception(
                    $"Error Gemini API: {json}");
            }

            using var doc = JsonDocument.Parse(json);
=======
                _logger.LogError("Gemini API error: {StatusCode} - {Response}", response.StatusCode, json);
                throw new Exception($"Error de Gemini API ({response.StatusCode}): {json}");
            }

            using var doc = JsonDocument.Parse(json);
            if (!doc.RootElement.TryGetProperty("candidates", out var candidates) || candidates.GetArrayLength() == 0)
            {
                _logger.LogWarning("Gemini returned no candidates. Response: {Response}", json);
                throw new Exception("La IA no devolvió ninguna respuesta válida.");
            }
>>>>>>> 80b1d727e3a30f8d8a54dd1c3b6744a7b30d6864

            var raw = doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

<<<<<<< HEAD
            if (string.IsNullOrWhiteSpace(raw))
            {
                throw new Exception(
                    "Gemini devolvió respuesta vacía.");
            }

            raw = raw
                .Replace("```json", "")
                .Replace("```", "")
                .Trim();

            var resultado =
                JsonSerializer.Deserialize<AnalisisDocumentoDto>(
                    raw,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            if (resultado == null)
            {
                throw new Exception(
                    "No se pudo deserializar respuesta IA.");
            }

            return resultado;
=======
            if (!string.IsNullOrEmpty(raw))
            {
                raw = raw.Replace("```json", "").Replace("```", "").Trim();

                // Remove invalid control characters and BOM
                raw = System.Text.RegularExpressions.Regex.Replace(raw, @"[\u0000-\u001F\u0080-\u009F]", "");

                // Try to find JSON object in the response
                var start = raw.IndexOf('{');
                var end = raw.LastIndexOf('}');
                if (start >= 0 && end > start)
                    raw = raw.Substring(start, end - start + 1);
            }

            try
            {
                return JsonSerializer.Deserialize<AnalisisDocumentoDto>(
                           raw!,
                           new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                       ) ?? new AnalisisDocumentoDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al deserializar respuesta de Gemini. Raw: {RawResponse}", raw);
                return new AnalisisDocumentoDto
                {
                    Resumen = "No se pudo analizar el documento. Intente con un archivo más pequeño o diferente.",
                    TemasDetectados = new List<string>(),
                    Flashcards = new List<FlashcardDto>(),
                    Quizzes = new List<QuizDto>()
                };
            }
>>>>>>> 80b1d727e3a30f8d8a54dd1c3b6744a7b30d6864
        }
    }
}
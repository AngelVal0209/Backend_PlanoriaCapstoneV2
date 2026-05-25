using Microsoft.Extensions.Configuration;
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

        public GeminiService(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();

            _apiKey = configuration["Gemini:ApiKey"]
                ?? Environment.GetEnvironmentVariable("GEMINI_API_KEY")
                ?? throw new ArgumentNullException(
                    "Gemini API Key no configurada.");
        }

        public async Task<AnalisisDocumentoDto> AnalizarTextoAsync(
            string texto,
            int cantidadFlashcards,
            int cantidadPreguntas)
        {
            if (texto.Length > 15000)
            {
                texto = texto.Substring(0, 15000);
            }

            var url =
                "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent";

            var prompt = $@"
Eres un sistema educativo avanzado especializado en generación de material de estudio.

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
                    temperature = 0.4,
                    topK = 32,
                    topP = 1,
                    maxOutputTokens = 4096
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
                throw new Exception(
                    $"Error Gemini API: {json}");
            }

            using var doc = JsonDocument.Parse(json);

            var raw = doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

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
        }
    }
}
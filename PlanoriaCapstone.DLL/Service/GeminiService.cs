using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.DTOs.Archivo;
using System.Net.Http.Headers;
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

        public GeminiService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<GeminiService> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _logger = logger;

            _apiKey = !string.IsNullOrWhiteSpace(configuration["Gemini:ApiKey"])
                ? configuration["Gemini:ApiKey"]!
                : Environment.GetEnvironmentVariable("GEMINI_API_KEY")
                  ?? throw new ArgumentNullException(
                      "Gemini API Key no configurada.");

            _model = !string.IsNullOrWhiteSpace(configuration["Gemini:Model"])
                ? configuration["Gemini:Model"]!
                : "gemini-2.5-flash"; // Default to gemini-2.5-flash which is efficient and active
        }

        public async Task<AnalisisDocumentoDto> AnalizarTextoAsync(string texto)
        {
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

Texto a analizar:
{texto}";

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = prompt }
                        }
                    }
                },
                generationConfig = new
                {
                    temperature = 0.2,
                    maxOutputTokens = 8192
                }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("x-goog-api-key", _apiKey);
            request.Content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Gemini API error: {StatusCode} - {Response}", response.StatusCode, json);
                throw new Exception($"Error de Gemini API ({response.StatusCode}): {json}");
            }

            using var doc = JsonDocument.Parse(json);
            if (!doc.RootElement.TryGetProperty("candidates", out var candidates) || candidates.GetArrayLength() == 0)
            {
                _logger.LogWarning("Gemini returned no candidates. Response: {Response}", json);
                throw new Exception("La IA no devolvió ninguna respuesta válida.");
            }

            var raw = candidates[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

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
        }
    }
}
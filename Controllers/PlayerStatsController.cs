using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TesteGPT.API.Models;

namespace TesteGPT.API.Controllers
{
    [ApiController]
    [Route("api/player-stats")]
    public class PlayerStatsController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        public PlayerStatsController(HttpClient httpClient) {  
        _httpClient = httpClient; 
        }
        [HttpGet]
        public async Task<IActionResult> Get(string text, [FromServices] IConfiguration configuration) 
        {
            //Espaço para buscar a chave de configuração do token da API (openAI) de onde você tiver guardado.
            //Por questões de segurança, ao subir para o git, não vou colocar minha chave, você pode conseguir uma em https://platform.openai.com/account/api-keys
            var token = configuration.GetValue<string>("");           
            //Montando o token de autorização
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //Inicializando o modelo de entrada
            var model = new ChatGPTInputModel(text);

            //Serializo o modelo
            var requestBody = JsonSerializer.Serialize(model);

            //Inicio uma stringContent
            var content = new StringContent(requestBody, Encoding.UTF8,"application/json");

            //Realizo o post na api da openai
            var response = await _httpClient.PostAsync("https://api.openai.com/v1/completions", content);

            //Deserializo a resposta da API
            var result = await response.Content.ReadFromJsonAsync<ChatGptViewModel>();

            //Obtenho o primeiro elemento dentro do choices (Que é a resposta do chatGPT de fato)
            var promptResponse = result.choices.First();

            //Realizo o tratamento do response para ter uma visualização mais limpa da resposta
            return Ok (promptResponse.text.Replace("/n", "").Replace("/t",""));
        }
    }
}

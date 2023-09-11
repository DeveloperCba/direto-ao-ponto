using System.Text.Json;
using ConsoleDependencyInjection.Helpers;
using ConsoleDependencyInjection.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ConsoleDependencyInjection.Services;

public class CepService : ICepService
{
    private readonly AppSettings _appSettings;
    private readonly ILogger<CepService> _logger;
    private readonly HttpClient _httpClient;
    public CepService(
        ILogger<CepService> logger,
        IOptions<AppSettings> appSettings,
        IHttpClientFactory httpClient)
    {
        _logger = logger;
        _httpClient = httpClient.CreateClient();
        _appSettings = appSettings.Value;
    }

    public async Task<CepResponse> GetCepAsync(CepRequest request)
    {
        var url = string.Format(_appSettings.Url, request.Cep);

        var response = await _httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("Cep retornou com sucesso!!!");
            return JsonSerializer.Deserialize<CepResponse>(await response.Content.ReadAsStringAsync());
        }

        _logger.LogError("Falha ao retornar o Cep!!!");
        return null;
    }
}
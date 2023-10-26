using ConsoleDependencyInjection.Models;

namespace ConsoleDependencyInjection.Services;

public interface  ICepService
{
    Task<CepResponse> GetCepAsync(CepRequest request);
}
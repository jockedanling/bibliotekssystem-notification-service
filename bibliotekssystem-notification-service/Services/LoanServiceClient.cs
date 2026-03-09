using bibliotekssystem_notification_service.Models;

namespace bibliotekssystem_notification_service.Services;

public class LoanServiceClient
{
    private readonly HttpClient _httpClient;
    
    public LoanServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<LoanResponse>> GetLoansByBorrower(int borrowerId)
    {
        var response = await _httpClient.GetAsync($"api/Loans/borrower/{borrowerId}");

        if (!response.IsSuccessStatusCode)
            return new List<LoanResponse>();
        
        return await response.Content.ReadFromJsonAsync<List<LoanResponse>>() ?? new List<LoanResponse>();
    }
}
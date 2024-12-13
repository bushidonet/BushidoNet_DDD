using AMochika.Core.Interfaces;

namespace AMochika.Core.Services;

public class ClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IPurchaseRepository _purchaseRepository;

    public ClientService(IClientRepository clientRepository, IPurchaseRepository purchaseRepository)
    {
        _clientRepository = clientRepository;
        _purchaseRepository = purchaseRepository;
    }
    
    public async Task<bool> ValidateMonthlyPayment(int clientId)
    {
        var client = await _clientRepository.GetByIdAsync(clientId);
        if (client == null) return false;

        var lastPurchase = await _purchaseRepository.GetPurchaseByClientIdAsync(clientId);
        if (lastPurchase == null || lastPurchase.LastOrDefault()?.PurchaseDate.Month != DateTime.Now.Month)
        {
            return false;
        }
        // If last purchase is from the current month, we consider it as paid
        return true; 
    }
}
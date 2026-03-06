namespace CantineKata.Core;

public interface ICalculateurPriseEnCharge
{
    decimal ObtenirPriseEnCharge(TypeClient typeClient);
}

public class CalculateurPriseEnCharge : ICalculateurPriseEnCharge
{
    public decimal ObtenirPriseEnCharge(TypeClient typeClient)
    {
        return typeClient switch
        {
            TypeClient.Interne => 7.50m,
            TypeClient.Stagiaire => 10.00m,
            TypeClient.Prestataire => 6.00m,
            _ => 0m 
        };
    }
}
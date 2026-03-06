namespace CantineKata.Core;

public interface ICatalogueProduits
{
    decimal ObtenirPrix(TypeProduit type);
}

public class CatalogueProduits : ICatalogueProduits
{
    public decimal ObtenirPrix(TypeProduit type)
    {
        return type switch
        {
            TypeProduit.Boisson => 1.00m,
            TypeProduit.Fromage => 1.00m,
            TypeProduit.Pain => 0.40m,
            TypeProduit.PetiteSalade => 4.00m,
            TypeProduit.GrandeSalade => 6.00m,
            TypeProduit.PortionFruit => 1.00m,
            TypeProduit.Entree => 3.00m,  
            TypeProduit.Plat => 6.00m,    
            TypeProduit.Dessert => 3.00m, 
            _ => 0m 
        };
    }
}
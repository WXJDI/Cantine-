using System;

namespace CantineKata.Core;

public class Caisse
{
    public Ticket GenererTicket(Client client, Repas repas)
    {
        decimal total = 0m;

        foreach (var produit in repas.Produits)
        {
            total += ObtenirPrixProduit(produit.Type);
        }
        if (client.Type == TypeClient.Interne)
        {
            total -= 7.50m;
        }

        if (total < 0) 
        {
            total = 0m;
        }

        return new Ticket(total);

    }

    private decimal ObtenirPrixProduit(TypeProduit type)
    {
        return type switch
        {
            TypeProduit.Boisson => 1.00m,
            TypeProduit.Pain => 0.40m,
            TypeProduit.PetiteSalade => 4.00m,
            TypeProduit.GrandeSalade => 6.00m,
            _ => 0m 
            
        };
    }
}
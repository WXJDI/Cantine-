using System;

namespace CantineKata.Core;

public class Caisse
{
    public Ticket GenererTicket(Client client, Repas repas)
    {
        decimal total = 0m;
        if (client.Type == TypeClient.VIP)
        {
            return new Ticket(0m);
        }

        foreach (var produit in repas.Produits)
        {
            total += ObtenirPrixProduit(produit.Type);
        }
        if (client.Type == TypeClient.Interne)
        {
            total -= 7.50m;
        }

        else if (client.Type == TypeClient.Stagiaire)
        {
            total -= 10.00m;
        }

        return new Ticket(Math.Max(0, total));

    }

    private decimal ObtenirPrixProduit(TypeProduit type)
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
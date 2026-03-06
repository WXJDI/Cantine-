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
        int nbEntrees = repas.Produits.Count(p => p.Type == TypeProduit.Entree);
        int nbPlats = repas.Produits.Count(p => p.Type == TypeProduit.Plat);
        int nbDesserts = repas.Produits.Count(p => p.Type == TypeProduit.Dessert);
        int nbPains = repas.Produits.Count(p => p.Type == TypeProduit.Pain);

        // Le nombre de formules est limité par l'article le moins présent
        int nbFormules = Math.Min(Math.Min(nbEntrees, nbPlats), Math.Min(nbDesserts, nbPains));

        if (nbFormules > 0)
        {
            // Chaque formule fait économiser 2.40€ (12.40€ prix normal - 10.00€ prix formule)
            total -= (2.40m * nbFormules);
        }


        if (client.Type == TypeClient.Interne)
        {
            total -= 7.50m;
        }

        else if (client.Type == TypeClient.Stagiaire)
        {
            total -= 10.00m;
        }
        
        else if (client.Type == TypeClient.Prestataire)
        {
            total -= 6.00m;
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
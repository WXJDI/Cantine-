using System;
using System.Linq;

namespace CantineKata.Core;

public class Caisse
{
    private readonly ICatalogueProduits _catalogue;
    private readonly ICalculateurPriseEnCharge _calculateurPriseEnCharge;

    public Caisse()
    {
        _catalogue = new CatalogueProduits();
        _calculateurPriseEnCharge = new CalculateurPriseEnCharge();
    }

    public Ticket GenererTicket(Client client, Repas repas)
    {
        if (client.Type == TypeClient.VIP)
        {
            return new Ticket(0m);
        }

        decimal total = 0m;

        foreach (var produit in repas.Produits)
        {
            total += _catalogue.ObtenirPrix(produit.Type);
        }

        int nbEntrees = repas.Produits.Count(p => p.Type == TypeProduit.Entree);
        int nbPlats = repas.Produits.Count(p => p.Type == TypeProduit.Plat);
        int nbDesserts = repas.Produits.Count(p => p.Type == TypeProduit.Dessert);
        int nbPains = repas.Produits.Count(p => p.Type == TypeProduit.Pain);

        int nbFormules = Math.Min(Math.Min(nbEntrees, nbPlats), Math.Min(nbDesserts, nbPains));

        if (nbFormules > 0)
        {
            total -= (2.40m * nbFormules);
        }

        
        total -= _calculateurPriseEnCharge.ObtenirPriseEnCharge(client.Type);

        return new Ticket(Math.Max(0, total));
    }
}
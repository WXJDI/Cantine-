using System.Collections.Generic;

namespace CantineKata.Core;

public record Client(TypeClient Type);

public record Produit(string Nom, TypeProduit Type);

public record Ticket(decimal TotalAPayer);

public class Repas
{
    public List<Produit> Produits { get; } = new();

    public void AjouterProduit(Produit produit)
    {
        Produits.Add(produit);
    }
}
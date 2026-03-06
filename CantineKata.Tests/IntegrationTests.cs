using Xunit;
using CantineKata.Core;

namespace CantineKata.Tests;

public class IntegrationTests
{
    [Fact]
    public void GenererTicket_Scenario1_FormulePlusSupplements_SansReduction()
    {
        // Arrange : Un visiteur prend une formule complète + 1 boisson + 1 fromage
        var client = new Client(TypeClient.Visiteur);
        var repas = new Repas();
        repas.AjouterProduit(new Produit("Salade", TypeProduit.Entree));
        repas.AjouterProduit(new Produit("Steak", TypeProduit.Plat));
        repas.AjouterProduit(new Produit("Glace", TypeProduit.Dessert));
        repas.AjouterProduit(new Produit("Baguette", TypeProduit.Pain));
        
        repas.AjouterProduit(new Produit("Cola", TypeProduit.Boisson));
        repas.AjouterProduit(new Produit("Camembert", TypeProduit.Fromage));

        var caisse = new Caisse();

        // Act
        Ticket ticket = caisse.GenererTicket(client, repas);

        // Assert

        Assert.Equal(12.00m, ticket.TotalAPayer);
    }

    [Fact]
    public void GenererTicket_Scenario2_FormulePlusSupplements_AvecReductionInterne()
    {
        // Arrange : Un client interne prend la même chose (Formule + 2 suppléments)
        var client = new Client(TypeClient.Interne);
        var repas = new Repas();
        repas.AjouterProduit(new Produit("Salade", TypeProduit.Entree));
        repas.AjouterProduit(new Produit("Steak", TypeProduit.Plat));
        repas.AjouterProduit(new Produit("Glace", TypeProduit.Dessert));
        repas.AjouterProduit(new Produit("Baguette", TypeProduit.Pain));
        
        repas.AjouterProduit(new Produit("Cola", TypeProduit.Boisson));
        repas.AjouterProduit(new Produit("Camembert", TypeProduit.Fromage));

        var caisse = new Caisse();

        // Act
        Ticket ticket = caisse.GenererTicket(client, repas);

        // Assert
        // Calcul attendu : Formule (10€) + Suppléments (2€) = 12€ Brut. 
        // 12€ - 7.50€ (Réduction Interne) = 4.50€
        Assert.Equal(4.50m, ticket.TotalAPayer);
    }

    [Fact]
    public void GenererTicket_Scenario3_ClientGourmand_DeuxFormulesSurLeMemePlateau()
    {
        // Arrange : Un Prestataire prend de quoi faire 2 formules complètes + 1 entrée en trop
        var client = new Client(TypeClient.Prestataire);
        var repas = new Repas();
        
        // 3 Entrées (donc 1 en supplément)
        repas.AjouterProduit(new Produit("Salade", TypeProduit.Entree));
        repas.AjouterProduit(new Produit("Soupe", TypeProduit.Entree));
        repas.AjouterProduit(new Produit("Oeuf", TypeProduit.Entree)); 
        
        // 2 Plats, 2 Desserts, 2 Pains
        repas.AjouterProduit(new Produit("Steak", TypeProduit.Plat));
        repas.AjouterProduit(new Produit("Poulet", TypeProduit.Plat));
        repas.AjouterProduit(new Produit("Glace", TypeProduit.Dessert));
        repas.AjouterProduit(new Produit("Tarte", TypeProduit.Dessert));
        repas.AjouterProduit(new Produit("Pain", TypeProduit.Pain));
        repas.AjouterProduit(new Produit("Pain", TypeProduit.Pain));

        var caisse = new Caisse();

        // Act
        Ticket ticket = caisse.GenererTicket(client, repas);

        // Assert
        // Calcul attendu : 2 Formules (20€) + 1 Entrée supplémentaire (3€) = 23€ Brut.
        // 23€ - 6.00€ (Réduction Prestataire) = 17.00€
        Assert.Equal(17.00m, ticket.TotalAPayer);
    }
}
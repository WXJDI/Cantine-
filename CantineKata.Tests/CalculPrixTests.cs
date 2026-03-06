using Xunit;
using CantineKata.Core; 

namespace CantineKata.Tests;

public class CalculPrixTests
{
    // 1. TESTS DES PRIX DE BASE (Règle 2)


    [Fact]
    public void GenererTicket_VisiteurAvecUneBoisson_RetournePrixDeLaBoisson()
    {
        var client = new Client(TypeClient.Visiteur);
        var repas = new Repas();
        repas.AjouterProduit(new Produit("Canette Cola", TypeProduit.Boisson)); 

        var caisse = new Caisse();
        Ticket ticket = caisse.GenererTicket(client, repas);

        Assert.Equal(1.00m, ticket.TotalAPayer);
    }

    [Fact]
    public void GenererTicket_VisiteurAvecUnPain_RetournePrixDuPain()
    {
        var client = new Client(TypeClient.Visiteur);
        var repas = new Repas();
        repas.AjouterProduit(new Produit("Baguette", TypeProduit.Pain));

        var caisse = new Caisse();
        Ticket ticket = caisse.GenererTicket(client, repas);

        Assert.Equal(0.40m, ticket.TotalAPayer); 
    }

    [Theory]
    [InlineData(TypeClient.VIP, 0.0)]          
    [InlineData(TypeClient.Interne, 2.50)]     
    [InlineData(TypeClient.Prestataire, 4.00)] 
    [InlineData(TypeClient.Stagiaire, 0.0)]    
    public void GenererTicket_AppliqueLaBonneReductionSelonLeClient(TypeClient typeClient, double totalAttenduDouble)
    {
        // On convertit le double en decimal pour notre code métier
        decimal totalAttendu = (decimal)totalAttenduDouble;

        // Arrange
        var client = new Client(typeClient);
        var repas = new Repas();
        
        repas.AjouterProduit(new Produit("Plat", TypeProduit.Plat)); 
        repas.AjouterProduit(new Produit("Petite Salade", TypeProduit.PetiteSalade)); 

        var caisse = new Caisse();

        // Act
        Ticket ticket = caisse.GenererTicket(client, repas);

        // Assert
        Assert.Equal(totalAttendu, ticket.TotalAPayer);
    }


    // 3. TESTS DE LA FORMULE (Règle 1)

    [Fact]
    public void GenererTicket_VisiteurAvecFormuleComplete_Paye10Euros()
    {
        var client = new Client(TypeClient.Visiteur);
        var repas = new Repas();
        
        repas.AjouterProduit(new Produit("Salade cesar", TypeProduit.Entree));    
        repas.AjouterProduit(new Produit("Steak Frite", TypeProduit.Plat));      
        repas.AjouterProduit(new Produit("Glace vanille", TypeProduit.Dessert));    
        repas.AjouterProduit(new Produit("Baguette", TypeProduit.Pain));   

        var caisse = new Caisse();
        Ticket ticket = caisse.GenererTicket(client, repas);

        Assert.Equal(10.00m, ticket.TotalAPayer); 
    }
}
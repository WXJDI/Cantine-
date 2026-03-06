using Xunit;
using CantineKata.Core; 

namespace CantineKata.Tests;

public class CalculPrixTests
{
    [Fact]
    public void GenererTicket_VisiteurAvecUneBoisson_RetournePrixDeLaBoisson()
    {
        // Arrange
        var client = new Client(TypeClient.Visiteur);
        var repas = new Repas();
        repas.AjouterProduit(new Produit("Canette Cola", TypeProduit.Boisson)); // Le nom importe peu, c'est le Type qui fixe le prix

        var caisse = new Caisse();

        // Act
        Ticket ticket = caisse.GenererTicket(client, repas);

        // Assert
        Assert.Equal(1.00m, ticket.TotalAPayer);




    }

    [Fact]
    public void GenererTicket_VisiteurAvecUnPain_RetournePrixDuPain()
    {
        // Arrange
        var client = new Client(TypeClient.Visiteur);
        var repas = new Repas();
        repas.AjouterProduit(new Produit("Baguette", TypeProduit.Pain));

        var caisse = new Caisse();

        // Act
        Ticket ticket = caisse.GenererTicket(client, repas);

        // Assert
        Assert.Equal(0.40m, ticket.TotalAPayer); 
    }


    [Fact]
    public void GenererTicket_ClientInterne_AppliqueLaPriseEnCharge()
    {
        // Arrange
        var client = new Client(TypeClient.Interne); // Un client interne a 7.50€ de réduction
        var repas = new Repas();
        
        repas.AjouterProduit(new Produit("Grande Salade", TypeProduit.GrandeSalade)); 
        repas.AjouterProduit(new Produit("Petite Salade", TypeProduit.PetiteSalade)); 

        var caisse = new Caisse();

        // Act
        Ticket ticket = caisse.GenererTicket(client, repas);

        // Assert
        // 10 de total - 7.50 de prise en charge = 2.50 à payer
        Assert.Equal(2.50m, ticket.TotalAPayer); 
    }
    [Fact]
    public void GenererTicket_ClientVIP_NePayeRien()
    {
        // Arrange
        var client = new Client(TypeClient.VIP);
        var repas = new Repas();
        repas.AjouterProduit(new Produit("Grande Salade", TypeProduit.GrandeSalade)); // 6€
        repas.AjouterProduit(new Produit("Eau", TypeProduit.Boisson)); // 1€

        var caisse = new Caisse();

        // Act
        Ticket ticket = caisse.GenererTicket(client, repas);

   
        Assert.Equal(0m, ticket.TotalAPayer); 
    }

}
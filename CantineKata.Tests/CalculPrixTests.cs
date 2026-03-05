using Xunit;
using CantineKata.Core; // On importe ton projet principal

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
        Assert.Equal(0.40m, ticket.TotalAPayer); // 0.40€ pour le pain
    }
}
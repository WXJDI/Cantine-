#  Cantine Kata

Bienvenue dans le projet **Cantine Kata**. 
Ce projet a pour but de modéliser le système de facturation d'une cantine d'entreprise en appliquant des règles de gestion spécifiques (formules, suppléments, réductions par type de profil). 

Il a été développé en mettant l'accent sur la qualité du code, l'approche **TDD (Test-Driven Development)** et les principes **SOLID**.

##  Technologies & Outils
* **Langage :** C# (.NET 8)
* **Tests :** xUnit
* **Contrôle de version :** Git & GitHub

##  Stratégie de Branches (Git Flow)
Pour faciliter la relecture et démontrer l'évolution de la réflexion architecturale, le dépôt est organisé de la manière suivante :

1. **Branche `main` (Approche KISS) :** Contient la première itération du code. L'objectif était d'implémenter les règles métier de la manière la plus simple et directe possible (Keep It Simple, Stupid) pour faire passer les tests au vert.
   
2. **Pull Request `refactor/clean-code` (Approche SOLID) :** Une PR a été volontairement laissée ouverte pour illustrer la phase de refactoring. Elle permet de visualiser clairement le "Avant/Après" dans l'onglet *Files changed*.

##  Fonctionnalités implémentées

Le système de facturation gère la génération d'un ticket (`Ticket`) à partir d'un client (`Client`) et de son plateau (`Repas`), en respectant 3 règles fondamentales :

* **Règle 1 (La Formule) :** Détection automatique des menus complets (1 Entrée + 1 Plat + 1 Dessert + 1 Pain) et application d'un prix fixe de 10€.
* **Règle 2 (La Carte & Suppléments) :** Facturation à l'unité de tous les produits hors-formule ou en supplément, selon un catalogue de prix défini.
* **Règle 3 (Les Réductions Employeur) :** Prise en charge déduite du total en fonction du statut du client (Interne, Prestataire, Stagiaire, VIP, Visiteur).

##  Méthodologie et Étapes de Développement

### 1. Test-Driven Development (TDD)
Le développement a strictement suivi le cycle **Red -> Green -> Refactor**. 
Chaque règle métier a d'abord fait l'objet d'un test unitaire ou d'intégration en échec, avant d'écrire le code minimal pour le valider.

### 2. Tests d'intégration et Scénarios complexes
Mise en place de tests vérifiant la robustesse de l'algorithme lorsque les 3 règles se croisent (ex: *Un client Prestataire qui prend deux formules complètes + des suppléments*). 

### 3. Refactoring "Clean Code" (Branche dédiée)
Une fois la logique métier validée et couverte à 100% par les tests, l'architecture a été repensée :
* **Application du principe SRP (Single Responsibility Principle) :** La God Class `Caisse` a été découpée. Extraction du catalogue de prix (`ICatalogueProduits`) et du moteur de réductions (`ICalculateurPriseEnCharge`).
* **Application du principe OCP (Open/Closed Principle) :** Remplacement des structures `if/else if` par des *switch expressions* C# modernes, rendant l'ajout de nouveaux profils clients triviale sans modifier l'existant.
* **Refactoring des Tests (xUnit) :** * Utilisation de l'attribut `[Theory]` et `[InlineData]` pour mutualiser les tests de réductions et éviter la duplication de code (DRY).
  * Renommage des méthodes de test selon la convention **`Should_Return_When`** pour une meilleure lisibilité des rapports d'exécution.

##  Comment lancer les tests

Assurez-vous d'avoir le SDK .NET installé sur votre machine, puis exécutez la commande suivante à la racine du projet ou dans le dossier `CantineKata.Tests` :

```bash
dotnet test
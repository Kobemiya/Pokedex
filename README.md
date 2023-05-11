# Pokedex
Projet de dotNet

# Notre projet
## CONCEPT
Le concept principal de notre projet serait un Pokédex.
Pour le contexte, un Pokédex est une encyclopédie sur les Pokémons, qui se remplit au fil des captures
des différents Pokémons dans le jeu.
Ce Pokédex sera implémenté sous la forme d’un site web, dans lequel seront listés des Pokémons ainsi
que les informations qui leur sont relatives. Chaque utilisateur pourra aussi avoir des Pokémons en
favoris.

## FONCTIONNALITES
Les fonctionnalités visées sont les suivantes :
- Ajouter/supprimer des Pokémons de l’encyclopédie
- Modifier des statistiques des Pokémons
- Mettre des Pokémons en favoris
- Consulter sa liste de Pokémons favoris
- Rechercher des Pokémons à l’aide d’une barre de recherche
- Filtrer les pokémons par des caractéristiques spécifiques (types, etc.)
- Ajouter/supprimer les attaques que peuvent effectuer les Pokémons
CLIENT-WEB

## SPECIFICITES TECHNIQUES
Pour le client web, nous partirons sur ASP.NET en mode MVC. Les différents aspects seront délivrés
par des Razor pages qui représenteront les différentes pages d’actions, ou morceaux de page, que
l’utilisateur pourra explorer.

## DESIGN
Le design ressemblera à un véritable Pokédex se découpant en deux parties permettant de lister sur
un onglet la liste des Pokémons déjà ajoutés, et sur un autre onglet les différentes actions que
l’utilisateur pourra effectuer.

## FONCTIONNALITES
Il sera nécessaire de passer par une page d’authentification avant de pouvoir accéder aux Pokédex afin
de permettre aux utilisateurs d’avoir leur propre Pokédex et non un seul et même Pokédex, celui
partagé par tous.

Les principales actions qui pourront être effectuées sur l’onglet d’actions seront :
- Ajouter/supprimer des Pokémons de l’encyclopédie
- Afficher les statistiques du Pokémon
- Modifier les statistiques des Pokémons
Les principales actions que l’utilisateur pourra faire sur l’onglet de listing des Pokémons seront :
- Rechercher une entrée à partir d’un texte
- Ajout de favoris
- Filtrer par différentes statistiques : type, favoris…

## SPECIFICITES TECHNIQUES
Le backend sera écrit en C# et utilisera les controlleurs de .NET comme intermédiaire pour
communiquer avec la base de données SQL Server 2022.
Les deux seront hébergés sur un serveur web, sur lequel il sera possible d’envoyer des requêtes HTTP
pour interagir avec le back-end.

## FONCTIONNALITES
Les principaux endpoints seront les suivants :
- Ajout et suppression de Pokémons
- Modifier les informations des Pokémons
- Ajout et suppression des attaques des Pokémons
- Ajout et suppression de Pokémons en favoris
- CRUD Utilisateurs
- Endpoint connexion utilisateurs

## TESTS UNITAIRES
Les tests couvriront les aspects principaux du Pokédex de la création à la suppression des Pokémons
en passant par l’affichage des statistiques des Pokémons ou l’affichage des Pokémons en fonction des
recherches ou filtres

# Sujet

Bonjour à tous,

 

Dans le cadre du cours de .Net nous allons commencer le projet qui permettra d'évaluer vos connaissances sur la technologie.
Voici les deadlines :
Rendu des groupes => samedi 15 avril à 23h59 max.
Rendu description du porjet => samedi 22 avril à 23h59 max.
Rendu Projet => dimanche 21 mai à 12h59 max

 

Modalité pour les groupes :
Les groupes sont à rendre sur la spreadsheet suivante :
https://docs.google.com/spreadsheets/d/1ATdLb9VZnkLtDirycpWoF4HTeY0DoaYxS7bsyYArcaQ/edit#gid=0
Vous êtes libre de constituer vos groupes. Quand la deadline sera passée j'affecterai moi même les étudiants restants.

 

Modalités pour le projet :
Projet libre, à vous de définir votre sujet il n'y a pas de limite dans la mesure où cela respecte les conditions obligatoires ci dessous.

 

Condition sur le projet obligatoire :
- .NET 7
- Client Web (ASP.NET MVC) ou lourd
- utilisation du c#
- Tests Unitaires
- Un backend 
- Sql Server 2022
- Architecture : N-tiers
- un déploiement sur un serveur web (APache, nginx, IIS) pas de code qui tourne sous visual studio
Bonus : Toutes autres technologies Microsoft. Cependant l'utilisation de ces technologies devra être irréprochables

 

Modalité pour valider le projet :
Vous devez rendre 1 document permettant de décrire votre rpojet, ainsi que la facon dont vous allez le réaliser.
A rendre sur la ML de rendu .NEt avec les balises suivantes 
[MTI2024][Projet][spec] login_x
login_x correspond au login du responsable de projet, en pièce jointe :
- projet.pdf (5 pages)
Si vous n'avez pas de retour de ma part dans la semaine, vous pouvez considérer que votre projet est validé.

 

Modalités de soutenance :
Elles seront définies prochainement.

 

Si vous avez des questions nous pourrons en discuter jeudi lors du cours.

 

Bon courage à tous,
Arnaud

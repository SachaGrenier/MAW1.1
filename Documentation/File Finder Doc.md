# File Finder<img style="float: center; zoom: 35%" src="P:\MAW1.1\Documentation\logo.png">

<img style="float: left; zoom: 50%" src="P:\MAW1.1\Documentation\gadget.png">

> Xavier Schwab et Sacha Grenier
> Si-T1A – CPNV ES
> Chefs de projet : Chevillat Jerome & Hurni Pascal

## Table des matières
[TOC]
## Spécifications
### Titre
Application de recherche avancée de documents sur un répertoire précis
### Description
Ce projet nous a été demandé par un client. L’idée est de créer une application pour retrouver des fichiers qui avec le temps sont devenus trop nombreux. 

> Une réalisation générique de ce projet permettra d'utiliser la plateforme sur n’importe quel ordinateur équipé d’un système d’exploitation Windows.

### Matériel et logiciels à disposition
* Ordinateur standard CPNV
*	Microsoft Project
*	Logiciel de programmation (Visual studio)
*	Logiciels de gestion de projet
*	Logiciels de réalisation de documentation (Suite office)
*	Un dépôt GIT
### Prérequis
Un niveau en C# CFC est nécessaire
Ainsi que les connaissances suivantes :
*	Git
*	Kanban
*	XAML
### Cahier de charges
[Accéder au cahier de charges](https://github.com/SachaGrenier/MAW1.1/blob/master/CDCProjet/CDC/Cahier_des_charges.docx)

## Planification
Accéder à la planification GANTT en [cliquant ici](https://github.com/SachaGrenier/MAW1.1/blob/master/CDCProjet/Planification/MAW-grenier-schwab-Plannif-JDT.pdf)

![planif_part1](P:\MAW1.1\Documentation\planif_part1.png)
![planif_part1](P:\MAW1.1\Documentation\planif_part2.png)
![planif_part1](P:\MAW1.1\Documentation\planif_part3.png)
![planif_part1](P:\MAW1.1\Documentation\planif_part4.png)

## Analyse
### Oportunités
La création de cette application va nous permettre de :
*	Nous améliorer en C#, découvrir de nouvelles fonctions, libraires, etc.
*	Progresser en écriture de rapport de projet. 
*	Apprendre à utiliser le potentiel de chaque développeur en séparant le travail par tâches
Les difficultés seront :
*	Le développement de la fonction de recherche à l’intérieur des documents
*	Rendre la recherche la plus rapide possible
*	Le travail en groupe peut apporter des conflits

### Document d’analyse et conception
Le but est de créer une application qui permet d’effectuer des recherches avancées de documents sur un système Windows quelconque. Nous sommes mandatés par un client qui rencontre des difficultés à faire de recherches du a un grand nombre de documents.

> La fonctionnalité de recherche sera accompagnée de filtres qui permettront par exemple de visualiser uniquement les images, les vidéos ou les documents !

#### Maquettes
En ce qui concerne le visuel de l’application, nous avons créé des maquettes correspondant à chaque étape de la recherche : 

1. Démarrage de l’application.
![maq_0](P:\MAW1.1\Documentation\Maquette\maq_0.png)
2. Sélection du dossier d’où les recherches seront effectuées. Une fois le répertoire sélectionné, l’application se lance
![maq_1](P:\MAW1.1\Documentation\Maquette\maq_1.png)
<span style='color:orange'>Ce bouton permet de choisir un autre répertoire source</span>
3. Ici, on recherche « documents.docx ». Etant donné qu’il n’existe pas, aucun résultat n’apparait.
![maq_2](P:\MAW1.1\Documentation\Maquette\maq_2.png)
<span style='color:orange'>Ce bouton permet de choisir un autre répertoire source</span>
4. On recherche maintenant « mondocument » et plusieurs résultats apparaissent. 
![maq_3](P:\MAW1.1\Documentation\Maquette\maq_3.png)
5. On applique le filtre documents et on clique sur le document qui nous intéresse. On peut voir à droite qu’une fenêtre avec un aperçu du contenu du document est apparue.
![maq_4](P:\MAW1.1\Documentation\Maquette\maq_4.png)
<span style='color:orange'>L’aperçu du contenu du fichier fonctionnera uniquement avec des documents non formatés. Nous allons essayer de faire en sorte qu’il soit possible d’apercevoir le plus de documents possible. En ce qui concerne les images une miniature sera affichée, pour le reste rien n’est prévu à ce jour.</span>
6. Dernière fonctionnalité, lorsque l’on fait un clic droit sur le document qui nous intéresse, un menu propose d’ouvrir avec soit l’application correspondant à l’extension soit l’explorateur Windows avec l’emplacement du fichier.
![maq_5](P:\MAW1.1\Documentation\Maquette\maq_5.png)
#### Architecture

En ce qui concerne l’architecture de nos méthodes, le schéma
est plutôt simple :
![archi](P:\MAW1.1\Documentation\archi.png)

### Conception des tests
Nos tests vont être effectués suivant une grille qui comportera les colonnes suivantes :

| Fonctionnalité à tester            | Résultat attendu                                             | Date    | Résultat obtenu                 | Etat |
| ---------------------------------- | ------------------------------------------------------------ | ------- | ------------------------------- | ---- |
| Exemple: Clic sur le bouton Browse | Une fenêtre s'ouvre et l'utilisateur peut sélectionner un dossier à exploiter | 11 déc. | La fenêtre s'ouvre correctement | OK   |

Au fur et à mesure de l'avancement du projet, nous allons ajouter des fonctionnalités à tester dans la grille.

### Répartition des tâches

Il est vital pour un projet de groupe de faire une répartition des tâches, notamment
pour pouvoir utiliser correctement les capacités de chacun.

| Tâches Xavier                          | Tâches Sacha                    |
| -------------------------------------- | ------------------------------- |
| Développement général de l'application | Mise en place et gestion du git |
| Choix de technologies                  | Création des maquettes          |
| Valide les documents                   | Création de la planification    |
|                                        | Création du rapport de projet   |
|                                        | Participation au développement  |
|                                        |                                 |

## Dossier de réalisation
### Mise en place de l'environnement

D’abord, nous avons installé Visual Studio 2017 sur nos postes de travail. Ensuite, un dépôt git a été créé. Pour communiquer avec notre dépôt git nous avons installé l’application « git extensions » un logiciel de gestion de versions simple et efficace.

### Outils
#### Git

L’utilisation que nous avons fait de git n’était pas vraiment cohérente. Nous l’utilisons principalement pour pouvoir être plusieurs à coder sur le même fichier sans que cela pose des problèmes d’enregistrements. De ce fait nous avons chacun notre branche sur laquelle nous travaillons, et avec laquelle nous implémentons des fonctionnalités, des
éléments de l’application. Voici un schéma qui représente l’utilisation de nos branches :

![gitflow](P:\MAW1.1\Documentation\gitflow.PNG)

#### Trello

![trello_preview](P:\MAW1.1\Documentation\trello_preview.png)

#### FreeSpire

Pour rechercher à l’intérieur de des documents Word, nous avons utilisé une librairie nommée Freespire. <https://www.e-iceblue.com/Introduce/free-doc-component.html#.XAZ6Q2hKiUk>

Le logiciel comprenant plusieurs versions payantes nous propose une version gratuite, efficace pour Word et qui comporte aussi un module pour PDF, malheureusement pas assez complet et accessible. Nous avons donc choisi d’utiliser une autre librairie nommée :   

## Fonctionnalités
#### Rechercher

Pour rechercher efficacement dans un système de fichiers windows nous utilisons LinQ

#### Exécuter
#### Filtrer

Les filtres sont bien

#### Afficher

### Bugs connus



### Améliorations
## Tests
## Conclusion
### Bilan des fonctionnalités demandées

Dans l’ensemble le projet est tout de même bien avancé. Il est vrai qu’une fonctionnalité majeure est manquante et que des améliorations sont possibles. Le problème principal pour moi a été le temps. J’ai perdu pas mal de temps avec les fonctions qui affichent le planning, car c’était d’une grande difficulté. J’ai aussi perdu un peu de temps avec la première librairie que je voulais utiliser pour générer les PDF, mais j’ai pu trouver une alternative et faire fonctionner le tout, avec difficulté. 

J’ai réussi à rendre mon application générique, mais il est nécessaire d’y avoir 3 niveaux. 

 

### Bilan de la planification

Nous pensons avoir correctement suivi le planning. On a notamment perdu du temps sur l’analyse et sur le développement du de la recherche qui est la fonction qui nous a pris le plus de temps. 

De plus, nous avons rencontré des problèmes avec git, dus à des mauvaises manipulation

### Bilan du groupe (bilan personnel)
## Sources

Journal de travail :


# <img style="float: center; zoom: 35%" src="..\Documentation\logo.png">File Finder

<img style="float: left; zoom: 50%" src="..\Documentation\gadget.png">

> Xavier Schwab et Sacha Grenier
> Avec la participation de Nicolas Henry
> Si-T1A – CPNV ES
> Chefs de projet et clients : Chevillat Jerome & Hurni Pascal

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
* Visual Studio 2017
* Suite Adobe
* Suite Office 2017
* Application de gestion de projet (Trello)
* Un dépôt GIT
* C# .NET 4.6.1
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

![planif_part1](..\Documentation\planif_part1.png)
![planif_part1](..\Documentation\planif_part2.png)
![planif_part1](..\Documentation\planif_part3.png)
![planif_part1](..\Documentation\planif_part4.png)

## Analyse
### Oportunités
La création de cette application va nous permettre de :
*	Nous améliorer en C#, découvrir de nouvelles fonctions, libraires, etc.
*	Progresser en écriture de rapport de projet. 
*	Apprendre à utiliser le potentiel de chaque développeur en séparant le travail par tâches
*	Développer une fonction de recherche à l’intérieur des documents
*	Trouver un moyen de rendre la recherche la plus rapide possible
*	Le travail en groupe peut apporter des conflits, savoir les gérer

### Document d’analyse et conception
Le but est de créer une application qui permet d’effectuer des recherches avancées de documents sur un système Windows quelconque. Nous sommes mandatés par un client qui rencontre des difficultés à faire de recherches du a un grand nombre de documents.

> La fonctionnalité de recherche sera accompagnée de filtres qui permettront par exemple de visualiser uniquement les images, les vidéos ou les documents !

#### Maquettes
En ce qui concerne le visuel de l’application, nous avons créé des maquettes correspondant à chaque étape de la recherche : 

1. Démarrage de l’application.
    ![maq_0](..\Documentation\Maquette\maq_0.png)

2. Sélection du dossier d’où les recherches seront effectuées. Une fois le répertoire sélectionné, l’application se lance
    ![maq_1](..\Documentation\Maquette\maq_1.png)
    <span style='color:orange'>Ce bouton permet de choisir un autre répertoire source</span>

3. Ici, on recherche « documents.docx ». Etant donné qu’il n’existe pas, aucun résultat n’apparait.
    ![maq_2](..\Documentation\Maquette\maq_2.png)

    <span style='color:orange'>document.docx est recherché mais aucun résultat n'apparaît</span>

4. On recherche maintenant « mondocument » et plusieurs résultats apparaissent. 
    ![maq_3](..\Documentation\Maquette\maq_3.png)

5. On applique le filtre documents et on clique sur le document qui nous intéresse. On peut voir à droite qu’une fenêtre avec un aperçu du contenu du document est apparue.
    ![maq_4](..\Documentation\Maquette\maq_4.png)
    <span style='color:orange'>L’aperçu du contenu du fichier fonctionnera uniquement avec des documents non formatés. Nous allons essayer de faire en sorte qu’il soit possible d’apercevoir le plus de documents possible. En ce qui concerne les images une miniature sera affichée, pour le reste rien n’est prévu à ce jour.</span>

6. Dernière fonctionnalité, lorsque l’on fait un clic droit sur le document qui nous intéresse, un menu propose d’ouvrir avec soit l’application correspondant à l’extension soit l’explorateur Windows avec l’emplacement du fichier.
    ![maq_5](..\Documentation\Maquette\maq_5.png)
#### Architecture

En ce qui concerne l’architecture de nos méthodes, le schéma
est plutôt simple :
![archi](..\Documentation\archi.png)

### Conception des tests
Nos tests vont être effectués suivant une grille qui comportera les colonnes suivantes :

| Fonctionnalité à tester               | Résultat attendu                                             | Date    | Résultat obtenu                 | Etat                                |
| ------------------------------------- | ------------------------------------------------------------ | ------- | ------------------------------- | ----------------------------------- |
| Exemple: Clic sur le bouton Parcourir | Une fenêtre s'ouvre et l'utilisateur peut sélectionner un dossier à exploiter | 11 déc. | La fenêtre s'ouvre correctement | <span style="color:green">OK</span> |

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

En plein millieu de notre projet nous avons eu un nouvel élément pour nous épauler : **Nicolas Henry**
Etant donné que lorsqu'il est arrivé nous avions déjà bien avancé dans notre projet, il nous a aidés à :

* Ajouts d'éléments dans l'interface XAML
* Création de diagrammes
* Tests du programmes, complétion de la batterie de tests
* Aide au code
* Participation à la rédaction de certaines parties de la documentation

## Dossier de réalisation

### Mise en place de l'environnement

D’abord, nous avons installé Visual Studio 2017 sur nos postes de travail. Ensuite, un dépôt git a été créé. Pour communiquer avec notre dépôt git nous avons installé l’application « git extensions » un logiciel de gestion de versions simple et efficace.

### Outils
#### Git

Accès direct au répertoire Git : [Cliquer ici](http://www.github.com/SachaGrenier/MAW1.1)

L’utilisation que nous avons fait de git n’était pas vraiment cohérente. Nous l’utilisons principalement pour pouvoir être plusieurs à coder sur le même fichier sans que cela pose des problèmes d’enregistrements. De ce fait nous avons chacun notre branche sur laquelle nous travaillons, et avec laquelle nous implémentons des fonctionnalités, des
éléments de l’application. Voici un schéma qui représente l’utilisation de nos branches :

![gitflow](..\Documentation\gitflow.PNG)

Au total, plus de 100 commits ont été effectués, avec la participation des 3 développeurs :

![git_commits](..\Documentation\Screenshots\git_commits.PNG)

#### Trello

![trello_preview](..\Documentation\trello_preview.png)

L'application Trello ne nous a pas vraiment été utile durant le projet. A part les quelques fois où nous l'avons utilisée pour savoir ou nous en étions, elle à été pas mal délaissée. TODO -> que dire de plus ?

#### FreeSpire

Pour rechercher à l’intérieur de des documents Word, nous avons utilisé une librairie nommée [Freespire](https://www.e-iceblue.com/Introduce/free-doc-component.html#.XAZ6Q2hKiUk).

Le logiciel comprenant plusieurs versions payantes nous propose une version gratuite, efficace pour Word et qui comporte aussi un module pour PDF, malheureusement pas assez complet et accessible. Nous avons donc choisi d’utiliser une autre librairie nommée :   TODO  ON AFEKOI

#### iTextSharp

NuGet installé par nos soins qui nous a ajouté des fonctionnalités permettant de lire des fichiers .PDF

## Fonctionnalités
### Rechercher

![app_open](..\Documentation\Screenshots\app_open.PNG)

Pour récupérer le chemin à partir duquel l'utilisateur souhaite rechercher ses fichiers, une petite interface apparaît lorsque on lance l'application. 

Une fois le dossier sélectionné, l'application apparaît avec tous les résultats affichés dans la grille. Il est toujours possible de changer de dossier source à partir du bouton en haut à gauche.

Pour rechercher efficacement dans un système de fichiers Windows nous utilisons LinQ; une librairie intégrée à Visual Studio. LinQ est un composant développé par Microsoft, nous avons donc une documentation complète et précise pour nous épauler.

Cette libraire permet de faire des requêtes avec une syntaxe similaire au SQL, langage avec lequel le groupe est à l'aise.

Il faut savoir que la recherche est faite de manière récursive, donc si le fichier se situe dans un sous-dossier le programme se débrouillera pour le trouver.

Voici la classe que nous utilisons pour appeller  TODO refaire le screen avec le commentaire en anglais

![AllowLinq](..\Documentation\Screenshots\AllowLinq.PNG)

Voici comment nous faisons nos requêtes avec LinQ. 

![Linq_ABC](..\Documentation\Screenshots\Linq_ABC.PNG)Cette requête est utilisée lorsque le filtre "Word" est activé.

* <span style="color:orange">A</span> Vérifie que le filtre Word soit activé
* <span style="color:orange">B</span> Requête linQ qui permet de rechercher tous les fichiers Word ainsi que leur contenu si l'élément à une correspondance
* <span style="color:orange">C</span> Le fichier, s'il correspond, est ajouté au tableau de fichiers word. On vérifie ensuite qu'il n'y aie pas de doublons, on compte la quantitié d'éléments pour l'afficher dans l'application et on l'ajoute à la liste de résultats sous format Observable Collection.

Nous utilisons donc des fonctions offertes par LinQ tel que .AddRange() pour ajouter un élément à un tableau ou alors .Distinct() pour éviter les doublons dans nos résultats.

### Filtrer

![filterz](..\Documentation\Screenshots\filterz.PNG)

Les filtres sont affichés sous forme de boutons radio. Lorsque l'on clique sur une option, la liste de résultats se met à jour avec les nouvelles données.

Si quelque chose à déjà été tapé dans la barre de recherche et que vous choisissez un filtre, la liste de résultats se mettera à jour avec le filtre et la recherche.

Les filtres ont été faits à l'aide des extensions de fichiers que nous avons répertorié dans des listes.

Les filtres "Word" & "PDF" sont un peu différents des autres; lorsqu'ils sont sélectionnés, la recherche s'effectuera sur l'ensemble des documents Word et PDF ainsi que leur contenu

Le filtre "Autres" est utilisé pour toutes les extensions inconnues au bataillon.

### Afficher

La liste de résultats se met à jour après n'importe quel nouvelle lettre tapée, ou lorsque le filtre est changé. Pour l'implémentation des données dans le tableau nous avons utilisé un DataGrid. La quantité de résultats est affichée en bas à droite.

![datagrid_example](..\Documentation\Screenshots\datagrid_example.png)

Ceci est un exemple de DataGrid et les possibilités qu'il propose. Très pratique à utiliser car le type de source de données demandé est exactement celui que nous utilisons pour retourner les résultats de la recherche.

Voici à quoi ressemble notre liste de résultats

TODO -> insérer screen de l'app

FileDetails est la classe que nous utilisons pour chaque fichier qui n'est pas encore filtré.

![Filemodel](..\Documentation\Screenshots\Filemodel.PNG)

TODO -> améliorer

Voici la méthode makeList() que nous uitlisons pour filtrer ce que nous affichons dans le DataGrid.
C'est de cette manière que nous filtrons les fichiers, les autres fitres ont aussi leur propre méthode.

![ModelCreator](..\Documentation\Screenshots\model_ABC.PNG)

* <span style="color:orange">A</span> Vérifie si le filtre Image est sélectionné et vide la liste actuelle si elle n'est pas vide
* <span style="color:orange">B</span> Vide la liste imageFile, parcours la liste des fichier et cherche ceux dont l'extension correspond à la liste d'extensions(tappée en dur), puis construit l'objet id pour l'ajouter à la liste de fichiers. La liste "list" est le résultat de la recherche retournée par la méthode utilisant LinQ.
* <span style="color:orange">C</span> Vérifie qu'il n'y aie pas de doublons dans la liste de fichiers, compte combien d'éléments sont dans cette dernière(cette information est utilisée dans l'ui pour afficher le nombre de résultats) et ajoute à la liste de résultats sous format "Observable Collection".
* TODO check ce que jai écrit c'est juste

### Exécuter

Deux possibilités d'ouvrir un fichier :

Avec le clic droit :![right_click](..\Documentation\Screenshots\right_click.png)

Avec deux clics gauche :Ouverture du fichier avec le programme par défaut.

### Bugs/Problèmes connus

TODO

Utilisation du datagrid,

xaml merdique

rapidité de recherche

git lol

### Améliorations

Nous avons des idées pour améliorer notre application, en voici en liste non-exhaustive :

* Colorer élément en bleu lorsque un clic droit est effectué
* Améliorer la fluidité de la recherche
* Meilleure utilisation des header du tableau
* Faire un focus sur l'application une fois le dossier source séléctionné
* Une méthode générique pour les filtres pour que nous puissions en rajouter facilement
* Utilisation d'une banque de données pour les extensions 
* TODO -> Ajouter éléments

### Installation

Il s'agit d'un programme en C# développé avec Visual Studio, donc pour l'installation il faut reprendre tout le dossier Release ou se site la version de production de notre application. 

Le programme à été compilé à sa dernière version alors vous pourrez jouir de ses toutes dernières fonctionnalités

TODO -> C'est tout ?

## Tests

| Fonctionnalité   à tester                                    | Résultat attendu                                             | Date    | Résultat obtenu                                           | Etat                                   |
| ------------------------------------------------------------ | ------------------------------------------------------------ | ------- | --------------------------------------------------------- | -------------------------------------- |
| Clic sur le bouton "Parcourir"                               | Une fenêtre s'ouvre et l'utilisateur peut sélectionner un dossier à exploiter | 11 déc. | La fenêtre s'ouvre correctement, l'arborescence s'affiche | <span style="color:green;">OK</span>   |
| Lancer l'application                                         | Une recherche de dossier s'ouvre                             | 13 dec. | La fenêtre s'ouvre                                        | <span style="color:green;">OK</span>   |
| Sélection du dossier                                         | Affichage du dossier                                         | 13 déc. | Le contenu du dossier s'affiche                           | <span style="color:green;">OK</span>   |
| Recherche du fichier   "index"                               | Les fichiers dont le nom contient "index" s'affichent        | 13 déc. | Les fichiers s'affichent                                  | <span style="color:green;">OK</span>   |
| Ouverture du fichier                                         | Le fichier s'ouvre avec le programme par défaut              | 13 déc. | Le fichier s'ouvre ou propose un programme                | <span style="color:green;">OK</span>   |
| Clique droit sur l'élément de la liste+ Ouvrir l'emplacement du fichier | Le dossier s'ouvre                                           | 13 déc. | Le dossier s'ouvre                                        | <span style="color:green;">OK</span>   |
| Filtre des fichier                                           | Les fichiers du type s'affichent                             | 13 déc. | Les fichiers s'affichent en fonction de leurs type        | <span style="color:green;">OK</span>   |
| Ouvrir le fichier                                            | Le fichier s'ouvre avec le programme par défaut              | 13 déc. | Le fichier s'ouvre avec le programme par défaut           | <span style="color:green;">OK</span>   |
| Clique droit + afficher le   dossier                         | Le dossier s'ouvre                                           | 13 déc. | Le dossier s'ouvre                                        | <span style="color:green;">OK</span>   |
| Sélectionner un nouveau   dossier                            | Affichage du dossier                                         | 13 déc. | Le dossier s'affiche                                      | <span style="color:green;">OK</span>   |
| Recherche du fichier   "index"                               | Les fichiers "index" s'affichent                             | 13 déc. | Les fichiers s'affichent                                  | <span style="color:green;">OK</span>   |
| Ouverture du fichier                                         | Le fichier s'ouvre avec le programme par défaut              | 13 déc. | Le fichier s'ouvre ou propose un programme                | <span style="color:green;">OK</span>   |
| Clique droit + afficher le   dossier                         | Le dossier s'ouvre                                           | 13 déc. | Le dossier s'ouvre                                        | <span style="color:green;">OK</span>   |
| Filtre des fichier                                           | Les fichiers du type s'affichent                             | 13 déc. | Les fichiers s'affichent après une nouvelle recherche     | <span style="color:green;">OK</span>   |
| Ouvrir le fichier                                            | Le fichier s'ouvre avec le programme par défaut              | 13 déc. | Le fichier s'ouvre                                        | <span style="color:green;">OK</span>   |
| Clique droit + afficher le   dossier                         | Le dossier s'ouvre                                           | 13 déc. | Le dossier s'ouvre                                        | <span style="color:green;">OK</span>   |
| Lancer l'application sans   choisir de dossier source        | L'application se lance sans dossier                          | 13 déc. | L'application se lance                                    | <span style="color:green;">OK</span>   |
| Rechercher un document avec sa date de modification          | TODO                                                         |         |                                                           | <span style="color:darkred;">KO</span> |
| Rechercher un document avec sa date de création              | TODO                                                         |         |                                                           | <span style="color:darkred;">KO</span> |

## Conclusion

### Bilan Maquette/Produit final

TODO -> ajouter screen de chaque partie;

De manière générale, nous avons plutot bien respécté notre maquette. On peut remarquer une différence nette principalement sur le liste de résultats ainsi que la prévisualisation des fichiers. Cette différence est due au fait que nous ne maîtrisons pas le xaml qui fait que nous avons fait au plus simple. Nous trouvons tout de même que le résultat final se défend bien, l'espace est bien utilisé.

On peut remarquer aussi que les collones de la liste de résultat finale ne correspondent pas à la maquette, qui est clairement plus adaptée, plus complète. 

### Bilan des fonctionnalités demandées

Dans l’ensemble le projet est tout de même bien avancé. Il est vrai qu’une fonctionnalité majeure est manquante et que des améliorations sont possibles. Le problème principal pour moi a été le temps. J’ai perdu pas mal de temps avec les fonctions qui affichent le planning, car c’était d’une grande difficulté. J’ai aussi perdu un peu de temps avec la première librairie que je voulais utiliser pour générer les PDF, mais j’ai pu trouver une alternative et faire fonctionner le tout, avec difficulté. 

TODO -> check 

### Bilan de la planification

Nous pensons avoir correctement suivi le planning. On a notamment perdu du temps sur l’analyse et sur le développement du de la recherche qui est la fonction qui nous a pris le plus de temps. 

De plus, nous avons rencontré des problèmes avec git, dus à de mauvaises manipulation

## Sources

Journal de travail :TODO

Batterie de tests : TODO
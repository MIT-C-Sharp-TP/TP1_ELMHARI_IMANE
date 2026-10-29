# TP C# — Un duel au tour par tour dans la console

Projet de travail pour le TP C# (ENSAM Casablanca, 2026–2027).

## Contenu

- `Partie1/Exercice1_FichePersonnage` : variables et affichage d'une fiche.
- `Partie1/Exercice2_EtatSante` : conditions `if / else if / else`.
- `Partie1/Exercice3_Poison` : boucle `for`, dégâts et `break`.
- `Partie1/Exercice4_Soin` : méthode `Heal` et retour d'une valeur.
- `Partie2_Arena` : jeu console avec Aya, Goblin, Orc et Boss.
- `Partie3_ArenaAvancee` : résumé des notions avancées incluses dans le jeu.

## Prérequis

.NET SDK 10 et VS Code (ou Visual Studio).

## Lancer un exercice

Ouvrir le terminal dans le dossier de l'exercice, puis exécuter :

```bash
dotnet run
```

Chaque exercice de la Partie 1 est un projet console séparé. Pour lancer le jeu, ouvrir le terminal dans `Partie2_Arena` puis exécuter `dotnet run`.

## Contrôles du jeu

- `1` : attaquer.
- `2` : boire une potion.
- `y` : rejouer après une partie.
- toute autre réponse à la question de rejouer : quitter.

Le jeu enregistre les résultats dans `results.txt` dans le dossier d'exécution. Ce fichier est généré lors de l'exécution et n'a pas besoin d'être téléversé sur GitHub.

## Notions illustrées

Variables, conditions, boucles, méthodes, propriétés, classes, héritage, `List<Enemy>`, saisie sûre avec `int.TryParse`, `virtual/override`, événement `event`, LINQ, fichiers et `try/catch`.

**Important :** relisez et testez le code, et assurez-vous de pouvoir expliquer chaque partie avant de le présenter comme votre travail.

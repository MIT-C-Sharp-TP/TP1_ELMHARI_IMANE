# Partie 3 — Arena avancée

La version avancée est incluse directement dans `Partie2_Arena/Program.cs` pour éviter de dupliquer le code.
Elle ajoute :
- un Boss avec 60 HP et une armure de 6 ;
- la méthode virtuelle `Enemy.TakeDamage` et sa redéfinition dans `Boss` ;
- l’événement `Player.Died` ;
- plusieurs parties avec `PlayGame()` ;
- les trois meilleures victoires grâce à LINQ (`OrderBy` et `Take`) ;
- l’enregistrement de chaque résultat dans `results.txt`, avec `try/catch`.

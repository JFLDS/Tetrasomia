![image](https://user-images.githubusercontent.com/84011629/123290816-dcc52500-d511-11eb-9ed9-440eb77d4a26.png)

Description des classes principales

Dans la partie interface : 
On va avoir la classe "TableauDesScores" pour récupérer tout ce qui est information sur la partie et ses joueurs, avec par exemple le nombre de morts et de kills q'un joueur présent dans la partie aura.

Le "Lobby" qui sert à rejoindre une partie en local, seul ou en mode en ligne en lien avec notre network API qu'est Mirror.

Dans la partie graphisme :
La classe "Personnage" est la modélisation du personnage avec l'arme ainsi que les mouvements codés pour correspondre au déplacements.

La classe "Map" qui regroupe toute la partie graphique de la carte créée avec son terrain et son décor.

Dans la partie Gameplay :
La classe "Joueur" regroupe les informations concernant un joueur qui se connecte dans la partie. Ainsi que deux fonction qui lui permette de tirer, recharger et les déplacements possible comme avancer, reculer, sauter ou encore s'accroupir.

Les "Elements" sont des bonus choisi au début de la partie avec un choix entre 4 éléments qui correspondents chacun à un bonus sur le joueur, utilisable durant la partie.


Dans le cas où l'on se connecte en ligne avec Mirror, nous avons décider d'utiliser le logiciel "LogMeIn Hamachi" car Mirror ne prend que en compte les réseaux lan. Hamachi sert à simuler un réseau lan en étant à distance sur un réseau différent. Attention si vous utiliser Hamachi, ne pas oublier d'autoriser le logiciel dans les pare-feu si la connexion ne se fait pas.

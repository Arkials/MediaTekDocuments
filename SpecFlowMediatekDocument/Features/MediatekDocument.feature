Feature: MediatekDocument

Scenario: chercher un nom de livre
	Given Onglet livres cliqué
	And la textbox livres est cliqué
	And le mot "catastrophe" est tapé
	Then  La grille contient 1 résultat(s) dont le 1er est "Catastrophes au Brésil"

Scenario: chercher les livres par genre
	Given Onglet livres cliqué
	And la ligne "Roman" de la combo box "Genres" est cliqué
	Then La grille contient 7 résultat(s) dont le 1er est "Dans les coulisses du musée"

Scenario: chercher les livres par public
	Given Onglet livres cliqué
	And la ligne "Adultes" de la combo box "Publics" est cliqué
	Then La grille contient 12 résultat(s) dont le 1er est "Histoire du juif errant"

	Scenario: chercher les livres par rayon
	Given Onglet livres cliqué
	And la ligne "Jeunesse BD" de la combo box "Rayons" est cliqué
	Then La grille contient 1 résultat(s) dont le 1er est "Sacré Pêre Noël"

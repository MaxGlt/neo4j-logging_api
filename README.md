# API de Journalisation

## Description

L'API de journalisation est une application ASP.NET Core conçue pour recevoir, traiter, et stocker les informations des requêtes HTTP provenant du module de journalisation. Cette API interagit directement avec une base de données Neo4j pour représenter les appels inter-applicatifs sous forme de graphes, permettant de visualiser les interactions entre différentes applications au sein de l'infrastructure.

## Fonctionnalités

- **Réception des requêtes HTTP** : L'API reçoit les informations des requêtes HTTP et les valide avant traitement.
- **Stockage des données dans Neo4j** : Les requêtes sont enregistrées sous forme de nœuds et de relations dans Neo4j, offrant une représentation graphique des interactions.
- **Gestion des erreurs** : Capture et gestion des exceptions pour assurer la stabilité de l'application.

## Configuration de Neo4j

La configuration de l'intégration avec Neo4j est centralisée dans la classe `Neo4jConfiguration`. Cette classe utilise l'extension `IServiceCollection` pour injecter le contexte de la base de données Neo4j (`DbContext`) dans le pipeline de l'application ASP.NET Core.

### Connexion sécurisée

Le `DbContext` est configuré pour établir une connexion sécurisée avec Neo4j en utilisant les informations de connexion fournies dans le fichier de configuration (`appsettings.json`).

#### Exemple de configuration dans `appsettings.json`

```json
{
  "Neo4j": {
    "Uri": "bolt://neo4j-url:7687",
    "Username": "neo4j",
    "Password": "password"
  }
}
```

## Contribution

Les contributions sont les bienvenues ! Si vous avez des suggestions, des améliorations ou des rapports de bogues, n'hésitez pas à ouvrir une issue ou à soumettre une pull request.

## Licence

Ce projet est sous licence MIT. Voir le fichier LICENSE pour plus de détails.
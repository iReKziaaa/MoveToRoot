# MoveToRoot

> Outil Windows pour remonter tous les fichiers des sous-dossiers directement dans le dossier racine.

[![Platform](https://img.shields.io/badge/platform-Windows-blue)](https://www.microsoft.com/windows)
[![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.0+-512BD4)](https://dotnet.microsoft.com/)

MoveToRoot parcourt récursivement un dossier et **déplace** chaque fichier trouvé dans les sous-dossiers vers la racine. Plus besoin de le faire manuellement fichier par fichier.

## Fonctionnalités

- Parcourt tous les sous-dossiers en une seule commande
- Déplace les fichiers à la racine (ne copie pas)
- Renomme automatiquement les doublons (`fichier (1).txt`, `fichier (2).txt`, …)
- Supprime les sous-dossiers vides après déplacement
- Exécutable autonome — aucune installation requise

## Téléchargement

Téléchargez **`MoveToRoot.exe`** depuis l'onglet [Releases](../../releases).

Aucun autre fichier n'est nécessaire.

## Utilisation

1. Lancez **`MoveToRoot.exe`**
2. Entrez le chemin du dossier racine
3. Appuyez sur **Entrée**

```
Dossier racine : C:\Users\Nom\Documents\Mes fichiers
```

> **Astuce :** glissez-déposez un dossier dans la fenêtre pour coller son chemin automatiquement.

## Exemple

**Avant**

```
MonDossier/
├── deja-la.txt
├── DossierA/
│   └── photo.jpg
└── DossierB/
    └── doc.pdf
```

**Après**

```
MonDossier/
├── deja-la.txt
├── photo.jpg
└── doc.pdf
```

## Prérequis

| Requirement | Version |
|-------------|---------|
| OS | Windows 10 / 11 |
| Runtime | .NET Framework 4.0+ (inclus sur la plupart des PC Windows) |

## Compilation

Clonez le dépôt, puis compilez avec le compilateur C# inclus dans Windows :

```powershell
git clone https://github.com/VOTRE-UTILISATEUR/MoveToRoot.git
cd MoveToRoot
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /out:MoveToRoot.exe MoveToRoot.cs
```

## Avertissements

- Les fichiers sont **déplacés**, pas copiés.
- Les fichiers déjà présents à la racine ne sont pas modifiés.
- L'opération est **irréversible** — vérifiez le dossier cible avant de lancer l'outil.

## Licence

Libre d'utilisation.

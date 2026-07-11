using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.Title = "MoveToRoot";
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        AfficherBanniere();
        AfficherInstructions();

        string dossierRacine = LireDossierRacine();
        if (dossierRacine == null)
            return;

        dossierRacine = Path.GetFullPath(dossierRacine);

        var fichiers = Directory.GetFiles(dossierRacine, "*", SearchOption.AllDirectories)
            .Where(f => !string.Equals(Path.GetDirectoryName(f), dossierRacine, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (fichiers.Count == 0)
        {
            AfficherMessage("Aucun fichier trouve dans les sous-dossiers.", ConsoleColor.Yellow);
            AfficherChemin("Dossier", dossierRacine);
            AttendreFin();
            return;
        }

        AfficherResume(dossierRacine, fichiers.Count);
        AfficherSeparateur();

        int compteur = 0;
        int total = fichiers.Count;

        foreach (string fichier in fichiers)
        {
            compteur++;
            string destination = Path.Combine(dossierRacine, Path.GetFileName(fichier));

            if (File.Exists(destination))
                destination = GetNomUnique(destination);

            File.Move(fichier, destination);
            AfficherDeplacement(compteur, total, Path.GetFileName(fichier));
        }

        int dossiersSupprimes = SupprimerDossiersVides(dossierRacine);
        AfficherResultat(compteur, dossiersSupprimes);
        AttendreFin();
    }

    static void AfficherBanniere()
    {
        Console.Clear();
        Ecrire("", ConsoleColor.Cyan);
        Ecrire("  ╔══════════════════════════════════════════╗", ConsoleColor.Cyan);
        Ecrire("  ║                                          ║", ConsoleColor.Cyan);
        Ecrire("  ║               MoveToRoot                 ║", ConsoleColor.White);
        Ecrire("  ║        Fichiers → dossier racine         ║", ConsoleColor.DarkGray);
        Ecrire("  ║                                          ║", ConsoleColor.Cyan);
        Ecrire("  ╚══════════════════════════════════════════╝", ConsoleColor.Cyan);
        Ecrire("", ConsoleColor.Cyan);
    }

    static void AfficherInstructions()
    {
        Ecrire("  Entrez le chemin du dossier racine.", ConsoleColor.Gray);
        Ecrire("  Astuce : glissez-deposez un dossier ici.", ConsoleColor.DarkGray);
        Ecrire("  Exemple : C:\\Users\\Nom\\Documents\\Mes fichiers", ConsoleColor.DarkGray);
        Console.WriteLine();
    }

    static string LireDossierRacine()
    {
        Ecrire("  Dossier racine", ConsoleColor.White);
        Console.Write("  › ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        string dossierRacine = Console.ReadLine();
        Console.ResetColor();

        if (dossierRacine != null)
            dossierRacine = dossierRacine.Trim(' ', '"');

        if (string.IsNullOrWhiteSpace(dossierRacine))
        {
            Console.WriteLine();
            AfficherMessage("Erreur : aucun dossier renseigne.", ConsoleColor.Red);
            AttendreFin();
            return null;
        }

        if (!Directory.Exists(dossierRacine))
        {
            Console.WriteLine();
            AfficherMessage("Erreur : le dossier n'existe pas.", ConsoleColor.Red);
            AfficherChemin("Chemin", dossierRacine);
            AttendreFin();
            return null;
        }

        return dossierRacine;
    }

    static void AfficherResume(string dossierRacine, int nombreFichiers)
    {
        Console.WriteLine();
        Ecrire("  ┌─ Resume ─────────────────────────────────", ConsoleColor.DarkCyan);
        AfficherChemin("  │ Dossier", dossierRacine);
        Ecrire("  │ Fichiers a deplacer : " + nombreFichiers, ConsoleColor.White);
        Ecrire("  └──────────────────────────────────────────", ConsoleColor.DarkCyan);
        Console.WriteLine();
    }

    static void AfficherDeplacement(int index, int total, string nomFichier)
    {
        Console.Write("  ");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("[" + index + "/" + total + "] ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("✓ ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(nomFichier);
        Console.ResetColor();
    }

    static int SupprimerDossiersVides(string dossierRacine)
    {
        int supprimes = 0;

        foreach (string dossier in Directory.GetDirectories(dossierRacine, "*", SearchOption.AllDirectories)
            .OrderByDescending(d => d.Length))
        {
            if (!Directory.EnumerateFileSystemEntries(dossier).Any())
            {
                Directory.Delete(dossier, false);
                Console.Write("  ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("∅ dossier vide supprime : " + Path.GetFileName(dossier));
                Console.ResetColor();
                supprimes++;
            }
        }

        return supprimes;
    }

    static void AfficherResultat(int fichiersDeplaces, int dossiersSupprimes)
    {
        Console.WriteLine();
        Ecrire("  ╔══════════════════════════════════════════╗", ConsoleColor.Green);
        Ecrire("  ║              Termine !                   ║", ConsoleColor.Green);
        Ecrire("  ╠══════════════════════════════════════════╣", ConsoleColor.Green);
        Ecrire("  ║  Fichiers deplaces    : " + PadLabel(fichiersDeplaces), ConsoleColor.White);
        Ecrire("  ║  Dossiers supprimes   : " + PadLabel(dossiersSupprimes), ConsoleColor.White);
        Ecrire("  ╚══════════════════════════════════════════╝", ConsoleColor.Green);
    }

    static string PadLabel(int value)
    {
        return value.ToString().PadRight(18) + "║";
    }

    static void AfficherChemin(string label, string chemin)
    {
        Console.Write("  ");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write(label + " : ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(chemin);
        Console.ResetColor();
    }

    static void AfficherMessage(string message, ConsoleColor couleur)
    {
        Console.Write("  ");
        Console.ForegroundColor = couleur;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    static void AfficherSeparateur()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("  ──────────────────────────────────────────");
        Console.ResetColor();
    }

    static void AttendreFin()
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("  Appuyez sur Entree pour quitter...");
        Console.ResetColor();
        Console.ReadLine();
    }

    static void Ecrire(string texte, ConsoleColor couleur)
    {
        Console.ForegroundColor = couleur;
        Console.WriteLine(texte);
        Console.ResetColor();
    }

    static string GetNomUnique(string cheminComplet)
    {
        if (!File.Exists(cheminComplet))
            return cheminComplet;

        string dossier = Path.GetDirectoryName(cheminComplet);
        string nom = Path.GetFileNameWithoutExtension(cheminComplet);
        string ext = Path.GetExtension(cheminComplet);
        int i = 1;
        string nouveau;

        do
        {
            nouveau = Path.Combine(dossier, nom + " (" + i + ")" + ext);
            i++;
        }
        while (File.Exists(nouveau));

        return nouveau;
    }
}

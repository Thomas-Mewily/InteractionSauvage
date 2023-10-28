using InteractionSauvage;

namespace InteractionSauvage;

public class Grille
{
    private List<Entite>[][] EntiteGrille { get; set; }

    public int TailleCase { get; private set; }

    public int Hauteur  => NbCaseHauteur * TailleCase;
    public int Longueur => NbCaseLongueur * TailleCase;

    public int NbCaseHauteur  { get; private set; }
    public int NbCaseLongueur { get; private set; }

    public Grille(int hauteur, int longueur, int tailleCase)
    {
        NbCaseHauteur = hauteur;
        NbCaseLongueur = longueur;
        EntiteGrille = new List<Entite>[hauteur][];
        TailleCase = tailleCase;

        for (int i = 0; i < hauteur; i++)
        {
            EntiteGrille[i] = new List<Entite>[longueur];
            for (int j = 0; j < longueur; j++)
            {
                EntiteGrille[i][j] = new List<Entite>();
            }
        }
    }

    public void AjouterEntiteDansGrille(Entite entite)
    {
        int indiceX = (int)(entite.X / TailleCase);
        int indiceY = (int)(entite.Y / TailleCase);

        indiceX = Math.Max(0, Math.Min(indiceX, EntiteGrille.Length - 1));
        indiceY = Math.Max(0, Math.Min(indiceY, EntiteGrille[0].Length - 1));

        EntiteGrille[indiceX][indiceY].Add(entite);
    }

    public void RetirerEntiteDeGrille(Entite entite)
    {
        int indiceX = (int)(entite.X / TailleCase);
        int indiceY = (int)(entite.Y / TailleCase);

        indiceX = Math.Max(0, Math.Min(indiceX, EntiteGrille.Length - 1));
        indiceY = Math.Max(0, Math.Min(indiceY, EntiteGrille[0].Length - 1));


        EntiteGrille[indiceX][indiceY].Remove(entite);
    }
}
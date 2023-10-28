using InteractionSauvage;

namespace InteractionSauvage;

public class Grille
{
    private List<Entite>[,] EntiteGrille { get; set; }

    public int TailleCase { get; private set; }

    public int Hauteur  => NbCaseHauteur * TailleCase;
    public int Longueur => NbCaseLongueur * TailleCase;

    public int NbCaseHauteur  { get; private set; }
    public int NbCaseLongueur { get; private set; }

    public Grille(int hauteur, int longueur, int tailleCase)
    {
        NbCaseHauteur = hauteur;
        NbCaseLongueur = longueur;
        EntiteGrille = new List<Entite>[hauteur, longueur];
        TailleCase = tailleCase;

        for (int i = 0; i < NbCaseHauteur; i++)
        {
            for (int j = 0; j < NbCaseLongueur; j++)
            {
                EntiteGrille[i,j] = new List<Entite>();
            }
        }
    }

    public void Reset() 
    {
        for (int i = 0; i < NbCaseHauteur; i++)
        {
            for (int j = 0; j < NbCaseLongueur; j++)
            {
                EntiteGrille[i, j].Clear();
            }
        }
    }

    public List<Entite> RecupererCase(double x, double y)
    {
        int indiceX = (int)(x / TailleCase);
        int indiceY = (int)(y / TailleCase);

        return EntiteGrille[indiceX][indiceY];
    }
    public void AjouterEntiteDansGrille(Entite entite)
    public void Add(Entite entite)
    {
        int indiceX = (int)(entite.X / TailleCase);
        int indiceY = (int)(entite.Y / TailleCase);

        indiceX = Math.Max(0, Math.Min(indiceX, NbCaseLongueur - 1));
        indiceY = Math.Max(0, Math.Min(indiceY, NbCaseHauteur - 1));

        EntiteGrille[indiceX,indiceY].Add(entite);
    }

    public void Remove(Entite entite)
    {
        int indiceX = (int)(entite.X / TailleCase);
        int indiceY = (int)(entite.Y / TailleCase);

        indiceX = Math.Max(0, Math.Min(indiceX, NbCaseLongueur - 1));
        indiceY = Math.Max(0, Math.Min(indiceY, NbCaseHauteur  - 1));
        EntiteGrille[indiceX,indiceY].Remove(entite);
    }
}
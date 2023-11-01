using Geometry;
using InteractionSauvage;

namespace InteractionSauvage;

public class Grille
{
    private List<Entite>[,] EntiteGrille { get; set; }

    public int TailleCase { get; private set; }

    public Vec2 Size => new Vec2(Longueur, Hauteur);
    public int Hauteur  => NbCaseHauteur * TailleCase;
    public int Longueur => NbCaseLongueur * TailleCase;

    public int NbCaseHauteur  { get; private set; }
    public int NbCaseLongueur { get; private set; }

    public Grille(int longueur, int hauteur, int tailleCase)
    {
        NbCaseHauteur  = hauteur;
        NbCaseLongueur = longueur;
        EntiteGrille   = new List<Entite>[longueur, hauteur];
        TailleCase     = tailleCase;

        for (int i = 0; i < NbCaseLongueur; i++)
        {
            for (int j = 0; j < NbCaseHauteur; j++)
            {
                EntiteGrille[i,j] = new List<Entite>();
            }
        }
    }

    public void Reset() 
    {
        for (int i = 0; i < NbCaseLongueur; i++)
        {
            for (int j = 0; j < NbCaseHauteur; j++)
            {
                EntiteGrille[i, j].Clear();
            }
        }
    }

    public int GetIndiceX(double x) => Math.Max(0, Math.Min((int)(x / TailleCase), NbCaseLongueur - 1));
    public int GetIndiceY(double y) => Math.Max(0, Math.Min((int)(y / TailleCase), NbCaseHauteur  - 1));

    public List<Entite> Get(double x, double y) => EntiteGrille[GetIndiceX(x), GetIndiceY(y)];

    public void Add(Entite e)
    {
        Remove(e);
        e.GrilleIndiceX = GetIndiceX(e.X);
        e.GrilleIndiceY = GetIndiceY(e.Y);
        EntiteGrille[e.GrilleIndiceX, e.GrilleIndiceY].Add(e);
    }

    private void Remove(Entite e) => EntiteGrille[e.GrilleIndiceX, e.GrilleIndiceY].Remove(e);
}
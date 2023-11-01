using Geometry;
using InteractionSauvage;

namespace InteractionSauvage;

public class Grille
{
    private List<Entite>[,] EntiteGrille { get; set; }

    public int TailleCase { get; private set; }

    public Vec2 Size => new(Longueur, Hauteur);
    public int Hauteur  => NbCaseHauteur * TailleCase;
    public int Longueur => NbCaseLongueur * TailleCase;

    public int NbCaseHauteur  { get; private set; }
    public int NbCaseLongueur { get; private set; }

    public Grille(int longueur, int hauteur, int tailleCase)
    {
        NbCaseLongueur = longueur;
        NbCaseHauteur = hauteur;
        EntiteGrille   = new List<Entite>[longueur, hauteur];
        TailleCase     = tailleCase;

        for (int x = 0; x < NbCaseLongueur; x++)
        {
            for (int j = 0; j < NbCaseHauteur; j++)
            {
                EntiteGrille[x,j] = new List<Entite>();
            }
        }
    }

    public bool EstCaseDansVision(int x, int y, float posX, float posY, float distanceVision, Angle direction, Angle champsVision)
    {
        Angle debut = Angle.FromRadian(direction.Radian - champsVision.Radian / 2);
        Angle fin = Angle.FromRadian(direction.Radian + champsVision.Radian / 2);

        float xCarre = y * TailleCase;
        float yCarre = x * TailleCase;

        float distanceHorizontale = Math.Max(xCarre, Math.Min(xCarre + TailleCase, posX)) - posX;
        float distanceVerticale = Math.Max(yCarre, Math.Min(yCarre + TailleCase, posY)) - posY;

        float distanceSquared = (float) distanceHorizontale * distanceHorizontale + distanceVerticale * distanceVerticale;

        if (distanceSquared <= distanceVision * distanceVision)
        {
            Angle angle = Angle.FromRadian((float)Math.Atan2(distanceVerticale, distanceHorizontale));
            if (angle < 0)
            {
                angle.Radian += 2 * (float)Math.PI;
            }

            return angle.EstEntre(debut, fin);
        }

        return false;
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
    public List<Entite> GetByIndice(int x, int y) => EntiteGrille[x, y];

    public void Add(Entite e)
    {
        Remove(e);
        e.GrilleIndiceX = GetIndiceX(e.X);
        e.GrilleIndiceY = GetIndiceY(e.Y);
        EntiteGrille[e.GrilleIndiceX, e.GrilleIndiceY].Add(e);
    }

    private void Remove(Entite e) => EntiteGrille[e.GrilleIndiceX, e.GrilleIndiceY].Remove(e);
}
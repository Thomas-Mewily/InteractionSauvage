using Geometry;
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

    public bool EstCaseDansVision(int i, int j, float posX, float posY, float distanceVision, Angle direction, Angle champsVision)
    {
        Angle debut = Angle.FromRadian(direction.Radian - champsVision.Radian / 2);
        Angle fin = Angle.FromRadian(direction.Radian + champsVision.Radian / 2);

        float xCarre = j * TailleCase;
        float yCarre = i * TailleCase;

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
        for (int i = 0; i < NbCaseHauteur; i++)
        {
            for (int j = 0; j < NbCaseLongueur; j++)
            {
                EntiteGrille[i, j].Clear();
            }
        }
    }

    public int GetIndiceX(double x) => Math.Max(0, Math.Min((int)(x / TailleCase),    NbCaseLongueur - 1));
    public int GetIndiceY(double y) => Math.Max(0, Math.Min((int)(y / TailleCase),    NbCaseHauteur - 1));

    public List<Entite> Get(double x, double y) => EntiteGrille[GetIndiceY(y), GetIndiceX(x)];
    public List<Entite> GetByIndice(int i, int j) => EntiteGrille[i, j];

    public void Add(Entite e)
    {
        Remove(e);
        e.GrilleIndiceX = GetIndiceX(e.X);
        e.GrilleIndiceY = GetIndiceY(e.Y);
        EntiteGrille[e.GrilleIndiceY, e.GrilleIndiceX].Add(e);
    }

    private void Remove(Entite e) => EntiteGrille[e.GrilleIndiceY, e.GrilleIndiceX].Remove(e);
}
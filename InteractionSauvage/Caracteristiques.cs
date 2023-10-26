using System.Numerics;

namespace InteractionSauvage;

public struct Caracteristiques
{
    // [0, 1]. Si 0 => meurt
    public float Nourriture;

    // [0, 1]. Si 0 => Dodo forcé
    public float Energie;

    public double Direction;


    public int Age;
    public double VitesseMax;
    public double X = 0;
    public double Y = 0;
    public double VX = 0;
    public double VY = 0;

    public int EtatIndex;

    public Caracteristiques() { }
}
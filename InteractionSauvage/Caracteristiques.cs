namespace InteractionSauvage;

public struct Caracteristiques
{
    // [0, 1]. Si 0 => meurt
    public float Nourriture;

    // [0, 1]. Si 0 => Dodo forcé
    public float Fatigue;

    public float Direction;


    public int Age;
    public float VitesseMax;
    public float X = 0;
    public float Y = 0;
    public float VX = 0;
    public float VY = 0;

    public int EtatIndex;

    public Caracteristiques() { }
}
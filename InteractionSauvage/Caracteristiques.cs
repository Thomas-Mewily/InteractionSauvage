using InteractionSauvage.MachineEtats;
using System.Numerics;
using static InteractionSauvage.Categories;

namespace InteractionSauvage;

public struct Caracteristiques
{
    // [0, 1]. Si 0 => meurt
    public float Nourriture;

    // [0, 1]. Si 0 => Dodo forcé
    public float Energie;

    public float Direction;

    
    public int Age;
    public float Taille;
    public float VitesseMax = 0;
    public float X = 0;
    public float Y = 0;
    public float VX = 0;
    public float VY = 0;

    public float Score = 0;
    public int TempsDeRepos = 0;

    public string? EtatDeBase = null;

    public Categories Categorie;

    public Caracteristiques() { Categorie = new Categories(CategorieEnum.Plante);  }
}
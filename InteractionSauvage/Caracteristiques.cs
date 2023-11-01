using Geometry;
using InteractionSauvage.MachineEtats;
using System.Numerics;
using static InteractionSauvage.Categories;

namespace InteractionSauvage;

public struct Caracteristiques
{
    /// <summary> [0, 1]. Si 0 => meurt </summary>
    public float Nourriture;
    /// <summary> [0, 1]. Si 0 => Dodo forcé </summary>
    public float Energie;

    public float DistanceVision;
    public Angle ChampsVision;

    public Angle Direction;
    public Vec2 Position;
    public Vec2 Vitesse;

    public int Age;

    public float Rayon;
    public float VitesseMax = 0;

    public float Score = 0;
    public int TempsDeRepos = 0;

    public string? EtatDeBase = null;

    public Categories Categorie;

    public Caracteristiques() { Categorie = new Categories(CategorieEnum.Plante);  }
}
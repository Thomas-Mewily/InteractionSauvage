using Geometry;
using InteractionSauvage.MachineEtats;
using System.Numerics;
using static InteractionSauvage.Categories;

namespace InteractionSauvage;

public struct Caracteristiques
{
    public bool Vivant = true;

    /// <summary> [0, 1]. Si 0 => meurt </summary>
    //public float Nourriture;
    /// <summary> [0, 1]. Si 0 => Dodo forcé </summary>
    public float Energie;

    public float CoefAbandonEnergiePerduPendantLesDeplacements = 1;

    public float RayonVision;
    public Angle ChampsVision;
    public Angle MoitieChampsVision { get => ChampsVision / 2; set => ChampsVision = value * 2; }

    public Angle RotationParSeconde = Angle.FromDegree(180);
    public Angle Direction;
    public Angle DirectionTarget;
    public Vec2 Position;
    public Vec2 OldPosition;
    public Vec2 Vitesse;

    public int Age;

    public float Rayon;
    public float VitesseMax = 0;

    public float Score = 0;
    public int TempsDeRepos = 0;

    public string? EtatDeBase = null;

    public Entite? Target = null;
    public Etat? MaybeEtat = null;
    public Etat Etat
    {
        get => MaybeEtat!;
        set
        {
            MaybeEtat?.Fin();
            MaybeEtat = value!;
            MaybeEtat.Debut();
        }
    }
    public Categories Categorie;

    public Caracteristiques() { Categorie = new Categories(CategorieEnum.Plante);  }
}
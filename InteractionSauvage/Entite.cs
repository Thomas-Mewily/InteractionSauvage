using Geometry;
using InteractionSauvage.MachineEtats;
using Useful;

namespace InteractionSauvage;

public class Entite : SimulationComposante
{
    public string Nom;

    public MachineEtat MachineEtat;
    public string EtatDeBase => DeBase.EtatDeBase ?? MachineEtat.EtatSuggererParDefaut;

    public AnimationBase? Animation = null;

    #region Champs Affecté par la simulation
    public Etat? MaybeEtat = null;
    public Etat Etat
    {
        get => MaybeEtat!;
        set { 
            MaybeEtat?.Fin();
            MaybeEtat = value!;
            MaybeEtat.Debut();
        } 
    }

    public string EtatNom { get => Etat.Nom; set => Etat = MachineEtat[value]; }

    public bool Vivant { get => Actuel.Vivant; set => Actuel.Vivant = value; }
    public float Score { get => Actuel.Score; set => Actuel.Score = Score; }
    public int TempsDeRepos { get => Actuel.TempsDeRepos; set => Actuel.TempsDeRepos = value; }

    public Vec2 Position { get => Actuel.Position; set => Actuel.Position = value; } 
    public float X { get => Actuel.Position.X; set => Actuel.Position.X = value; }
    public float Y { get => Actuel.Position.Y; set => Actuel.Position.Y = value; }

    public int GrilleIndiceX = 0;
    public int GrilleIndiceY = 0;

    public Vec2 Vitesse { get => Actuel.Vitesse; set => Actuel.Vitesse = value; }
    public float VX { get => Actuel.Vitesse.X; set => Actuel.Vitesse.X = value; }
    public float VY { get => Actuel.Vitesse.Y; set => Actuel.Vitesse.Y = value; }

    public float VitesseMax { get => Actuel.VitesseMax; set => Actuel.VitesseMax = value; }
    public Angle Direction { get => Actuel.Direction; set => Actuel.Direction = value; }
    
    public float Energie { get => Actuel.Energie; set => Actuel.Energie = value; }
    public float Nourriture { get => Actuel.Nourriture; set => Actuel.Nourriture = value; }
    
    public int Age { get => Actuel.Age; set => Actuel.Age = value; }

    // Taille & Rayon : même chose
    public float Taille { get => Actuel.Rayon; set => Actuel.Rayon = value; }
    public float Rayon { get => Actuel.Rayon; set => Actuel.Rayon = value; }

    public float RayonVision { get => Actuel.RayonVision; set => Actuel.RayonVision = value; }
    public Angle ChampsVision { get => Actuel.ChampsVision; set => Actuel.ChampsVision = value; }

    public Entite? Target { get => Actuel.Target; set => Actuel.Target = value; }
    public Categories Categorie { get => Actuel.Categorie; set => Actuel.Categorie = value; }
    #endregion

    private Caracteristiques Actuel;
    public Caracteristiques DeBase 
    { 
        get => CheckPoints.Count == 0 ? Actuel : CheckPoints.Peek(); 
        set 
        { 
            if(CheckPoints.Count == 0) 
            {
                Actuel = value;
            }
            else 
            {
                CheckPoints[0] = value;
            }
        } 
    }
    public List<Caracteristiques> CheckPoints = new() { new Caracteristiques() };

    public Entite(Simulation simu, string? nom = null, MachineEtat ? e = null)
    {
        Nom = nom ?? "";
        MachineEtat = e ?? new MachineEtat();
        Vivant = true;

        Load(simu); // because of the need of Simu for rand

        //simu.Grille.Add(this);
    }

    public void Meurt()
    {
        if (Vivant)
        {
            Grille.Remove(this);
            Simu.ToutesLesEntites.Remove(this);
            Vivant = false;
        }
    }

    public override void Load() 
    {
        MachineEtat.Load(this);
    }

    public void Update()
    {
        Animation?.Update(this);

        Actuel.Age++;
        TempsDeRepos = TempsDeRepos > 0 ? TempsDeRepos - 1 : 0;

        Etat.Update();
    }


    public void RngDirection()
    {
        Actuel.Direction = (Rand.NextFloat(2f * MathF.PI));
    }

    public bool EstDansVision(int x, int y, float distanceVision, Angle direction, Angle champsVision)
    {
        Angle debut = Angle.FromRadian(direction.Radian - champsVision.Radian / 2);
        Angle fin = Angle.FromRadian(direction.Radian + champsVision.Radian / 2);


        float distanceHorizontale = X - x;
        float distanceVerticale = Y - y;

        float distanceSquared = (float)distanceHorizontale * distanceHorizontale + distanceVerticale * distanceVerticale;

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


    public IEnumerable<Entite> EntitiesNearsBy(float range)
    {
        int minY = Math.Max(0, (int)((Y - RayonVision) / Grille.TailleCase));
        int maxY = Math.Min(Grille.NbCaseHauteur - 1, (int)((Y + RayonVision) / Grille.TailleCase));
        int minX = Math.Max(0, (int)((X - RayonVision) / Grille.TailleCase));
        int maxX = Math.Min(Grille.NbCaseLongueur - 1, (int)((X + RayonVision) / Grille.TailleCase));

        for (int y = minY; y < maxY; y++)
        {
            for (int x = minX; x < maxX; x++)
            {
                foreach (var e in Grille.Get(x, y))
                {
                    yield return e;
                }
            }
        }
    }

    public void NouritureDirection()
    {
        float distancePlusProche = float.MaxValue;
        Entite? plusProche = null;

        int minY = Math.Max(0, (int)((Y - RayonVision) / Grille.TailleCase));
        int maxY = Math.Min(Grille.NbCaseHauteur - 1, (int)((Y + RayonVision) / Grille.TailleCase));
        int minX = Math.Max(0, (int)((X - RayonVision) / Grille.TailleCase));
        int maxX = Math.Min(Grille.NbCaseLongueur - 1, (int)((X + RayonVision) / Grille.TailleCase));

        for (int y = minY; y < maxY; y++)
        {
            for (int x = minX; x < maxX; x++)
            {
                if (Grille.EstCaseDansVision(x, y, X, Y, RayonVision, Direction, ChampsVision))
                {
                    foreach (Entite e in Grille.Get(x, y))
                    {
                        //Console.WriteLine(e);
                        if (Categorie.CategoriesNouritures.Contains(e.Categorie.Categorie))
                        {
                            float distance = (e.X - X) * (e.X - X) + (e.Y - Y) * (e.Y - Y);
                            if (distance < distancePlusProche)
                            {
                                distancePlusProche = distance;
                                plusProche = e;
                            }
                        }
                    }
                }

            }
        }

        if (distancePlusProche != float.MaxValue) 
        {
            Target = plusProche;
            Direction = Angle.FromRadian(float.Atan2((plusProche!.Y - Y), (plusProche!.X - X)));
        }
        else
        {
            if (Rand.NextDouble() <= 0.1)
            {
                RngDirection();
            }
        }
    }

    public void PositionChanger() => Grille.Add(this);

    public bool Collision(double x, double y)
    {
        foreach (Entite e in Grille.Get(x, y))
        {
            double distance = Math.Pow(x - e.X, 2) + Math.Pow(y - e.Y, 2);

            if (e != this && e.Categorie != Categorie && distance <= Math.Pow(Rayon + e.Rayon, 2)) return true; 
        }

        return false;
    }

    public void Avancer(float coef = 1)
    {
        float deltaX = coef * VitesseMax * Direction.Cos;
        float deltaY = coef * VitesseMax * Direction.Sin;

        if(deltaX* deltaX + deltaY* deltaY < 0.1) { return; }

        if (X + deltaX > 0 && X + deltaX < Grille.Longueur && Y + deltaY > 0 && Y + deltaY < Grille.Hauteur && !Collision(X+deltaX, Y+deltaY))
        {
            X += deltaX;
            Y += deltaY;
            Energie -= coef;

            PositionChanger();
        }
        else 
        {
            Avancer(coef / 2);
            Avancer(coef / 2);
        }

        Nourriture -= 1 * coef;
        Energie    -= 1 * coef;
    }

    public float ProbaManger(Entite e)
    {
        return -0.5f*(e.Taille/Taille)+1f;
    }


    public float DistanceTo(Entite e) => float.Sqrt((e.X - X) * (e.X - X) + (e.Y - Y) * (e.Y - Y));


    public void Affiche()
    {
        Console.WriteLine("================= " + Nom + " =========");
        Console.WriteLine("Categorie   = " + Actuel.Categorie);
        Console.WriteLine("Etat        = " + Etat);
        Console.WriteLine("TempsDeRepos= " + TempsDeRepos);
        Console.WriteLine("Nouriture   = " + Nourriture);
        Console.WriteLine("Energie     = " + Energie);
        Console.WriteLine("Age         = " + Age);
        Console.WriteLine("Taille      = " + Taille);
        Console.WriteLine("Vivant      = " + Vivant);
        Console.WriteLine("VitesseMax  = " + VitesseMax);
        Console.WriteLine("Direction   = " + Direction);
        Console.WriteLine("X,  Y       = (" + X  + ", " + Y  + ")");
        Console.WriteLine("VX, VY      = (" + VX + ", " + VY + ")");
    }

    public override string ToString() => Nom.Length == 0 ? GetType().Name : Nom;

    #region CheckPoint
    public override void CheckPointReset()
    {
        EtatNom = EtatDeBase;
        base.CheckPointReset();
    }

    public override void CheckPointAdd()
    {
        CheckPoints.Add(Actuel);
        MachineEtat.CheckPointAdd();

        base.CheckPointAdd();
    }

    public override void CheckPointRemove()
    {
        base.CheckPointRemove();
    }

    public override void CheckPointRollBack()
    {
        base.CheckPointRollBack();
    }
    #endregion
}
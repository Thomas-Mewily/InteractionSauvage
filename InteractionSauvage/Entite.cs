using Geometry;
using InteractionSauvage.MachineEtats;
using InteractionSauvage.Passifs;
using SimulationConsole;
using Useful;

namespace InteractionSauvage;

public struct EntiteDistance 
{
    public Entite E;
    public float Distance;

    public EntiteDistance(Entite e, float distance)
    {
        this.E = e;
        this.Distance = distance;
    }
}

public class Entite : SimulationComposante
{
    public string Nom;

    public MachineEtat MachineEtat;
    public string EtatDeBase => DeBase.EtatDeBase ?? MachineEtat.EtatSuggererParDefaut;

    public AnimationBase? Animation = null;

    #region Champs Affecté par la simulation
    public Etat Etat { get => Actuel.Etat; set => Actuel.Etat = value; }
    public string EtatNom { get => Etat.Nom; set => Etat = MachineEtat[value]; }

    public bool Vivant { get => Actuel.Vivant; set => Actuel.Vivant = value; }
    public float Score { get => Actuel.Score; set => Actuel.Score = Score; }

    public Vec2 OldPosition { get => Actuel.OldPosition; set => Actuel.OldPosition = value; } 
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
    public Angle DirectionTarget { get => Actuel.DirectionTarget; set => Actuel.DirectionTarget = value; }
    
    public float Energie
    {
        get => Actuel.Energie; 
        //set => Actuel.Energie = value;
        set => Actuel.Energie = Math.Min(1, Math.Max(0, value));
    }
    //public float Nourriture { get => Actuel.Nourriture; set => Actuel.Nourriture = value; }
    
    public int Age { get => Actuel.Age; set => Actuel.Age = value; }

    // Taille & Rayon : même chose
    public float Taille { get => Actuel.Rayon; set => Actuel.Rayon = value; }
    public float Rayon { get => Actuel.Rayon; set => Actuel.Rayon = value; }

    public float CoefAbandonEnergiePerduPendantLesDeplacements { get => Actuel.CoefAbandonEnergiePerduPendantLesDeplacements; set => Actuel.CoefAbandonEnergiePerduPendantLesDeplacements = value; }
    public float RayonVision { get => Actuel.RayonVision; set => Actuel.RayonVision = value; }
    public Angle ChampsVision { get => Actuel.ChampsVision; set => Actuel.ChampsVision = value; }
    public Angle MoitieChampsVision { get => Actuel.MoitieChampsVision; set => Actuel.MoitieChampsVision = value; }
    public Angle RotationParSeconde { get => Actuel.RotationParSeconde; set => Actuel.RotationParSeconde = value; }

    public Entite? Target { get => Actuel.Target; set => Actuel.Target = value; }
    public Categories Categorie { get => Actuel.Categorie; set => Actuel.Categorie = value; }

    public bool Dors => Etat.Passif is Dormir;
    #endregion

    public Caracteristiques Actuel;
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
            Animation?.Meurt(this);
            Grille.Remove(this);
            Simu.ToutesLesEntites.Remove(this);
            Vivant = false;
        }
    }

    public void Replication()
    {
        if(Rand.Next(0, Grille.Get(GrilleIndiceX, GrilleIndiceY).Count * Grille.Get(GrilleIndiceX, GrilleIndiceY).Count) > 10) 
        {
            return;
        }
        float newX = X + Taille * Rand.FloatUniform(-1, 1) * 4;
        float newY = Y + Taille * Rand.FloatUniform(-1, 1) * 4;

        newX = (newX + Grille.Longueur) % Grille.Longueur;
        newY = (newY + Grille.Hauteur) % Grille.Hauteur;

        Entite newEntite = new Entite(Simu, "~" + Nom, MachineEtat.Clone());
        newEntite.Actuel = Actuel;

        newEntite.DeBase = DeBase;
        newEntite.WithPosition(newX, newY);
        newEntite.Target = null;
        newEntite.Taille *= Rand.FloatUniform(0.8f, 1.2f);
        //if(newEntite.Taille < 0) newEntite.Taille = -newEntite.Taille;
        newEntite.Categorie = Categorie;
        newEntite.EtatNom = newEntite.MachineEtat.EtatSuggererParDefaut;
        newEntite.Animation = Animation;

        newEntite.Load();
        Simu.ToutesLesEntites.Add(newEntite);
    }

    public override void Load() 
    {
        MachineEtat.Load(this);
        Animation?.Load(this);
    }

    public const float EnergiePerduParTour = 0.00001f;

    public void Update()
    {
        if (!Vivant) return;

        OldPosition = Position;

        if(Target != null && (Target.Vivant == false || PeutVoir(Target) == false || Rand.Next()%500==0))
        {
            Target = null;
        }
        
        if(Direction != DirectionTarget && Dors == false) 
        {
            var dif = (Direction - DirectionTarget).NormalizedCenter;
            Angle turnPerTick = (RotationParSeconde / Temps.OneSecond).NormalizedCenter;

            if(dif > turnPerTick) { dif = turnPerTick;  }
            else if(dif < -turnPerTick) { dif = -turnPerTick; }

            Direction -= dif;
        }
        Animation?.Update(this);

        Actuel.Age++;
        Energie = Energie > 0 ? Energie - EnergiePerduParTour : 0;

        Etat.Update();
    }


    public void RngDirection()
    {
        DirectionTarget = (Rand.NextFloat(2f * MathF.PI));
    }

    public bool PeutVoir(Entite e) 
    {
        // trop loin
        //if(DistanceVisionTo(e) < RayonVision) { return false; }

        var dir = new Vec2(Position, e.Position);
            
        if(dir.Angle.Inside(Direction - MoitieChampsVision, Direction + MoitieChampsVision)) { return true; } // clairement devant lui

        // sur les côté
        Angle bonus = MathF.Sinh(e.Rayon / dir.Length);
        if (dir.Angle.Inside(Direction - MoitieChampsVision - bonus, Direction + MoitieChampsVision + bonus)) { return true; }

        return false;
    }
    public bool PeutManger(Entite e) => Categorie.CategoriesNouritures.Contains(e.Categorie.Categorie) && e != this;
    public bool Collision(Entite e) => (Position - e.Position).LengthSquared <= (Rayon + e.Rayon) * (Rayon + e.Rayon);
    public float DistanceVisionTo(Entite e) => float.Max(0, (Position - e.Position).Length-e.Rayon);
    public float DistanceToNoHitbox(Entite e) => (Position - e.Position).Length;

    #region Chunks / Vision
    public const float BonusRadiusBigEntityMax = 2;

    public IEnumerable<EntiteDistance> EntitesProcheAvecDistance() => EntitesProcheAvecDistance(RayonVision);
    public IEnumerable<EntiteDistance> EntitesProcheAvecDistance(float radius)
    {
        foreach (var e in EntitesProcheCarre(radius + BonusRadiusBigEntityMax))
        {
            float d = DistanceVisionTo(e);
            if (d < radius)
            {
                yield return new EntiteDistance(e, d);
            }
        }
    }

    public IEnumerable<Entite> EntitesProche() => EntitesProche(RayonVision);
    public IEnumerable<Entite> EntitesProche(float radius) => EntitesProcheAvecDistance(radius).Select(t => t.E);
        /*
        var radius_squared = radius * radius;
        foreach (var e in EntitesProcheCarre(radius + BonusRadiusBigEntityMax))
        {
            float distance = Math.Max(0, (e.X - X) * (e.X - X) + (e.Y - Y) * (e.Y - Y) - e.Rayon * e.Rayon);
            if (distance < radius_squared)
            {
                yield return e;
            }
        }*/

    public IEnumerable<Entite> EntitesVisibles() => EntitesVisibles(RayonVision);
    public IEnumerable<Entite> EntitesVisibles(float radius) => EntitesProche(radius).Where(t => PeutVoir(t));

    public IEnumerable<Entite> EntitesProcheCarre() => EntitesProcheCarre(RayonVision);
    public IEnumerable<Entite> EntitesProcheCarre(float demi_cote) => Grille.EntitesDansRectangle(X, Y, demi_cote * 2, demi_cote * 2).Where(t => t != this);
    #endregion


    public Entite? NouritureDirection()
    {
        var plusProche = TrouverNouritureDirection();

        if(plusProche != null && plusProche.Vivant) 
        {
            Target = plusProche;
            DirectionTarget = new Vec2(Position, plusProche.Position).Angle;
        }
        else 
        {
            if (Rand.NextDouble() <= 0.02)
            {
                RngDirection();
            }
        }
        return plusProche; // déjà dessus
    }

    public Entite? TrouverNouritureDirection()
    {
        float distancePlusProche = float.MaxValue;
        Entite? plusProche = null;

        foreach (var info in EntitesProcheAvecDistance(RayonVision))
        {
            var entite = info.E;
            var distance = info.Distance;

            if (distance < distancePlusProche && PeutManger(entite) && PeutVoir(entite))
            {
                distancePlusProche = info.Distance;
                plusProche = info.E;
            }
        }

        return plusProche;
    }

    public void PositionChanger() => Grille.Add(this);

    /*
    public bool Collision(double x, double y)
    {
        foreach (Entite e in Grille.Get(x, y))
        {
            double distance = Math.Pow(x - e.X, 2) + Math.Pow(y - e.Y, 2);

            if (e != this && e.Categorie != Categorie && distance <= Math.Pow(Rayon + e.Rayon, 2)) 
            {
                return true;
            }
        }

        return false;
    }*/

    public void Avancer(float coef = 1)
    {
        float deltaX = coef * VitesseMax * Direction.Cos;
        float deltaY = coef * VitesseMax * Direction.Sin;

        if(deltaX* deltaX + deltaY* deltaY < 0.0001f) { return; }

        if (X + deltaX > 0 && X + deltaX < Grille.Longueur && Y + deltaY > 0 && Y + deltaY < Grille.Hauteur)// && !Collision(X+deltaX, Y+deltaY))
        {
            X += deltaX;
            Y += deltaY;
            Energie -= coef * VitesseMax * 0.05f * CoefAbandonEnergiePerduPendantLesDeplacements;
            //Nourriture -= coef;

            PositionChanger();
        }
        else 
        {
            Avancer(coef / 2);
            Avancer(coef / 2);
        }

    }

    public float ProbaManger(Entite e)
    {
        return -0.5f*(e.Taille/Taille)+1f;
    }

    public void Absorber(Entite e, float pourcentage=1f) 
    {
        float cible_rayon_absorber = e.Rayon * pourcentage;
        e.Rayon -= cible_rayon_absorber;

        if (e.Rayon < 0.1f) 
        {
            cible_rayon_absorber += e.Rayon;
            e.Rayon = 0;
        }

        float radius_add = float.Sqrt(Rayon * Rayon + cible_rayon_absorber * cible_rayon_absorber) - Rayon;
        Rayon += radius_add/5;
        float energie_gagne = radius_add * radius_add * 700;
        Energie += energie_gagne;

        if (e.Rayon == 0f)
        { 
            e.Meurt();
        }
    }

    public void Affiche()
    {
        Console.WriteLine("================= " + Nom + " =========");
        Console.WriteLine("Categorie   = " + Actuel.Categorie);
        Console.WriteLine("Etat        = " + Etat);
        Console.WriteLine("TempsDeRepos= " + Energie);
        //Console.WriteLine("Nouriture   = " + Nourriture);
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

        MachineEtat.CheckPointReset();
    }

    public override void CheckPointAdd()
    {
        base.CheckPointAdd();

        CheckPoints.Add(Actuel);
        MachineEtat.CheckPointAdd();
    }

    public override void CheckPointRemove()
    {
        base.CheckPointRemove();
        CheckPoints.Pop();
        MachineEtat.CheckPointRemove();
    }

    public override void CheckPointRollBack()
    {
        base.CheckPointRollBack();
        Actuel = CheckPoints.Peek();
        MachineEtat.CheckPointRollBack();
    }
    #endregion
}
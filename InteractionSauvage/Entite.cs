﻿using Geometry;
using InteractionSauvage.Interruptions;
using InteractionSauvage.MachineEtats;
using InteractionSauvage.Passifs;
using System.ComponentModel;
using Useful;
using static InteractionSauvage.MachineEtats.Etat;

namespace InteractionSauvage;

public class Entite : SimulationComposante
{
    public string Nom;

    public MachineEtat MachineEtat;
    public string EtatDeBase => DeBase.EtatDeBase == null ? MachineEtat.EtatSuggererParDefaut : DeBase.EtatDeBase;

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

    public float Score { get => Actuel.Score; set => Actuel.Score = Score; }
    public int TempsDeRepos { get => Actuel.TempsDeRepos; set => Actuel.TempsDeRepos = TempsDeRepos; }

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

    public float Rayon { get => Actuel.Rayon; set => Actuel.Rayon = value; }

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
    public List<Caracteristiques> CheckPoints = new List<Caracteristiques>() { new Caracteristiques() };

    public Entite(Simulation simu, string? nom = null, MachineEtat ? e = null)
    {
        Nom = nom ?? "";
        MachineEtat = e ?? new MachineEtat();

        Load(simu); // because of the need of Simu for rand
    }

    public override void Load() 
    {
        MachineEtat.Load(this);
    }

    public void Update()
    {
        Actuel.Age++;
        TempsDeRepos = TempsDeRepos > 0 ? TempsDeRepos - 1 : 0;

        Etat.Update();
    }


    public void RngDirection()
    {
        Actuel.Direction = (Rand.NextFloat(2f * MathF.PI));
    }

    public void PositionChanger() => Grille.Add(this);

    public bool Collision(double x, double y)
    {
        foreach (Entite e in Grille.Get(x, y))
        {
            double distance = Math.Pow(x - e.X, 2) + Math.Pow(y - e.Y, 2);

            if (e != this && distance <= Math.Pow(Rayon + e.Rayon, 2)) return true; 
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
    }


    public void Affiche()
    {
        Console.WriteLine("================= " + Nom + " =========");
        Console.WriteLine("Categorie   = "  + Actuel.Categorie);
        Console.WriteLine("Etat        = "  + Etat);
        Console.WriteLine("TempsDeRepos= "  + TempsDeRepos);
        Console.WriteLine("Nouriture   = "  + Nourriture);
        Console.WriteLine("Energie     = "  + Energie);
        Console.WriteLine("Age         = "  + Age);
        Console.WriteLine("VitesseMax  = "  + VitesseMax);
        Console.WriteLine("Direction   = "  + Direction);
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
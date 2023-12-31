﻿using System.Collections;
using Useful;

namespace InteractionSauvage;

public struct SimulationChamps 
{
    public List<Entite> Entites = new();
    public Temps Time;

    public SimulationChamps() { }
}

public class Simulation : CheckPointable
{
    SimulationChamps Actuel;
    public List<SimulationChamps> CheckPoints = new() { };

    public List<Entite> ToutesLesEntites { get => Actuel.Entites; set => Actuel.Entites = value; }
    public List<Entite> EntitesDeBase => CheckPoints.Count == 0 ? Actuel.Entites : CheckPoints[0].Entites;
    
    public Temps Time { get => Actuel.Time; set => Actuel.Time = value; }

    private bool IsInit = false;

    public Grille Grille;
    public Random Rand;

    public void Add(Entite e) 
    {
        ToutesLesEntites.Add(e);
    }

    public Simulation(int longeur = 160*2, int largeur = 90*2, int tailleChunck = 4)
    {
        Time = new Temps();

        Actuel.Time = 0;
        Actuel.Entites = new List<Entite>();

        Grille = new Grille(longeur/10, largeur/10, tailleChunck);
        Rand = new Random();
    }

    private void InitComposants() 
    {
        foreach (var e in EntitesDeBase)
        {
            // Initialise
            e.Load(this);
        }
    }

    public void Update(int nbStep) 
    {
        for (int i = 0; i < nbStep; i++)
        {
            Update();
        }
    }


    public void Update()
    {
        Time++;
        foreach (var e in ToutesLesEntites.ToList()) 
        {
            e.Update();
        }
    }

    public void LancerSimulation(int nbStep = 200)
    {
        Reset();
        ToutesLesEntites.ForEach(entite => { entite.Affiche(); });
        for (int i = 0; i < nbStep; i++)
        {
            Console.WriteLine("-----------" + i + "-----------");
            Update();
            ToutesLesEntites.ForEach(entite => { entite.Affiche(); });
        }
    }

    #region CheckPoint
    public override void CheckPointAdd()
    {
        CheckPoints.Add(Actuel);
        Actuel.Entites = Actuel.Entites.ToList();
        ToutesLesEntites.ForEach(e => e.CheckPointAdd());
        base.CheckPointAdd();
    }

    public override void CheckPointRemove()
    {
        CheckPoints.Pop();
        ToutesLesEntites.ForEach(e => e.CheckPointRemove());
        base.CheckPointRemove();
    }

    public override void CheckPointRollBack()
    {
        Actuel = CheckPoints.Peek();
        Actuel.Entites = Actuel.Entites.ToList();

        foreach (var e in ToutesLesEntites)
        {
            e.CheckPointRollBack();
        }

        Grille.Reset(); // certaines entité n'existait pas encore
        foreach (var e in ToutesLesEntites)
        {
            Grille.Add(e);
        }

        base.CheckPointRollBack();
    }

    public void Reset() => CheckPointReset();
    public override void CheckPointReset()
    {
        if (IsInit == false && EntitesDeBase.Count != 0)
        {
            CheckPointAdd();
            InitComposants();
            IsInit = true;
        }

        ToutesLesEntites.ForEach(e => e.CheckPointReset());
        base.CheckPointReset();
    }
    #endregion
}
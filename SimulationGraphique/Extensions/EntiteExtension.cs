using Render;
using InteractionSauvage;

namespace SimulationGraphique;

public static class EntiteExtension
{
    public static bool IsSelected(this Entite e) 
    {
        return (Camera.Peek().WorldPosition(All.Input.Mouse.X, All.Input.Mouse.Y) - e.Position).LengthSquared < e.Rayon * e.Rayon && e.Animation != null;
    }
}

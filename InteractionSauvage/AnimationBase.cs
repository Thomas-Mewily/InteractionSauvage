namespace InteractionSauvage;

public abstract class AnimationBase
{
    public virtual void Update(Entite e) { }
    public virtual void Draw(Entite e) { }

    public virtual void Meurt(Entite e) { }

    public virtual void Load(Entite e) { }

    public virtual void DrawExtraInfo(Entite e) { }
    public virtual void DrawChampsVision(Entite e) { }
}
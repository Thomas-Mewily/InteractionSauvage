using System.Collections;
using Useful;

namespace InteractionSauvage;

// Pas une interface car l'héritage suffit amplement...
public abstract class CheckPointable
{
    public int NbCheckPoint = 0;
    public virtual void CheckPointAdd() { NbCheckPoint++; }
    public virtual void CheckPointRemove() { NbCheckPoint--; }
    public virtual void CheckPointRollBack() { }

    public virtual void CheckPointReset()
    {
        (NbCheckPoint >= 1).Check();
        while(NbCheckPoint > 1) { CheckPointRemove();}
    }
}

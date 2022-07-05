using NeoLib.component;

namespace NeoLib.system;

public abstract class System
{
    protected List<Component> Components;

    protected int Mask;

    protected void SetMask(int newMask)
    {
        Mask = newMask;
    }

    public int GetMask()
    {
        return Mask;
    }

    public List<Component> Components1 => Components;

    protected System()
    {
        Components = new List<Component>();
    }

    public abstract void UpdateComponents(float dt, List<Component> components);
}
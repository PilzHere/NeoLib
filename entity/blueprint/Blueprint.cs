namespace NeoLib.entity.blueprint;

public abstract class Blueprint
{
    protected Blueprint()
    {
        
    }

    public abstract Entity Build(Entity entity);
}
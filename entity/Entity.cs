using NeoLib.component;
using NeoLib.entity.blueprint;

namespace NeoLib.entity;

public class Entity
{
    private readonly int _id;

    public int Id => _id;

    private List<Component> _components;

    public List<Component> Components => _components;

    public Entity(int id)
    {
        _id = id;

        _components = new List<Component>();
    }

    public void AddComponent(Component component)
    {
        _components.Add(component);
    }

    public void RemoveComponent(Component component)
    {
        _components.Remove(component);
    }

    public void RemoveAllComponents()
    {
        while (_components.Count > 0)
        {
            _components.RemoveAt(_components.Count - 1);
        }
    }
}
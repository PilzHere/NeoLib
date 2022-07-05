using NeoLib.component;
using NeoLib.entity;
using NeoLib.entity.blueprint;
using NeoLib.math;
using NeoLib.system.logic;
using NeoLib.system.render;

namespace NeoLib.engine;

/*
? If this is ever slow try implementing ECS by Adam Martin instead.
https://gist.github.com/LearnCocos2D/77f0ced228292676689f#ecs-by-adam-martin
*/

public class EcsEngine
{
    private List<Entity> _entities;
    private List<int> _unusedIds;

    private List<system.System> _logicSystems;
    private List<system.System> _renderSystems;

    private readonly bool _isUsingRenderSystems;

    public EcsEngine(bool isUsingRenderSystems)
    {
        _isUsingRenderSystems = isUsingRenderSystems;

        _logicSystems = new List<system.System>();

        if (_isUsingRenderSystems)
            _renderSystems = new List<system.System>();

        _entities = new List<Entity>(); // ? Not max size?: Out of Memory!
        _unusedIds = new List<int>();
    }

    public void Start()
    {
        // ! The order of systems is critical. It is in this sequence entities/components will update.

        // * Logic components
        AddLogicSystem(new InputSystem());
        AddLogicSystem(new PhysicsSystem());
        AddLogicSystem(new TransformSystem());

        // * Render components
        if (_isUsingRenderSystems)
        {
            AddRenderSystem(new ShapeSystem());
        }
    }

    public void CreateEntity(Blueprint blueprint)
    {
        var newEntity = new Entity(GetNewEntityId());
        newEntity = blueprint.Build(newEntity);

        AddEntity(newEntity);
    }

    private int GetNewEntityId()
    {
        if (_unusedIds.Count > 0)
        {
            return _unusedIds.ElementAt(0);
        }

        return _entities.Count;
    }

    public void AddLogicSystem(system.System newSystem)
    {
        if (!_logicSystems.Contains(newSystem))
        {
            _logicSystems.Add(newSystem);
        }
    }

    public void AddRenderSystem(system.System newSystem)
    {
        if (!_renderSystems.Contains(newSystem))
        {
            _renderSystems.Add(newSystem);
        }
    }

    public void RemoveLogicSystem(system.System system)
    {
        if (_logicSystems.Contains(system))
        {
            _logicSystems.Remove(system);
        }
    }

    public void RemoveRenderSystem(system.System system)
    {
        if (_renderSystems.Contains(system))
        {
            _renderSystems.Remove(system);
        }
    }

    public void RemoveAllSystems()
    {
        if (_isUsingRenderSystems)
            _renderSystems.Clear();

        _logicSystems.Clear();

        Console.WriteLine("ECS: All systems removed.");
    }

    public void AddEntity(Entity newEntity)
    {
        _entities.Add(newEntity);
    }

    public void RemoveEntity(Entity entity)
    {
        _unusedIds.Add(entity.Id);

        _entities.Remove(entity);
    }

    public void RemoveAllEntities()
    {
        _entities.Clear();

        Console.WriteLine("ECS: All entities removed.");
    }

    private void RemoveComponentlessEntities()
    {
        foreach (var entity in _entities)
        {
            if (entity.Components.Count == 0)
                _entities.Remove(entity);
        }
    }

    public void Update(float dt)
    {
        //Console.WriteLine("ECS: LOGIC Systems -- NOW --");

        RemoveComponentlessEntities();

        var componentsToSystem = new List<Component>();

        foreach (var entity in _entities)
        {
            foreach (var system in _logicSystems)
            {
                foreach (var component in entity.Components)
                {
                    // * (Does this system want this component?)
                    if (GameMath.MaskContainsBits(system.GetMask(), component.GetBits()))
                        componentsToSystem.Add(component);
                }

                system.UpdateComponents(dt, componentsToSystem);
                componentsToSystem.Clear();
            }
        }
    }

    public void Render()
    {
        //Console.WriteLine("ECS: RENDER Systems -- NOW --");

        if (_isUsingRenderSystems)
        {
            var componentsToSystem = new List<Component>();

            foreach (var entity in _entities)
            {
                foreach (var system in _renderSystems)
                {
                    foreach (var component in entity.Components)
                    {
                        // * (Does this system want this component?)
                        if (GameMath.MaskContainsBits(system.GetMask(), component.GetBits()))
                            componentsToSystem.Add(component);
                    }

                    // ? 0 = DeltaTime not needed in rendering.
                    system.UpdateComponents(0, componentsToSystem);
                    componentsToSystem.Clear();
                }
            }
        }
    }

    public void Stop()
    {
        RemoveAllSystems();
        RemoveAllEntities();
    }
}
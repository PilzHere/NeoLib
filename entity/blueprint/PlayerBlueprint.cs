using System.Numerics;
using NeoLib.component.logic;
using NeoLib.component.render;
using Raylib_cs;

namespace NeoLib.entity.blueprint;

public class PlayerBlueprint : Blueprint
{
    private Vector3 _position, _scale;
    private Vector4 _rotation;
    
    public PlayerBlueprint(Vector3 position, Vector4 rotation, Vector3 scale)
    {
        _position = position;
        _rotation = rotation;
        _scale = scale;
    }

    public override Entity Build(Entity entity)
    {
        var velocity = Vector3.Zero;
        var width = 200f;
        var height = 200f;
        
        //entity.AddComponent(new NetworkIdComponent());
        entity.AddComponent(new InputComponent());
        entity.AddComponent(new VelocityComponent(velocity));
        entity.AddComponent(new TransformComponent(_position, _rotation, _scale));
        entity.AddComponent(new RectangleComponent(new Vector2(_position.X, _position.Y), new Vector2(width, height),
            Color.RED, Color.GOLD));

        return entity;
    }
}
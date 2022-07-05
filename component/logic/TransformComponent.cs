using System.Numerics;
using Raylib_cs;

namespace NeoLib.component.logic;

public class TransformComponent : Component
{
    public new const int Bits = 2; // 0b0010
    
    private Transform _transform;

    public Vector3 OldPos;
    
    //public Vector3 Direction;
    
    //public float currentVelocityX;
    //public float currentSpeedY;
    //public float currentSpeedZ;

    public float directionX;
    
    public TransformComponent(Vector3 position, Vector4 rotation, Vector3 scale)
    {
        base.Bits = Bits;
        
        _transform = new Transform();
        
        _transform.scale = scale;
        _transform.rotation = rotation;
        _transform.translation = position;

        OldPos = _transform.translation;
        
        //Direction = new Vector3();
    }

    public Transform Transform
    {
        get => _transform;
        set => _transform = value;
    }
}
using System.Numerics;

namespace NeoLib.component.logic;

public class VelocityComponent : Component
{
    public new const int Bits = 1; // 0b0001

    private Vector3 _velocity;
    private Vector3 _direction;

    public VelocityComponent(Vector3 velocity)
    {
        base.Bits = Bits;
        
        _velocity = velocity;
        _direction = Direction;
    }

    public Vector3 Velocity
    {
        get => _velocity;
        set => _velocity = value;
    }

    public Vector3 Direction
    {
        get
        {
            if (!_velocity.Equals(Vector3.Zero))
            {
                _direction = Vector3.Normalize(_velocity);
                return _direction;
            }

            return _velocity;
        }
    }
}
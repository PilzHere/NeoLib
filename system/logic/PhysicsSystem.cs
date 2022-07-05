using System.Numerics;
using NeoLib.component.logic;
using NeoLib.math;
using Raylib_cs;

namespace NeoLib.system.logic;

public class PhysicsSystem : System
{
    public PhysicsSystem()
    {
        Mask = VelocityComponent.Bits | TransformComponent.Bits;
    }

    public override void UpdateComponents(float dt, List<component.Component> components)
    {
        Update(dt, components);
    }

    public void Update(float dt, List<component.Component> components)
    {
        //Console.WriteLine("<VELOCITY System>");

        VelocityComponent veloComp = null;
        TransformComponent transComp = null;

        foreach (var component in components)
        {
            switch (component.GetBits())
            {
                case VelocityComponent.Bits:
                    veloComp = (VelocityComponent)component;
                    break;
                case TransformComponent.Bits:
                    transComp = (TransformComponent)component;
                    break;
            }
        }

        var gravityForceY = 0; // -9.81f

        var currentVeloX = veloComp.Velocity.X;
        var currentVeloY = veloComp.Velocity.Y;

        const float dampenForceX = 2.5f;
        if (currentVeloX > 0)
            currentVeloX -= dt * dampenForceX;
        else if (currentVeloX < 0)
            currentVeloX += dt * dampenForceX;

        const float dampenForceY = 2.5f;
        if (currentVeloY > 0)
            currentVeloY -= dt * dampenForceY;
        else if (currentVeloY < 0)
            currentVeloY += dt * dampenForceY;

        const float stopRangeX = 0.1f;
        if (GameMath.InRange(currentVeloX, -stopRangeX, stopRangeX))
            currentVeloX = 0;

        const float stopRangeY = 0.1f;
        if (GameMath.InRange(currentVeloY, -stopRangeY, stopRangeY))
            currentVeloY = 0;

        const float maxVelocityX = 10f;
        currentVeloX = GameMath.Limit(currentVeloX, -maxVelocityX, maxVelocityX);

        const float maxVelocityY = 10f;
        currentVeloY = GameMath.Limit(currentVeloY, -maxVelocityY, maxVelocityY);

        // apply gravity force
        currentVeloY -= dt * gravityForceY;

        var newVelo = new Vector3(
            currentVeloX,
            currentVeloY,
            veloComp.Velocity.Z);

        veloComp.Velocity = newVelo;
    }
}
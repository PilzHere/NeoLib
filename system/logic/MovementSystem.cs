using System.Numerics;
using NeoLib.component;
using NeoLib.component.logic;
using NeoLib.math;
using Raylib_cs;

namespace NeoLib.system.logic;

public class TransformSystem : System
{
    public List<Component> Components2 => Components;

    public TransformSystem()
    {
        Mask = VelocityComponent.Bits | TransformComponent.Bits | InputComponent.Bits;
    }

    private float _currentMoveSpeedY;
    private float _currentMoveSpeedZ;

    public override void UpdateComponents(float dt, List<Component> components)
    {
        VelocityComponent veloComp = null;
        TransformComponent transComp = null;
        InputComponent inComp = null;

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
                case InputComponent.Bits:
                    inComp = (InputComponent)component;
                    break;
            }
        }

        var currentVelocityX = veloComp.Velocity.X;
        var currentVelocityY = veloComp.Velocity.Y;
        var currentVelocityZ = veloComp.Velocity.Z;

        const float speedIncrementX = 25f;
        const float speedIncrementY = 25f;

        if (inComp != null)
        {
            if (inComp.KeyRightDown)
            {
                currentVelocityX += dt * speedIncrementX;
            }

            if (inComp.KeyLeftDown)
            {
                currentVelocityX -= dt * speedIncrementX;
            }

            if (inComp.KeyUpDown)
            {
                currentVelocityY -= dt * speedIncrementY;
            }

            if (inComp.KeyDownDown)
            {
                currentVelocityY += dt * speedIncrementY;
            }
        }

        veloComp.Velocity = new Vector3(
            currentVelocityX,
            currentVelocityY,
            currentVelocityZ);

        var newTrans = new Transform
        {
            scale = transComp.Transform.scale,
            rotation = transComp.Transform.rotation,
            translation = new Vector3(
                transComp.Transform.translation.X + veloComp.Velocity.X,
                transComp.Transform.translation.Y + veloComp.Velocity.Y,
                transComp.Transform.translation.Z + veloComp.Velocity.Z)
        };

        transComp.Transform = newTrans;

        //Console.WriteLine("currentSpeedX: " + currentVelocityX);
        //Console.WriteLine("directionX: " + directionX);
        //Console.WriteLine("veloX: " + veloComp.Velocity.X);
        //Console.WriteLine("posX: " + transComp.Transform.translation.X);

        var distance = Vector3.Distance(transComp.Transform.translation, transComp.OldPos);
        //Console.WriteLine("FrameDistance: " + distance);

        transComp.OldPos = transComp.Transform.translation;
    }
}
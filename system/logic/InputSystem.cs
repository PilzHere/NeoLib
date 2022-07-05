using NeoLib.component.logic;
using Raylib_cs;

namespace NeoLib.system.logic;

public class InputSystem : System
{
    public InputSystem()
    {
        Mask = InputComponent.Bits;
    }

    public override void UpdateComponents(float dt, List<component.Component> components)
    {
        Update(dt, components);
    }

    public void Update(float dt, List<component.Component> components)
    {
        var comp = (InputComponent)components[0]; // * There is only one.

        // read input
        comp.KeyRightDown = Raylib.IsKeyDown(KeyboardKey.KEY_D);
        comp.KeyLeftDown = Raylib.IsKeyDown(KeyboardKey.KEY_A);
        comp.KeyUpDown = Raylib.IsKeyDown(KeyboardKey.KEY_W);
        comp.KeyDownDown = Raylib.IsKeyDown(KeyboardKey.KEY_S);

        /*
        if (comp.KeyRightDown)
            Console.WriteLine("KEY RIGHT DOWN (D)");
        
        if (comp.KeyLeftDown)
            Console.WriteLine("KEY LEFT DOWN (A)");
        
        if (comp.KeyUpDown)
            Console.WriteLine("KEY UP DOWN (W)");
        
        if (comp.KeyDownDown)
            Console.WriteLine("KEY DOWN DOWN (S)");
            */
    }
}
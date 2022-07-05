using NeoLib.component;
using NeoLib.component.logic;
using NeoLib.component.render;
using Raylib_cs;

namespace NeoLib.system.render;

public class ShapeSystem : System
{
    public ShapeSystem()
    {
        Mask = TransformComponent.Bits | RectangleComponent.Bits;
    }

    public override void UpdateComponents(float dt, List<Component> components)
    {
        //Console.WriteLine("<SHAPE System>");

        TransformComponent transComp = null;
        RectangleComponent rectComp = null;

        foreach (var component in components)
        {
            switch (component.GetBits())
            {
                case RectangleComponent.Bits:
                    rectComp = (RectangleComponent)component;
                    break;
                case TransformComponent.Bits:
                    transComp = (TransformComponent)component;
                    break;
            }
        }

        var position = transComp.Transform.translation;
        var width = rectComp.Size.X;
        var height = rectComp.Size.Y;

        Raylib.DrawRectangleGradientH((int)position.X, (int)position.Y,
            (int)width, (int)height, rectComp.Color1, rectComp.Color2);
    }
}
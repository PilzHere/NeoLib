using System.Numerics;
using Raylib_cs;

namespace NeoLib.component.render;

public class RectangleComponent : Component
{
    public new const int Bits = 4; // 0b0100

    private readonly Vector2 _position;
    private readonly Vector2 _size;
    private readonly Color _color1;
    private readonly Color _color2;

    public Vector2 Position => _position;
    public Vector2 Size => _size;
    public Color Color1 => _color1;
    public Color Color2 => _color2;

    public RectangleComponent(Vector2 position, Vector2 size, Color color1, Color color2)
    {
        base.Bits = Bits;

        _position = position;
        _size = size;
        _color1 = color1;
        _color2 = color2;
    }
}
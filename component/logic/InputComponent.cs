using Raylib_cs;

namespace NeoLib.component.logic;

public class InputComponent : Component
{
    public new const int Bits = 8; // 0b1000

    public InputComponent()
    {
        base.Bits = Bits;
    }

    public CBool KeyUpDown { get; set; }

    public CBool KeyDownDown { get; set; }

    public CBool KeyRightDown { get; set; }

    public CBool KeyLeftDown { get; set; }
}
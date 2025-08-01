using Godot;
using System;

public partial class SkipButton : Sprite2D
{
    [Export] Dialog dialog;
    [Export] Camera2D zoom;
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && GetRect().HasPoint(ToLocal(mouseEvent.Position / zoom.Zoom)))
            dialog.NextLine();
    }

}

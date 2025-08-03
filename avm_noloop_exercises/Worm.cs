using Godot;
using System;

public partial class Worm : Sprite2D
{
    public bool Hiding = true;
    public override void _Ready()
    {
        Scale = Scale with { Y = 0 };
    }

    public override void _Process(double delta)
    {
        Scale = Scale with { Y = (Scale.Y + (Hiding ? 0 : 3)) / 2};
    }

}

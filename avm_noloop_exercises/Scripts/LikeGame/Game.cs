using Godot;
using System;

public partial class Game : Sprite2D
{
    [Export] PackedScene[] decors;
    [Export] int decorCount;
    [Export] float rotationSpeed = 1, radius = 1;
    float frame;
    public override void _Ready()
    {
        Vector2 localPos;
        for (int i = decorCount; i > 0; i--)
        {
            do
                localPos = new((float)GD.RandRange(-1.0, 1.0), (float)GD.RandRange(-1.0, 1.0));
            while (localPos.LengthSquared() > 1);
            Node2D decor = decors[GD.RandRange(0, decors.Length - 1)].Instantiate() as Node2D;
            AddChild(decor);
            decor.Position = localPos * radius;
            decor.RotationDegrees += Mathf.RadToDeg(localPos.Angle()) + 90;
        }
        
    }

    public override void _Process(double delta)
    {
        frame += (float)delta;
        if (frame < FramerateManager.FrameTime) return;
        frame -= FramerateManager.FrameTime;
        RotationDegrees += (float)(rotationSpeed / FramerateManager.FrameTime);
    }

}

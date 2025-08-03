using Godot;
public partial class Booka : Sprite2D
{
    [Export] public float Speed;
    private float frame;
    public override void _Process(double delta)
    {
        frame += (float)delta;
        if (frame < FramerateManager.FrameTime) return;
        frame -= FramerateManager.FrameTime;
        if(Position.X > -300)
            Position = Position with { X = Position.X - (float)(Speed / FramerateManager.FrameTime) };
    }
}

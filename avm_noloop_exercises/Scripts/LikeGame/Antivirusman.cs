using Godot;
public partial class Antivirusman : Node2D
{
    private float velocity, force;
    [Export] private float speed, directionChangeRate;
    [Export] private Vector2 bounds;
    public override void _PhysicsProcess(double delta)
    {
        if ((Position.X >= bounds.X || velocity >= 0) && (Position.X <= bounds.Y || velocity <= 0))
            Position = Position with { X = Position.X + velocity };
        velocity = (velocity + force * speed) / 2;
        if (GD.Randf() < delta / directionChangeRate)
            switch (force)
            {
                case 1:
                    force = 0;
                    break;
                case -1:
                    force = 0;
                    break;
                default:
                    force = GD.RandRange(-1, 1);
                    break;
            }
    }
}

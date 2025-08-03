using Godot;
public partial class Rocket : Node2D
{
    [Export] Vector2 Speed;
    public override void _Process(double delta) => Translate(Speed * (float)delta);
}

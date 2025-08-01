using Godot;
public partial class Dialog : Node2D
{
    public System.Collections.Generic.Queue<Replica> Replics = new();
    [Export] public RichTextLabel text;


    public void NextLine() => Replics.Dequeue();
    public override void _Process(double delta)
    {
        bool hasReplica = Replics.Count != 0;
        Visible = hasReplica;
        Vector2 actorPos = (GetNode(string.Concat("../" + Replics.Peek().actor, "/SpeakerPos")) as Node2D).GlobalPosition;
        Position = (Position + actorPos * 2) / 3;
        if (hasReplica && (Position - actorPos).LengthSquared() < 4)
            text.Text = Replics.Peek().text;
    }
}

using Godot;
public partial class Dialog : Node2D
{
    private static System.Collections.Generic.Queue<Replica> Replics = new();
    [Export] public RichTextLabel text;
    const float TIME_PER_CHAR = 0.15f;
    public static double timerToNextReplica = 0;
    public void NextLine()
    {
        Replics.Dequeue().OnEnd?.Invoke();
        if (Replics.Count != 0)
            ProcessShow();
    }
    public static void AddLine(Replica replica)
    {
        Replics.Enqueue(replica);
        if (Replics.Count == 1)
            ProcessShow();
    }
    public static void ProcessShow()
    {
        timerToNextReplica = Replics.Peek().text.Length * TIME_PER_CHAR;
        Replics.Peek().OnShow?.Invoke();
    }

    public override void _Process(double delta)
    {
        bool hasReplica = Replics.Count != 0;
        Visible = hasReplica;
        if (!hasReplica) return;
        Vector2 actorPos = (GetNode(string.Concat("../" + Replics.Peek().actor, "/SpeakerPos")) as Node2D).GlobalPosition;
        Position = (Position + actorPos * 2) / 3;
        if ((Position - actorPos).LengthSquared() < 4)
            text.Text = Replics.Peek().text;

        timerToNextReplica -= delta;
        if (timerToNextReplica < 0)
            NextLine();
    }
}

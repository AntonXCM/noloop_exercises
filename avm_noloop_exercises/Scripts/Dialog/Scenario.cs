using Godot;
public partial class Scenario : Node
{
    [Export] Godot.Collections.Array<Replica> replics;
    public override void _EnterTree()
    {
        if (GetParent() is Dialog dialog)
        {
            foreach (var replica in replics)
                dialog.Replics.Enqueue(replica);
            Dispose();
        }
    }

}

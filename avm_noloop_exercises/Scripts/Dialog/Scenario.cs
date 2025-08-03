using Godot;
public partial class Scenario : Node
{
    [Export] public Godot.Collections.Array<Replica> replics;
    public void Start()
    {
        foreach (var replica in replics)
        {
            replica.text = replica.text.Replace("username", System.Environment.UserName);
            Dialog.AddLine(replica);
        }
        Dispose();
    }
}

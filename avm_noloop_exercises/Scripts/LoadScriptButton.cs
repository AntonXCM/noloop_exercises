using Godot;
public partial class LoadScriptButton : Button
{
    [Export] FileDialog dialog;
    [Export] Level level;
    public override void _Ready()
    {
        dialog.FileSelected += DialogConfirmed;
    }

    public override void _Pressed()
    {
        dialog.Popup();
    }
    private void DialogConfirmed(string str)
    {
        level.UserCodePath = str;
    }
}

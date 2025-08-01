using Godot;
public partial class CodeColumn : Node
{
    [Export] public CodeEdit code;
    public string Code
    {
        get => code.Text;
        set => code.Text = value.TrimEnd();
    }
    [Export] RichTextLabel results, label, path;
    public string Results
    {
        get => results.Text;
        set => results.Text = value;
    }
    public string Path
    {
        get => path.Text;
        set => path.Text = value;
    }
    public override void _Ready()
    {
        label.Text = Name;
    }
}

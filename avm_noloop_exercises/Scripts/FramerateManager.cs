using Godot;
public partial class FramerateManager : RichTextLabel
{
    private static int fps;
    public static int FPS
    {
        get => fps;
        set
        {
            fps = value;
            Engine.MaxFps = fps;
            Instance.Text = "FPS=" + fps;
        }
    }
    public static FramerateManager Instance;
    public override void _Ready()
    {
        Instance = this;
    }

}

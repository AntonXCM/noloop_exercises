using Godot;
public partial class FramerateManager : RichTextLabel
{
    public static float FrameTime { get; private set; }
    private static int fps;
    public static int FPS
    {
        get => fps;
        set
        {
            fps = value;
            FrameTime = 1f / fps;
            Instance.Text = "FPS=" + fps;
        }
    }
    public static FramerateManager Instance;
    public override void _Ready()
    {
        Instance = this;
    }

}

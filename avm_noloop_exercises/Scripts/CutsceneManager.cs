using System.Linq;
using Godot;

public partial class CutsceneManager : Node
{
    // This code is awful!
    [Export] private Camera2D camera;
    [Export] private Booka booka, ambulance;
    [Export] private PackedScene[] Scenarios;
    [Export] private AnimationPlayer diplom;
    [Export] private Node2D ash;
    [Export] private Worm worm;
    [Export] private AudioStreamPlayer2D trivoga;
    [Export] private GpuParticles2D rocket;
    [Export] private PackedScene bigRocket;
    private int level = 1;
    private Vector2 zoom;
    private float bookaSpeed, ambulanceSpeed;
    public override void _Ready()
    {
        bookaSpeed = booka.Speed;
        booka.Speed = 0;
        ambulanceSpeed = ambulance.Speed;
        ambulance.Speed = 0;
        zoom = camera.Zoom;
        Level.Instance.OnLevelLoad += OnLevelLoad;
        SpawnScenario(0);
    }

    public void OnLevelLoad(int level)
    {
        if (level == 3) booka.Speed = bookaSpeed;
        if (level == 4) booka.Speed *= -2;
        if (level == 6) trivoga.Play();
        if (level == 8) worm.Hiding = false;
        if (level == 9) ash.Visible = true;
        SpawnScenario(level);
        this.level = level;
    }
    void SpawnScenario(int level)
    {
        if (level >= Scenarios.Length || Scenarios[level] == null)
            return;
        Scenario scenario = Scenarios[level].Instantiate() as Scenario;
        if (level is 2)
        {
            scenario.replics.Last().OnShow += () => diplom.Play("show");
            scenario.replics.Last().OnEnd += () => diplom.Play("hide");
        }
        if (level is 6)
        {
            scenario.replics.Last().OnShow += () => ambulance.Speed = ambulanceSpeed;
            scenario.replics.Last().OnEnd += () => rocket.Visible = true;
        }
        if (level is 8)
            scenario.replics.Last().OnShow += () =>
            {
                Node2D rocket = bigRocket.Instantiate() as Node2D;
                GetParent().AddChild(rocket);
                rocket.Position = new Vector2(480, 550);
            };
        scenario.Start();
    }
    public override void _Process(double delta)
    {
        if (level == 9)
            camera.Zoom = zoom + new Vector2((float)GD.RandRange(-0.005f, 0.005f), (float)GD.RandRange(-0.005f, 0.005f));
    }

}

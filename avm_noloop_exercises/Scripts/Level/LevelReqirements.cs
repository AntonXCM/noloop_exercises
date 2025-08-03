using Godot;

public partial class Level : Button
{
    struct LevelRequirements
    {
        public readonly int microseconds, lines, chars;
        public readonly string comparasion;
        public LevelRequirements(GodotObject level)
        {
            microseconds = level.Get("REQUIRE_MS").AsInt32();
            lines = level.Get("REQUIRE_LINES").AsInt32();
            chars = level.Get("REQUIRE_CHARS").AsInt32();
            comparasion = level.Get("COMPARASION").AsString();
        }
    }
}

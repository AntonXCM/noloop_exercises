using Godot;

public partial class Level : Button
{ 
    struct TestResults
    {
        public override string ToString() => $"Solution time (μs) = {stopwatch.Elapsed.TotalMicroseconds}\nResult = {solution.Obj}\nLines = {lines}, Symbols = {symbols}";
        public string ToStringCompareWith(TestResults compareWith, LevelRequirements requirements)
        {
            Color ok = Instance.okColor, good = Instance.goodColor, bad = Instance.badColor;
            //I don't want to use StringBuilders here :)
            string Colorize(double value, double other, double required)
            {
                double diff = other - value;
                Color color = diff < 0 ? bad : ColorLerp.OkHsl(ok, good, (float)(diff / (other - required)));
                return $"[color={color.ToHtml()}]{value}[/color]";
            }

            string
                time =      Colorize(stopwatch.Elapsed.TotalMicroseconds, compareWith.stopwatch.Elapsed.TotalMicroseconds, requirements.microseconds),
                result =    Equals(solution.Obj, compareWith.solution.Obj) ? $"[color={good.ToHtml()}]{solution.Obj}[/color]" : $"[color={bad.ToHtml()}]{solution.Obj}[/color]",
                lines =     Colorize(this.lines, compareWith.lines, requirements.lines),
                symbols =   Colorize(this.symbols, compareWith.symbols, requirements.chars);

            return $"Solution time (μs) = {time}\nResult = {result}\nLines = {lines}, Symbols = {symbols}";
        }


        public string text, textShort;
        public int symbols, lines;
        public System.Diagnostics.Stopwatch stopwatch;
        public Variant solution;
    }
}
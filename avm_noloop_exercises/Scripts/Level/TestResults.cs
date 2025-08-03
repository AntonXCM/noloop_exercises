using System.Linq;
using System.Text;
using Godot;
using Godot.Collections;

public partial class Level : Button
{ 
    struct TestResults
    {
        public override string ToString() => $"Solution time (μs) = {stopwatch.Elapsed.TotalMicroseconds}\nResult = {SolutionString()}\nLines = {lines}, Symbols = {symbols}";
        public string ToStringCompareWith(TestResults compareWith, LevelRequirements requirements)
        {
            Color ok = Instance.okColor, good = Instance.goodColor, bad = Instance.badColor;
            //I don't want to use StringBuilders here :)
            string Colorize(double value, double other, double required)
            {
                double diff = Mathf.Max(other, required) - value;
                Color color = diff < 0 ? bad : ColorLerp.OkHsl(ok, good, (float)(diff / (other - required)));
                return $"[color={color.ToHtml()}]{value}[/color]";
            }

            string
                time =      Colorize(stopwatch.Elapsed.TotalMicroseconds, compareWith.stopwatch.Elapsed.TotalMicroseconds, requirements.microseconds),
                result =    ResultIsOk(compareWith.solution.Obj, requirements.comparasion) ? $"[color={good.ToHtml()}]{SolutionString()}[/color]" : $"[color={bad.ToHtml()}]{SolutionString()}[/color]",
                lines =     Colorize(this.lines, compareWith.lines, requirements.lines),
                symbols =   Colorize(this.symbols, compareWith.symbols, requirements.chars);
            return $"Solution time (μs) = {time}\nResult = {result}\nLines = {lines}, Symbols = {symbols}";
        }
        public bool ResultIsOk(object compareWith, string comparasion)
        {
            if (compareWith is Dictionary compareDict && solution.Obj is Dictionary dict)
            {
                if (compareDict.Count != dict.Count)
                    return false;

                foreach (var key in compareDict.Keys)
                {
                    if (!dict.ContainsKey(key))
                        return false;

                    var v1 = compareDict[key].Obj;
                    var v2 = dict[key].Obj;

                    if (v1 is Array list1 && v2 is Array list2)
                    {
                        if (list1.Count != list2.Count)
                            return false;
                    }
                    else if (!Equals(v1, v2))
                        return false;
                }

                return true;
            }
            if (compareWith is Array compareArray && solution.Obj is Array array)
            {
                if (comparasion == "StartEnd")
                    return array[0].Equals(compareArray[0]) && array.Last().Equals(compareArray.Last());
                if (comparasion == "Same")
                {
                    if (compareArray.Count != array.Count)
                        return false;
                    foreach (var item in compareArray)
                    {
                        if (item.Obj is Vector2 vector)
                        {
                            if (!array.Any(i => 0.5f > ((Vector2)i.Obj - vector).Length()))
                                return false;
                        }
                        else if(!array.Contains(item))
                            return false;
                    }
                    return true;
                }
            }
            if (compareWith is string && solution.Obj is string text && !string.IsNullOrEmpty(comparasion))
                return text == comparasion;
            return solution.Obj.Equals(compareWith);
        }
        public string SolutionString() => SolutionString(solution.Obj);
        public string SolutionSubstring(object solution)
        {
            if (solution is Array array && array.Count > 9)
                return array.Count.ToString() + " items";
            return SolutionString(solution);
        }
        public string SolutionString(object solution)
        {
            if (solution is Array array && array.Count > 9)
                return $"{array[0].Obj}, ... {array.Last().Obj}";
            if (solution is Dictionary dict)
            {
                StringBuilder builder = new();
                builder.Append('{');
                foreach (var entry in dict)
                    builder.Append($"{SolutionSubstring(entry.Key.Obj)} : {SolutionSubstring(entry.Value.Obj)},");
                builder.Append('}');
                return builder.ToString();
            }
            if (solution is string str)
            {
                str = str.Replace('\t', '\n');
                return $"[font=res://Fonts/FiraCode-Light.ttf]{str}[/font]";
            }

            return solution.ToString();
        }
        public float CompareWith(TestResults compareWith, LevelRequirements requirements)
        {
            float Compare(double value, double other, double required)
            {
                double diff = Mathf.Max(other, required) - value;
                return diff < 0 ? -1 : Mathf.Min((float)(diff / (other - required)), 1);
            }

            float
                time = Compare(stopwatch.Elapsed.TotalMicroseconds, compareWith.stopwatch.Elapsed.TotalMicroseconds, requirements.microseconds),
                lines = Compare(this.lines, compareWith.lines, requirements.lines),
                symbols = Compare(this.symbols, compareWith.symbols, requirements.chars);

            return ResultIsOk(compareWith.solution.Obj, requirements.comparasion) ? (time + lines + symbols) * 2 - 2 : -5;
        }
        public string text, textShort;
        public int symbols, lines;
        public System.Diagnostics.Stopwatch stopwatch;
        public Variant solution;
    }
}
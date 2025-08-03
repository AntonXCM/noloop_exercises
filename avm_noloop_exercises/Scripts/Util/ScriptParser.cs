using System.Text;
using Godot;
static class ScriptParser
{
    public static string SolutionOf(string code)
    {
        var result = new StringBuilder();
        bool inSolution = false;

        foreach (var rawLine in code.Split('\n'))
        {
            string line = rawLine.TrimEnd('\r'), trimmed = line.TrimStart();

            if (!inSolution)
            {
                if (trimmed.StartsWith("func solution("))
                    inSolution = true;
                else
                    continue;
            }
            
            result.AppendLine(line);
        }

        return result.ToString();
    }

    public static string Shorten(string code)
    {
        code = SolutionOf(code);
        var lines = code.Split('\n');
        var result = new StringBuilder();

        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line.TrimStart()))
                continue;

            if (line.StartsWith("func solution("))
                continue;

            string cleaned = RemoveCommentOutsideString(line).TrimSuffix(";");

            if (string.IsNullOrWhiteSpace(cleaned))
                continue;

            result.AppendLine(cleaned);
        }
        return result.ToString();
    }

    private static string RemoveCommentOutsideString(string line)
    {
        var result = new StringBuilder();

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (c == '"' || c == '\'')
            {
                char quote = c;
                result.Append(c);
                i++;

                while (i < line.Length)
                {
                    result.Append(line[i]);

                    if (line[i] == quote && line[i - 1] != '\\')
                        break;

                    i++;
                }
            }
            else if (c == '#')
                break;
            else
                result.Append(c);
        }

        return result.ToString().TrimEnd();
    }
}
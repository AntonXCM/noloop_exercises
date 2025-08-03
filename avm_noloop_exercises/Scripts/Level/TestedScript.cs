using System;
using System.IO;
using Godot;

public partial class Level : Button
{
    class TestedScript : IDisposable
    {
        GDScript script;
        public GodotObject scriptInstance;
        public string text, path;
        public event Action onChanged;
        public TestResults Test(Godot.Collections.Array input)
        {
            TestResults results;

            results.stopwatch = System.Diagnostics.Stopwatch.StartNew();
            results.solution = scriptInstance.Callv("solution", input);
            results.stopwatch.Stop();

            results.text = text;
            results.textShort = ScriptParser.Shorten(text);
            results.lines = results.textShort.Count("\n");
            results.symbols = results.textShort.Length - results.textShort.Count(" ") - results.lines;

            return results;
        }
        public TestedScript RegenerateFrom(string path)
        {
            this.path = path;
            Compile();
            text = script.SourceCode;
            onChanged?.Invoke();
            return this;
        }
        public static TestedScript GenerateFrom(string path) => new TestedScript().RegenerateFrom(path);

        public void Dispose()
        {
            scriptInstance?.Dispose();
            script?.Dispose();
        }

        public void SaveCompile()
        {
            if (string.IsNullOrWhiteSpace(path)) return;
            File.WriteAllText(path, text);
            Compile();
        }
        public void Compile()
        {
            Dispose();
            script = ResourceLoader.Load<GDScript>(path, cacheMode: ResourceLoader.CacheMode.Ignore);
            
            scriptInstance = (GodotObject)script.New();
        }
    }
}
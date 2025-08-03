using System;
using System.IO;
using System.Text.RegularExpressions;
using Godot;

public partial class Level : Button
{
    [Export] private GDScript dummyScript;
    [Export] private int index = 1;
    [Export] private CodeColumn levelCode, userCode;
    [Export] private RichTextLabel label;
    [Export] private string levelCodePath, userCodePath;
    [Export] private Color badColor, okColor, goodColor;
    [Export] private FileDialog fileDialog;
    [Export] private Button nextLevel;
    private float currentLevelPerformance = 0;
    private TestedScript levelScript, userScript;
    public static Level Instance;

    public event Action<int> OnLevelLoad;

    public string LevelCodePath
    {
        get => levelScript.path;
        set => levelScript.RegenerateFrom(value);
    }
    public string UserCodePath
    {
        get => userScript.path;
        set => userScript.RegenerateFrom(value);
    }
    public override void _Ready()
    {
        fileDialog.DirSelected += FolderPicked;
        fileDialog.Canceled += RequestFolder;
        RequestFolder();
        FramerateManager.FPS = 5;
        Instance = this;
        nextLevel.Pressed += () => LoadLevel(index + 1);
    }
    public void RequestFolder()
    {
        GetWindow().MousePassthrough = true;
        fileDialog.Popup();
    }
    public void FolderPicked(string path)
    {
        GetWindow().MousePassthrough = false;
        GetWindow().GrabFocus();
        userCodePath = Path.Combine(path, userCodePath);
        LoadLevel(index);
    }
    public void LoadLevel(int index)
    {
        FramerateManager.FPS += (int)Mathf.Round(currentLevelPerformance / 3);
        currentLevelPerformance = 0;
        string
            actualLevelCodePath = string.Concat(levelCodePath, index, ".gd"),
            actualUserCodePath = string.Concat(userCodePath, index, ".gd");
        if (!File.Exists(actualUserCodePath))
            File.WriteAllText(actualUserCodePath, dummyScript.SourceCode);
        if (levelScript is null)
        {
            levelScript = TestedScript.GenerateFrom(actualLevelCodePath);
            levelScript.onChanged += GenerateUI;
            userScript = TestedScript.GenerateFrom(actualUserCodePath);
            userScript.onChanged += GenerateUI;
        }
        else
        {
            levelScript.RegenerateFrom(actualLevelCodePath);
            userScript.RegenerateFrom(actualUserCodePath);
        }
        GenerateUI();
        this.index = index;
        // nextLevel.Disabled = true;
        OnLevelLoad?.Invoke(index);
    }
    public override void _Pressed()
    {
        userScript.text = userCode.Code;
        userScript.SaveCompile();

        Godot.Collections.Array input = levelScript.scriptInstance.Call("get_args").AsGodotArray();
        LevelRequirements requirements = new(levelScript.scriptInstance);

        var levelResults = levelScript.Test(input);
        var userResults = userScript.Test(input);

        levelCode.Results = levelResults.ToString();
        userCode.Results = userResults.ToStringCompareWith(levelResults, requirements);
        float score = userResults.CompareWith(levelResults, requirements);
        if (score > 0)
        {
            nextLevel.Disabled = false;
            currentLevelPerformance = Math.Max(currentLevelPerformance, score);
        }
    }
    private void GenerateUI()
    {
        levelCode.Code = ScriptParser.SolutionOf(levelScript.text);
        levelCode.Path = LevelCodePath;
        userCode.Code = userScript.text;
        userCode.Path = UserCodePath;

        label.Text = Regex.Match(levelScript.text, @"var\s+DESCRIPTION\s*:\s*String\s*=\s*""([^""]*)""").Groups[1].Value;
    }
}
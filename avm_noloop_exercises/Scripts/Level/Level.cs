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
    private TestedScript levelScript, userScript;
    public static Level Instance;
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
        fileDialog.Popup();
        fileDialog.DirSelected += FolderPicked;
        FramerateManager.FPS = 5;
        Instance = this;
    }
    public void FolderPicked(string path)
    {
        userCodePath = Path.Combine(path, userCodePath);
        LoadLevel(index);
    }
    public void LoadLevel(int index)
    {
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
    }
    public override void _Pressed()
    {
        userScript.text = userCode.Code;
        userScript.SaveCompile();

        Godot.Collections.Array input = levelScript.scriptInstance.Get("ARGS").AsGodotArray();
        LevelRequirements requirements = new(levelScript.scriptInstance);
        
        var levelResults = levelScript.Test(input);
        var userResults = userScript.Test(input);

        levelCode.Results = levelResults.ToString();
        userCode.Results = userResults.ToStringCompareWith(levelResults, requirements);
        LoadLevel(2);
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
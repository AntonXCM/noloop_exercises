using System;
using Godot;

[GlobalClass]
public partial class Replica : Resource
{
    [Export] public string text, actor;
    public Action OnShow, OnEnd;
}
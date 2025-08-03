using Godot;
using System;

public partial class UsernameText : Label
{
    public override void _Ready()
    {
        Text = System.Environment.UserName;
    }

}

using Godot;
using System;

public partial class QuitButton : Button
{
	public override void _Ready() { Pressed += OnQuitGame; }

	private void OnQuitGame() { GetTree().Quit(); }
}

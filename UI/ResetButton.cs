using Godot;

public partial class ResetButton : Button
{
	public override void _Ready() { Pressed += OnResetGame; }
	public void OnResetGame()
	{
		GetTree().Paused = false;
		GetTree().ReloadCurrentScene();
	}
}

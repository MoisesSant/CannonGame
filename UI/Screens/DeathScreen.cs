using Godot;

public partial class DeathScreen : Control
{
	[Export] private Player Player;
	[Export] private PauseManu PauseManu;
	[Export] private ResetButton Reset;
	[Export] private QuitButton Quit;
	public override void _Ready()
	{
		Player.IsDeath += GameOver;
		Reset.Pressed += PauseManu.OnResetGame;
		Quit.Pressed += PauseManu.OnQuitGame;
	}

	private void GameOver()
	{
		GetNode<Control>("%DeathScreen").Visible = true;
		GetTree().Paused = true;
	}


}

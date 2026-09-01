using Godot;

public partial class PauseManu : Control
{
	[Export] private ResumeButton Resume;
	[Export] private ResetButton Reset;
	[Export] private QuitButton Quit;
	[Export] private GameManager Game;
	public override void _Ready()
	{
		base._Ready();
		Resume.ResumeGame += OnResumeGame;
		Reset.ResetGame += OnResetGame;
		Quit.QuitGame += OnQuitGame;
	}

	public void OnResumeGame()
	{
		GetTree().Paused = false;
		Game.IsPaused = false;
		GetNode<Control>("%PauseMenu").Visible = false;
	}

	public void OnResetGame()
	{
		GetTree().Paused = false;
		GetTree().ReloadCurrentScene();
	}

	public void OnQuitGame()
	{
		GetTree().Quit();
	}
}

using Godot;

public partial class ResumeButton : Button
{
	[Signal] public delegate void ResumeGameEventHandler();
	[Export] private GameManager Game;

	public override void _Ready()
	{
		base._Ready();
		Pressed += OnResumeGame;
	}

	public void OnResumeGame()
	{
		EmitSignal(SignalName.ResumeGame);
	}
}

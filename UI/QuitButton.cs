using Godot;

public partial class QuitButton : Button
{
	[Signal] public delegate void QuitGameEventHandler();
	public override void _Ready()
	{
		base._Ready();
		Pressed += OnQuitGame;
	}

	private void OnQuitGame()
	{
		EmitSignal(SignalName.QuitGame);
	}
}

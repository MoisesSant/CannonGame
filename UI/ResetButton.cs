using Godot;

public partial class ResetButton : Button
{
	[Signal] public delegate void ResetGameEventHandler();
	public override void _Ready()
	{
		base._Ready();
		Pressed += OnResetGame;
	}

	public void OnResetGame()
	{
		EmitSignal(SignalName.ResetGame);
	}
}

using Godot;

public partial class ScreenManager : State
{
	[Export] private Player Player { get; set; }
	public bool IsDead { get; set; } = false;
	public bool IsPaused { get; set; } = false;
	private Timer _timer;
	private int Waves;

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("ui_cancel"))
		{
			GetTree().Paused = !GetTree().Paused;
			IsPaused = !IsPaused;
			GetNode<Control>("%PauseMenu").Visible = IsPaused; // true or false
		}
	}


}

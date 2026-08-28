using Godot;

public partial class InvecibleState : State
{
	private Timer _timer;
	public override void Enter()
	{
		base.Enter();
		_timer = GetNode<Timer>("Timer");
		_timer.Start();

		_timer.Timeout += _OnTimerTimeout;
	}

	public override void Exit()
	{
		base.Exit();
		_timer.Stop();
	}


	private void _OnTimerTimeout()
	{
		GD.Print("Timeout!");

	}

}

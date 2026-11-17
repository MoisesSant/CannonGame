using Godot;

public partial class Enemy : CharacterBody2D
{
	[Signal] public delegate void EnemyDeplatedEventHandler();
	[Export] private MovementComponent Movement { get; set; }
	[Export] private HealthComponent Health { get; set; }
	[Export] ProgressBar HealthBar { get; set; }

	private Vector2 Direction { get; set; }
	private CharacterBody2D Player { get; set; }


	public override void _Ready()
	{
		base._Ready();
		AddToGroup("Enemy");
		Health.Depleted += Death;

		Player = (CharacterBody2D)GetTree().GetFirstNodeInGroup("Player");

		if (Movement == null) // Checagem de erro
		{
			GD.PrintErr(what: "Movement was not founded");
			return;
		}

		if (Player == null) // Checagem de erro
		{
			GD.PrintErr(what: "Player was not founded");
			return;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		Direction = GlobalPosition.DirectionTo(Player.GlobalPosition); // Basic and temporary form to follow the player (Without AIComponent)
		Velocity = Movement.ApplyLinearMovement(Direction, this);
		HealthBar.Value = Health.GetHealthPorcentage();
	}

	private void Death()
	{
		EmitSignal(SignalName.EnemyDeplated);
		QueueFree();
	}

}
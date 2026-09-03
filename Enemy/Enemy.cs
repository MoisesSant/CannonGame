using Godot;

public partial class Enemy : CharacterBody2D
{
	[Export] private MovementComponent Movement;
	[Export] private HealthComponent Health;
	[Export] ProgressBar HealthBar { get; set; }

	private Vector2 Direction { get; set; }
	private CharacterBody2D Player { get; set; }
	private Global Global;

	public override void _Ready()
	{
		base._Ready();
		AddToGroup("Enemy");
		Health.Depleted += Death;

		Global = (Global)GetTree().GetFirstNodeInGroup("GlobalStats");
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

	private void Death()
	{
		Global.PlayerPoints += 1;
		GD.Print(Global.PlayerPoints);
		QueueFree();
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		Direction = GlobalPosition.DirectionTo(Player.GlobalPosition);
		Velocity = Movement.ApplyLinearMovement(Direction, this);

		HealthBar.Value = Health.PorcentageHealth();
	}
}
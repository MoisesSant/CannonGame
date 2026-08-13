using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	[Export] public MovementComponent Movement;
	[Export] public HealthComponent Health;
	private Vector2 Direction { get; set; }
	private CharacterBody2D Player { get; set; }
	public override void _Ready()
	{
		base._Ready();
		AddToGroup("Enemy");
		Health.Depleted += Death; // connecta ao signal

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
		QueueFree();
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		Direction = GlobalPosition.DirectionTo(Player.GlobalPosition);
		Velocity = Movement.ApplyLinearMovement(Direction, this);
	}
}
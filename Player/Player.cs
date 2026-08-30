using Godot;
using System;

// 150, 250, 50;
public partial class Player : CharacterBody2D
{
  [Export] private MovementComponent Movement { get; set; }
  [Export] private HealthComponent Health { get; set; }
  [Export] private ProgressBar HealthBar { get; set; }
  [Export] private Global Global;
  private Vector2 PlayerDirection { get; set; }
  private Vector2 MouseDirection => GetGlobalMousePosition();
  private double RotationSpeed { get; set; } = 10.0f;
  private double TargetDirection => MouseDistance.Angle() + Math.PI / 2;
  private Vector2 MouseDistance => MouseDirection - GlobalPosition;
  public override void _Ready()
  {
    base._Ready();
    AddToGroup("Player");

    Health.Depleted += Death;

    if (Movement == null)
    {
      GD.PrintErr("Movement is not defined");
    }
  }

  public override void _PhysicsProcess(double delta)
  {
    base._PhysicsProcess(delta);

    HealthBar.Value = Health.PorcentageHealth();

    PlayerDirection = Input.GetVector(
      "Move_Left",
      "Move_Right",
      "Move_Up",
      "Move_Down"
    );
    Velocity = Movement.ApplyLinearMovement(PlayerDirection, this);
    LookToMouse(delta);


  }

  private void LookToMouse(double delta)
  {
    if (MouseDirection == Vector2.Zero) { return; } // Evita erro caso o Mouse estiver encima do Player
    Rotation = (float)Mathf.LerpAngle(
      Rotation,
      TargetDirection,
      RotationSpeed * (float)delta
    );
  }

  public void Death()
  {
    GetNode<Control>("%DeathScreen").Visible = true;
    GetTree().Paused = true;
  }
}
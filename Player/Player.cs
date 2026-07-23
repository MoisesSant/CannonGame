using Godot;
using System;

public partial class Player : CharacterBody2D
{
  [Export] public MovementComponent Movement { get; set; }
  public Vector2 PlayerDirection { get; set; }
  public Vector2 MouseDirection => GetGlobalMousePosition();
  private Vector2 MouseDistance => MouseDirection - GlobalPosition;
  public double RotationSpeed { get; set; } = 10.0f;
  public double TargetDirection => MouseDistance.Angle() + Math.PI / 2;
  public override void _Ready()
  {
    base._Ready();
    if (Movement == null)
    {
      GD.PrintErr("Movement is not defined");
    }
  }

  public override void _PhysicsProcess(double delta)
  {
    base._PhysicsProcess(delta);
    PlayerDirection = Input.GetVector(
      "move_left",
      "move_right",
      "move_up",
      "move_down"
    );
    Velocity = Movement.ApplyProgressiveMovement(delta, PlayerDirection, this);
    LookToMouse(delta);
  }

  public void LookToMouse(double delta)
  {
    if (MouseDirection == Vector2.Zero) { return; } // Evita erro caso o Mouse estiver encima do Player
    Rotation = (float)Mathf.LerpAngle(
      Rotation,
      TargetDirection,
      RotationSpeed * (float)delta
    );
  }
}
using Godot;
using System;

// 150, 250, 50;
public partial class Player : CharacterBody2D
{
  [Export] private MovementComponent Movement { get; set; }
  [Export] private HealthComponent Health { get; set; }
  [Export] private AnimationPlayer Animation { get; set; }
  [Export] private ProgressBar HealthBar { get; set; }
  [Export] private Global Global;

  [Signal] public delegate void IsDeathEventHandler();
  private Vector2 PlayerDirection { get; set; }
  private Vector2 MouseDirection => GetGlobalMousePosition();
  private double RotationSpeed { get; set; } = 10.0f;
  private double TargetDirection => MouseDistance.Angle() + Math.PI / 2;
  private Vector2 MouseDistance => MouseDirection - GlobalPosition;
  private bool IsPlayerDead { get; set; } = false;

  public override void _Ready()
  {
    base._Ready();
    AddToGroup("Player");

    Health.Depleted += () => IsPlayerDead = true;

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

  public override void _Process(double delta)
  {
    base._Process(delta);
    if (Health.IsInvencible == true)
    {
      Animation.Play("Invencible");
    }

    if (IsPlayerDead)
      EmitSignal(SignalName.IsDeath);
  }
}
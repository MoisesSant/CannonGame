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
  private Vector2 MouseDistance => MouseDirection - GlobalPosition;

  private double TargetDirection => MouseDistance.Angle() + Math.PI / 2;
  private double RotationSpeed { get; set; } = 10.0f;
  private bool IsPlayerDead { get; set; } = false;

  public override void _Ready()
  {
    base._Ready();
    AddToGroup("Player");

    // Error Check
    if (Health == null) { GD.PrintErr($"There is no {Health.GetClass} setup"); return; }
    if (Movement == null) { GD.PrintErr($"There is no {Movement.GetClass} setup"); return; }
    if (HealthBar == null) { GD.PrintErr($"There is no {HealthBar.GetClass} setup"); return; }

    // Connections
    Health.Depleted += () => IsPlayerDead = true;
    Health.HealthChanged += (current, max) => { HealthBar.Value = Health.GetHealthPorcentage(); };
    HealthBar.Value = Health.GetHealthPorcentage();

    // Debug
    /*---*/
  }

  public override void _PhysicsProcess(double delta)
  {
    base._PhysicsProcess(delta);

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
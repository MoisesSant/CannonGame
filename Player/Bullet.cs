using Godot;


public partial class Bullet : Area2D
{

  [Export] public float Speed { get; set; } = 500.0f;
  [Export] public float LifeTime { get; set; } = 2.0f;
  [Export] private HitboxComponent Hitbox { get; set; }
  private Timer _lifeTimer { get; set; }
  public const float SPEED = 500.0f;
  public override void _Ready()
  {
    base._Ready();
    AddToGroup("Bullets");

    _lifeTimer = new Timer // Cria o timer como filho do bullet
    {
      WaitTime = LifeTime,
      OneShot = true,
      Autostart = true
    };
    AddChild(_lifeTimer);
    _lifeTimer.Timeout += OnLifeTimerTimeout;
  }

  private void OnLifeTimerTimeout()
  {
    QueueFree();
  }
  public override void _Process(double delta)
  {
    base._Process(delta);
    Position += Transform.X * Speed * (float)delta; // Adiciona velocidade no eixo X ao Bullet

    if (Hitbox._hasCollided == true)
    {
      QueueFree();
    }
  }
}

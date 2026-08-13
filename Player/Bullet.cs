using Godot;


public partial class Bullet : Area2D
{
  /*    -------- FEITO POR MIM COM POUCA CONSULTA ---------

  public Timer timer { get; set; }
  public const float SPEED = 500.0f;

  public override void _Ready()
  {
    base._Ready();
    AddToGroup("Bullets");
    timer = new Timer // Cria uma nova configuração do Timer
    {
      WaitTime = 2.0f,
      OneShot = true,
      Autostart = true
    };

    timer.Timeout += () =>// Conectando o Signal Timeout com o Timer 
    {
      QueueFree();
      GetTree().Root.GetNode<Timer>("/root/"); // Finalizar; tentar pegar o timer e dar QueueFree
    };

    GetTree().Root.AddChild(timer); // Adicionando o Timer na raiz da cena
    timer.Start();
  }
  public override void _Process(double delta)
  {
    base._Process(delta);
    Position += Transform.X * SPEED * (float)delta; // Adiciona velocidade no eixo X ao Bullet
  }
  */

  //     ------ CORREÇÃO FEITA POR IA: (NVIDIA: Nemotron 3 Super) ------
  [Export] public float Speed { get; set; } = 500.0f;
  [Export] public float LifeTime { get; set; } = 2.0f;
  private Timer _lifeTimer { get; set; }
  public const float SPEED = 500.0f;
  public override void _Ready()
  {
    base._Ready();
    AddToGroup("Bullets");

    _lifeTimer = new Timer // Cria o timer como FILHO do bullet (não da raíz!)
    {
      WaitTime = LifeTime,
      OneShot = true,
      Autostart = true
    };
    AddChild(_lifeTimer);
    _lifeTimer.Timeout += OnLifeTimerTimeout; // ← CORRETO: Timer é filho do bullet
  }

  private void OnLifeTimerTimeout()
  {
    QueueFree(); // Libera o bullet E SEU TIMER FILHO automaticamente
  }
  public override void _Process(double delta)
  {
    base._Process(delta);
    Position += Transform.X * Speed * (float)delta; // Adiciona velocidade no eixo X ao Bullet
  }
}

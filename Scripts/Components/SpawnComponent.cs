using System;
using Godot;

public partial class SpawnComponent : Node
{
  [Export] public float SpawnRate { get; set; } = 0.5f;
  [Export] public PackedScene EnemyScene { get; set; }
  private Timer _spawnTimer;
  public override void _Ready()
  {
    base._Ready();
    _spawnTimer = new Timer
    {
      WaitTime = SpawnRate,
      Autostart = true,
      OneShot = false
    };
    _spawnTimer.Timeout += () => _on_SpawnTimer_timeout();

    AddChild(_spawnTimer);
  }

  public void _on_SpawnTimer_timeout()
  {
    if (EnemyScene is null)
    {
      GD.PrintErr("EnemyScene is not defined!");
      return;
    }
    SpawnAtBorder();
  }
  public void SpawnAtBorder()
  {
    CharacterBody2D enemy = EnemyScene.Instantiate<CharacterBody2D>();
    var ScreenSize = GetViewport().GetVisibleRect().Size;
    float Max_x = ScreenSize.X;
    float Max_y = ScreenSize.Y;

    var rInt = new Random();
    int Side = rInt.Next() % 4;

    switch (Side)
    {
      case 0: // Topo
        enemy.Position = new Vector2((float)GD.RandRange(0, Max_x), 0);
        break;
      case 1: // Direita
        enemy.Position = new Vector2(Max_x, (float)GD.RandRange(0, Max_y));
        break;
      case 2: // Baixo
        enemy.Position = new Vector2((float)GD.RandRange(0, Max_x), Max_y);
        break;
      case 3: // Esquerda
        enemy.Position = new Vector2(0, (float)GD.RandRange(0, Max_y));
        break;
    }

    AddChild(enemy);
  }
}
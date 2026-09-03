using System;
using Godot;

public partial class WaveComponent : Node
{
  [Export] private PackedScene EnemyScene { get; set; }
  [Export] private Marker2D[] SpawnPoints { get; set; }
  [Export] private float SpawnRate { get; set; } = 5.0f;
  [Export] private float WaveTime { get; set; } = 20.0f;
  public int CurrentWave { get; set; }
  private Timer _spawnTimer;
  private Timer _waveTimer;
  public override void _Ready()
  {
    base._Ready();

    _waveTimer = new Timer
    {
      WaitTime = WaveTime,
      Autostart = true,
      OneShot = false,
    };
    _waveTimer.Timeout += WaveSetup;
    AddChild(_waveTimer);

    _spawnTimer = new Timer
    {
      WaitTime = SpawnRate,
      Autostart = true,
      OneShot = false
    };
    _spawnTimer.Timeout += _on_SpawnTimer_timeout;
    AddChild(_spawnTimer);
  }

  public override void _Process(double delta)
  {
    base._Process(delta);
    _spawnTimer.WaitTime = SpawnRate;
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


  public void WaveSetup()
  {
    CurrentWave += 1;
    switch (CurrentWave)
    {
      case 1:
        SpawnRate = 3.5f;
        break;

      case 2:
        SpawnRate = 3.0f;
        break;

      case 3:
        SpawnRate = 2.5f;
        break;

      case 4:
        SpawnRate = 2.0f;
        break;

      case 5:
        SpawnRate = 1.5f;
        break;

      case 6:
        SpawnRate = 1.0f;
        break;
    }
    GD.Print("Current SpawnTime: ", _spawnTimer.WaitTime);

  }

  public void SpawnAtBorder()
  {
    Marker2D spawnPoint = SpawnPoints[GD.RandRange(0, SpawnPoints.Length - 1)];
    CharacterBody2D Enemy = EnemyScene.Instantiate<CharacterBody2D>();

    Enemy.GlobalPosition = spawnPoint.GlobalPosition;
    AddChild(Enemy);
  }
}
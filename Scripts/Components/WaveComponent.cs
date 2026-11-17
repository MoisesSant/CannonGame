using Godot;

public partial class WaveComponent : Node
{
    [ExportGroup("Settings")]
    [Export] private float SpawnRate { get; set; } = 5.0f;
    [Export] private float WaveTime { get; set; } = 20.0f;

    [ExportGroup("References")]
    [Export] public Global Global { get; set; }
    [Export] private Player Player { get; set; }
    [Export] private PackedScene EnemyScene { get; set; }
    [Export] private Marker2D[] SpawnPoints { get; set; }
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
        _spawnTimer.WaitTime = SpawnRate;
        AddChild(_spawnTimer);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
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


    public async void WaveSetup()
    {
        if (CurrentWave == 1 && Global.PlayerXP == 1)
        {
            CurrentWave++;

            SpawnRate -= 0.3f;

            if (SpawnRate < 0.5f) // Limitador
                SpawnRate = 0.5f;

            _spawnTimer.WaitTime = SpawnRate;
            _spawnTimer.Start();

        }

        GD.Print("Current SpawnTime: ", _spawnTimer.WaitTime);
    }

    public void SpawnAtBorder()
    {

        Marker2D spawnPoint = SpawnPoints[GD.RandRange(0, SpawnPoints.Length - 1)];
        Enemy Enemy = EnemyScene.Instantiate<Enemy>();

        Enemy.GlobalPosition = spawnPoint.GlobalPosition;
        AddChild(Enemy);

        Enemy.EnemyDeplated += OnEnemyDeplated;
    }

    public void OnEnemyDeplated()
    {

        Global.PlayerXP += 5;

        GD.Print("XP: ", Global.PlayerXP);
        GD.Print("%: ", Global.GetLevelPorcentage());
    }
}
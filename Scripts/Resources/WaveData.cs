using Godot;

public enum SpawnMode { Fixed, Random, Progressive }

[GlobalClass]
public partial class WaveData : Resource
{
    [ExportCategory("Ritmo")]
    [ExportGroup("Spawn")]
    [Export(PropertyHint.Range, "1,999,1")]
    private int WaveNumber { get; set; } = 1;

    [Export] public SpawnMode Mode = SpawnMode.Fixed;

    [Export(PropertyHint.Range, "1,120,0.5")]
    public float Duration { get; set; } = 20.0f;

    [Export(PropertyHint.Range, "5.0,0.5,-0.5")]
    private float SpawnRate { get; set; } = 2.0f;
    [Export]
    private PackedScene EnemyScene { get; set; }

    [Export(PropertyHint.Range, "1,500,1,or_greater")]
    private int MaxEnemies { get; set; } = 10;
}
using Godot;

public partial class HitboxComponent : Area2D
{
	[Signal] public delegate void HitEventHandler(HitData hitData, bool _hasCollided);

	[ExportCategory("Settings")]

	[Export(PropertyHint.Range, "0.1, 100, or_greater")]
	private float Damage { get; set; } = 10.0f;

	[Export(PropertyHint.Range, "0.1, 1, or_greater")]
	private float TickTime { get; set; } = 0.7f;

	private Timer _ticktimer;

	public bool _hasCollided;
	public override void _Ready()
	{
		AreaEntered += OnAreaEnteredSignal; // Conecta o signal à função

		_ticktimer = new Timer
		{
			WaitTime = TickTime,
			Autostart = false,
			OneShot = false,
		};
		AddChild(_ticktimer);
		_ticktimer.Timeout += TickDamage;
	}

	private void OnAreaEnteredSignal(Area2D Area)
	{
		TickDamage();
		_hasCollided = true;
	}

	private void TickDamage()
	{
		var hitData = new HitData
		{
			Damage = Damage,
			TickTime = TickTime,
			_hasCollided = _hasCollided,
		};

		foreach (Area2D Areas in GetOverlappingAreas())
		{
			if (Areas is not HealthComponent) continue; // Ignora qualquer area que não for HealthComp.
			((HealthComponent)Areas).ReceiveHit(hitData); // Chama a função dentro do HealthComp. e aplica o hitData
			EmitSignal(SignalName.Hit, hitData); // Transmite os dados contidos
			_ticktimer.Start();
		}
	}

}

// classe de Data para o signal
public partial class HitData : RefCounted // Faz Tipagem dos dados contidos em HitData com classes
{
	public float Damage { get; set; }
	public float TickTime { get; set; }
	public bool _hasCollided { get; set; }
}
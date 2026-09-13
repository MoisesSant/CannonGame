using Godot;

public partial class HitboxComponent : Area2D
{
	[Export] public float Damage { get; set; } = 10.0f;
	[Signal] public delegate void HitEventHandler(HitData hitData, bool _hasCollided);

	public bool _hasCollided;
	public override void _Ready()
	{
		AreaEntered += OnAreaEnteredSignal; // Conecta o signal à função
	}

	private void OnAreaEnteredSignal(Area2D Area)
	{
		if (Area is not HealthComponent) return; // Ignora qualquer area que não for HeakthComp.

		var hitData = new HitData
		{
			Damage = Damage,
			_hasCollided = _hasCollided,
		};

		((HealthComponent)Area).ReceiveHit(hitData); // Chama a função dentro do HeakthComp. e aplica o hitData

		EmitSignal(SignalName.Hit, hitData); // Transmite os dados contidos

		_hasCollided = true;
	}
}

// classe de Data para o signal
public partial class HitData : RefCounted // Faz Tipagem dos dados contidos em HitData com classes
{
	public float Damage { get; set; }
	public bool _hasCollided { get; set; }
}
using Godot;

public partial class HealthComponent : Area2D
{

	[ExportCategory("Attributes")]
	[ExportGroup("Invencibility")]
	[Export(PropertyHint.Enum, "True,False")] private bool CanBecomeInvencible { get; set; } = false;
	[Export(PropertyHint.Range, "0.1,3, or_greater")] private float InvencibilityTime { get; set; } = 1;

	[ExportGroup("Health")]
	[Export(PropertyHint.Range, "1.0,100.0, or_greater")] private float MaxHealth { get; set; } = 100.0f;

	[Signal] public delegate void HealthChangedEventHandler(float Current, float Max);
	[Signal] public delegate void DepletedEventHandler(); // Quando a vida acaba
	[Signal] public delegate void DamageTakenEventHandler(HitData hitData); // Envia a quantidade de dano que foi recebido

	public float CurrentHealth { get; private set; }
	public bool IsInvencible { get; set; } = false;
	private Timer _timer;
	public override void _Ready() { CurrentHealth = MaxHealth; }

	public void TakeDamage(HitData hitData) // Função que adiministra o dano e mudança da vida
	{
		if (hitData.Damage <= 0) return; // dano nulo
		if (IsInvencible == true) return;
		CurrentHealth -= hitData.Damage;

		if (CurrentHealth <= 0) EmitSignal(SignalName.Depleted); // Caso a vida = 0; envia que a entidade perdeu todas as suas vidas

		EmitSignal(SignalName.DamageTaken, hitData);
		EmitSignal(SignalName.HealthChanged, CurrentHealth, MaxHealth);

		if (CanBecomeInvencible == false) return;
		IsInvencible = true;
		SetInvencibility();
	}

	public float GetHealthPorcentage()
	{
		float porcentage = CurrentHealth / MaxHealth * 100;
		return porcentage;
	}

	public void SetInvencibility()
	{
		_timer = new Timer
		{
			WaitTime = InvencibilityTime,
			Autostart = false,
			OneShot = true
		};
		AddChild(_timer);
		_timer.Start();
		_timer.Timeout += () => IsInvencible = false;
	}

	public void ReceiveHit(HitData hitData)
	{
		if (hitData == null)
		{
			GD.PrintErr(what: $"{this} was not able to receive hit in hitData");
			return;
		}
		TakeDamage(hitData);
	}
}
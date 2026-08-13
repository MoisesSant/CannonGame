using Godot;

public partial class HealthComponent : Node
{
  [Export] public float MaxHealth { get; private set; } = 100.0f;
  public float CurrentHealth { get; private set; }

  [Signal] public delegate void HealthChangedEventHandler(float Current, float Max);
  [Signal] public delegate void DepletedEventHandler();
  [Signal] public delegate void DamageTakenEventHandler(HitData hitData); // Envia a quantidade de dano que foi recebido

  public override void _Ready()
  {
    base._Ready();
    CurrentHealth = MaxHealth;
  }

  public void TakeDamage(HitData hitData) // Função que adiministra o dano e mudança da vida
  {
    if (hitData.Damage <= 0) return; // dano nulo

    CurrentHealth -= hitData.Damage;

    EmitSignal(SignalName.DamageTaken, hitData);
    EmitSignal(SignalName.HealthChanged, CurrentHealth, MaxHealth);

    if (CurrentHealth <= 0) EmitSignal(SignalName.Depleted); // Caso a vida = 0; envia que a entidade perdeu todas as suas vidas
  }
}
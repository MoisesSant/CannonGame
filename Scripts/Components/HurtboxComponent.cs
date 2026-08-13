using Godot;

public partial class HurtboxComponent : Area2D
{
  [Export] public HealthComponent Health = null;
  public void ReceiveHit(HitData hitData)
  {
    if (Health == null || hitData == null) return;
    Health.TakeDamage(hitData);
  }
}

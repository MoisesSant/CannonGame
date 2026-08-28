using Godot;

public partial class HurtboxComponent : Area2D
{
  [Export] public HealthComponent Health = null;
  public void ReceiveHit(HitData hitData)
  {
    if (Health == null)
    {
      GD.PrintErr(what: "Health reference was not founded");
      return;
    }

    if (hitData == null)
    {
      GD.PrintErr(what: "There is no Data in hitData");
      return;
    }

    if (hitData._hasHit == true) return;

    Health.TakeDamage(hitData);
  }
}

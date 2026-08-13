using Godot;

public partial class HitboxComponent : Area2D
{
  [Export] public float Damage { get; set; } = 10;
  [Export] public float KnockbackForce { get; set; } = 200f;
  [Export] public Area2D Hurtbox { get; set; } = null;

  [Signal] public delegate void HitEventHandler(HitData hitData);

  public override void _Ready()
  {
    AreaEntered += OnAreaEnteredSignal; // Connecta o signal à função
  }

  private void OnAreaEnteredSignal(Area2D Area)
  {
    if (Area is not HurtboxComponent) return; // Ignora qualquer area que não for HurtBox
    if (Area == Hurtbox) return; // Evita self-hit

    var hitData = new HitData // Armazena os dados passados
    {
      Damage = Damage,
      Knockback = KnockbackForce,
      Hitbox = this,
      Hurtbox = (HurtboxComponent)Hurtbox,
      HitPosition = GlobalPosition
    };

    ((HurtboxComponent)Area).ReceiveHit(hitData); // Chama a função dentro do Hurtbox e aplica o hitData

    EmitSignal(SignalName.Hit, hitData); // Transmite os dados contidos
  }
}

// classe de Data para o signal
public partial class HitData : RefCounted // Faz Tipagem dos dados contidos em HitData com classes
{
  public float Damage { get; set; }
  public float Knockback { get; set; }
  public HitboxComponent Hitbox { get; set; }
  public HurtboxComponent Hurtbox { get; set; }
  public Vector2 HitPosition { get; set; }
}
using Godot;

public partial class HitboxComponent : Area2D
{
  [Export] public float Damage { get; set; } = 10.0f;
  [Signal] public delegate void HitEventHandler(HitData hitData, bool _hasHit);

  public bool _hasHit;
  public float invecibility;
  public override void _Ready()
  {
    AreaEntered += OnAreaEnteredSignal; // Conecta o signal à função
  }

  private void OnAreaEnteredSignal(Area2D Area)
  {
    if (Area is not HurtboxComponent) return; // Ignora qualquer area que não for HurtBox

    var hitData = new HitData
    {
      Damage = Damage,
      _hasHit = _hasHit,
      Invecibility = invecibility
    };

    ((HurtboxComponent)Area).ReceiveHit(hitData); // Chama a função dentro do Hurtbox e aplica o hitData

    EmitSignal(SignalName.Hit, hitData); // Transmite os dados contidos

    _hasHit = true;
  }
}

// classe de Data para o signal
public partial class HitData : RefCounted // Faz Tipagem dos dados contidos em HitData com classes
{
  public float Damage { get; set; }
  public bool _hasHit { get; set; }
  public float Invecibility { get; set; }
}
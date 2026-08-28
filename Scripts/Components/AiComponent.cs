using Godot;

public partial class AiComponent : Node
{
	private Node2D _target;
	private CharacterBody2D OwnerBody { get; set; }
	private Godot.Collections.Array<Node> Players { get; set; }
	private float Distance { get; set; }
	[Signal] public delegate void TargetAcquiredEventHandler(Node2D Target);
	[Signal] public delegate void TargetLostEventHandler();

	public override void _Ready()
	{
		base._Ready();
		OwnerBody = GetParent<CharacterBody2D>(); //define o corpo do Owner
		_findTarget();
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		if (_target == null || !IsInstanceValid(_target))
		{
			_findTarget();
			return;
		}
		Distance = OwnerBody.GlobalPosition.DistanceTo(_target.GlobalPosition);
	}

	private void _findTarget()
	{
		Players = GetTree().GetNodesInGroup("Player");

		if (Players.Count > 0)
		{
			_target = (Node2D)Players[0];
			EmitSignal(SignalName.TargetAcquired, _target);
		}
	}
}
using Godot;

public partial class State : Node
{
	public StateMachine fsm;

	public virtual void Enter() { }
	public virtual void Exit() { }

	public new virtual void Ready() { }
	public virtual void Update(double delta) { }
	public virtual void PhysicsUpdate(double delta) { }
	public virtual void HandleInput(InputEvent @event) { }
}

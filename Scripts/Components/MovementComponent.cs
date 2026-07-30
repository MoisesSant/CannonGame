using Godot;

public partial class MovementComponent : Node
{
	// EXPORTS
	[ExportGroup("Movement")]
	[Export] public float Speed { get; set; } = 300.0f;
	[Export] public float Acceleration { get; set; } = 250.0f;
	[Export] public float Friction { get; set; } = 50.0f;

	// INTERNAL
	public Vector2 _velocity { get; set; } = Vector2.Zero;

	// METHODS
	public Vector2 ApplyLinearMovement(
		Vector2 inputDirection,
		CharacterBody2D Body)
	{
		Vector2 targetVelocity = inputDirection.Normalized() * Speed;

		if (inputDirection != Vector2.Zero)
		{
			_velocity = _velocity.MoveToward(targetVelocity, Acceleration);
		}
		else
		{
			_velocity = _velocity.MoveToward(Vector2.Zero, Friction);
		}
		Body.MoveAndSlide();
		return _velocity;
	}

	public Vector2 ApplyProgressiveMovement(
		double delta,
		Vector2 inputDirection,
		CharacterBody2D Body)
	{
		_velocity = _velocity.LimitLength(Speed);

		if (inputDirection != Vector2.Zero)
		{
			_velocity += inputDirection * Acceleration * (float)delta;
		}
		else
		{
			_velocity *= Friction * (float)delta;
		}
		Body.MoveAndSlide();
		return _velocity;
	}
}
using Godot;

public partial class MovementComponent : Node
{
	// EXPORTS
	[ExportGroup("Movement")]
	[Export] public float Speed { get; set; } = 300.0f;
	[Export] public float Acceleration { get; set; } = 250.0f;
	[Export] public float Friction { get; set; } = 50.0f;

	// INTERNAL
	public Vector2 Velocity { get; set; } = Vector2.Zero;

	// METHODS
	public Vector2 ApplyLinearMovement(
		Vector2 inputDirection,
		CharacterBody2D Body)
	{
		Vector2 targetVelocity = inputDirection.Normalized() * Speed;

		if (inputDirection != Vector2.Zero)
		{
			Velocity = Velocity.MoveToward(targetVelocity, Acceleration);
		}
		else
		{
			Velocity = Velocity.MoveToward(Vector2.Zero, Friction);
		}
		Body.MoveAndSlide();
		return Velocity;
	}

	public Vector2 ApplyProgressiveMovement(
		double delta,
		Vector2 inputDirection,
		CharacterBody2D Body)
	{
		Velocity = Velocity.LimitLength(Speed);

		if (inputDirection != Vector2.Zero)
		{
			Velocity += inputDirection * Acceleration * (float)delta;
		}
		else
		{
			Velocity *= Friction * (float)delta;
		}
		Body.MoveAndSlide();
		return Velocity;
	}
}
/*
como eu posso melhorar esse código deixando o mais proficional possível?, e quando eu disparo um projétil ele cria um timer extra fora da SceneTree do bullet e não desaparece quando timeout é chamado, como eu posso resolver isso? e como eu posso de forma eficáz e acertiva pegar a referência desses timers extra?

Nota: O primeiro timer que é criado fora da SceneTree do bullet surgindo na árvore Root tem o nome @Timer@2 e depois sucessivamente @Timer@3...
*/
using Godot;

namespace SmartZombies.Scenes
{
	public abstract class Character : KinematicBody2D
	{
		[Export] protected readonly float MaxSpeed;

		[Export] public readonly float Mass;

		public Vector2 Velocity;
		public Vector2 Heading;
		public bool Tagged = false;

		public override void _PhysicsProcess(float delta)
		{
			if (Velocity.LengthSquared() > 0.0001)
			{
				Heading = Velocity.Normalized();
			} 

			Velocity = Velocity.Clamped(MaxSpeed);
			MoveAndSlide(Velocity);
		}
	}
}

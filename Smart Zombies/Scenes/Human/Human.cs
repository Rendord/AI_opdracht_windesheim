using Godot;

namespace SmartZombies.Scenes.Human
{
	public class Human : Character
	{
		public override void _PhysicsProcess(float delta)
		{
			Velocity += new Vector2(-MaxSpeed, 1);
			base._PhysicsProcess(delta);
		}
	}
}

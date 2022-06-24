using Godot;

namespace SmartZombies.Scenes
{
    public class Character : KinematicBody2D
    {
        [Export] protected readonly float MaxSpeed;

        [Export] public readonly float Mass;

        public Vector2 Velocity;
        public Vector2 Heading;
        public bool Tagged = false;
    }
}

using Godot;

namespace SmartZombies.Scenes
{
    public class Character : KinematicBody2D
    {
        protected const float MaxSpeed = 500;

        public Vector2 Velocity;
        public float Mass;
        public Vector2 Heading;
        public bool Tagged = false;
    }
}

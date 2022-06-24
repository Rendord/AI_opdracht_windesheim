using System;
using Godot;

namespace SmartZombies.Scenes.Zombie
{
    public class Zombie : Character
    {
        private enum Deceleration
        {
            Fast = 1,
            Normal = 2,
            Slow = 3,
        }

        private Character _target;
        private RayCast2D _velocityRayCast;

        public override void _Ready()
        {
            _target = GetTree().CurrentScene.GetNode<Character>("Human");
            _velocityRayCast = GetNode<RayCast2D>("Velocity");
        }

        public override void _PhysicsProcess(float delta)
        {
            // Velocity += SeekBehaviour(_target.Position);
            Velocity += PursuitBehaviour(_target);
            Velocity += ArriveBehaviour(_target.Position, Deceleration.Fast);

            _velocityRayCast.CastTo = Velocity;

            base._PhysicsProcess(delta);
        }

        private Vector2 SeekBehaviour(Vector2 targetPosition)
        {
            var desiredVelocity = (targetPosition - Position).Normalized() * MaxSpeed;
            return desiredVelocity - Velocity;
        }

        private Vector2 FleeBehaviour(Vector2 targetPosition)
        {
            var desiredVelocity = (Position - targetPosition).Normalized() * MaxSpeed;
            return desiredVelocity - Velocity;
        }

        private Vector2 ArriveBehaviour(Vector2 targetPosition, Deceleration speed)
        {
            var toTarget = targetPosition - Position;
            var dist = toTarget.Length();

            if (!(dist > 0))
            {
                return Vector2.One;
            }

            const float decelerationTweaker = 0.3f;
            var calculatedSpeed = dist / ((int)speed * decelerationTweaker);
            var desiredVelocity = toTarget * calculatedSpeed / dist;
            return (desiredVelocity - Velocity).Normalized() * MaxSpeed;
        }

        private Vector2 PursuitBehaviour(Character evader)
        {
            var toEvader = evader.Position - Position;
            var relativeHeading = Heading.Dot(evader.Heading);
            if (toEvader.Dot(Heading) > 0 && relativeHeading < -0.95)
            {
                return SeekBehaviour(evader.Position);
            }

            var lookAheadTime = toEvader.Length() / MaxSpeed + evader.Velocity.Length();
            return SeekBehaviour(evader.Position + evader.Velocity * lookAheadTime);
        }
    }
}

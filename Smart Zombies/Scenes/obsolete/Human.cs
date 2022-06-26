//using System;
//using System.Collections.Generic;
//using System.Drawing.Drawing2D;
//using Godot;
//
//namespace SmartZombies.Scenes.Human
//{
//	public class Human : Character
//	{
//		public List<Obstacle> Obstacles;
//		public Zombie.Zombie Target; 
//		private enum Deceleration
//		{
//			Fast = 1,
//			Normal = 2,
//			Slow = 3,
//		}
//
//		public override void _Ready()
//		{
//			MaxSpeed = 100;
//			RayCasts = GetNode<Node2D>("RayCasts");
//			base._Ready();
//		}
//
//		public override void _PhysicsProcess(float delta)
//		{
//
//			//Velocity += HideBehaviour(Target, Obstacles);
//			Vector2 steeringForce = Vector2.Zero;
//			//steeringForce += ObstacleAvoidance(Obstacles);
//			steeringForce += ArriveBehaviour(new Vector2(880, 272), Deceleration.Fast);
//			steeringForce += ObstacleAvoidBehaviour();
//			steeringForce.Clamped(MaxSteering);
//			Velocity += steeringForce;
//
//
//			base._PhysicsProcess(delta);
//
//
//		}
//
//		private Vector2 FleeBehaviour(Vector2 targetPosition)
//		{
//			var desiredVelocity = (Position - targetPosition).Normalized() * MaxSpeed;
//			return desiredVelocity - Velocity;
//		}
//
//		private Vector2 EvadeBehaviour(Character pursuer)
//		{
//			var toPursuer = pursuer.Position - Position;
//			var LookAheadTime = toPursuer.Length() / MaxSpeed + pursuer.Velocity.Length();
//			return FleeBehaviour(pursuer.Position + pursuer.Velocity * LookAheadTime);
//		}
//
//		private Vector2 ArriveBehaviour(Vector2 targetPosition, Deceleration deceleration)
//		{
//			var toTarget = targetPosition - Position;
//			var dist = toTarget.Length();
//
//			if (dist > 0)
//			{
//				const float decelerationTweaker = 0.3f;
//				var calculatedSpeed = dist / ((int)deceleration * decelerationTweaker);
//				calculatedSpeed = Math.Min(calculatedSpeed, MaxSpeed);
//				var desiredVelocity = toTarget * calculatedSpeed / dist;
//				return (desiredVelocity - Velocity);
//			}
//
//			return Vector2.Zero;
//		}
//		private Vector2 GetHidingPosition(Vector2 posOb, double radiusOb, Vector2 posTarget)
//		{
//			float distanceFromBoundary = 30;
//			float DistAway = (float)radiusOb + distanceFromBoundary;
//
//			Vector2 ToOb = (posOb - posTarget).Normalized();
//
//			return (ToOb * DistAway) + posOb;
//		}
//
//		private Vector2 HideBehaviour(Character target, List<Obstacle> obstacles)
//		{
//			double DistToClosest = 3000;
//			Vector2 BestHidingSpot = Vector2.Zero;
//			foreach (Obstacle obstacle in obstacles)
//			{
//				Vector2 HidingSpot = GetHidingPosition(obstacle.position, obstacle.radius, target.Position);
//
//				double dist = HidingSpot.DistanceSquaredTo(Position);
//
//				if (dist < DistToClosest)
//				{
//					DistToClosest = dist;
//					BestHidingSpot = HidingSpot;
//					//Console.WriteLine(BestHidingSpot);
//				}
//			}
//
//			if (DistToClosest == 3000)
//			{
//				return EvadeBehaviour(target);
//				//return FleeBehaviour(target.Position);
//			}
//			else
//			{
//				return ArriveBehaviour(BestHidingSpot, Deceleration.Fast);
//			}
//
//		}
//
//		private Vector2 ObstacleAvoidBehaviour()
//		{
//			RayCasts.Rotation = Velocity.Angle();
//			foreach (RayCast2D raycast in RayCasts.GetChildren())
//			{
//
//				raycast.CastTo = new Vector2(Velocity.Length(), 0);
//				if (raycast.IsColliding())
//				{
//					PhysicsBody2D obstacle = (PhysicsBody2D)raycast.GetCollider();
//
//					return (Position + Velocity - obstacle.Position).Normalized() * (float)AvoidForce;
//				}
//
//			}
//
//			return Vector2.Zero;
//		}
//
//		private Vector2 ObstacleAvoidanceDeprecated(List<Obstacle> obstacles)
//		{
//			double detectionBoxLength = 64;
//			TagObstaclesWithinRadius(Obstacles, detectionBoxLength, Position);
//			Obstacle CIB = null;
//			double distToCIB = Double.MaxValue;
//			Vector2 LocalPosOfCIB = Vector2.Zero;
//
//			foreach (Obstacle obs in obstacles)
//			{
//				if (obs.tagged)
//				{
//					Vector2 LocalPos = ToLocal(obs.position);
//
//					double ExpandedRadius = obs.radius + Radius;
//
//					if (LocalPos.x >= 0)
//					{
//						if (Math.Abs(LocalPos.y) < ExpandedRadius)
//						{
//							double cX = LocalPos.x;
//							double cY = LocalPos.y;
//
//							double Sqrtpart = Math.Sqrt(ExpandedRadius * ExpandedRadius - cY * cY);
//
//							double intersection_point = cX - Sqrtpart;
//							if (intersection_point <= 0)
//							{
//								intersection_point = cX + Sqrtpart;
//							}
//
//							if (intersection_point < distToCIB)
//							{
//								distToCIB = intersection_point;
//
//								CIB = obs;
//
//								LocalPosOfCIB = LocalPos;
//
//							}
//						}
//					}
//				}
//			}
//			Vector2 SteeringForce = Vector2.Zero;
//
//			if (CIB != null)
//			{
//				double multiplier = 1 + (detectionBoxLength - LocalPosOfCIB.x) / detectionBoxLength;
//				SteeringForce.y = (float)((CIB.radius - LocalPosOfCIB.y) * multiplier);
//				double brakingForce = -0.2;
//
//				SteeringForce.x = (float)((CIB.radius - LocalPosOfCIB.x) * brakingForce);
//
//				return ToGlobal(SteeringForce);
//
//			}
//
//			return Vector2.Zero;
//
//		}
//
//		private void TagObstaclesWithinRadius(List<Obstacle> obstacles, double radius, Vector2 pos)
//		{
//			double squaredradius = radius * radius;
//			foreach (Obstacle obs in obstacles)
//			{
//				obs.tagged = false;
//				if (obs.position.DistanceSquaredTo(pos) < squaredradius)
//				{
//					obs.tagged = true;
//				}
//			}
//		}
//
//
//
//	}
//}

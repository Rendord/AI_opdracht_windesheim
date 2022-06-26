using System;
using System.Collections.Generic;
using Godot;

namespace SmartZombies.Scenes
{
	public abstract class Character : KinematicBody2D
	{
		[Export] public float MaxSpeed = 50;

		[Export] public float Mass = 10;

		public Vector2 Velocity;
		public Vector2 Heading;
		public bool Tagged = false;
		public double Radius = 8;
		public Node2D RayCasts;
		public double AvoidForce = 1000;
		public float MaxSteering = 2.5f;

		public override void _Ready()
		{
			// RayCasts = new Node2D();
			// var raycast1 = new RayCast2D();
			// var raycast2 = new RayCast2D();
			// var raycast3 = new RayCast2D();
			// raycast2.Position = new Vector2(0, 8);
			// raycast3.Position = new Vector2(0, -8);
			// RayCasts.AddChild(raycast1);
			// RayCasts.AddChild(raycast2);
			// RayCasts.AddChild(raycast3);
			// foreach (RayCast2D raycast in RayCasts.GetChildren())
			// {
			// 	raycast.CastTo = new Vector2(50, y:0);
			// 	raycast.Enabled = true;
			// 	raycast.CollideWithBodies = true;
			// 	//raycast.CollideWithAreas = true;
			// }
			// AddChild(RayCasts);
			
		}

		public override void _PhysicsProcess(float delta)
		{
			
			if (Velocity.LengthSquared() > 0.0001)
			{
				Heading = Velocity.Normalized();
			} 

			Velocity = Velocity.Clamped(MaxSpeed);
			//Rotate(GetAngleTo(Velocity));
			MoveAndSlide(Velocity);
		}
	}
}

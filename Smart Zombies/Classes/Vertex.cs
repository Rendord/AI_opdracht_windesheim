using Godot;
using System;
using System.Collections.Generic;
using SmartZombies.Classes;

public class Vertex
{
	
	public Vector2 position;
	public List<Edge> adjacent;
	public double distance;
	public bool known;
	
	

	public Vertex(Vector2 position)
	{
		this.position = position;
		adjacent = new List<Edge>();
		
	}

	public List<Vector2> CheckNeighbours(RayCast2D raycast, List<Vector2> directions)
	{
		List<Vector2> possiblePositions = new List<Vector2>();
		raycast.Position = position;
		foreach (Vector2 dir in directions)
		{
			if (!CheckForCollision(dir, raycast))
			{
				possiblePositions.Add(position + dir);
			}
		}

		return possiblePositions;

	}

	private bool CheckForCollision(Vector2 target, RayCast2D raycast)
	{
		raycast.CastTo = target;
		raycast.ForceRaycastUpdate();
		return raycast.IsColliding();
	}
}

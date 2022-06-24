// using System.Collections.Generic;
//
// namespace SmartZombies.Scenes.Grid.GridNode
// {
//     public class test
//     {
//         private void Generate()
//         {
//             Queue<Vertex> toEvaluate = new Queue<Vertex>();
//             List<Vector2D> directions = new List<Vector2D> {
//                 new Vector2D(0,spacing), new Vector2D(0,-spacing),
//                 new Vector2D(spacing,-0), new Vector2D(-spacing,0),
//                 new Vector2D(spacing, spacing), new Vector2D(spacing,-spacing),
//                 new Vector2D(-spacing, spacing), new Vector2D(-spacing, -spacing)};
//
//             toEvaluate.Enqueue(GetVertex($"{start.X},{start.Y}"));
//
//             while (toEvaluate.Count != 0)
//             {
//                 Vertex vertex = toEvaluate.Dequeue();
//
//                 //check each direction for collisions
//                 foreach (var direction in directions)
//                 {
//                     bool noCollision = true;
//                     Vector2D value; // unused
//
//                     // checks for collisions with walls
//                     foreach (var wall in walls)
//                     {
//                         if (Vector2D.LineSegementsIntersect(vertex.Pos, vertex.Pos + direction,
//                                 wall.Pos, wall.Pos2, out value))
//                             noCollision = false;
//                     }
//
//                     if (noCollision)
//                     {
//                         // if vertex already exists, don't add it to evaluate queue
//                         if (!vertexMap.ContainsKey((vertex.Pos + direction).ToString()))
//                         {
//                             toEvaluate.Enqueue(GetVertex((vertex.Pos + direction).ToString()));
//                         }
//                         // add new edge
//                         AddEdge(vertex.Pos.ToString(), (vertex.Pos + direction).ToString(), direction.Length());
//                     }
//                 }
//             }
//         }
//     }
// }

using Godot;
    
namespace SmartZombies.Scenes

{
    public class Obstacle : Godot.Object
    {
        public string name;
        public Vector2 position;
        public double radius;
        public bool tagged;

        public Obstacle(Vector2 position, double radius, string name)
        {
            this.position = position;
            this.radius = radius;
            this.name = name;
        }
    }
}
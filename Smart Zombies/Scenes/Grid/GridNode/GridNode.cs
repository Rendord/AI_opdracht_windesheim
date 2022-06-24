using Godot;

namespace SmartZombies.Scenes.Grid.GridNode
{
    public class GridNode : Node2D
    {
        public const int Width = 16;
        public const int Height = 16;

        public void Init(Vector2 position)
        {
            Name = "Name";
            Position = position + GetOffSet();
        }

        private Vector2 GetOffSet()
        {
            return new Vector2(Width * 0.5f, Height * 0.5f);
        }
    }
}

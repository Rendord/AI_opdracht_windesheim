using System.Collections.Generic;
using Godot;
using SmartZombies.Scenes.Grid;

namespace SmartZombies
{
    public class Main : Node2D
    {
        public override void _Ready()
        {
            InitGrid();
        }

        private void InitGrid()
        {
            var map = GetNode<Node2D>("Map");
            var wallTileMaps = new List<TileMap>
            {
                map.GetNode<TileMap>("Walls"),
                map.GetNode<TileMap>("WallAuto")
            };

            var gridSceneInstance = GD.Load<PackedScene>("Scenes/Grid/Grid.tscn").Instance<Grid>();
            AddChild(gridSceneInstance);
            gridSceneInstance.Init(wallTileMaps);
        }
    }
}

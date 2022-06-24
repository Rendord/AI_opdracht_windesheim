using System;
using System.Collections.Generic;
using Godot;

namespace SmartZombies.Scenes.Grid
{
    public class Grid : Node2D
    {
        public readonly List<List<GridNode.GridNode>> GridNodes = new List<List<GridNode.GridNode>>();

        private PackedScene _gridNodeScene;
        private List<TileMap> _colissionTileMaps;

        public void Init(List<TileMap> tileMaps)
        {
            Name = "Grid";
            _colissionTileMaps = tileMaps;

            _gridNodeScene = GD.Load<PackedScene>("Scenes/Grid/GridNode/GridNode.tscn");
            GenerateGrid();
        }

        private void GenerateGrid()
        {
            var noGridNodesX = Math.Floor(GetViewport().Size.x / GridNode.GridNode.Width);
            var noGridNodesY = Math.Floor(GetViewport().Size.y / GridNode.GridNode.Height);
            for (var i = 0; i < noGridNodesY; i++)
            {
                var globalPosition = new Vector2(0, i * GridNode.GridNode.Height);
                var rowNode = AddRowNode(globalPosition);
                GridNodes.Add(new List<GridNode.GridNode>());
                for (var j = 0; j < noGridNodesX; j++)
                {
                    globalPosition.x = j * GridNode.GridNode.Width;
                    var gridNode = AddGridNode(rowNode, globalPosition);
                    if (gridNode != null)
                    {
                        GridNodes[i].Add(gridNode);
                    }
                }
            }
        }

        private Node AddRowNode(Vector2 globalPosition)
        {
            var rowNode = new Node2D
            {
                Name = "row",
                Position = new Vector2(0, globalPosition.y)
            };
            AddChild(rowNode);
            return rowNode;
        }

        private GridNode.GridNode AddGridNode(Node rowNode, Vector2 globalPosition)
        {
            if (CheckTileCollision(globalPosition)) return null;

            var gridNode = _gridNodeScene.Instance<GridNode.GridNode>();
            gridNode.Init(new Vector2(globalPosition.x, 0));
            rowNode.AddChild(gridNode);
            return gridNode;
        }

        private bool CheckTileCollision(Vector2 globalPosition)
        {
            var wallFloorTile = new Vector2(1, 1);
            foreach (var tileMap in _colissionTileMaps)
            {
                var tileInTileMapCoord = tileMap.WorldToMap(globalPosition);
                var tileId = tileMap.GetCellv(tileInTileMapCoord);

                var tileAutoId = Vector2.Zero;
                if (tileMap.Name == "WallAuto")
                {
                    tileAutoId = tileMap.GetCellAutotileCoord(
                        (int)tileInTileMapCoord.x,
                        (int)tileInTileMapCoord.y);
                }

                if (tileId != TileMap.InvalidCell && tileAutoId != wallFloorTile)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

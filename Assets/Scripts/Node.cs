using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Terrain
{
    public class Node
    {
        private int x;
        private int y;
        private TileBase _tileBase;
        public Node(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Node(int x, int y, TileBase tile)
        {
            this.x = x;
            this.y = y;
            _tileBase = tile;
        }
        public void SetTile(TileBase tile)
        {
            _tileBase = tile; 
        }
    }
    public class Height
    {
        public int x { private set; get; }
        public int height { private set; get; }
        public Height(int x, int height)
        {
            this.height = height;
            this.x = x;
        }
    }


}

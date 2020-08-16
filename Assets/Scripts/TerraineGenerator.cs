using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Terrain;

public class TerraineGenerator : MonoBehaviour
{
    public int  Width;
    public int Frequency, Noise;
    public List<int> HeightMap { get  => _heightMap; }
    public GroundTile GroundTile;
    public Tilemap Tilemap;

    [SerializeField]
    private TileBase _grass;
    private List<int> _heightMap;

    public void Generate()
    {
        int groundHeight = 0;
        int groundZeroCount = 0;
        int groundOver10Count = 0;
        for (int x = 0; x < Width; x++)
        {
            if (x % Frequency == 0)
            {
                groundHeight += Random.Range(-1, Noise);
            }

            if (groundHeight <= 0)
                groundZeroCount++;

            if (groundHeight > 10)
            {
                groundOver10Count++;
            }

            if (groundZeroCount > 1)
            {
                groundZeroCount = 0;
                groundHeight = 0;
            }
            if(groundOver10Count>1)
            {
                groundOver10Count = 0;
                groundHeight = 10;
            }
            _heightMap.Add(groundHeight);
        }
        int i = 0;
        foreach (var height in _heightMap)
        {
            Tilemap.SetTile(new Vector3Int(i, height, 0), _grass);
            for (int j = height - 1; j >= 0; j--)
            {
                Tilemap.SetTile(new Vector3Int(i, j, 0), _grass);
            }
            i++;
        }
    }
    public void ClearTerraine()
    {
        _heightMap.Clear();
        Tilemap.ClearAllTiles();
    }
}
namespace Terrain
{
    public class HeightMap
    {
        private int _count { set => _heights.Sum(); }
        private int _width;
        private List<int> _heights;

        public List<int> Heights { get => _heights; }
        public int MaxHeight { get => _heights.Max(); }
        public int Length { get => _heights.Count; }
        public HeightMap(int width)
        {
            _width = width;
        }
        public int this[int index]
        {
            get => _heights[index];
            set
            {
                _heights[index] = value;
            }
        }

        public void SetHeight(int x, int height)
        {
            _heights[x] = height;
        }
        public void Clear()
        {
            if(_heights.Count > 0 )
            _heights.Clear();
        }
        public void Add(int value)
        {
            _heights.Add(value);
        }

    
    }
    [CreateAssetMenu(menuName = "Ground Tile")]
    public class GroundTile : ScriptableObject
    {
        public TileBase Left, Right, Top, Bottom, TopLeft, TopRight, BottomRight, BottomLeft, Solo, HillRightBottom, HillRight, HillLeft, HillleftBottom;
        public TileBase[] Tiles = new TileBase[8];
        public Vector2Int[] TileIndexes = new Vector2Int[8];
        public GroundTile()
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    TileIndexes[x + y] = new Vector2Int(x - 1, y - 1);
                }
            }
        }
    }
    public class TerrainRenderar : MonoBehaviour
    {
        public List<int> HeightMap;
        public GroundTile Tiles;
        public Tilemap Tilemap;
        public TerrainRenderar(List<int> heightMap, GroundTile groundTile, Tilemap tilemap)
        {
            HeightMap = heightMap;
            Tiles = groundTile;
            Tilemap = tilemap;

        }
        public void Render()
        {
            bool[] isOtherTiles = new bool [8];

            Vector3Int tilePos;

            for (int i = 0; i < HeightMap.Count; i++)
             {
                for (int j = HeightMap[i]; j >= 0; j--)
                {
                    tilePos = new Vector3Int(i, j, 0);
                    for (int x = 0; x < 3; x++)
                    {
                        for (int y = 0; y < 3; y++)
                        {
                            isOtherTiles[x+y] = !IsNotNullTile(tilePos, new Vector3Int(x - 1, y - 1,0));
                        }
                    }

                    SetTiles(0);
                }
             }

            bool IsNotNullTile (Vector3Int pos, Vector3Int shift)
            {
                return Tilemap.GetTile(pos + shift) == null;
            }

            void SetTiles(int index)
            {
                
                
                if (index > 7)
                {
                    return;
                }
                else
                {
                    if (isOtherTiles[index])
                    {
                        Tilemap.SetTile(tilePos, Tiles.Tiles[index]);
                        SetTiles(index + 1 );
                    }
                    else
                    {
                        SetTiles(index + 1);
                    }
                }
            }

        }
    }

    public class TileType
    {
        public bool[,] GridLogicalePosition;
        public int Width, Height;

        public TileType(int width, int height)
        {
            Width = width;
            Height = height;
            GridLogicalePosition = new bool[Width, Height];
           for(int y= 0; y< height; y++)
           {
                for(int x = 0; x < Width; x++)
                {
                    GridLogicalePosition[x, y] = false;
                }
           }

        }

        public void SetTileTypes(Vector3Int pos)
        {
            for(int i = -1; i < 1; i++)
            {
                for(int j = -1; j < 1; j++)
                {
                    GridLogicalePosition[pos.x + j, pos.y + i] = true;
                }
            }
        }

    }
}



using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Terrain;

public class TerraineGenerator : MonoBehaviour
{
    public int Width;
    public int Frequency, Noise;
    public GroundTile GroundTile;
    public Tilemap Tilemap;
    public Energy IEnergy;
    public int StartGroundHeight;
    [System.Serializable]
    public class Energy {
        public TileBase Grass;
        [SerializeField]
        public int EnergyPointFriqency;
        [SerializeField]
        public GameObject EnergyPoint;
        public GameObject CheckpointsParents;
    }
    public HeightMap Generate()
    {
        HeightMap heightMap = new HeightMap(Width);
        IEnergy.CheckpointsParents = new GameObject("Checkpoints");
        IEnergy.CheckpointsParents.transform.position = Vector3.zero;

        int groundHeight = StartGroundHeight;
        int groundZeroCount = 0;
        int groundOver10Count = 0;
        heightMap[0] = 0; heightMap[Width - 1] = 0;
        for (int x = 1; x < Width - 1; x++)
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
            heightMap.SetHeight(x, groundHeight);

            if(x % IEnergy.EnergyPointFriqency == 0)
            {
                EnergyPointGenerate(groundHeight, x, IEnergy.CheckpointsParents.transform);
            }
        }
        return heightMap;
      
    }
    public void EnergyPointGenerate(int height, int width, Transform parent)
    {
            Vector3Int position = new Vector3Int(width, height + 2, 0);
            Instantiate(IEnergy.EnergyPoint, Tilemap.CellToWorld(position), Quaternion.identity, parent);
    }
    public void EnergyPointsGenerate(HeightMap map)
    {
        for(int i = 0;  i <= map.Count - IEnergy.EnergyPointFriqency; i+=IEnergy.EnergyPointFriqency)
        {
            EnergyPointGenerate(map[i] + 2, i, IEnergy.CheckpointsParents.transform);
        }
    }
    public void ClearTerraine(HeightMap heightMap)
    {
        heightMap.Clear();
        Tilemap.ClearAllTiles();
#if UNITY_EDITOR
            DestroyImmediate(IEnergy.CheckpointsParents);
#else
          foreach(var child in FindObjectsOfType<EnergyRecovery>())
        {
            Destroy(child.gameObject);
        }
#endif
    }
}
namespace Terrain
{
    public class HeightMap
    {
        public int Count {get => _heights.Sum(); }
        private int _width;
        private int[] _heights;

        public int[] Heights { get => _heights; }
        public int MaxHeight { get => _heights.Max(); }
        public int Length { get => _heights.Length; }
        public int Width { get => _width; }
        public HeightMap(int width)
        {
            _width = width;
            _heights = new int[_width];
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
            _heights = null;
        }    
    }
    [CreateAssetMenu(menuName = "Ground Tile")]
    public class GroundTile : ScriptableObject
    {
        public TileBase Left, Right, Top, Bottom, MiddleTop, MiddleBottom, Middle, TopLeft, TopRight, BottomRight, BottomLeft, Single,  Center;
        public TileBase[] Tiles;

        public TileBase[] Tops = new TileBase[5];
        public TileBase[] Bottoms = new TileBase[5];
        public TileBase[] BottomsTops = new TileBase[5];
        public TileBase[] Middles = new TileBase[5];

    }

    public class CustomArray<T>
    {
        public T[] Array;
        private int _length;
        public int Length { get => _length; }

        public CustomArray(T[] array)
        {
            _length = array.Length;
            Array = new T[_length];

            for (int i = 0; i < _length; i++)
            {
                Array[i] = default;
                Array[i] = array[i];
            }
        }
        public CustomArray(int length)
        {
            _length = length;

            for (int i = 0; i < _length; i++)
            {
                Array[i] = default;
            }
        }
        public T this[int index]
        {
            get => Array[index];
        }

        public void SetArrayObject(int index, T value)
        {
            Array[index] = value;
        }

    }

    public class TerrainRenderar : MonoBehaviour
    {
        public GroundTile GroundTile;
        public Tilemap Tilemap;
    

        public TerrainRenderar(GroundTile groundTile, Tilemap tilemap)
        {
            GroundTile = groundTile;
            Tilemap = tilemap;
        }
       
        public void Render(HeightMap heights)
        {
            for (int i = 1; i < heights.Width - 1; i++)
            {
                
                for (int j = heights[i]; j >= 0; j--)
                {
                    Vector3Int currentTilePosition = new Vector3Int(i, j, 0);
                    Tilemap.SetTile(currentTilePosition, GetTile(i, j));
                }
            }       
           
            TileBase GetTile(int x, int y)
            {
                TileBase currentTile = GroundTile.Single;
                bool isAboveLeft = heights[x - 1] > y, isAboveRight = heights[x + 1] > y;
                bool isBelowLeft = heights[x - 1] < y, isBelowRight = heights[x + 1] < y;
                SetMiddleTiles(GroundTile.Middles);
                if (heights[x] == y)
                {
                    SetMiddleTiles(GroundTile.Tops);
                }
                if (y == 0 && heights[x] ==0)
                {
                    SetMiddleTiles(GroundTile.BottomsTops);
                }
                if(y == 0 && heights[x] != 0)
                {

                    SetMiddleTiles(GroundTile.Bottoms);
                }
                void SetMiddleTiles(TileBase[] tiles)
                {
                    if (isBelowRight && isBelowLeft)
                    {
                        currentTile = tiles[0];
                    }
                    else if (isAboveLeft && isAboveRight)
                    {
                        currentTile = tiles[1];
                    }
                    else if (isBelowRight && isAboveLeft)
                    {
                        currentTile = tiles[2];
                    }
                    else if (isAboveRight && isBelowRight)
                    {
                        currentTile = tiles[3];
                    }
                    else
                    {
                        currentTile = tiles[4];
                    }
                }
                return currentTile;
            }
        }
    }
}




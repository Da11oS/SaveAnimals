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

    [SerializeField]
    private TileBase _grass;
    [SerializeField]
    private int _energyPointFriqency;
    [SerializeField]
    private GameObject _energyPoint;
    GameObject _checkpointsParents;
    public HeightMap Generate()
    {
        HeightMap heightMap = new HeightMap(Width);
        _checkpointsParents = new GameObject("Checkpoints");
        _checkpointsParents.transform.position = Vector3.zero;

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
            heightMap[x] = groundHeight;

            if(x % _energyPointFriqency == 0)
            {
                EnergyPointsGenerate(groundHeight, x, _checkpointsParents.transform);
            }
        }
        for (int i = 0; i < Width; i++)//var height in _heightMap)
        {
            if (heightMap[i] >= 0)
            {
                Tilemap.SetTile(new Vector3Int(i, heightMap[i], 0), _grass);
                for (int j = heightMap[i]; j >= 0; j--)
                {
                    Tilemap.SetTile(new Vector3Int(i, j, 0), _grass);
                }
            }
        }
        return heightMap;
      
    }
    public void EnergyPointsGenerate(int height, int width, Transform parent)
    {
            Vector3Int position = new Vector3Int(width, height + 2, 0);
            Instantiate(_energyPoint, Tilemap.CellToWorld(position), Quaternion.identity, parent);
    }
    public void ClearTerraine(HeightMap heightMap)
    {
        heightMap.Clear();
        Tilemap.ClearAllTiles();
#if UNITY_EDITOR
            DestroyImmediate(_checkpointsParents);
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
        private int _count { set => _heights.Sum(); }
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
        public TileBase Left, Right, Top, Bottom, MiddleTop, MiddleBottom, Middle, TopLeft, TopRight, BottomRight, BottomLeft, Solo, HillRightBottom, HillRight, HillLeft, HillleftBottom, Center;
        public TileBase[] Tiles;
       
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
    
        public int ExistsLength { get => Exists.Count; }
        public List<CustomArray<bool>> Exists;
        public const int Length = 4;

		private int[] _tempExists = new int[Length];
        public TerrainRenderar(GroundTile groundTile, Tilemap tilemap)
        {
          
            GroundTile = groundTile;
            Tilemap = tilemap;
            for (int i = 0; i < Length; i++)
            {
                _tempExists[i] = 0;
            }
            Exists = default;
            Plunk(0);
            GroundTile.Tiles = new TileBase[ExistsLength];
        }
        public void Render(HeightMap heightMap)
        {
            for (int i = 0; i < heightMap.Width; i++)
            {
                for (int j = heightMap[i]; j >= 0; j--)
                {
                    SetTile(new Vector3Int(i, j, 0));
                }
            }
       
            void SetTile(Vector3Int currentTilePosition)
            {
                bool[] currentTileExists = new bool[Length];
               
                currentTileExists[0] = IsNotNullTile(currentTilePosition, Vector3Int.up);
                currentTileExists[1] = IsNotNullTile(currentTilePosition, Vector3Int.right);
                currentTileExists[2] = IsNotNullTile(currentTilePosition, Vector3Int.down);
                currentTileExists[3] = IsNotNullTile(currentTilePosition, Vector3Int.up);

                for (int i = 0; i < ExistsLength; i++)
                {
                    bool isSame = true;
                    for (int j = 0; j < Length; j++ )
                    {
                        if(currentTileExists[j] != Exists[i].Array[j])
                        {
                            isSame = false;
                            break;
                        }
                    }
                    if(isSame)
                    {
                        Tilemap.SetTile(currentTilePosition, GroundTile.Tiles[i]);
                    }
                }

            }
        }
        public void Plunk(int index)
        {
            if (index > Length - 1)
            {
                bool[] temp = new bool[Length];
                
                for(int i = 0; i < Length; i++)
                {
                    if(_tempExists[i] == 1)
                    {
                        temp[i] = true;
                    }
                    else temp[i] = false;
                }
                CustomArray<bool> customArrayTemp = new CustomArray<bool>(temp);
                Exists.Add(customArrayTemp);
                return;
            }

            for (int i = 0; i <= 1; i++)
            {
                _tempExists[index] = i;
                Plunk(index + 1);
            }

        }
        bool IsNotNullTile(Vector3Int pos, Vector3Int shift)
        {
            return Tilemap.GetTile(pos + shift) == null;
        }
    }
}




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
    public List<int> HeightMap { get  => _heightMap; }
    public GroundTile GroundTile;
    public Tilemap Tilemap;

    [SerializeField]
    private TileBase _grass;
    private List<int> _heightMap;
    [SerializeField]
    private int _energyPointFriqency;
    [SerializeField]
    private GameObject _energyPoint;

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

            if(x % _energyPointFriqency == 0)
            {
                EnergyPointsGenerate(groundHeight, x);
            }
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
    public void EnergyPointsGenerate(int height, int width)
    {
            Vector3Int position = new Vector3Int(width, height + 2, 0);
            Instantiate(_energyPoint, Tilemap.CellToWorld(position), Quaternion.identity);
    }
    public void ClearTerraine()
    {
        _heightMap.Clear();
        Tilemap.ClearAllTiles();
#if UNITY_EDITOR
        foreach(var child in FindObjectsOfType<EnergyRecovery>())
        {
            DestroyImmediate(child.gameObject);
        }
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

            for (int i = 0; i < _length; i++)
            {
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
        public List<int> HeightMap;
        public GroundTile GroundTile;
        public Tilemap Tilemap;
    
        public int ExistsLength;
        public List<CustomArray<bool>> Exists;
        public int Length;
        private bool[] _tempExists;
        public TerrainRenderar(List<int> heightMap, GroundTile groundTile, Tilemap tilemap)
        {
            Length = 4;
            HeightMap = heightMap;
            GroundTile = groundTile;
            Tilemap = tilemap;
            _tempExists = new bool[Length];
            for (int i = 0; i < Length; i++)
            {
                _tempExists = default;
            }
            Plunk(0);
            ExistsLength = Exists.Count;
            GroundTile.Tiles = new TileBase[ExistsLength];
        }
        public void Render()
        {
            for (int i = 0; i < HeightMap.Count; i++)
            {
                for (int j = HeightMap[i]; j >= 0; j--)
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
            if (index >= Length)
            {
                Exists.Add(new CustomArray<bool>(_tempExists));
                return;
            }

            for (int i = 0; i <= 1; i++)
            {
                if (i == 0)
                {
                    _tempExists[index] = false;
                }
                else _tempExists[index] = true;
                Plunk(index + 1);
            }

        }
        bool IsNotNullTile(Vector3Int pos, Vector3Int shift)
        {
            return Tilemap.GetTile(pos + shift) == null;
        }
        //public void Render()
        //{
        //    bool[] isOtherTiles = new bool [8];

        //    Vector3Int tilePos;
        //    bool isLeft, isRight, isTop, isBottom; 
        //    for (int i = 0; i < HeightMap.Count; i++)
        //     {
        //        for (int j = HeightMap[i]; j >= 0; j--)
        //        {
        //            tilePos = new Vector3Int(i, j, 0);
        //            isLeft = IsNotNullTile(tilePos, Vector3Int.left);
        //            isRight = IsNotNullTile(tilePos, Vector3Int.right);
        //            isTop = IsNotNullTile(tilePos, Vector3Int.up);
        //            isBottom = IsNotNullTile(tilePos, Vector3Int.down);
        //            if (isLeft)
        //            {
        //                if(isRight)
        //                {
        //                    if(isTop)
        //                    {
        //                        if(isBottom)
        //                        {

        //                        }
        //                        else
        //                        {

        //                        }
        //                    }
        //                    else if(isBottom)
        //                    {

        //                    }
        //                    else
        //                    {

        //                    }
        //                }
        //                else if(isTop)
        //                {
        //                    if (isBottom)
        //                    {

        //                    }
        //                    else if (isBottom)
        //                    {

        //                    }
        //                    else
        //                    {

        //                    }
        //                }
        //                else if(isBottom)
        //                {

        //                }
        //                Tilemap.SetTile(tilePos, Tiles.Left);
        //            }
        //            else if(isLeft)
        //            {
        //                Tilemap.SetTile(tilePos, Tiles.Middle);
        //            }
        //            else if(isTop)
        //            {

        //            }
        //            else if(isBottom)
        //            {

        //            }
        //        }
        //     }


        //    bool IsNotNullTile (Vector3Int pos, Vector3Int shift)
        //    {
        //        return Tilemap.GetTile(pos + shift) == null;
        //    }

        //}
    }
}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/* 
 * 描述：生成地牢
 * 作者：sine5RAD
 */
public class DungonMapGen : MonoBehaviour
{
    private Tilemap _groundTileMap;
    private Tilemap _buildingTileMap;
    public int seed;
    public bool useRandomSeed;

    [Range(0, 1f)]
    public float groundProbability;  //生成地板的频率

    [Range(0, 0.1f)]
    public float lancunarity;        //缩放
    public TileBase groundTile;      //地板的瓷砖
    public TileBase wallTile;        //墙面的瓷砖
    public int width, height;//地图大小

    private bool[,] _groundData;//True: 地板 Falue: 空
    private int[, ] _buildingData;
    private enum GroundTileType
    {
        None,
        Ground
    }

    private enum BuildingType
    {
        Wall
    }
    // Start is called before the first frame update
    void Start()
    {
        _groundTileMap = transform.Find("DungonGround").GetComponent<Tilemap>();
        _buildingTileMap = transform.Find("DungonBuilding").GetComponent<Tilemap>();
    }

    public void GenerateMap()
    {
        GenerateMapData();
        GenerateTileMap();
    }

    private void GenerateMapData()
    {
        if(useRandomSeed)
        {
            seed = Time.time.GetHashCode();
        }
        UnityEngine.Random.InitState(seed);
        float randomOffset = UnityEngine.Random.Range(-10000, 10000);

        _groundData = new bool[width, height];
        _buildingData = new int[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float noiseValue = Mathf.PerlinNoise(i * lancunarity + randomOffset, j * lancunarity + randomOffset);
                if(noiseValue < groundProbability)
                {
                    _groundData[i, j] = true;
                }
                else _groundData[i, j] = false;
            }
        }
    }

    public void GenerateTileMap()
    {
        CleanTileMap();
        for (int i = 0; i < width; i++)
        {
            for(int j = 0;j < height; j++)
            {
                TileBase tile = _groundData[i, j] ? groundTile : null;
                _groundTileMap.SetTile(new Vector3Int(i, j), tile);
            }
        }
    }

    public void CleanTileMap()
    {
        _groundTileMap.ClearAllTiles();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

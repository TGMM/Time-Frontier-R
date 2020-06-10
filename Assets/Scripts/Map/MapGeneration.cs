using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class MapGeneration : MonoBehaviour
{
    private Tilemap _map;
    private Vector3Int _mapSize = new Vector3Int(12,8, 0);

    private TileTypesManager _tileTypes;

    private void Start()
    {
        _map = transform.GetChild(0).GetComponent<Tilemap>();
        Debug.Log(_map.name);

        _tileTypes = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<TileTypesManager>();

        GenerateMap();

        //_map.SetTile(new Vector3Int(0,4,0), tiles.BasicRoad);
        //_map.SetTile(new Vector3Int(0, -3, 0), tiles.BasicRoad);
    }

    private void ClearMap()
    {
        _map.ClearAllTiles();
    }

    private void GenerateMap()
    {
        ClearMap();
        PlaceRandomTileOnEveryColumn();
    }

    private void PlaceRandomTileOnEveryColumn()
    {
        var placedTiles = new List<Vector3Int>();

        var startingX = 0-(_mapSize.x / 2);

        var minY = -(_mapSize.y / 2);
        var maxY = (_mapSize.y / 2);

        var minimumDifference = 2;

        var lastPosition = 0;

        bool skipCurrent = true;

        for (var currentTile = 0; currentTile < _mapSize.x; currentTile++)
        {
            skipCurrent = !skipCurrent;
            if (skipCurrent) continue;

            var skip = Random.Range(0,6);
            if(skip == 0) continue;

            var posY = Random.Range(minY, maxY);

            while (Mathf.Abs(posY - lastPosition) < minimumDifference)
            {
                posY = Random.Range(minY, maxY+1);
            }

            lastPosition = posY;

            var newPos = new Vector3Int(startingX + currentTile, posY, 0);

            _map.SetTile(newPos, _tileTypes.basicRoad);
            placedTiles.Add(newPos);
        }

        ConnectTiles(placedTiles);
    }

    private void ConnectTiles(List<Vector3Int> placedTiles)
    {

    }
}

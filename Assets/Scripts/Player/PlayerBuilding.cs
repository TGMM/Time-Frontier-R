using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerBuilding : MonoBehaviour
{
    private Tilemap _baseMap;
    private Tilemap _buildingPreviewMap;
    private Tilemap _buildingMap;

    private Camera _mainCamera;

    private TileTypesManager _tileTypes;

    private bool _building;

    private readonly Color _redPreview = new Color(1, 0.2f, 0.2f);
    private Dictionary<string, Tile> _previewTiles;

    private void Start()
    {
        _tileTypes = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<TileTypesManager>();

        var grid = GameObject.Find("Grid");
        _baseMap = grid.transform.GetChild(0).GetComponent<Tilemap>();
        _buildingPreviewMap = grid.transform.GetChild(1).GetComponent<Tilemap>();
        _buildingMap = grid.transform.GetChild(2).GetComponent<Tilemap>();

        _mainCamera = Camera.main;

        _previewTiles = new Dictionary<string, Tile>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _buildingPreviewMap.ClearAllTiles();
            _building = !_building;
        }

        if (!_building) return;

        Vector3 mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int position = _baseMap.WorldToCell(mousePos);

        _buildingPreviewMap.ClearAllTiles();

        PreviewTileToPlace(position);
        CheckForPlaceTile(position);
    }

    private void PreviewTileToPlace(Vector3Int position)
    {
        if (!_previewTiles.ContainsKey(_tileTypes.basicRoad.name))
        {
            SetUpPreviewTiles(_tileTypes.basicRoad);
        }

        _buildingPreviewMap.SetTile(position, _previewTiles[_tileTypes.basicRoad.name]);
    }

    private void CheckForPlaceTile(Vector3Int position)
    {
        if (Input.GetMouseButton(0))
        {
            _buildingMap.SetTile(position, _tileTypes.basicRoad);
        }
        else if (Input.GetMouseButton(1))
        {
            _buildingMap.SetTile(position, ScriptableObject.CreateInstance<Tile>());
        }
    }

    private void SetUpPreviewTiles(Tile tileToAdd)
    {
        Tile previewTile = Instantiate(_tileTypes.basicRoad);
        previewTile.color = _redPreview;

        _previewTiles.Add(tileToAdd.name, previewTile);
    }
}

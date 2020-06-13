using Map;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Player
{
    public class PlayerBuilding : MonoBehaviour
    {
        private Tilemap _baseMap;
        private Tilemap _buildingPreviewMap;
        private Tilemap _buildingMap;

        private Camera _mainCamera;

        private TileTypesManager _tileTypes;

        private bool _building;

        private void Start()
        {
            _tileTypes = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<TileTypesManager>();

            var grid = GameObject.Find("Grid");
            _baseMap = grid.transform.GetChild(0).GetComponent<Tilemap>();
            _buildingPreviewMap = grid.transform.GetChild(1).GetComponent<Tilemap>();
            _buildingMap = grid.transform.GetChild(2).GetComponent<Tilemap>();

            _mainCamera = Camera.main;
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
            _buildingPreviewMap.SetTile(position, _tileTypes.grass);
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
    }
}

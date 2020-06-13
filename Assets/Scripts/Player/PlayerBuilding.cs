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

        [SerializeField] private GameObject cannon;

        private Camera _mainCamera;

        private TileTypesManager _tileTypes;
        private PlayerValues _playerValues;

        private bool _building;

        private Color _previewGreen = Color.green;
        private Color _previewRed = Color.red;

        private void Start()
        {
            _previewGreen.a = 0.5f;
            _previewRed.a = 0.5f;

            _tileTypes = FindObjectOfType<TileTypesManager>();
            _playerValues = FindObjectOfType<PlayerValues>();

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
            if (_playerValues.Coins >= 50 && CanPlace(position))
            {
                _buildingPreviewMap.color = _previewGreen;
            }
            else
            {
                _buildingPreviewMap.color = _previewRed;
            }
            _buildingPreviewMap.SetTile(position, _tileTypes.preview);
        }

        private bool CanPlace(Vector3Int position)
        {
            var worldPos = _baseMap.CellToWorld(position);
            var entities = Physics2D.OverlapBoxAll(worldPos + new Vector3(0.5f, 0.5f), 
                new Vector2(0.5f,0.5f), 0f);

            return _baseMap.GetTile(position).name == _tileTypes.grass.name && !(entities.Length >= 1);
        }

        private void CheckForPlaceTile(Vector3Int position)
        {
            if (Input.GetMouseButtonDown(0) && CanPlace(position))
            {
                if (_playerValues.Coins >= 50)
                {
                    Instantiate(cannon, position + new Vector3(0.5f, 0.5f), Quaternion.identity);
                    _playerValues.ChangeCoins(-50);
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                _buildingMap.SetTile(position, null);
            }
        }
    }
}

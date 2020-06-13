using System.Collections;
using System.Collections.Generic;
using Map;
using Player;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        private Tilemap _map;
        private TileTypesManager _tileTypes;
        private string _allowedTileName;

        private List<Vector3Int> _visitedTiles;

        public Vector3 offset = Vector3.zero;
        public float speed = 1f;

        private void Start()
        {
            _tileTypes = FindObjectOfType<TileTypesManager>();
            _allowedTileName = _tileTypes.basicRoad.name;
            _map = FindObjectOfType<Grid>().transform.GetChild(0).GetComponent<Tilemap>();
            _visitedTiles = new List<Vector3Int>();

            StartCoroutine(nameof(GoToNextTile));
        }

        private IEnumerator GoToNextTile()
        {
            var next = FindNextTile();
            if (next == Vector3Int.one)
            {
                FindObjectOfType<PlayerValues>().ChangeHp(-10);
                Destroy(gameObject);
                yield break;
            }

            var time = 1 / speed;
            var elapsedTime = 0f;
            
            var startingPos = transform.position;
            var newPosition = _map.CellToWorld(next) + offset;

            while (elapsedTime < time)
            {
                transform.position = Vector3.Lerp(startingPos, newPosition, (elapsedTime / time));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            StartCoroutine(nameof(GoToNextTile));
        }

        private Vector3Int FindNextTile()
        {
            var currentCell = _map.WorldToCell(transform.position);

            var cellUp = currentCell + Vector3Int.up;
            var tileUp = _map.GetTile(cellUp);
            if (tileUp != null && !_visitedTiles.Contains(cellUp) && tileUp.name == _allowedTileName)
            {
                _visitedTiles.Add(cellUp);
                transform.rotation = Quaternion.Euler(Vector3.zero);
                return cellUp;
            }

            var cellRight = currentCell + Vector3Int.right;
            var tileRight = _map.GetTile(cellRight);
            if (tileRight != null && !_visitedTiles.Contains(cellRight) && tileRight.name == _allowedTileName)
            {
                _visitedTiles.Add(cellRight);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                return cellRight;
            }

            var cellDown = currentCell + Vector3Int.down;
            var tileDown = _map.GetTile(cellDown);
            if (tileDown != null && !_visitedTiles.Contains(cellDown) && tileDown.name == _allowedTileName)
            {
                _visitedTiles.Add(cellDown);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, -180));
                return cellDown;
            }

            return Vector3Int.one;
        }
    }
}

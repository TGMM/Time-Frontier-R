using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map
{
    [Serializable]
    public class TileTypesManager : MonoBehaviour
    {
        public TileBase basicRoad;
        public TileBase grass;
        public TileBase brick;
        public TileBase endStone;
    }
}

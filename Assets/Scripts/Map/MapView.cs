using System.Collections.Generic;
using UnityEngine;

namespace Map{
    public class MapView : MonoBehaviour 
    {
        public enum MapOrientation {
            BottomToTop,
            TopToBottom,
            RightToLeft,
            LeftToRight
        }

        public MapManager mapManager;
        public MapOrientation orientation;

        [Tooltip("맵 구축을 위해 사용될 수 있는 모든 MapConfig Scriptable Object들. " +
                 "슬더스의 Act와 비슷하다. (전반적인 레이아웃, 보스의 종류 등을 정의)")]
        public List<MapConfig> allMapConfigs;
        public GameObject nodePrefab;
        [Tooltip("Offset of the start/end nodes of the map from the edges of the screen")]
        public float orientationOffset;

        [Header("Background Settings")]
        [Tooltip("If the background sprite is null, background will not be shown")]
        public Sprite background;
        public Color32 backgroundColor = Color.white;
        public float xSize;
        public float yOffset;

        [Header("Line Settings")]
        public GameObject linePrefab;
        [Tooltip("Line point count should be > 2 to get smooth color gradients")]
        [Range(3,10)]
        public int linePointsCount = 10;
        [Tooltip("distance from the node till the line starting point")]
        public float offsetFromNodes = 0.5f;
        [Header("Colors")]
        [Tooltip("Node Visited or Attainable Color")]
        public Color32 visitedColor = Color.white;
        [Tooltip("Locked node color")]
        public Color32 lockedColor = Color.grey;
        [Tooltip("Visited or available path color")]
        public Color32 lineVisitedColor = Color.white;
        [Tooltip("Unavailable path color")]
        public Color32 lineLockedColor = Color.gray;

        protected GameObject firstParent;
        protected GameObject mapParent;
        private List<List<Point>> paths;
        private Camera cam;
        //All nodes:
        public readonly List<MapNode> MapNodes = new List<MapNode>();        
    }
}
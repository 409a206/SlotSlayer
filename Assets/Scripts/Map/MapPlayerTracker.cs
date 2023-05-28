using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map{
    public class MapPlayerTracker : MonoBehaviour
    {
        public bool lockAfterSelecting = false;
        public float enterNodeDelay = 1f;
        public MapManager mapManager;
        public MapView view;

        public static MapPlayerTracker Instance;

        public bool Locked{get;set;}

        private void Awake() {
            Instance = this;
        }
        
        public void SelectNode(MapNode mapNode) {
            if(Locked) return;
            
        }

    }
}

using System.Collections.Generic;
using Malee;
using OneLine;
using UnityEngine;

namespace Map
    {
        [CreateAssetMenu]
        public class MapConfig
        {
            public List<NodeBlueprint> nodeBlueprints;
            public int GridWidth => Mathf.Max(numOfPreBossNodes.max, numOfStartingNodes.max);

            public IntMinMax numOfPreBossNodes;
            public IntMinMax numOfStartingNodes;

            [Tooltip("Path 수를 늘리고 싶다면 이 수치를 늘리면 됩니다.")]
            public int extraPaths;
            
            public ListOfMapLayers layers;

            [System.Serializable]
            public class ListOfMapLayers : ReorderableArray<MapLayer> {}
        }   
    }
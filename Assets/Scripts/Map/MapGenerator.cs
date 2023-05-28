using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Map
{
    public static class MapGenerator
    {
        private static MapConfig config;

        private static readonly List<NodeType> RandomNodes = new List<NodeType> {
            NodeType.Mystery, NodeType.Store, NodeType.Treasure, NodeType.MinorEnemy, NodeType.RestSite
        };

        private static List<float> layerDistances;
        private static List<List<Point>> paths;
        // ALL nodes by layer:
        private static readonly List<List<Node>> nodes = new List<List<Node>>();

        private static Map GetMap(MapConfig conf)
        {
            if(conf == null) {
                Debug.LogWarning("Config was null in MapGenerator.Generate()");
                return null;
            }

            config = conf;
            nodes.Clear();

            GenerateLayerDistances();

            for (var i = 0; i < conf.layers.Count; i++) PlaceLayer(i);
            
            GeneratePaths();
            RandomizeNodePositions();
            SetUpConnections();
            RemoveCrossConnections();

            //select all the nodes with connections:
            //var nodesList = nodes.SelectMany(n => n).Where<

            return null;
        }

        private static void RemoveCrossConnections()
        {
            throw new NotImplementedException();
        }

        private static void SetUpConnections()
        {
            throw new NotImplementedException();
        }

        private static void RandomizeNodePositions()
        {
            throw new NotImplementedException();
        }

        private static void GeneratePaths()
        {
            throw new NotImplementedException();
        }

        private static void PlaceLayer(int i)
        {
            throw new NotImplementedException();
        }

        private static void GenerateLayerDistances()
        {
            throw new NotImplementedException();
        }
    }
}
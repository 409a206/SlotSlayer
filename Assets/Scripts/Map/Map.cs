using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Map
{
    public class Map : MonoBehaviour
    {
        public List<Node> nodes;
        public List<Point> path;
        public string bossNodeName;
        public string configName; //act name in slay the spire

        public Map(string configName, string bossNodeName, List<Node> nodes, List<Point> path) {
            this.configName = configName;
            this.bossNodeName = bossNodeName;
            this.nodes = nodes;
            this.path = path;
        }

        public Node GetBossNode() {
            return nodes.FirstOrDefault(n => n.nodeType == NodeType.Boss);
        }

        public float DistanceBetweenFirstAndLastLayers() {
            var bossNode = GetBossNode();
            var firstLayerNode = nodes.FirstOrDefault(n => n.point.y == 0);

            if(bossNode == null || firstLayerNode == null) return 0f;

            return bossNode.position.y - firstLayerNode.position.y;
       }

        public Node GetNode(Point point) {
            return nodes.FirstOrDefault(n => n.point.Equals(point));
        }

        public string ToJson() {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings{
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
    }
}

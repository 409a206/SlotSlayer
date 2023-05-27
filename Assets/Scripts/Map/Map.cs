using System.Collections;
using System.Collections.Generic;
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
    }
}

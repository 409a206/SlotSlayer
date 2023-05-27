using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    public enum NodeStates {
        Locked,
        Visited,
        Attainable
    }

    public class MapNode : MonoBehaviour
    {
        public SpriteRenderer _spriteRenderer;
        public Image _image;
        public SpriteRenderer visitedCircle;
        public Image circleImage;
        public Image visitedCircleImage;

        public Node node{get;private set;}
        public NodeBlueprint blueprint{get;private set;}
        
    }
}
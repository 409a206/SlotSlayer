using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotItem : MonoBehaviour
{
    [SerializeField]
    private string name;
    [SerializeField]
    private string code;

    
    private Vector2 _position;

    void Start()
    {
        _position = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

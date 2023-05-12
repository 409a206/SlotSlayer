using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotItem : MonoBehaviour
{
    [SerializeField]
    private string slotItemName;

    public string SlotItemName {
        get {return slotItemName;}
        set {slotItemName = value;}
    }
    [SerializeField]
    private string code;
    
    [SerializeField]
    AudioClip stopSound;
    
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Scriptable Object로 추후 변경 필요!!
public class SlotItem : MonoBehaviour
{
    [SerializeField]
    private string slotItemName;

    private bool isSelected = false;
    public bool IsSelected {
        get {return isSelected;}
        set {isSelected = value;}
    }

    public string SlotItemName {
        get {return slotItemName;}
        set {slotItemName = value;}
    }

    [SerializeField]
    private string code;
    
    [SerializeField]
    AudioClip stopSound;

    protected SlotManager slotManager;
    
    public virtual void Activate() {}
    
    void Start()
    {
       slotManager = FindObjectOfType<SlotManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

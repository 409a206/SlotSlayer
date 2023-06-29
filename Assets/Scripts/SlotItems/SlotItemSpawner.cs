using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotItemType{}

public class SlotItemSpawner : MonoBehaviour
{
    [SerializeField]
    private List<SlotItemType> slotItemTypes;
    [SerializeField]
    private GameObject slotItemPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

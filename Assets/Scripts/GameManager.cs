using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SlotManager slotManager;
    public BattleManager battleManager;
    public SoundManager soundManager;
    public ActionOnTimer actionOnTimer;

    // Start is called before the first frame update
    void Start()
    {
        slotManager = FindObjectOfType<SlotManager>();
        battleManager = FindObjectOfType<BattleManager>();
        soundManager = FindObjectOfType<SoundManager>();
        actionOnTimer = GetComponent<ActionOnTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

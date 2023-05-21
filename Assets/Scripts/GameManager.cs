using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SlotManager slotManager;
    public BattleManager battleManager;
    public SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        slotManager = FindObjectOfType<SlotManager>();
        battleManager = FindObjectOfType<BattleManager>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

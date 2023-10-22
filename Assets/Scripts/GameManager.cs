using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SlotManager slotManager;
    public BattleManager battleManager;
    public SoundManager soundManager;
    public ActionOnTimer actionOnTimer;
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        slotManager = FindObjectOfType<SlotManager>();
        battleManager = FindObjectOfType<BattleManager>();
        soundManager = FindObjectOfType<SoundManager>();
        actionOnTimer = GetComponent<ActionOnTimer>();
    }
}

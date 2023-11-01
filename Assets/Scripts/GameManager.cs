using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public SlotManager slotManager;
    public BattleManager battleManager;
    public SoundManager soundManager;
    public ActionOnTimer actionOnTimer;
    public Camera mainCamera;

    // Start is called before the first frame update
    void Awake()
    {
        mainCamera = FindObjectOfType<Camera>();
        slotManager = FindObjectOfType<SlotManager>();
        battleManager = FindObjectOfType<BattleManager>();
        soundManager = FindObjectOfType<SoundManager>();
        actionOnTimer = GetComponent<ActionOnTimer>();
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reel : MonoBehaviour
{   
    //hold number of rotation steps
    private int randomValue;
    //used to slow the movement of rows
    private float timeInterval;
    [SerializeField]
    private GameObject CorrespondingSlot;
    [SerializeField]
    private float slotInterval = 0.75f;
    private float lastSlotYPos;
    private float firstSlotYPos;

    public bool reelStopped;
    public string stoppedReel;
    // Start is called before the first frame update
    void Start()
    {
        reelStopped = true;
        firstSlotYPos = CorrespondingSlot.transform.position.y;
        //the number needs to be changed!!
        lastSlotYPos = CorrespondingSlot.transform.position.y - slotInterval * 3;
        GameControl.HandlePulled += StartRotating;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    private void StartRotating()
    {
        stoppedReel = "";
        StartCoroutine("Rotate");
    }

    private IEnumerator Rotate() {

        reelStopped = false;
        timeInterval = 0.025f;

        for (int i = 0; i < 30; i++)
        {
            if(transform.position.y <= lastSlotYPos) {
                transform.position = new Vector2(transform.position.x, firstSlotYPos);
            }

            transform.position = new Vector2(transform.position.x, transform.position.y - slotInterval);
            yield return new WaitForSeconds(timeInterval);
        }

        randomValue = UnityEngine.Random.Range(60, 100);

        //correcting random value
        //this is because we have 3 steps between each item in a row
        switch (randomValue % 3)
        {
            case 1 :
                randomValue += 2;
                break;
            case 2 : 
                randomValue += 1;
                break; 
           
        }

        for (int i = 0; i < randomValue; i++)
        {
            if(transform.position.y <= lastSlotYPos) {
                transform.position = new Vector2(transform.position.x, firstSlotYPos);
            }

            transform.position = new Vector2(transform.position.x, transform.position.y - slotInterval);

            if(i > Mathf.RoundToInt(randomValue * slotInterval)) timeInterval = 0.05f;
            if(i > Mathf.RoundToInt(randomValue * slotInterval * 2)) timeInterval = 0.1f;
            if(i > Mathf.RoundToInt(randomValue * slotInterval * 3)) timeInterval = 0.15f;
            if(i > Mathf.RoundToInt(randomValue * slotInterval * 4)) timeInterval = 0.2f;

            yield return new WaitForSeconds(timeInterval);
        }

        //customization needed
        if(transform.position.y == lastSlotYPos) {
            Debug.Log("Diamond");
            stoppedReel = "diamond";
        } else if(transform.position.y == 1.75f) {
            stoppedReel = "wefwef";
        }

        reelStopped = true;

    }

    private void OnDestroy() {
        GameControl.HandlePulled -= StartRotating;
    }
}

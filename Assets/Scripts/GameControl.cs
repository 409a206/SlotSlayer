using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public static event Action HandlePulled = delegate {};

    // [SerializeField]
    // private Text prizeText;
    [SerializeField]
    private Row[] rows;

    // [SerializeField]
    // private Transform handle;
    private int prizeValue;

    //this variable is so that it does not check the results multiple times when row stops spinning
    private bool resultsChecked = false;

    // Update is called once per frame
    void Update()
    {
        CheckRows();   
    }

    void CheckRows(){
        //모든 row들이 멈출때까지 기다리기
        if(!rows[0].rowStopped || !rows[1].rowStopped || !rows[2].rowStopped) {
            //prizeValue = 0;
            //prizeText.enabled = false;
            resultsChecked = false;
        }

        //모든 row들이 멈췄고 아직 result가 체크되지 않았다면
        if(rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && !resultsChecked) {
            CheckResults();
            // prizeText.enabled = true;
            // prizeText.text = "Prize: " + prizeValue;
        }
    }

    //also works with finger touches
    //mobile touch recommended for more complicated uses
    private void OnMouseDown() {
        Debug.Log("OnMouseDown Called");
        if(rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped) {
            HandlePulled();
        }
    }

    //obsolete
    // private IEnumerator PullHandle() {

    //     for(int i = 0; i < 15; i += 5) {
    //         handle.Rotate(0f,0f,i);
    //         yield return new WaitForSeconds(0.1f);
    //     }

    //     HandlePulled();

    //      for(int i = 0; i < 15; i += 5) {
    //         handle.Rotate(0f,0f,-i);
    //         yield return new WaitForSeconds(0.1f);
    //     }
    // }

    private void CheckResults()
    {
        //바꿔야함
        if(rows[0].stoppedSlot == "Diamond"
           && rows[1].stoppedSlot == "Diamond"
           && rows[2].stoppedSlot == "Diamond") {

            prizeValue = 200;

        } else if(rows[0].stoppedSlot == "Crown"
                  && rows[1].stoppedSlot == "Crown"
                  && rows[2].stoppedSlot == "Crown") {

                  prizeValue = 400;

        } else if(rows[0].stoppedSlot == "Melon"
                  && rows[1].stoppedSlot == "Melon"
                  && rows[2].stoppedSlot == "Melon") {

                  prizeValue = 600;

        } else if(rows[0].stoppedSlot == "Bar"
                  && rows[1].stoppedSlot == "Bar"
                  && rows[2].stoppedSlot == "Bar") {

                  prizeValue = 800;

        } else if(rows[0].stoppedSlot == "Seven"
                  && rows[1].stoppedSlot == "Seven"
                  && rows[2].stoppedSlot == "Seven") {

                  prizeValue = 1500;

        } else if(rows[0].stoppedSlot == "Cherry"
                  && rows[1].stoppedSlot == "Cherry"
                  && rows[2].stoppedSlot == "Cherry") {

                  prizeValue = 3000;

        } else if(rows[0].stoppedSlot == "Lemon"
                  && rows[1].stoppedSlot == "Lemon"
                  && rows[2].stoppedSlot == "Lemon") {

                  prizeValue = 5000;

        }

        resultsChecked = true;
    }
}

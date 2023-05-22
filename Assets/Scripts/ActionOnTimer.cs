using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//일정 시간마다 Action을 callback하게 해주는 클래스
public class ActionOnTimer : MonoBehaviour
{
    private Action timerCallBack;
    private float timer;

    public void SetTimer(float timer, Action timerCallBack) {
        this.timer = timer;
        this.timerCallBack = timerCallBack;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0f) {
            timer -= Time.deltaTime;

            if(IsTimerComplete()) {
                timerCallBack();
            }

        }
    }

    private bool IsTimerComplete()
    {
        return timer <= 0f;
    }
}

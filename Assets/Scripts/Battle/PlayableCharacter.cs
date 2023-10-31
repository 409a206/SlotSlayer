using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayableCharacter : Unit
{

    private bool isControllable = false;
    public bool IsControllable 
    {
        get 
        {
            return isControllable;
        }
        set 
        {
            isControllable = value;
        }
    }

    GameObject reticleGo;
    GameObject attackTarget;
    
    void Attack(Enemy target) {

    }

    private void OnMouseDown() {
        Debug.Log("OnMouseDown Called");
        if(battleManager.currentBattleState == BattleState.SPINPAUSE && isControllable) {
            reticleGo = Instantiate(Resources.Load<GameObject>("Prefabs/Dummy/Reticle"));
        }
    }

    private void OnMouseDrag() {
        if(reticleGo != null) {
            Vector3 mouseWorldPosition = battleManager.gameManager.mainCamera.ScreenToWorldPoint(Input.mousePosition);
            reticleGo.transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0f);
        }
    }

    private void OnMouseUp() {
        if(reticleGo != null) {
            Debug.Log("OnMouseUp Called");

            attackTarget = reticleGo.GetComponent<Reticle>().target;

            if(attackTarget != null) {
                battleManager.Attack();
                isControllable = false;
            }

            Destroy(reticleGo);
        }
    }
}

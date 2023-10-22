using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    BattleManager battleManager;

    public GameObject target;

    private void Awake() {
        battleManager = FindObjectOfType<BattleManager>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
            if(other.tag == "Enemy") {
                Debug.Log("reticle has entered enemy collider");
                this.GetComponent<Animator>().SetBool("IsTargetValid", true);
                target = other.gameObject;
            }
        
    }

    private void OnTriggerExit2D(Collider2D other) {
            if(other.tag == "Enemy") {
                Debug.Log("reticle has exited enemy collider");
                this.GetComponent<Animator>().SetBool("IsTargetValid", false);
                target = null;
            }
}
}
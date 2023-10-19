using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayableCharacter : Unit
{

    private bool isControllable = false;
    void Attack(Enemy target) {

    }

    private void OnMouseDown() {
        Debug.Log("OnMouseDown Called");
        GameObject reticleGo = Resources.Load<GameObject>("Prefabs/Dummy/Reticle");
        reticleGo.transform.position = Vector3.zero;
    }

    private void OnMouseDrag() {
        Debug.Log("OnMouseDrag Called");
    }

    private void OnMouseUp() {
        Debug.Log("OnMouseUp Called");
    }
}

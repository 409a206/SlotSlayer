using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayableCharacter : Unit, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    void Attack(Enemy target) {

    }
     public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("PlayableCharacter.OnDrag() called");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject reticleGo = Resources.Load<GameObject>("Prefabs/Dummy/Reticle");
        Debug.Log("PlayableCharacter.OnBeginDrag() called");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("PlayableCharacter.OnEndDrag() called");
    }
}

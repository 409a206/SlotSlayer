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

    //슬롯간의 간격
    [SerializeField]
    private float slotInterval = 0.75f;
    //가장 위의 슬롯 위치
    private float lastSlotLocalPosY;
    //가장 아래의 슬롯 위치
    private float fistSlotLocalPosY = 0;

    public bool reelStopped;
    public string stoppedReel;

    [SerializeField]
    private GameControl gameControl;

    [SerializeField]
    private List<SlotItem> slotItems;

    // Start is called before the first frame update
    void Start()
    {
        gameControl = GameObject.FindObjectOfType<GameControl>();

        //fortest
        // RegisterSlotItem(Resources.Load<SlotItem>("Prefabs/Dummy/Heal"));
        // RegisterSlotItem(Resources.Load<SlotItem>("Prefabs/Dummy/Attack"));

        reelStopped = true;
        lastSlotLocalPosY = CorrespondingSlot.transform.position.y - slotInterval * (gameControl.reels.Length - 1);
        Debug.Log("firstSlotYPos: " + fistSlotLocalPosY);
        Debug.Log("lastSlotYPos: " + lastSlotLocalPosY);
        GameControl.OnSpinButtonClicked += StartRotating;
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
            if(transform.position.y <= lastSlotLocalPosY) {
                transform.position = new Vector2(transform.position.x, fistSlotLocalPosY);
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
            if(transform.position.y <= lastSlotLocalPosY) {
                transform.position = new Vector2(transform.position.x, fistSlotLocalPosY);
            }

            transform.position = new Vector2(transform.position.x, transform.position.y - slotInterval);

            if(i > Mathf.RoundToInt(randomValue * slotInterval)) timeInterval = 0.05f;
            if(i > Mathf.RoundToInt(randomValue * slotInterval * 2)) timeInterval = 0.1f;
            if(i > Mathf.RoundToInt(randomValue * slotInterval * 3)) timeInterval = 0.15f;
            if(i > Mathf.RoundToInt(randomValue * slotInterval * 4)) timeInterval = 0.2f;

            yield return new WaitForSeconds(timeInterval);
        }

        //customization needed
        if(transform.position.y == lastSlotLocalPosY) {
            //Debug.Log("Diamond");
            stoppedReel = "diamond";
        } else if(transform.position.y == 1.75f) {
            stoppedReel = "wefwef";
        }

        reelStopped = true;

    }

    //slotItem 등록
    private void RegisterSlotItem(SlotItem slotItem) {
        
        int slotItemCount = CountSlotItems();

        Vector3 SlotItemRegisterPosition = new Vector3(0, slotItemCount * slotInterval, 0);

        slotItems.Add(slotItem);

        SlotItem instantiatedSlotItem = Instantiate(slotItem, new Vector3(), Quaternion.identity);
        instantiatedSlotItem.transform.parent = this.gameObject.transform;
        instantiatedSlotItem.transform.localPosition = SlotItemRegisterPosition;

    }

    //Reel에 등록되어있는 slotItem 갯수 카운트 
    private int CountSlotItems() {

        SlotItem[] slotItems = this.gameObject.GetComponentsInChildren<SlotItem>();

        //Debug.Log(this.gameObject.name + " has " + slotItems.Length + " slot items");

        return slotItems.Length;
    }

    private void OnDestroy() {
        GameControl.OnSpinButtonClicked -= StartRotating;
    }
}

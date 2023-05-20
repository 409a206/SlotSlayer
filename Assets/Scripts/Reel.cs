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
    //corresponding empty slot
    [SerializeField]
    private GameObject CorrespondingSlot;

    private float originPosY;

    //슬롯간의 간격
    [SerializeField]
    private float slotInterval = 0.75f;
    //가장 위의 슬롯 로컬 위치
    private float lastSlotLocalPosY;
    //가장 아래의 슬롯 로컬 위치
    private float fistSlotLocalPosY = 0;
    //현재 정지되어있는 슬롯의 로컬 위치
    private float StoppedSlotLocalPosY;

    // //가장 위의 슬롯 글로벌 위치
    // private float lastSlotGlobalPosY;
    // //가장 아래의 슬롯 글로벌 위치
    // private float fistSlotGlobalPosY = 0;

    public bool reelStopped;
    public string stoppedRow;

    [SerializeField]
    private GameControl gameControl;

    [SerializeField]
    private List<SlotItem> slotItems;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.position = CorrespondingSlot.transform.position;
        originPosY = this.transform.position.y;

        gameControl = GameObject.FindObjectOfType<GameControl>();


        reelStopped = true;
        lastSlotLocalPosY = slotInterval * (slotItems.Count - 1);
        // fistSlotGlobalPosY = CorrespondingSlot.transform.position.y;
        // lastSlotGlobalPosY = fistSlotGlobalPosY - lastSlotLocalPosY;

        //fortest
        RegisterSlotItem(Resources.Load<SlotItem>("Prefabs/Dummy/Heal"));
        RegisterSlotItem(Resources.Load<SlotItem>("Prefabs/Dummy/Attack"));

        Debug.Log("fistSlotLocalPosY: " + fistSlotLocalPosY);
        Debug.Log("lastSlotLocalPosY: " + lastSlotLocalPosY);
        // Debug.Log("fistSlotGlobalPosY: " + fistSlotGlobalPosY);
        // Debug.Log("lastSlotGlobalPosY: " + lastSlotGlobalPosY);

        GameControl.OnSpinButtonClicked += StartRotating;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    private void StartRotating()
    {
     stoppedRow = "";
     StartCoroutine("Rotate");
    }

    private IEnumerator Rotate() {

        reelStopped = false;
        timeInterval = 0.025f;

        //for test
        //timeInterval = 1.0f;

        for (int i = 0; i < 30; i++)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - slotInterval);

            if(transform.position.y <= CorrespondingSlot.transform.position.y - lastSlotLocalPosY) {
                transform.position = new Vector2(transform.position.x, CorrespondingSlot.transform.position.y);
            }


            yield return new WaitForSeconds(timeInterval);
        }

        //elaboration needed
        #region randomizeResults
        
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
            transform.position = new Vector2(transform.position.x, transform.position.y - slotInterval);


            if(i > Mathf.RoundToInt(randomValue * slotInterval)) timeInterval = 0.05f;
            if(i > Mathf.RoundToInt(randomValue * slotInterval * 2)) timeInterval = 0.1f;
            if(i > Mathf.RoundToInt(randomValue * slotInterval * 3)) timeInterval = 0.15f;
            if(i > Mathf.RoundToInt(randomValue * slotInterval * 4)) timeInterval = 0.2f;

            if(transform.position.y <= CorrespondingSlot.transform.position.y - lastSlotLocalPosY) {
                transform.position = new Vector2(transform.position.x, CorrespondingSlot.transform.position.y);
            }

            yield return new WaitForSeconds(timeInterval);
        }
        
        #endregion

        CalculateStoppedRow();
        
        reelStopped = true;

    }

    //정지한 행 구하기
    private void CalculateStoppedRow() {

        StoppedSlotLocalPosY = originPosY - this.transform.position.y;

        //Debug.Log(this.name + "StoppedSlotLocalPosY: " + StoppedSlotLocalPosY);

        //Debug.Log(this.name + "slotItems.Count: " + slotItems.Count);

        for (int i = 0; i < slotItems.Count; i++)
        {
            //Debug.Log(this.name + " slotItems[" + i + "].transform.localPosition.y: " + slotItems[i].transform.localPosition.y);
            if(slotItems[i].transform.localPosition.y.ToString() == StoppedSlotLocalPosY.ToString()) {
                stoppedRow = slotItems[i].SlotItemName;
                break;
            }
        }

        Debug.Log(this.name + " stoppedRow: " + stoppedRow);

    }

    //slotItem 등록
    private void RegisterSlotItem(SlotItem slotItem) {
        
        int slotItemCount = CountSlotItems();

        Vector3 SlotItemRegisterPosition = new Vector3(0, fistSlotLocalPosY + slotItemCount * slotInterval, 0);

        //slotItems.Add(slotItem);

        SlotItem instantiatedSlotItem = Instantiate(slotItem, new Vector3(), Quaternion.identity);
        instantiatedSlotItem.transform.parent = this.gameObject.transform;
        instantiatedSlotItem.transform.localPosition = SlotItemRegisterPosition;

        slotItems.Add(instantiatedSlotItem);

        Debug.Log(this.name + " registered slot local pos y: " + slotItems[CountSlotItems() - 1].transform.localPosition.y);

        UpdateSlotData();

    }

    //Reel에 등록되어있는 slotItem 갯수 카운트 
    private int CountSlotItems() {

        SlotItem[] slotItems = this.gameObject.GetComponentsInChildren<SlotItem>();

        //Debug.Log(this.gameObject.name + " has " + slotItems.Length + " slot items");

        return slotItems.Length;
    }

    private void UpdateSlotData() {
        lastSlotLocalPosY = fistSlotLocalPosY + slotInterval * (slotItems.Count - 1);
        // Debug.Log("slotItems.Count: " + slotItems.Count);
        //Debug.Log("lastSlotLocalPosY: " + lastSlotLocalPosY);
    }

    private void OnDestroy() {
        GameControl.OnSpinButtonClicked -= StartRotating;
    }
}

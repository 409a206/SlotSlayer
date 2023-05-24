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

    //슬롯 아이템의 간격과 위치에 관한 변수
    //슬롯 아이템의 초기 글로벌 좌표
    private float originPosY;
    //슬롯간의 간격
    [SerializeField]
    private float slotInterval = 0.75f;
    //가장 위의 슬롯 로컬 위치
    private float lastSlotItemLocalPosY;
    //가장 아래의 슬롯 로컬 위치
    private float firstSlotItemLocalPosY = 0;
    //현재 정지되어있는 슬롯의 로컬 위치
    private float StoppedSlotLocalPosY;

    //스핀시 슬롯 아이템 Lerp를 위한 변수들
    //lerp할 때 걸리는 시간
    [SerializeField]
    float lerpTime = 1f;
    //lerp 시작 후 몇초 경과했는지
    float elapsedTime = 0f;

    //randomization 전 스핀할 슬롯칸 횟수
    [SerializeField]
    int spinCount = 10; 

    public bool reelStopped;
    public string stoppedRow;

    //레퍼런스 변수들
    private SlotManager slotManager;
    //corresponding empty slot
    [SerializeField]
    private GameObject CorrespondingSlot;
    //릴에 할당되어있는 슬롯 아이템의 리스트
    [SerializeField]
    private List<SlotItem> slotItems;

    void Start()
    {
        this.gameObject.transform.position = CorrespondingSlot.transform.position;
        originPosY = this.transform.position.y;

        slotManager = GameObject.FindObjectOfType<SlotManager>();


        reelStopped = true;
        lastSlotItemLocalPosY = slotInterval * (slotItems.Count - 1);
        // fistSlotGlobalPosY = CorrespondingSlot.transform.position.y;
        // lastSlotGlobalPosY = fistSlotGlobalPosY - lastSlotItemLocalPosY;

        //fortest
        RegisterSlotItem(Resources.Load<SlotItem>("Prefabs/Dummy/Heal"));
        RegisterSlotItem(Resources.Load<SlotItem>("Prefabs/Dummy/Attack"));

        // Debug.Log("firstSlotItemLocalPosY: " + firstSlotItemLocalPosY);
        // Debug.Log("lastSlotItemLocalPosY: " + lastSlotItemLocalPosY);
        // Debug.Log("fistSlotGlobalPosY: " + fistSlotGlobalPosY);
        // Debug.Log("lastSlotGlobalPosY: " + lastSlotGlobalPosY);

        SlotManager.OnSpinButtonClicked += StartRotating;
    }

    private void StartRotating()
    {
     stoppedRow = "";
     StartCoroutine("Rotate");
    }

    private IEnumerator Rotate() {

        reelStopped = false;

        elapsedTime = 0f;
        //timeInterval = 0.025f;

        Vector2 startPosition; 
        Vector2 endPosition;

        for(int i = 0; i < spinCount; i++) {
            
            startPosition = this.transform.position;
            endPosition = new Vector2(transform.position.x, this.transform.position.y - slotInterval);

            elapsedTime = 0;

            while(elapsedTime < lerpTime) {
                elapsedTime += Time.deltaTime;
                if(elapsedTime >= lerpTime) elapsedTime = lerpTime;

                transform.position = Vector2.Lerp(startPosition, endPosition, elapsedTime / lerpTime);

                yield return null;
            }

            if(transform.position.y <= CorrespondingSlot.transform.position.y - (lastSlotItemLocalPosY - slotInterval)) {
                    transform.position = new Vector2(transform.position.x, CorrespondingSlot.transform.position.y);
            }

        }

        //elaboration needed
        #region randomizeResults
        Debug.Log("#region randomizeResults");


        

        // randomValue = UnityEngine.Random.Range(60, 100);

        // //correcting random value
        // //this is because we have 3 steps between each item in a row
        // switch (randomValue % 3)
        // {
        //     case 1 :
        //         randomValue += 2;
        //         break;
        //     case 2 : 
        //         randomValue += 1;
        //         break; 
           
        // }

        // for (int i = 0; i < randomValue; i++)
        // {
        //     transform.position = new Vector2(transform.position.x, transform.position.y - slotInterval);


        //     if(i > Mathf.RoundToInt(randomValue * slotInterval)) timeInterval = 0.05f;
        //     if(i > Mathf.RoundToInt(randomValue * slotInterval * 2)) timeInterval = 0.1f;
        //     if(i > Mathf.RoundToInt(randomValue * slotInterval * 3)) timeInterval = 0.15f;
        //     if(i > Mathf.RoundToInt(randomValue * slotInterval * 4)) timeInterval = 0.2f;

        //     if(transform.position.y <= CorrespondingSlot.transform.position.y - lastSlotItemLocalPosY) {
        //         transform.position = new Vector2(transform.position.x, CorrespondingSlot.transform.position.y);
        //     }

        //     yield return new WaitForSeconds(timeInterval);
        // }
        
        
        #endregion
        
        CalculateStoppedRow();

        reelStopped = true;

    }

    //정지한 행(SlotItem) 구하기
    private SlotItem CalculateStoppedRow() {

        StoppedSlotLocalPosY = originPosY - this.transform.position.y;
        
        SlotItem stoppedSlotItem = new SlotItem();

        //Debug.Log(this.name + "StoppedSlotLocalPosY: " + StoppedSlotLocalPosY);

        //Debug.Log(this.name + "slotItems.Count: " + slotItems.Count);

        for (int i = 0; i < slotItems.Count; i++)
        {
            //Debug.Log(this.name + " slotItems[" + i + "].transform.localPosition.y: " + slotItems[i].transform.localPosition.y);
            if(slotItems[i].transform.localPosition.y.ToString() == StoppedSlotLocalPosY.ToString()) {
                stoppedSlotItem = slotItems[i];
                break;
            }
        }
        
        stoppedRow = stoppedSlotItem?.SlotItemName ?? "Null";
        
        //Debug.Log(this.name + " stoppedRow: " + stoppedRow);
        
        //Registering to delegate 'OnSpinStopped' functions to activate
        SlotManager.OnSpinStopped += stoppedSlotItem.Activate;

        return stoppedSlotItem;
    }

    //slotItem 등록
    private void RegisterSlotItem(SlotItem slotItem) {
        
        int slotItemCount = CountSlotItems();

        Vector3 SlotItemRegisterPosition = new Vector3(0, firstSlotItemLocalPosY + slotItemCount * slotInterval, 0);

        //slotItems.Add(slotItem);

        SlotItem instantiatedSlotItem = Instantiate(slotItem, new Vector3(), Quaternion.identity);
        instantiatedSlotItem.transform.parent = this.gameObject.transform;
        instantiatedSlotItem.transform.localPosition = SlotItemRegisterPosition;

        slotItems.Add(instantiatedSlotItem);

        //Debug.Log(this.name + " registered slot local pos y: " + slotItems[CountSlotItems() - 1].transform.localPosition.y);

        UpdateSlotData();

    }

    //Reel에 등록되어있는 slotItem 갯수 카운트 
    private int CountSlotItems() {

        SlotItem[] slotItems = this.gameObject.GetComponentsInChildren<SlotItem>();

        //Debug.Log(this.gameObject.name + " has " + slotItems.Length + " slot items");

        return slotItems.Length;
    }

    //가장 위의 슬롯 위치 업데이트
    private void UpdateSlotData() {
        lastSlotItemLocalPosY = firstSlotItemLocalPosY + slotInterval * (slotItems.Count - 1);
        // Debug.Log("slotItems.Count: " + slotItems.Count);
        //Debug.Log("lastSlotItemLocalPosY: " + lastSlotItemLocalPosY);
    }

    private void OnDestroy() {
        SlotManager.OnSpinButtonClicked -= StartRotating;
    }
}

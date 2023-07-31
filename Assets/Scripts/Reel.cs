using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reel : MonoBehaviour
{   
    //슬롯 아이템의 간격과 위치에 관한 변수
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

    //spin할 칸 횟수
    [SerializeField]
    int spinCount = 10; 

    public bool reelStopped;
    public string stoppedRow;

    //레퍼런스 변수들
    private SlotManager slotManager;
    //corresponding empty slot
    [SerializeField]
    private GameObject correspondingSlot;
    //릴에 할당되어있는 슬롯 아이템의 리스트
    [SerializeField]
    private List<SlotItem> slotItems;

    void Start()
    {
        this.gameObject.transform.position = correspondingSlot.transform.position;
        slotManager = GameObject.FindObjectOfType<SlotManager>();


        reelStopped = true;
        lastSlotItemLocalPosY = slotInterval * (slotItems.Count - 1);
        
        //테스트용으로 릴에 슬롯 아이템 등록
        SlotItem defend = Resources.Load<SlotItem>("Prefabs/Dummy/SlotItems/Defend");
        defend.slotItemData = Resources.Load<SlotItemData>("Scriptable Objects/Dummy/Defend");
       
        SlotItem heal = Resources.Load<SlotItem>("Prefabs/Dummy/SlotItems/Heal");
        heal.slotItemData = Resources.Load<SlotItemData>("Scriptable Objects/Dummy/Heal");
      
        SlotItem attack = Resources.Load<SlotItem>("Prefabs/Dummy/SlotItems/Attack");
        attack.slotItemData = Resources.Load<SlotItemData>("Scriptable Objects/Dummy/Attack");
        
        slotItems.Add(defend);
        slotItems.Add(heal);
        slotItems.Add(attack);
        // slotItems.Add(Resources.Load<SlotItem>("Prefabs/Dummy/SlotItem"));
        // slotItems.Add(Resources.Load<SlotItem>("Prefabs/Dummy/SlotItem"));
        // slotItems.Add(Resources.Load<SlotItem>("Prefabs/Dummy/SlotItem"));
        //

        InstantiateRandomSlotItems(2);

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

        SlotItem[] instantiatedSlotItems = GetComponentsInChildren<SlotItem>();

        for(int i = 0; i < spinCount; i++) {
            
            elapsedTime = 0;
            
            InstantiateRandomSlotItems(1);

            while(elapsedTime < lerpTime) {
                elapsedTime += Time.deltaTime;
                if(elapsedTime >= lerpTime) elapsedTime = lerpTime;
                
                instantiatedSlotItems = GetComponentsInChildren<SlotItem>();

                for (int j = 0; j < instantiatedSlotItems.Length; j++)
                {
                    instantiatedSlotItems[j].transform.localPosition = 
                        Vector2.Lerp(new Vector2(0,j * slotInterval), new Vector2(0, j * slotInterval - slotInterval), elapsedTime/lerpTime);
                }
                
                yield return null;
            }
            
            GameObject.Destroy(instantiatedSlotItems[0].gameObject); 
            
            yield return null;
        }
        
        yield return null;
        
        //CalculateStoppedRow();

        reelStopped = true;

    }

    //slotItems list에 등록된 무작위의 슬롯 아이템을 생성
    //위로 count개만큼 쌓아올린다!
    private void InstantiateRandomSlotItems(int count) {
        
        //인수로 사용할 임의의 수
        int randomNumber;
        
        Vector3[] SlotItemRegisterPositions = new Vector3[count];

        for (int i = 0; i < SlotItemRegisterPositions.Length; i++)
        {
            randomNumber = UnityEngine.Random.Range(0, slotItems.Count);

            SlotItemRegisterPositions[i] = new Vector3(0, firstSlotItemLocalPosY + (slotInterval * CountSlotItemGosInChildren()), 0);
            
            SlotItem instantiatedSlotItem = Instantiate(slotItems[randomNumber], new Vector3(), Quaternion.identity);
            instantiatedSlotItem.transform.parent = this.gameObject.transform;
            instantiatedSlotItem.transform.localPosition = SlotItemRegisterPositions[i];
            UpdateSlotData();
        }
    }

    //정지한 행(SlotItem) 구하기
    private SlotItem CalculateStoppedRow() {

        SlotItem stoppedSlotItem = GetComponentsInChildren<SlotItem>()[0];

        stoppedRow = stoppedSlotItem?.slotItemData.SlotItemName ?? "Null";
        
        //Registering to delegate 'OnSpinStopped' functions to activate
        SlotManager.OnSpinStopped += stoppedSlotItem.Activate;

        return stoppedSlotItem;
    }

    public List<SlotItem> GetRegisteredSlotItemsFromReel(Reel reel) {
        return reel.slotItems;
    }

    //List에서 슬롯 아이템 삭제
    public void DeleteSlotItemFromList(SlotItem slotItem) {
        slotItems.Remove(slotItem);
    }

    //Reel의 자식 slotItem 게임 오브젝트 갯수 카운트 
    private int CountSlotItemGosInChildren() {

        SlotItem[] slotItems = this.gameObject.GetComponentsInChildren<SlotItem>();

        //Debug.Log(this.gameObject.name + " has " + slotItems.Length + " slot items");

        return slotItems.Length;
    }

    //가장 위의 슬롯 위치 업데이트
    private void UpdateSlotData() {
        lastSlotItemLocalPosY = firstSlotItemLocalPosY + slotInterval * (slotItems.Count - 1);
    }

    private void OnDestroy() {
        SlotManager.OnSpinButtonClicked -= StartRotating;
    }
}

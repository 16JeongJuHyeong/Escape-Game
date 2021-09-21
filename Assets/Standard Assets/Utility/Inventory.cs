using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;
    [SerializeField] private GameObject go_InventoryBase;  //인벤토리 창
    [SerializeField] private GameObject go_SlotsParent; //슬롯들을 부모 객체가 담당하게 해서 아래 배열에 넣기 쉽게 함
    [SerializeField] private ToolTip theToolTip;
    private Slot[] slots; //슬롯들

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>(); //슬롯들을 배열에 한번에 넣음
    }

    void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated;

            if (inventoryActivated)
                OpenInventory();
            else
                CloseInventory();
        }
    }

    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
    }

    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
        theToolTip.HideToolTip();
    }

    public void AcquireItem(Item _item, int _count = 1) // 아이템 습득하기 (ActionColtroll 스크립트에서 수행)
    {
        if (Item.ItemType.Data != _item.itemType) //획득한 아이템 타입이 소모품이라면
        {
            for (int i = 0; i < slots.Length; i++) //슬롯 전체를 돌게 됨
            {
                if (slots[i].item != null) //슬롯 창이 빈 상태가 아니라면
                {
                    if (slots[i].item.itemName == _item.itemName) //빈 상태가 아닌데 획득한 아이템과 지금 들어있는 아이템의 이름이 같다면(같은 아이템이라면)
                    {
                        slots[i].SetSlotCount(_count); //디폴트가 1 -> 한개 주울 때
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null) //슬롯이 비었다면
            {
                slots[i].AddItem(_item, _count); //그 빈 슬롯에 아이템을 넣는다
                return;
            }
        }
    }
}

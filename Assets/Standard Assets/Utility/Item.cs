using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/it em")]
public class Item : ScriptableObject
{ 
    public string itemName; // 아이템 이름
    [TextArea]
    public string itemDesc;
    public ItemType itemType; // 아이템 유형
    public Sprite itemImage; // 인벤토리에 띄울 아이템 이미지
    public GameObject itemPrefab; // 아이템 프리팹

    public enum ItemType
    {
        Data,
        Used,
    }
}

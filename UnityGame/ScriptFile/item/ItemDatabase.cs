using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance; // ItemDatabase 클래스 instance화
    public List<Item> items = new List<Item>(); // Item 들의 내용을 담을 리스트
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Add(100000, "axe", 1, 500, "날이 덜든 도끼", ItemType.Equipment); // 도끼 추가
        Add(100001, "apple", 1, 100, "잘 익은 사과", ItemType.Consumption); // 사과 추가
    }
    void Add(int itemID, string itemName, int itemValue, int itemPrice, string itemDesc, ItemType itemType)
    {
        items.Add(new Item(itemID,itemName, itemValue, itemPrice, itemDesc, itemType, Resources.Load<Sprite>("ItemImages/" + itemName))); // 아이템 정보 불러오기
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum ItemType
{
    Equipment, // 장비
    Consumption, // 소모
    Misc // 기타
}
[Serializable]
public class Item // 오브젝트에 붙지 않아도 되기 때문에 MonoBehaviour 삭제
{
    public int itemID; // 아이템의 ID
    public string itemName; // 아이템의 이름
    public int itemValue; // 가치
    public int itemPrice; // 가격
    public string itemDesc; // 설명
    public ItemType itemType; // 타입
    public Sprite itemImage; // 이미지

    public Item(int _itemID, string _itemName, int _itemValue, int _itemPrice, string _itemDesc, ItemType _itemType, Sprite _itemImage)
    {
        itemID = _itemID;
        itemName = _itemName;
        itemValue = _itemValue;
        itemPrice = _itemPrice;
        itemDesc = _itemDesc;
        itemType = _itemType;
        itemImage = _itemImage;
    }

}

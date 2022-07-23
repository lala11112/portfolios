using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDp : MonoBehaviour
{
    public GameObject[] itemGameObject; // 떨어뜨릴 아이템 배열
    public void ItemDrop()
    {
        int itemRandNum = Random.Range(0,itemGameObject.Length); // item을 랜덤으로 줌
        if(itemGameObject[itemRandNum] != null) // 만약 숫자에 아이템이 있다면
            Instantiate(itemGameObject[itemRandNum], transform.position, transform.rotation);
    }
}
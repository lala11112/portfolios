using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    void Use(GameObject target); // 아이템의 사용
    string Explanation(); // 아이템의 설명
}

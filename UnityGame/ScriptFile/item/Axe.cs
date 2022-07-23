using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour,IItem
{
    public int attackPower = 10;
    public void Use(GameObject target)
    {
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.weaponPower = playerHealth.weaponPower + attackPower; // attackPower 만큼 공격력 Up
        }
        Destroy(gameObject);
    }
    public string Explanation()
    {
        string explanationText;
        explanationText = "공격력 + " + attackPower;

        return explanationText;
    }
}

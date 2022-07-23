using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour, IItem
{
    public float reHealth = 10f;
    
    public void Use(GameObject target)
    {
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
        if(playerHealth != null)
        {
            playerHealth.RestoreHealth(reHealth);
        }
        Destroy(gameObject);
    }
    public string Explanation()
    {
        string explanationText;
        explanationText = "체력 " + reHealth + " 회복";

        return explanationText;
    }
}

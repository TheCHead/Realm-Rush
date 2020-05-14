using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health = 5;

    public void DecreaseHealth(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            PlayerDeathSequence();
        }
    }

    private void PlayerDeathSequence()
    {
        Debug.Log("Player Base defeated");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    [SerializeField] private int Health = 100;
    [SerializeField] private int MaxHealth = 100;



    public void Damage(int value)
    {
        Health -= value;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }

    }

    public void Heal(int value)
    {
        Health += value;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }

    }

}

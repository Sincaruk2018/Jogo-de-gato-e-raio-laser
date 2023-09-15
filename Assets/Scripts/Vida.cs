using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    [SerializeField] public int Health = 100;
    [SerializeField] public int MaxHealth = 100;



    public void Damage(int value)
    {
        Health -= value;
       

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

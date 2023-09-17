using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    [SerializeField] public float Health = 100;
    [SerializeField] public float MaxHealth = 100;



    public void Damage(float value)
    {
        Health -= value;
       

    }

    public void Heal(float value)
    {
        Health += value;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatinhoIA : MonoBehaviour
{
    [SerializeField] private float Speed = 2f;
    [SerializeField] private float attackTimer = 1f;
    [SerializeField] private int damage = 2;
    private GameObject Player;
    void Start()
    {
        Player = GameObject.Find("GatoProtagonista");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, Speed * Time.deltaTime);
        if(attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
        
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.name == "GatoProtagonista" )
        {
         
                collider.GetComponent<Vida>().Damage(damage);
                attackTimer = 1;
                
        }
    }

}

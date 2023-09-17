using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatinhoIA : MonoBehaviour
{
    [SerializeField] private float Speed = 2f;
    [SerializeField] private float knockbackTimer = 0f;
    [SerializeField] private int damage = 2;
    [SerializeField] private GameObject money;
    [SerializeField] private GameObject heart;
    private float TrueSpeed;
    private GameObject Player;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Vector2 direction;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        Player = GameObject.Find("GatoProtagonista");
        sprite = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Vida>().Health <=0)
        {
            if (Random.Range(100, 0) <= 80f)
            {
                Instantiate(money, transform.position, transform.rotation);
            }
            if (Random.Range(100, 0) <= 10f)
            {
                Instantiate(heart, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
        
    }

    void FixedUpdate()
    {
        if(Player.transform.position.x > gameObject.transform.position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }


        if(knockbackTimer > 0)
        {
            knockbackTimer -= Time.fixedDeltaTime;
            TrueSpeed = TrueSpeed - Speed/0.8f * Time.fixedDeltaTime;
        }
        else
        {
            direction = ((Vector2)Player.transform.position - (Vector2)gameObject.transform.position).normalized;
            TrueSpeed = Speed;
        }

        rb.MovePosition((Vector2)gameObject.transform.position + direction*Time.fixedDeltaTime*TrueSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && collider == collider.GetComponent<BoxCollider2D>())
        {

            collider.GetComponent<PlayerManager>().tomarDano(damage);
            knockbackTimer = 0.8f;
            direction = - direction;
                
        }
    }

}

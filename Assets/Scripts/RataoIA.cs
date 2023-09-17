using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RataoIA : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private int damage = 2;
    [SerializeField] private GameObject money;
    [SerializeField] private GameObject heart;
    private float TrueSpeed;
    private GameObject Player;
    private float luck;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Vector2 direction;
    private float dashingTimer = 0f;
    private float predash = 0f;
    private bool dashing = false;
    private Vector3 offset;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        Player = GameObject.Find("GatoProtagonista");
        luck = Player.GetComponent<PlayerManager>().getSOR();
        sprite = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Vida>().Health <= 0)
        {
            if (Random.Range(100, 0) <= 70f + luck)
            {
                offset = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0f);
                Instantiate(money, transform.position + offset, transform.rotation);
                if (Random.Range(100, 0) <= luck / 2)
                {   
                    for(int i = 0; i < 10; i++)
                    {
                        Instantiate(money, transform.position, transform.rotation);
                    }
                    
                }
            }
            if (Random.Range(100, 0) <= 10f + luck)
            {
                offset = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0f);
                Instantiate(heart, transform.position + offset, transform.rotation);
                if (Random.Range(100, 0) <= luck / 2)
                {
                    Instantiate(heart, transform.position, transform.rotation);
                }
            }

            Destroy(gameObject);
        }

    }

    void FixedUpdate()
    {
        if (dashing == false)
        {
            if(Vector3.Distance(transform.position,Player.transform.position) < 3)
            {
                dash(1f);
            }
            if (Player.transform.position.x > gameObject.transform.position.x)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
        }

        if(predash > 0)
        {
            predash -= Time.fixedDeltaTime;
            direction = ((Vector2)Player.transform.position - (Vector2)gameObject.transform.position).normalized;
        }
        else
        {
            if (dashingTimer > 0)
            {
                TrueSpeed = 7 * Speed;
                dashingTimer -= Time.fixedDeltaTime;
            }
            else
            {
                dashing = false;
                TrueSpeed = Speed;
                direction = ((Vector2)Player.transform.position - (Vector2)gameObject.transform.position).normalized;
            }
            rb.MovePosition((Vector2)gameObject.transform.position + direction * Time.fixedDeltaTime * TrueSpeed);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && collider == collider.GetComponent<BoxCollider2D>())
        {
            collider.GetComponent<PlayerManager>().tomarDano(damage);
        }
    }
    
    public void dash(float timer)
    {
        if (dashing == false)
        {
            dashingTimer = timer;
            predash = 0.2f;
            dashing = true;
        }
    }
}

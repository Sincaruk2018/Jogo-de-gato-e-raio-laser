using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtiradorIA : MonoBehaviour
{
    [SerializeField] private GameObject money;
    [SerializeField] private GameObject heart;
    [SerializeField] private GameObject projectile;
    private Vector2 targetDir;
    private float shootTimer;
    private SpriteRenderer sprite;
    private GameObject Player;
    private float luck;
    private float projectileSpeed;

    void Start()
    {
        projectileSpeed = 2f;
        Player = GameObject.Find("GatoProtagonista");
        luck = Player.GetComponent<PlayerManager>().getSOR();
        sprite = this.GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        if (GetComponent<Vida>().Health <= 0)
        {
            if (Random.Range(100, 0) <= 70f+luck)
            {
                Instantiate(money, transform.position, transform.rotation);
                if (Random.Range(100, 0) <= luck/2)
                {
                    Instantiate(money, transform.position, transform.rotation);
                }
            }
            if (Random.Range(100, 0) <= 10f +luck)
            {
                Instantiate(heart, transform.position, transform.rotation);
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
        targetDir = ((Vector2)Player.transform.position - (Vector2)gameObject.transform.position).normalized;

        if (Player.transform.position.x > gameObject.transform.position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
        if(shootTimer > 0)
        {
            shootTimer -= Time.fixedDeltaTime;
        }
        else
        {
            GameObject Shot = Instantiate(projectile, transform.position, transform.rotation);
            Shot.GetComponent<Rigidbody2D>().AddForce(projectileSpeed * targetDir, ForceMode2D.Impulse);
            shootTimer = 2f;
        }
    } 
}

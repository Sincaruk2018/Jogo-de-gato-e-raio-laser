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
    [SerializeField] private float projectileSpeed;
    private Vector3 offset;

    void Start()
    {
        //projectileSpeed = 2f;
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
                offset = new Vector3(Random.Range(-1.2f, 1.2f), Random.Range(-1.2f, 1.2f), 0f);
                Instantiate(money, transform.position + offset, transform.rotation);
                if (Random.Range(100, 0) <= luck/2)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Instantiate(money, transform.position, transform.rotation);
                    }
                }
            }
            if (Random.Range(100, 0) <= 10f +luck)
            {
                offset = new Vector3(Random.Range(-1.2f, 1.2f), Random.Range(-1.2f, 1.2f), 0f);
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
            GameObject Shot = Instantiate(projectile, transform.position, Quaternion.FromToRotation(transform.right, targetDir));
            Shot.GetComponent<Rigidbody2D>().AddForce(projectileSpeed * targetDir, ForceMode2D.Impulse);
            shootTimer = 2f;
        }
    } 
}

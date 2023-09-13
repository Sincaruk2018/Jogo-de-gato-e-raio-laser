using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerShoot : MonoBehaviour
{
    private CircleCollider2D range;
    private List<GameObject> priorityList;
    // private bool newTarget = false; // N sei se vai precisar mesmo

    private Quaternion defaultRotation;

    [Header("Stats de Jogador")]
    private PlayerManager pm;

    [Header("Atirar")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform spawnPointProjectile;
    public float projectileSpeed = 2f;
    public float projectileCooldown = 1f;

    private SpriteRenderer gunSprite;
 
    void Start()
    {
        pm = GetComponentInParent<PlayerManager>();
        gunSprite = GetComponentInChildren<SpriteRenderer>();

        defaultRotation = this.transform.rotation;
        range = GetComponent<CircleCollider2D>();
        priorityList = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Achei algo!");
        if(other.gameObject.tag == "enemie"){
            priorityList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "enemie"){
            priorityList.Remove(other.gameObject);
        }
    }


    private float cooldownCounter = 0f;
    void Update()
    {
        if(priorityList.Count == 0){
            this.transform.rotation = defaultRotation;
            gunSprite.flipY = false;
            return;
        }

        if(priorityList.Count != 0){
            Vector3 target = priorityList[0].GetComponent<Transform>().position;
            Vector3 targetDir = new Vector3(target.x - transform.position.x, target.y - transform.position.y, 0);
            
            // Olhar para o inimigo
            if(targetDir.x < 0 && !gunSprite.flipY){
                gunSprite.flipY = true;
            }
            else if(targetDir.x > 0 && gunSprite.flipY){
                gunSprite.flipY = false;
            }

            Vector3 rotationDir = Quaternion.Euler(0, 0, 90) * targetDir;
            transform.rotation = Quaternion.LookRotation(forward: Vector3.forward, rotationDir);

            // Atirar
            // TODO Passar dano, cooldown
            // TODO TODO Testar métodos pra outros tiros (buckshot, spray, ...)
            if(cooldownCounter <= 0){
                GameObject bullet = Instantiate(projectile, spawnPointProjectile.position, spawnPointProjectile.rotation);
                bullet.GetComponent<Rigidbody2D>().AddForce(projectileSpeed * targetDir, ForceMode2D.Impulse);
                cooldownCounter = pm.getAttackSpeed();
            }
            else cooldownCounter -= Time.deltaTime;
        }
    }
}

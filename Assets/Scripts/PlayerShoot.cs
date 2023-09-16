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

    private Vector3 targetDir;
    private Quaternion defaultRotation;
    private SpriteRenderer gunSprite;

    [Header("Stats de Jogador")]
    private PlayerManager pm;

    [Header("Stats da arma")]
    [SerializeField] private Gun gun;

    [Header("Atirar")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform spawnPointProjectile;

 
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
            targetDir = new Vector3(target.x - transform.position.x, target.y - transform.position.y, 0);
            
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
                gun.bulletsLeft = gun.bulletsPerShot;
                if(gun.isDefault || (!gun.isDefault && gun.ammo > 0)){
                    ShootAt();
                }
                cooldownCounter = pm.getAttackSpeed();
                
                if(!gun.isDefault && gun.ammo <= 0){
                    pm.ResetWeapon();
                }
            }
            else cooldownCounter -= Time.deltaTime;
        }
    }

    private void ShootAt(){
        float x = Random.Range(-gun.spread, gun.spread);
        float y = Random.Range(-gun.spread, gun.spread);
        Vector3 spreading = targetDir + new Vector3(x, y, 0);

        GameObject bullet = Instantiate(projectile, spawnPointProjectile.position, spawnPointProjectile.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(gun.projectileSpeed * spreading, ForceMode2D.Impulse);
        
        gun.bulletsLeft--;
        if(gun.bulletsLeft > 0 && gun.ammo > 0){
            Invoke("ShootAt", gun.fireRate);
        }

        gun.ammo--;
    }
}

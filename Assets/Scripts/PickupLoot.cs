using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PickupLoot : MonoBehaviour
{
    private PlayerManager pm;
    private Rigidbody2D rb;
    public float attractionForce = 2f;

    [Header("Modifier")]
    public float value = 1f;

    private void Start() {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player" && !other.isTrigger){
            if(this.gameObject.tag == "xp"){
                pm.AddXP((int) value);
            }
            else if(this.gameObject.tag == "cura"){
                pm.curarVida(value);
            }
            else if(this.gameObject.tag == "arma"){
                //pm.AddXP();
            }
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            Transform otherPos = other.gameObject.transform;
            Vector3 attractionDir = new Vector3(otherPos.position.x - transform.position.x, otherPos.position.y - transform.position.y, 0);
            rb.AddForce(attractionDir.normalized * attractionForce);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            rb.velocity = Vector2.zero;
        }
    }
}

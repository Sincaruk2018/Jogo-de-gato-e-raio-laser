using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private PlayerManager pm;
    private float dano;

    // Start is called before the first frame update
    private void Awake()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        dano = pm.getDano();

        Destroy(this.gameObject, 5f);
    }

    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){ return; }
        
        Destroy(this.gameObject);
        if(other.gameObject.tag == "enemie"){
            // TODO adicionar logica de dano do inimigo aqui
            // Chamar uma funcao TomarDano(dano) do inimigo
        }
    }
}

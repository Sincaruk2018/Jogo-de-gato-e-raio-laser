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
        Debug.Log(pm);
        
        dano = pm.getDano();
        float dice = Random.Range(0, 100);
        if(dice / 100f <= pm.getCriticalPercentage()){
            Debug.Log("Critico!");
            dano *= 2;
        }

        Destroy(this.gameObject, 5f);
    }

    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "enemie"){
            // TODO adicionar logica de dano do inimigo aqui
            // Chamar uma funcao TomarDano(dano) do inimigo
            other.GetComponent<Vida>().Damage(dano);
            Destroy(this.gameObject);
        }

        if(other.gameObject.tag == "obstacle"){
            Destroy(this.gameObject);
        }
    }
}

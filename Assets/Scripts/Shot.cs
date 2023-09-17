using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField]private float dano;
    private GameObject Player;

    // Start is called before the first frame update
    private void Awake()
    {
        Player = GameObject.Find("GatoProtagonista");
        Destroy(this.gameObject, 10f);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && other == other.GetComponent<BoxCollider2D>())
        {
            other.GetComponent<PlayerManager>().tomarDano(dano);
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "obstacle")
        {
            Destroy(this.gameObject);
        }
    }
}

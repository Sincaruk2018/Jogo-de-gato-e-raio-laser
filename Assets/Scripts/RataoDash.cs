using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RataoDash : MonoBehaviour
{
    private GameObject Player;

    void Start()
    {
        Player = GameObject.Find("GatoProtagonista");
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && collider == collider.GetComponent<BoxCollider2D>())
        {

            gameObject.GetComponentInParent<RataoIA>(false).dash(1f);

        }
    }
}

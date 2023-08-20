using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerShoot : MonoBehaviour
{
    private CircleCollider2D range;
    private List<GameObject> priorityList;
    private bool newTarget = false;
 
    void Start()
    {
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


    // Update is called once per frame
    void Update()
    {
        if(priorityList.Count != 0){
            Vector3 target = priorityList[0].GetComponent<Transform>().position;
            Vector3 targetDir = new Vector3(target.x - transform.position.x, 0, target.z - transform.position.z);

            transform.rotation = Quaternion.LookRotation(targetDir);
        }
    }
}

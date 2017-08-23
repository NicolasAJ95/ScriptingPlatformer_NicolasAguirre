using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {
    [SerializeField]
    private float damage = 200;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {

            col.SendMessageUpwards("Damage", damage);
        }
        else
            Destroy(col.gameObject);
    }

    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player"))
        {

            col.SendMessageUpwards("Damage", damage);
        }
        else
            Destroy(col.gameObject);
    }
}

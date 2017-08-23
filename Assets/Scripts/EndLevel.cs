using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour {


    GameManager gM;
	// Use this for initialization
	void Start () {
        gM = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {

            gM.WinLevel();

        }
    }
}

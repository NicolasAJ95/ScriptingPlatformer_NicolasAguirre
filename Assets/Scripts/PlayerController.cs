﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody my_RigidBody;

    private GameManager gM;

    public float health = 100;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private bool canJump;
    [SerializeField]
    private bool isMoving;
    [SerializeField]
    private bool facingRight;

    private float prevRot;

    // Use this for initialization
    void Start () {
        my_RigidBody = GetComponent<Rigidbody>();
        health = 100;
        gM = FindObjectOfType<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        Mathf.Clamp(health, 0.0f, 100.0f);
        Shader.SetGlobalFloat("_HealthValue", this.health);

        if (health <= 0)
            Die();

        var horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;


        Flip(horizontal);
    }

    void FixedUpdate()
    {
        var vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime ;
        var horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        if (vertical != 0 || horizontal != 0)
            isMoving = true;
        else
            isMoving = false;


        MovePlayer(horizontal, vertical);

        if(Input .GetKeyDown (KeyCode .Space) && canJump == true)
        {
            Jump();
        }

    }

    private void Flip(float horizontal)
    {
        Vector3 localTransform = transform.localScale;
        Quaternion localRotation = transform.rotation;

        if(horizontal > 0)
        {
            facingRight = true;
            prevRot = 180;
        }
        if (horizontal < 0)
        {
            facingRight = false;
            prevRot = 0;
        }
        if (horizontal == 0)
        {
            localRotation = Quaternion.Euler(0, prevRot, 0);
        }

        if (facingRight == true)
        {
            localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            localRotation = Quaternion.Euler(0, 180, 0);
        }


        transform.rotation = localRotation;
    }

    private void MovePlayer(float horizontal, float vertical)
    {
        Vector3 forces = Vector3.zero;
        if(facingRight == true)
            forces += transform.right * horizontal;
        if (facingRight == false)
            forces += -transform.right * horizontal;
        forces.y = 0.0f;

        my_RigidBody.position += new Vector3(forces.x, forces.y, 0);

    }

    private void Jump()
    {
        Vector3 force = Vector3.zero;
        force += transform.up * jumpForce;
        my_RigidBody.AddForce(force, ForceMode.Impulse);
        canJump = false;
    }

    public void Damage(int damage)
    {
        if (damage >= health)
            health = 0.0f;
        if (damage < health)
            health -= damage;
    }

    private void Die()
    {
        Debug.Log("He muerto");
        gM.ReloadLevel();
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject .CompareTag("Floor"))
        {
            Debug.Log("grounded");
            canJump = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutant : MonoBehaviour {

    private GameManager gM;

    [SerializeField]
    private float health = 100;
    [SerializeField]
    private Vector3 range = new Vector3 (5,5,5);
    private Rigidbody rb;
    public float speed = 300f;

    private GameObject  target;

    public bool attacking;
    private bool facingRight;

    private float attackTimer = 0.0f;

    public float attackCooldown = 0.4f;
    public Collider meleeTrigger;
    public float implodeTime = 0.7f;

    public GameObject bloodParticle;
    public AudioClip explosionSound;
    private ParticleSystem blood;
    // Use this for initialization
    void Start () {
        meleeTrigger.enabled = false;
        attacking = false;
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player");
        gM = FindObjectOfType<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
        {
            gM.MutantKilled();
            Die();
        }

        Vector3 dir = transform.position - target.GetComponent<Transform>().position;
        transform.position += new Vector3(-dir.normalized .x, 0, 0) * speed * Time.fixedDeltaTime;
        if (dir.x < range.x)
            Implode();

        //Flip conditions
        if (dir.x < 0)
            facingRight = true;
        else
            facingRight = false;

        Flip();
    }

    void Flip()
    {
        Vector3 localTransform = transform.localScale;

        if (facingRight == true)
        {
            localTransform.z = -2f;
        }
        else
        {
            localTransform.z = 2f;
        }

        transform.localScale = localTransform;

    }

    void Implode()
    {
        Debug.Log("allah uh akbar");
        meleeTrigger.enabled = true;
        InvokeRepeating("Die", implodeTime, 1);
    }

    public void Damage(float damage)
    {

        health -= damage;
    }

    void Die()
    {
        Instantiate(bloodParticle, transform.position + new Vector3(0,2,0), Quaternion.Euler(-90, 0, 0));
        blood = bloodParticle.GetComponent<ParticleSystem>();
        blood.Play();
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);

        Destroy(gameObject);      
    }

    void FixedUpdate()
    {
        
    }
}

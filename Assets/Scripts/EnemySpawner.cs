using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    private float life = 100;
    [SerializeField]
    private float spawnTime = 1.5f;
    [SerializeField]
    private float spawnRate = 0.5f;
    
    public bool playerIsNear;
    public GameObject enemyPrefab;

    public GameObject explosionParticle;
    public AudioClip explosionSound;
    public AudioClip monsterSound;

    // Use this for initialization
    void Start () {
        playerIsNear = false;
        InvokeRepeating("SpawnEnemy", spawnTime, spawnRate * Time.deltaTime);
    }
	
	// Update is called once per frame
	void Update () {
        if (life <= 0)
            Die();
	}

    private void SpawnEnemy()
    {
        if (playerIsNear == true)
        {
            AudioSource.PlayClipAtPoint(monsterSound, transform.position, 5);
            GameObject enemy = Instantiate ( enemyPrefab, transform .position, Quaternion.Euler (0, -90,0)) as GameObject ;
            Rigidbody  enemyRigidBody = enemy.GetComponent<Rigidbody>();
            
        }
    }

    void Die()
    {
        
        GameObject exp = Instantiate(explosionParticle, transform.position + new Vector3(0, 2, 0), transform .rotation );
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        //explosion = exp.GetComponent<ParticleSystem>();
        Destroy(gameObject);
    }

    public void Damage(float damage)
    {
        life -= damage;
    }

}

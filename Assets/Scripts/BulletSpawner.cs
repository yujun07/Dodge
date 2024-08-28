using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float spawnRateMin = 0.5f;
    public float spawnRateMax = 3f;
    public int initialPoolSize = 10;

    public Transform target;
    private float spawnRate;
    private float timeAfterSpawn;

    public List<GameObject> pool;


    public GameManager gameManager;

    void Awake()
    {
        pool = new List<GameObject>();

        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(bulletPrefab,transform.position,transform.rotation);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        timeAfterSpawn = 0f;

        spawnRate = Random.Range(spawnRateMin, spawnRateMax);

        target = FindObjectOfType<PlayerController>().transform;
    }
    void SpawnBullet()
    {
        if (!gameManager.isGameover && pool.Count > 0)
        {
            if (timeAfterSpawn >= spawnRate)
            {
                timeAfterSpawn = 0f;

                GameObject bullet = pool[0];
                pool.Remove(bullet);
                bullet.SetActive(true);

                

                spawnRate = Random.Range(spawnRateMin, spawnRateMax);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        timeAfterSpawn += Time.deltaTime;
        GameObject bullet = pool[0];
        bullet.transform.LookAt(target);
        SpawnBullet();
        
    }
}

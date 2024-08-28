using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    private Rigidbody bulletRigidbody;

    BulletSpawner spawner;
    // Start is called before the first frame update
    void OnEnable()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        spawner = FindObjectOfType<BulletSpawner>();

        bulletRigidbody.velocity = transform.forward * speed;

        StartCoroutine(DestroyBullet());
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(3f);

        gameObject.SetActive(false);
        spawner.pool.Add(gameObject);
        yield return null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            if(playerController != null )
            {
                playerController.Die();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

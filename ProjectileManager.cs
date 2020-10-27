using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public int id;
    public GameObject explosionPrefab;
    public LayerMask isExplotable;

    public void Initialize(int _id)
    {
        FindObjectOfType<AudioManager>().Play("Shoot");
        id = _id;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & isExplotable) != 0)
        {
            print("Explosi[");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(((1 << collision.collider.gameObject.layer) & isExplotable) != 0)
        {
            print("Explosi[");
        }
    }

    public void Explode(Vector3 _position)
    {
        transform.position = _position;
        
        GameObject particles = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        
        FindObjectOfType<AudioManager>().Play("Explosion");

        print("Wea");

        if (!GameManager.projectiles.ContainsKey(id)) return;

        GameManager.projectiles.Remove(id);
        Destroy(particles, 2f);
        Destroy(gameObject);
    }
}

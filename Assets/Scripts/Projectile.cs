using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base projectile class.
/// Projectile checks it's own collisions and will trigger enemies to take damage.
/// As of right now, I think enemies will have their own base projectile class...
/// </summary>
public class Projectile : MonoBehaviour
{
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected float projectilePower = 5.0f;
    [SerializeField] protected float maxlifespan = 1.0f;
    protected Vector3 spawn_point;

    protected virtual void Awake()
    {

        spawn_point = gameObject.transform.position;
    }

    protected virtual void Start() {

        StartCoroutine(DestroySelf(maxlifespan));
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            collision.gameObject.GetComponent<EnemyBase>().takeDamage(projectilePower);
            Destroy(gameObject);
        }
    }
    protected IEnumerator DestroySelf(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }


}

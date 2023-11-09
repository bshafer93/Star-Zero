using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderGuitarProjectile : Projectile
{
    [SerializeField]
    public float explosivePower;

    [SerializeField]
    public float explosiveRadius;
        


    // Start is called before the first frame update
    override protected void Start()
    {
        projectilePower = 10.0f;
        explosivePower = 3000.0f;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Thunder guitar collisions also check to see if what they are colliding against are moveable.
    /// If they are, then it will add force to object.
    /// </summary>
    /// <param name="col"></param>
    protected override void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.layer == 3)
        {
            col.gameObject.GetComponent<EnemyBase>().takeDamage(projectilePower);
        }

        if (col.gameObject.layer == 14) {
            Vector3 hit_vector = col.contacts[0].point - spawn_point;
            col.gameObject.GetComponent<Rigidbody>().AddForce(hit_vector*explosivePower, ForceMode.Impulse);
        }

        Destroy(gameObject);
    }




}

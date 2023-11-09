using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepProjectile : Projectile
{
    // Start is called before the first frame update
    override protected void Start()
    {
        projectilePower = 50.0f;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.layer == 6)
        {
            col.gameObject.GetComponent<PlayerInfo>().takeDamage(projectilePower);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}

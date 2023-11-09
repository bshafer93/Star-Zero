using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ElectricGuitarProjectile : Projectile
{
   
    // Start is called before the first frame update
     override protected void Start()
    {
        projectilePower = 10.0f;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }


    protected override void OnCollisionEnter(Collision col)
    {

        if(col.gameObject.layer == 3)
        {
            col.gameObject.GetComponent<EnemyBase>().takeDamage(projectilePower);
        }
        Destroy(gameObject);
    }




}

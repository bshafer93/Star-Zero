using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardProjectile : Projectile
{
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(DestroySelf(2));
    }

    // Update is called once per frame
    void Update()
    {

    }
}

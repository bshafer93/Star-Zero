using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Base class for all Instruments we will use. 
/// Will keep track of projectile spawnrate, damage, ...
/// </summary>
public class Instrument : MonoBehaviour
{
    public GameObject projectileNormal;
    public GameObject projectileSpecial;
    public Transform projectileSpawn;
    [SerializeField] Transform projectileParent;
    [SerializeField] float projectileSpeed = 10.0f;
    public const int maxActiveProjectiles=10;
    [SerializeField] [Range(1.0f, 10.0f)] float projectilesPerSecond = 3.00f;
    protected float timeSinceLastFired;
    protected int activeProjectileCount =0;

   public virtual void Start()
    {

    }

    // Update is called once per frame
   public virtual void Update()
    {

    }

    /// <summary>
    /// A inheritable and overrideable(i dont think it's spelled like that) function for a basic projectile spawning. 
    /// </summary>
    public virtual void Fire(){
        activeProjectileCount++;

        GameObject gb = Instantiate<GameObject>(projectileNormal,projectileSpawn.position,projectileSpawn.rotation,projectileParent);
        
        gb.GetComponent<Rigidbody>().AddForce(projectileSpawn.forward * projectileSpeed);

    }

}

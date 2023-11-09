using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBase : EnemyBase
{
    /*
    // Start is called before the first frame update
    [SerializeField]
    Animator animController;

    public bool trackingPlayer = false;

    [SerializeField] GameObject robotHead;

    public int projecilesPerSecond = 1;
    [SerializeField] Transform projectileSpawn;
    [SerializeField] Transform projectileParent;
    [SerializeField] GameObject projectilePrefab;
    public float projectileSpeed = 10.0f;
    bool isFiring = false;

    [SerializeField]
    HealthBarScript health_bar_script;




    [SerializeField]
    public GameObject destination;

    [SerializeField]
    NavMeshAgent nav_agent;

    void Start()
    {
        nav_agent.destination = destination.transform.position;
        animController.SetBool("Walk", true);
    }

    // Update is called once per frame
    void Update()
    {
        trackPlayer();
        if (trackingPlayer && !isFiring)
        {
            StartCoroutine(shootProjectile());
        }
        if (Vector3.Distance (nav_agent.destination, destination.transform.position) > 1.0f) {
            nav_agent.destination = destination.transform.position;
            animController.SetBool("Walk", true);
        }

        if (Vector3.Distance(nav_agent.destination, destination.transform.position) > 3.0f) {

            
        }

    }

    public override void takeDamage(float damageAmount)
    {
        base.takeDamage(damageAmount);
        Debug.Log("Hit" + base.Stats.health);
        health_bar_script.setByPercent(base.Stats.health / base.Stats.baseHealth);
    }


    void trackPlayer()
    {
        if (trackingPlayer)
        {
            gameObject.transform.LookAt(PlayerInfo.player_position);
        }
    }
    public override void killed(bool isDead)
    {
        if (isDead == false)
        {
            return;
        }
        animController.SetBool("Dead", true);
        // Since we know turretbot will only have 2 weapons, no need to iterate with a loop

        Destroy(gameObject, 1.25f);

    }

    public void AttackAttack()
    {
        Debug.Log("I ATTACK!");
    }

    public IEnumerator shootProjectile()
    {
        isFiring = true;
        float pps = 1 / projecilesPerSecond;
        animController.SetTrigger("Melee Attack");
        yield return new WaitForSeconds(0.55f);
        GameObject p = Instantiate<GameObject>(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation, projectileParent);

        p.GetComponent<Rigidbody>().AddForce(projectileSpawn.forward * projectileSpeed);
        yield return new WaitForSeconds(1);
        isFiring = false;
    }
    */
}

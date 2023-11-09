using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepBot : EnemyBase
{

    public bool noMove = false;

    [SerializeField]
    GameObject[] weaponGlow;

    public int projecilesPerSecond = 1;
    [SerializeField] Transform projectileSpawn;
    [SerializeField] Transform projectileParent;
    [SerializeField] GameObject projectilePrefab;
    public float projectileSpeed = 10.0f;
    bool isFiring = false;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (noMove == true) {
            animController.SetBool("Walk", false);
            RobotAI.MoveTo(gameObject.transform, agent);
        }

    }
    public override void killed(bool isDead)
    {
        base.killed(isDead);
        if (isDead == false)
        {
            return;
        }
        weaponGlow[0].SetActive(false);
        weaponGlow[1].SetActive(false);
        
        Destroy(gameObject, 1.25f);

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

    public override void chasePlayer()
    {
        base.chasePlayer();

    }


    public override void Attack() {

        base.Attack();

        RotateTowards(PlayerInfo.player_position);
        if (!isFiring) {
            StartCoroutine(shootProjectile());
        }


            
    }
}

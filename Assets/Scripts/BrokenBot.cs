using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBot : EnemyBase
{

    // Start is called before the first frame update
    [SerializeField]
    GameObject[] weaponGlow;

    public override void Awake()
    {


    }
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
       
    }

    public override void killed(bool isDead)
    {
        if (isDead == false)
        {
            return;
        }
        animController.SetBool("Dead", true);
        for (int i = 0; i < weaponGlow.Length; i++)
        {
            weaponGlow[i].SetActive(false);
        }

        Destroy(gameObject, 1.25f);

    }


    
}
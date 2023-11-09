using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Base class for information on enemy. Similar to player info. 
/// TODO - Lots of stuff, but will need to impelment the robot prefabs.  
/// </summary>
public class EnemyBase : MonoBehaviour
{
    public AREA_NAME assignedRoom; 
    public enum RobotStates {IDLE, CHASE, ATTACK, SEARCH, RETURN };

    [SerializeField] Stats stats = new Stats(25, 3.00f);
    Color baseColor;
    [SerializeField] Material flashMaterial;
    [SerializeField] Texture flashTexture;
    [SerializeField] Texture baseTexture;
    Material[] baseMaterials;
    [SerializeField] 
    public Renderer renderer;
    int flashMaterialIndex;

    public Action[] behaviorList;


    public RobotStates currentState;

    //=========AI_STUFF==============
    public Transform startLocation;

    public NavMeshAgent agent;

    public Animator animController;

    public bool trackingPlayer = false;

    public float attackRange = 10.0f;

    public float maxDetectionRange = 20.0f;

    public HealthBarScript health_bar_script;

    public float playerLastSeen = 100;
    public float maxSearchTime = 4.0f;
    //===============================

    internal Stats Stats { get => stats; set => stats = value; }

    // Start is called before the first frame update
   public virtual void Start()
    {
        currentState = RobotStates.IDLE;
        baseColor = renderer.material.color;
        animController.Play("Idle");
        behaviorList = new Action[5];
        behaviorList[(int)RobotStates.IDLE] = Idle;
        behaviorList[(int)RobotStates.CHASE] = Chase;
        behaviorList[(int)RobotStates.ATTACK] = Attack;
        behaviorList[(int)RobotStates.SEARCH] = Search;
        behaviorList[(int)RobotStates.RETURN] = ReturnHome;

    }

    public virtual void Awake() {


        startLocation = gameObject.transform;
        agent = GetComponent<NavMeshAgent>();


    }

    public virtual void Update()
    {
        if (Stats.health <= 0)
        {
            killed(true);
            trackingPlayer = false;
        }
        else {

            behaviorList[(int)currentState].Invoke();
        }

       

    }

    public virtual void takeDamage(float damageAmount) {
        StartCoroutine(flashColor(Color.red));
        killed(!stats.checkHealth(-damageAmount));
        health_bar_script.setByPercent(Stats.health / Stats.baseHealth);
    }

    public virtual void killed(bool isDead)
    {
        if (isDead == false) {
            return;
        }

        animController.SetBool("Dead", true);
        agent.destination = gameObject.transform.position;

    }


    /// <summary>
    /// flashes white on enemy mesh. 
    /// If enemy does not have textured material, then it defaults to a color.
    /// </summary>
    /// <param name="c">Color to flash</param>
    /// <returns></returns>
    IEnumerator flashColor(Color c) {

        if (baseTexture == null)
        {
            renderer.material.color = c;
            yield return new WaitForSeconds(0.05f);
            renderer.material.color = baseColor;
        }
        else {
            renderer.material.mainTexture = flashTexture;
            yield return new WaitForSeconds(0.1f);
            renderer.material.mainTexture = baseTexture;
        }


    }

    public virtual void basicAttack() { 
    
    
    
    
    }

    //==================AI Methods===============================================================================
    public virtual void chasePlayer()
    {
        animController.SetBool("Walk", true);
        RobotAI.MoveTo(PlayerInfo.player_position, agent);   

    }

    public virtual void returnHome()
    {
       animController.SetBool("Walk", true);
       RobotAI.MoveTo(startLocation, agent);
               
    }
    //===========================================================================================================

    public virtual void RotateTowards(Transform target) {

        gameObject.transform.LookAt(target);
    }

    public virtual bool PlayerInBasicAttackRange() {
        return (RobotAI.getDistance(gameObject.transform.position, PlayerInfo.player_position.position) <= attackRange);
    }

    public virtual bool isPlayerDetected() {
        bool detected = RobotAI.detection(gameObject.transform, maxDetectionRange);
        if (detected) {
            playerLastSeen = Time.time;
        }
        return detected;
    }


    public virtual void Idle() {

        animController.SetBool("Walk", false);
        if (isPlayerDetected() && PlayerInfo.player_area == assignedRoom) {
            Debug.Log("CHASE");
            currentState = RobotStates.CHASE;
        
        }
    
    }

    public virtual void Chase()
    {
        
        chasePlayer();

        if (PlayerInBasicAttackRange() && PlayerInfo.player_area == assignedRoom)
        {
            Debug.Log("ATTACK");
            RobotAI.MoveTo(gameObject.transform, agent);
            currentState = RobotStates.ATTACK;
        }

        if (!isPlayerDetected())
        {
            Debug.Log("SEARCH");
            currentState = RobotStates.SEARCH;
        }
    }

    public virtual void Attack()
    {
        animController.SetBool("Walk", false);
        if (!PlayerInBasicAttackRange()) {
            Debug.Log("CHASE");
            currentState = RobotStates.CHASE;
        }

        if (!isPlayerDetected())
        {
            Debug.Log("SEARCH");
            currentState = RobotStates.SEARCH;
        }



    }

    public virtual void Search()
    {
       
        animController.SetBool("Walk", false);
        RobotAI.MoveTo(gameObject.transform, agent);

        playerLastSeen = Time.time - playerLastSeen;

        if (playerLastSeen >= maxSearchTime) {
            Debug.Log("RETURNHOME");
            currentState = RobotStates.RETURN;
        }

        if (isPlayerDetected() && PlayerInfo.player_area == assignedRoom) {
            Debug.Log("CHASE");
            currentState = RobotStates.CHASE;

        }

    }

    public virtual void ReturnHome()
    {
  
        if (Vector3.Distance(gameObject.transform.position, startLocation.position) > 1)
        {
                    returnHome();
        }
        else
        {
            Debug.Log("IDLE");
            currentState = RobotStates.IDLE;

        }
    }


}

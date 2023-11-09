using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



/// <summary>
/// Ideally, player info handles player stats and functions that affect those. 
/// Will also handle player inventory logic, such as instruments, equipping, and keeping track consumables, such as access keys. 
/// </summary>
public class PlayerInfo : MonoBehaviour
{
    [SerializeField] public PlayerInventory playerInventory;
    [SerializeField] Stats stats = new Stats(100.0f, 8.0f);
    [SerializeField] Renderer renderer;

    [SerializeField]
    public Room_Manager[] room_managers;
    public float dodgeStrength = 650.0f;
    public Transform instrumentContainer;
    [SerializeField] static public Transform player_position ;
    [SerializeField] static public AREA_NAME player_area = AREA_NAME.RM1;
    Color baseColor;
    public PlayerHealthBar health_bar_script;
    internal Stats Stats { get => stats; set => stats = value; }
    static public bool bossDoorOpen = false;
    void Awake()
    {
        PlayerInfo.player_position = gameObject.transform;
        baseColor = renderer.material.color;
        room_managers = FindObjectsOfType<Room_Manager>(true);
        
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    public virtual void takeDamage(float damageAmount)
    {
        StartCoroutine(flashColor(Color.red));
        killed(!stats.checkHealth(-damageAmount));
        health_bar_script.setByPercent(Stats.health / Stats.baseHealth);
    }

    public virtual void killed(bool isDead)
    {
        if (isDead == false)
        {
            return;
        }
        Debug.Log("You Dead!");

        SceneManager.LoadScene("Demo_Dungeon");

    }



    /// <summary>
    /// Fires the currently equipped guitar.
    /// Does nothing if nothing is equipped.
    /// </summary>
    public void fireInstrument() {
        if (playerInventory.currentInstrumentIndex != -1 ) {
            playerInventory.getCurrentInstrument().Fire();
        }
    }


    /// <summary>
    /// Checks to see if player has accessKey for locked doors.
    /// If player has key, returns true and decrements the players access Keys
    /// </summary>
    static public bool useKey()
    {
        if (PlayerInventory.accessKeys > 0) {
            PlayerInventory.accessKeys--;
            return true;
        }
        Debug.Log("No access Key found!");
        return false;
    }

    public float getSpeed()
    {
        return stats.speed;
    }

    public void stopMove()
    {
        stats.speed = 0.0f;
    }

    public void resetSpeed()
    {
        stats.speed = stats.baseSpeed;
    }
    IEnumerator flashColor(Color c)
    {        
            renderer.material.color = c;
            yield return new WaitForSeconds(0.1f);
            renderer.material.color = baseColor;
    }

       public IEnumerator checkArea() {


        foreach(Room_Manager rm in room_managers) {
            rm.enabled = true;
            
        }

        yield return new WaitForFixedUpdate();

        foreach (Room_Manager rm in room_managers)
        {
            rm.enabled = false;

        }

    }

}

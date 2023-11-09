using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Will implement Boss Door logic.
/// Many ways to handle a boss, but the door itself could check if it's pre requisites have been fufilled, or we can have a puzzle trigger the script to open the door. 
/// Soo many ways !
/// </summary>
public class BossDoor : BasicDoor
{

    public RythymManager rm;
    public GameObject demoEndText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rm.songFinished && rm.CheckBossDoor())
        {

            Open();
            demoEndText.SetActive(true);
        }

        if (PlayerInfo.bossDoorOpen) {

            Open();
            demoEndText.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public virtual void OnTriggerStay(Collider col)
    {

    }

    public override void OnTriggerEnter(Collider col)
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Locked door inherits from basic door, but this door requires players to have keys.
/// TODO - Show prompt when player enters trigger "Hit (gamepad button) to open door"
/// Right now, players key is consumed automatically. but that doest feel as good as hitting a button, yah know?
/// </summary>
public class LockedDoor : BasicDoor
{

    [SerializeField]
    public AudioSource sfx_source;

    [SerializeField]
    public AudioClip sfx_sound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider col)
    {

        GameObject parent = col.gameObject.transform.parent.gameObject;
        if (parent.tag == "Player" && isOpened == false)
        {
            if (PlayerInfo.useKey())
            {
                Open();
                sfx_source.PlayOneShot(sfx_sound);
            }
            else
            {
                Debug.Log("Requires Access Key!");
            }

        }
    }

    private void OnTriggerStay(Collider col)
    {

        GameObject parent = col.gameObject.transform.parent.gameObject;
        if (parent.tag == "Player" && isOpened == false)
        {


        }
    }

}

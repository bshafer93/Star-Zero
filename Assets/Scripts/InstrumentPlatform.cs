using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class that holds instruments the player can pick up!
/// </summary>
public class InstrumentPlatform : MonoBehaviour
{
    [SerializeField]
    Transform GuitarContainer;

    [SerializeField]
    GameObject thunderGuitarPrefab;

    [SerializeField]
    public AudioClip[] grab_sound;

    [SerializeField]
    public AudioSource a_source;

    public float floatSpeed = 1.0f;

    public float floatHeight = 5.0f;

    public bool addRythym = false;

    
       
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GuitarContainer.localPosition = new Vector3(GuitarContainer.localPosition.x, 10 + Mathf.Sin(Time.time * floatSpeed), GuitarContainer.localPosition.z) * floatHeight;
    }

    void OnTriggerEnter(Collider col)
    {
        if (addRythym)
        {
            if (col.gameObject.layer == 6 && PlayerInventory.hasRythym == false)
            {
                PlayerInventory.hasRythym = true;
                a_source.PlayOneShot(grab_sound[0]);
                thunderGuitarPrefab.SetActive(false);
            }
        }
        else if (col.gameObject.layer == 6 && PlayerInventory.hasThunderGuitar == false)
        {
            PlayerInventory.hasThunderGuitar = true;
            a_source.PlayOneShot(grab_sound[0]);
            thunderGuitarPrefab.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessKey : MonoBehaviour
{

    public float floatSpeed = 1.0f;
    public float floatHeight = 5.0f;

    [SerializeField]
    AudioSource sfx_source;

    [SerializeField]
    AudioClip sfx_sound;


    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.layer == 6)
        {
            PlayerInventory.accessKeys += 1;
            sfx_source.PlayOneShot(sfx_sound);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            Destroy(gameObject, 0.22f);

        }
        
    }

    



    void Update() {




    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class of the lock on reticle that is triggered by the player. 
/// When instantiated, it positions it self a bit above the targets.
/// </summary>
public class LockOnReticle : MonoBehaviour
{
    [SerializeField] Camera main;
    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main;
        Vector3 newPosition = transform.position + new Vector3(0, 2.5f, 0);
        transform.position = newPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        transform.LookAt(main.transform);
    }
}

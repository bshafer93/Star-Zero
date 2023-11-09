using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Manager : MonoBehaviour
{
    [SerializeField]
    public AREA_NAME NAME;
    
    void Awake() { 
    
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider col)
    {
        GameObject parent = col.gameObject.transform.parent.gameObject;
        if (col.gameObject.layer == 6)
        {
            PlayerInfo.player_area = NAME;   
        }
    }
}

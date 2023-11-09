using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDoor : MonoBehaviour
{
    [SerializeField]
    protected Animator animController;
    protected bool isOpened = false;

    [SerializeField]
    public string room_reveal_name;

    [SerializeField]
    public DungeonManager dm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Opens the door, by setting the animationController bool "isOpened".	
    /// </summary>
    public virtual void Open() {


        animController.SetBool("isOpened", true);
        isOpened = true;
        dm.RevealArea(room_reveal_name);

    }

    /// <summary>
    /// Closes the door, by setting the animationController bool "isOpened".	
    /// </summary>
    protected virtual void Close() {

        animController.SetBool("isOpened", false);
        isOpened = false;

    }

    public virtual void OnTriggerEnter(Collider col)
    {
        GameObject parent = col.gameObject.transform.parent.gameObject;
        if (parent.tag == "Player" && isOpened == false)
        {
          
            Open();
        }
    }
}

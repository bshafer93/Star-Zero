using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum NOTE_HIT {OKAY, GREAT, MISS};

public class ButtonHitMessage : MonoBehaviour
{
    [SerializeField]
    public Text message;

    public NOTE_HIT note_hit = NOTE_HIT.OKAY;

    public void init(NOTE_HIT n){
            note_hit = n;

            switch(n){
                case NOTE_HIT.OKAY:
                message.color = Color.yellow;
                message.text = "Okay";
                    break;
                case NOTE_HIT.GREAT:
                message.color = Color.green;
                message.text = "Great!";
                    break;
                case NOTE_HIT.MISS:
                message.color = Color.red;
                message.text = "Miss";
                    break;                
            }

    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Color c = message.color;
        c.a -= 1.0f * Time.deltaTime;
        message.color = c;
    }
}

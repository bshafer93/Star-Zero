using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SongInfo : MonoBehaviour
{

    [SerializeField]
    public AudioClip song_clip;

    [SerializeField]
    public TextAsset note_list_file;

    [SerializeField]
    public int bpm;  // beats per minute



    public float songPosition;     // Current position of song in seconds
    public float dsptimesong;      // How much time, in seconds, has passed since the song started
    public GameObject[] notes;          // holds the position-in-beats of all notes in the song
    public int nextIndex = 0;      // the index of the next note to be spawned


    public bool started = false;

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {                         //calculate how many seconds is 1 beat
        dsptimesong = (float)AudioSettings.dspTime;    //record the time when the song starts
    }

    // Update is called once per frame
    void Update(){


    }


}


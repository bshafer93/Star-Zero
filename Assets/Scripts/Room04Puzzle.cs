using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NotesIndex { 
    public const int A=0;
    public const int B=1;
    public const int C=2;
    public const int D=3;
    public const int E=4;
    public const int F=5;
    public const int G=6;
}


public class Room04Puzzle : MonoBehaviour
{
    
    [SerializeField]
    GameObject[] note_prop;

    [SerializeField]
    public GameObject access_key;

    [SerializeField]
    static AudioSource note_source;

    [SerializeField]
    static AudioClip wrong_answer_sound;

    [SerializeField]
     AudioClip wrong_answer_sound_ns;

    [SerializeField]
    static AudioClip puzzle_solved_sound;

    [SerializeField]
     AudioClip puzzle_solved_sound_ns;

    public Queue<char> note_order_key;
    //static char[] answer_key = new char[] { 'E', 'D', 'E', 'D', 'E', 'B', 'D', 'C', 'A' };
    //static int[] answer_key_index = new int[] { 4, 3, 4, 3, 4, 1, 3, 2, 0 };
    static char[] answer_key = new char[] { 'E', 'D', 'E', 'B' };
    static int[] answer_key_index = new int[] { NotesIndex.E, NotesIndex.D, NotesIndex.E, NotesIndex.B, };
    public static int notes_played = 0;
    public static Queue<char> note_queue;

    [SerializeField]
    static public Room04Puzzle self;

    [SerializeField]
    public Room04Puzzle self_ns;

    public static bool puzzle_solved = false;

    bool cutscenePlayed = false;

    [SerializeField]
    public PlayerInfo player_info;
       
    // Start is called before the first frame update
    void Start()
    {
        wrong_answer_sound = wrong_answer_sound_ns;
        puzzle_solved_sound = puzzle_solved_sound_ns;
        note_source = GetComponent<AudioSource>();
        self = self_ns;
 
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void solved_cutscene() {

        
        StartCoroutine(playWon());
        
    }

    static public void checkNote(char n) {

   
        if (n == answer_key[notes_played])
        {
            notes_played++;
        }
        else {
            notes_played = 0;
            note_source.PlayOneShot(wrong_answer_sound);
        }

        if(notes_played == answer_key.Length)
        {
            puzzle_solved = true;
            notes_played = 0;
            self.solved_cutscene();

        }
    
    }

     IEnumerator playWon()
    {
        yield return new WaitForSeconds(1.5f);
        access_key.SetActive(true);
        note_source.PlayOneShot(puzzle_solved_sound);
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 6 && cutscenePlayed == false)
        {
            Debug.Log("Player entered Puzzle.");
            StartCoroutine(PlayCutScene());
            cutscenePlayed = true;
        }
    }

    IEnumerator PlayCutScene()
    {


        player_info.stopMove();
        for (int i =0; i < answer_key_index.Length; i++) {
            note_prop[answer_key_index[i]].GetComponent<PuzzlePropNote>().playNote();
            //yield return new WaitForSeconds(note_prop[answer_key_index[i]].GetComponent<PuzzlePropNote>().note_sound.length);
            yield return new WaitForSeconds(0.5f);
        }

        player_info.resetSpeed();


        

    }




}

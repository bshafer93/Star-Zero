using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RythymManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    public SongInfo song;

    [SerializeField]
    public GameObject song_prefab;

    [SerializeField]
    public AudioSource audio_source;

    [SerializeField]
    public GameObject[] notes;


    [SerializeField]
    public GameObject songnote_prefab;

    public GameObject notehit_prefab;

    public SongNote next_note;
     public GameObject next_go;

    public int note_index = 0;

    public float time_before_spawn = 4.0f;

    const float OKAY_TIME = 0.5f;

    const float GREAT_TIME = 0.1f;

    const float MISS_TIME = 0.6f;

    public int totalNotes;
    public int notesHit;
    public bool songFinished = false;
    [SerializeField]
    public GameObject BossDoor;

     void Awake()
    {
        song = song_prefab.GetComponent<SongInfo>();
    }

    void Start()
    {

        notesHit = 0;
        NoteParser np = new NoteParser(song.note_list_file);
        np.notes.Sort((a, b) => a.song_time.CompareTo(b.song_time));
        NoteInfo[] ni = np.notes.ToArray();

        notes = new GameObject[ni.Length];
        totalNotes = notes.Length;
        for (int i = 0; i < totalNotes; i++)
        {   
            var note = Instantiate(songnote_prefab, gameObject.transform);
            SongNote SongNote = note.GetComponent<SongNote>();
            SongNote.button_name = ni[i].button_name;
            SongNote.note_time = ni[i].song_time;
            notes[i] = note;
            notes[i].SetActive(false);

        }

    }

    public void StartSong()
    {
        audio_source.clip = song.song_clip;
        song.started = true;
        song.dsptimesong = (float)AudioSettings.dspTime;    //record the time when the song starts
        next_go = notes[note_index];
        next_note = next_go.GetComponent<SongNote>();
        audio_source.Play();

    }

    // Update is called once per frame
    void Update()
    {

        song.songPosition = (float)(AudioSettings.dspTime - song.dsptimesong);
        if (audio_source.isPlaying){

            foreach (GameObject note in notes){
                SongNote sn = note.GetComponent<SongNote>();
                float time_left = sn.note_time - song.songPosition;
                float percent = 100 - (time_left / (time_before_spawn)) * 100;

                if (time_left <= time_before_spawn && !note.activeInHierarchy && time_left >= 0){
                    if (!note.activeInHierarchy && !sn.note_finished){
                        note.SetActive(true);
                    }
                }

                if (note.activeInHierarchy){

                    sn.note_slider.value = percent;
                }

                if(note.activeInHierarchy && sn == next_note){
                    if(time_left <= -MISS_TIME || percent >= 111 || percent < 0){
                        SpawnNoteHit(NOTE_HIT.MISS);
                        sn.note_finished = true;
                        note.SetActive(false);
                        note_index++;
                        if(note_index < notes.Length){
                            next_go = notes[note_index];
                            next_note = next_go.GetComponent<SongNote>();
                        }
                        if (note_index >= notes.Length) {
                            songFinished = true;
                        
                        }
                    }
                }

            }

            
        }
        
        if (!audio_source.isPlaying && song.started) {
            song.started = false;
            CheckBossDoor();
        }




    }

    void SpawnNoteHit(NOTE_HIT n){

        GameObject h = Instantiate(notehit_prefab,gameObject.transform);
        h.GetComponent<ButtonHitMessage>().init(n);

    }

    public bool CheckBossDoor() {
        float percentHit = (notesHit / totalNotes);
        Debug.Log(percentHit);
        if (percentHit >= 0.5f) {
            PlayerInfo.bossDoorOpen = true;
            Debug.Log("Boss Door Open!");
            return true;
        }
        return false;
    }

    public void CheckNoteHit(string button_pressed, float time_pressed){

        if(button_pressed == next_note.button_name){

            if(time_pressed <= (next_note.note_time + GREAT_TIME) && time_pressed >= (next_note.note_time -GREAT_TIME) ){
                notesHit++;
                SpawnNoteHit(NOTE_HIT.GREAT);
                next_note.note_finished = true;
                next_go.SetActive(false);
                note_index++;
                if(note_index < notes.Length){
                    next_go = notes[note_index];
                    next_note = next_go.GetComponent<SongNote>();
                }
            }else{
            
                if(time_pressed <= (next_note.note_time + OKAY_TIME) && time_pressed >= (next_note.note_time -OKAY_TIME) ){
                    notesHit++;
                    SpawnNoteHit(NOTE_HIT.OKAY);
                    next_note.note_finished = true;
                    next_go.SetActive(false);
                    note_index++;
                if(note_index < notes.Length){
                    next_go = notes[note_index];
                    next_note = next_go.GetComponent<SongNote>();
                }
                }

            }



        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePropNote : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    AudioSource note_source;

    [SerializeField]
    public char note_name;

    public GameObject propLight;

    [SerializeField]
   public AudioClip note_sound;

    void Start()
    {
        note_source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator toggleLight(float seconds)
    {
        if (!propLight.activeInHierarchy)
            propLight.SetActive(true);

        yield return new WaitForSeconds(seconds);

        propLight.SetActive(false);
        
    }

    public void playNote() {

        note_source.PlayOneShot(note_sound, 0.7f);
        StartCoroutine(toggleLight(0.5f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.layer == 7)
        {
            playNote();
            Room04Puzzle.checkNote(note_name);
        }
    }
}

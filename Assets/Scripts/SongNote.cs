using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SongNote : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public Sprite controller_input_image;

    [SerializeField]
    public Slider note_slider;

    [SerializeField]
    public Image slider_image;

    [SerializeField]
    public string button_name;

    public float note_time;

    public bool note_finished = false;

    [SerializeField]
    public Sprite controller_A;
    [SerializeField]
    public Sprite controller_X;
    [SerializeField]
    public Sprite controller_Y;

    void Awake()
    {
        slider_image.sprite = controller_input_image;
    }

    void Start()
    {
        switch (button_name)
        {
            case "A":
                slider_image.sprite = controller_A;
                break;
            case "Y":
                slider_image.sprite = controller_Y;
                break;
            case "X":
                slider_image.sprite = controller_X;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

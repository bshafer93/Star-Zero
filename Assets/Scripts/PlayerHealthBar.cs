using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthBar : MonoBehaviour
{

    private float CANVAS_WIDTH = 2.0f;
    [SerializeField]
    public Canvas canvas;
    public Color bar_color;

    [SerializeField]
    public Image hb_image;

    public float start_width;

    [SerializeField]
    public RectTransform hb_area_transform;

    // Start is called before the first frame update
    void Start()
    {
        hb_image.color = bar_color;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setByPercent(float percent)
    {
        //Debug.Log("Health:" + percent);
        //Debug.Log("Adjusted01:" + CANVAS_WIDTH*percent);
        if (percent >= 1.0)
        {
            hb_area_transform.sizeDelta = new Vector2(start_width * percent, hb_area_transform.sizeDelta.y);
        }
        else
        {
            hb_area_transform.sizeDelta = new Vector2(start_width * percent, hb_area_transform.sizeDelta.y);
        }
        if (percent <= 0)
        {
            hb_area_transform.sizeDelta =new Vector2(0, hb_area_transform.sizeDelta.y);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    private float CANVAS_WIDTH = 2.0f;
    [SerializeField] 
    public Canvas canvas;
    public Color bar_color = Color.red;

    [SerializeField] 
    public Image hb_image;

    [SerializeField] 
    public RectTransform hb_transform;

    // Start is called before the first frame update
    void Start()
    {
        RectTransform canvas_rect = canvas.GetComponent<RectTransform>();
        CANVAS_WIDTH = canvas_rect.rect.width;
        hb_image.color = bar_color;
    }

    // Update is called once per frame
    void Update()
    {
        //If health bar is meant to be diagetic, then it should face main camera
        if(canvas.renderMode == RenderMode.WorldSpace){
            gameObject.transform.LookAt(Camera.main.transform);
        }
         
    }


   public void setByPercent(float percent){
      //Debug.Log("Health:" + percent);
       //Debug.Log("Adjusted01:" + CANVAS_WIDTH*percent);
        if(percent >= 1.0){
            hb_transform.offsetMax = new Vector2( 0,hb_transform.offsetMax.y);
        }else{
            hb_transform.offsetMax = new Vector2( -(CANVAS_WIDTH-(CANVAS_WIDTH*percent)),hb_transform.offsetMax.y);
        }
        if(percent <= 0 ){
            hb_transform.offsetMax = new Vector2( -(CANVAS_WIDTH),hb_transform.offsetMax.y);
        }
    }
}

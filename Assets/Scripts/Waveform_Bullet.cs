using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waveform_Bullet : MonoBehaviour
{

    [SerializeField]
    public GameObject bar_prefab;

    [SerializeField]
    public int sample_jump_amount = 1024;

    public Material[] bar_materials;

    [SerializeField]
    public int NumberOfBars = 12;

  
    int sample_index =0;

    [SerializeField]
    public float bar_strength = 5.0f;
    
    double sample_time = 0.0;
        
    [SerializeField]
    public double time_jump_amount = 1.0;
    // Start is called before the first frame update
    void Awake()
    {

        bar_materials = new Material[NumberOfBars];

        for (int i = 0; i < NumberOfBars; i++)
        {
            Transform parent = gameObject.GetComponentInParent<Transform>();
            GameObject bar = Instantiate<GameObject>(bar_prefab,parent.localPosition,new Quaternion(0,0,0,0),parent);
            bar.transform.localPosition = parent.forward * i*-0.5f;
            bar_materials[i] = bar.GetComponent<Renderer>().material;

        }
        gameObject.GetComponentInParent<Transform>().rotation = new Quaternion(0, 0, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < NumberOfBars; i++)
        {

            int index = sample_index + i * sample_jump_amount > Waveform_Data.SAMPLE_SIZE[0] ? (sample_index + i * sample_jump_amount)- Waveform_Data.SAMPLE_SIZE[0] : sample_index + i * sample_jump_amount;
            bar_materials[i].SetFloat("_Amplitude", Waveform_Data.samples[0][index]);
            float strength = bar_strength * (1 - (float)i / (float)NumberOfBars);
            if (strength == 0) {
                strength = 1;
            }
            bar_materials[i].SetFloat("_Strength", strength);

            sample_time += time_jump_amount;
            sample_index = (int)sample_time;
            if (sample_index >= Waveform_Data.SAMPLE_SIZE[0])
            {
                sample_index = 0;
                sample_time = 0;
            }
        }

    }

 float normalizeValue(float inRangeStart,float inRangeEnd, float outRangeStart,float outRangeEnd,float inValue)
    {
        

        float outValue = inValue;

        outValue = outValue - inRangeStart;
        outValue = outValue / (inRangeEnd - inRangeStart);
        outValue = outValue * (outRangeEnd - outRangeStart);
        outValue = outValue + outRangeStart;

        return outValue;

 }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waveform_Data : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public AudioClip[] riffs;

    public static float[][] samples = new float[2][];

    public static int[] SAMPLE_SIZE = new int[2];

    public static int[] riff_sample_size = new int[2];
    public static int[] riff_num_channels= new int[2];

    void Awake()
    {
        riff_sample_size[0] = riffs[0].samples;
        riff_num_channels[0] = riffs[0].channels;

        riff_sample_size[1] = riffs[1].samples;
        riff_num_channels[1] = riffs[1].channels;

        SAMPLE_SIZE[0] = riffs[0].samples * riffs[0].channels;
        SAMPLE_SIZE[1] = riffs[1].samples * riffs[1].channels;

        samples[0] = new float[SAMPLE_SIZE[0]];
        samples[1] = new float[SAMPLE_SIZE[1]];

        riffs[0].GetData(samples[0], 0);
        riffs[1].GetData(samples[1], 0);

        for (int i = 0; i < samples[0].Length; ++i)
        {
            float normalized = normalize_sample(samples[0][i]);
            samples[0][i] = normalized;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    static float normalize_sample(float inValue)
    {
        float normalized = inValue;

        normalized = normalized - -1;
        normalized = normalized / (1 - -1);

        return normalized;

    }
}

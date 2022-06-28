using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicGen : MonoBehaviour
{
    private const int frameSize = 10;
    public int size = 2048;
    public PolygonCollider2D poly;

    private readonly int lineScale = 5;
    private readonly int quality = 100;
    private int sampleCount = 0;

    private float[] waveFormArray;
    private float[] samples;

    private Vector2[] path;
    private AudioSource myAudio;

    private void Start()
    {
        myAudio = gameObject.GetComponent<AudioSource>();

        int freq = myAudio.clip.frequency;
        sampleCount = freq / quality;

        samples = new float[myAudio.clip.samples * myAudio.clip.channels];

        myAudio.clip.GetData(samples, 0);
        waveFormArray = new float[(samples.Length / sampleCount)];

        for (int i = 0; i < waveFormArray.Length; i++)
        {
            waveFormArray[i] = 0;

            for (int j = 0; j < sampleCount; j++)
            {
                waveFormArray[i] += Mathf.Abs(samples[(i * sampleCount) + j]);
            }

            waveFormArray[i] /= sampleCount * 2;

        }

        //�������� ���������� ������, � ������� ���� frameSize
        float[] avgArray = MovingAverage(frameSize, waveFormArray);
        path = CreatePath(avgArray);
        poly.points = path;

    }

    private Vector2[] CreatePath(float[] src)
    {
        Vector2[] result = new Vector2[src.Length];

        for (int i = 0; i < size; i++)
        {
            result[i] = new Vector2(i * 0.01f, Mathf.Abs(src[i] * lineScale));
        }

        return result;
    }

    private float[] MovingAverage(int frameSize, float[] data)
    {
        float sum = 0;
        float[] avgPoints = new float[data.Length - frameSize + 1];

        for (int counter = 0; counter <= data.Length - frameSize; counter++)
        {
            int innerLoopCounter = 0;
            int index = counter;

            while (innerLoopCounter < frameSize)
            {
                sum = sum + data[index];
                innerLoopCounter += 1;
                index += 1;
            }

            avgPoints[counter] = sum / frameSize;
            sum = 0;

        }
        return avgPoints;
    }
}
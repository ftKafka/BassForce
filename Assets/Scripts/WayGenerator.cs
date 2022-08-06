using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayGenerator : MonoBehaviour
{
    public GameObject Player;
    // меш Вершины и тринглы
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    //
    public int size = 2048;

    private readonly int quality = 100;
    private int sampleCount = 0;

    private float[] waveFormArray;
    private float[] samples;

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

        //Получаем сглаженный массив, с шириной окна frameSize
        /*
        //float[] avgArray = MovingAverage(frameSize, waveFormArray);
        //path = CreatePath(avgArray);
        //poly.points = path;
        */

        //Полигоны

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        
        CreateShape(waveFormArray);

        UpdateMesh();
    }

    private void Update()
    {
        
    }


    //релод полигончиков
    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
    /*
    //кружочки
    private void OnDrawGizmos()
    {
        if (vertices == null)
            return;

        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }
    */

    //полигоны
    private void CreateShape(float[] waveFormArray)
    {
        vertices = new Vector3[waveFormArray.Length * 3 + 1];

        //perling noize
        for (int z = 0, i = 0; z <= waveFormArray.Length; z++)
        {
            for (int x = 0; x <= 1; x++)
            {
                //float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
                float y = 0;
                if (z < waveFormArray.Length)
                    y = waveFormArray[z]*100;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        Debug.Log(waveFormArray.Length);
        triangles = new int[waveFormArray.Length * 6];
        Debug.Log(triangles.Length);
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < waveFormArray.Length; z++)
        {
            triangles[tris + 0] = vert + 0;
            triangles[tris + 1] = vert + 1 + 1;
            triangles[tris + 2] = vert + 1;
            triangles[tris + 3] = vert + 1;
            triangles[tris + 4] = vert + 1 + 1;
            triangles[tris + 5] = vert + 1 + 2;


            //один верт идет после фора и один для состыкови по оси фронтали (я соединил)
            vert += 2;
            tris += 6;
        }

        Player.GetComponent<ForwardRun>().longPlay = myAudio.clip.length;
        Player.GetComponent<ForwardRun>().verticesForvard = vertices;
        Player.GetComponent<ForwardRun>().Iterial();
    }
}

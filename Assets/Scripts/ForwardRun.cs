using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ForwardRun : MonoBehaviour
{
    public Vector3[] verticesForvard;
    public float longPlay;
    private float timeTick;
    public int tick = 0;
    private Vector3 moveTo;
    public float timerIn;


    private void Start()
    {
        tick = 0;
        timeTick = longPlay / verticesForvard.Length;
    }

    void Update()
    {
        timerIn += Time.deltaTime;

        tick = Mathf.RoundToInt(verticesForvard.Length * (timerIn / longPlay));
        moveTo = verticesForvard[tick] - this.transform.position;
        
        //timeTick = verticesForvard[tick].z / verticesForvard[verticesForvard.Length - 1].z;
        this.transform.position += moveTo / Time.deltaTime * timeTick;
    }
}

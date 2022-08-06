using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardRun : MonoBehaviour
{
    public Vector3[] verticesForvard;
    public float longPlay;


    public void Iterial()
    {
        
    }

    void Update()
    {
        //gameObject.transform.position = new Vector3( , , );
        //gameObject.transform.position = verticesForvard[10];
        //gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        float x = verticesForvard[verticesForvard.Length - 2].x;
        float y = verticesForvard[verticesForvard.Length - 2].y;
        float z = Vector3.Distance(gameObject.transform.position, verticesForvard[verticesForvard.Length - 1]);
        float _z = Mathf.Lerp(0, z, longPlay);
        transform.position = new Vector3(transform.position.x, transform.position.y, _z);
    }
}

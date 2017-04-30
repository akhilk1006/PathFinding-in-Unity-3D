using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start1 : MonoBehaviour {

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(i * 10f, 0.5f, j * 10f);
                cube.transform.localScale = new Vector3(9.9f, 1f, 9.9f);
            }
        }
    }



}

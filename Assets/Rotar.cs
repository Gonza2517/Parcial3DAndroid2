using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotar : MonoBehaviour
{
    public float Rotacion = 1.2f;
    // Update is called once per frame
    void Update()
    {
        GetComponent<Skybox>().material.SetFloat("_Rotation", Time.time * 1.23f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sph3d : MonoBehaviour
{
    float velocidadX = 0.0f;
    float velocidadY = 0.0f;
    float velocidadZ = 0.0f;

    void Start()
    {

    }

    void Update()
    {

    }

    public Vector3 getVelocidad()
    {
        return new Vector3(velocidadX, velocidadY, velocidadZ);
    }

    public void setVelocidad(Vector3 velocidad)
    {
        velocidadX = velocidad.x;
        velocidadY = velocidad.y;
        velocidadZ = velocidad.z;
    }
}
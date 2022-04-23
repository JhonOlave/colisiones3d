using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class col3d : MonoBehaviour
{
    public Transform Sphere_1;
    public Transform Sphere_2;
    public GameObject Parent;
    private Vector3 velocidad_s1; 
    private Vector3 velocidad_s2; 
    private Vector3 posicion_s1; 
    private Vector3 posicion_s2; 
    float angulo, anguloxz, anguloyz;
    float masa_s1 = 1.0f, masa_s2 = 1.0f;
    float e = 1.0f;
    float radio_s = 0.5f;
    public GameObject[] Particulas;

    // Start is called before the first frame update
    void Start()
    {
        velocidad_s1 = new Vector3(3.0f, 3.0f, 3.0f);
        velocidad_s2 = new Vector3(-2.0f, -2.0f, -2.0f);
        posicion_s1 = new Vector3(-1.0f, -1.0f, -1.0f);
        posicion_s2 = new Vector3(3.75f, 3.75f, 4.7f);

        Sphere_1 = this.gameObject.transform.GetChild(1);
        Sphere_2 = this.gameObject.transform.GetChild(2);

        Sphere_1.position = new Vector3(posicion_s1.x, posicion_s1.y, posicion_s1.z);
        Sphere_2.position = new Vector3(posicion_s2.x, posicion_s2.y, posicion_s2.z);
        Sphere_1.GetComponent<sph3d>().setVelocidad(new Vector3(velocidad_s1.x, velocidad_s1.y, velocidad_s1.z));
        Sphere_2.GetComponent<sph3d>().setVelocidad(new Vector3(velocidad_s2.x, velocidad_s2.y, velocidad_s2.z));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distancia = Mathf.Sqrt(Mathf.Pow(posicion_s2.x - posicion_s1.x, 2) + Mathf.Pow(posicion_s2.y - posicion_s1.y, 2) + Mathf.Pow(posicion_s2.z - posicion_s1.z, 2));
        float aux = 1.0f / (masa_s1 + masa_s2);

        if (distancia <= 2.0*radio_s)
        {
            angulo = Mathf.Atan(((Mathf.Pow(posicion_s1.x, 2) / 2) + (Mathf.Pow(posicion_s2.x, 2) / 2)) / ((Mathf.Pow(posicion_s1.y, 2) / 2) + (Mathf.Pow(posicion_s2.y, 2) / 2)));
            anguloxz = Mathf.Atan(((Mathf.Pow(posicion_s1.x, 2) / 2) + (Mathf.Pow(posicion_s2.x, 2) / 2)) / ((Mathf.Pow(posicion_s1.z, 2) / 2) + (Mathf.Pow(posicion_s2.z, 2) / 2)));
            anguloyz = Mathf.Atan(((Mathf.Pow(posicion_s1.y, 2) / 2) + (Mathf.Pow(posicion_s2.y, 2) / 2)) / ((Mathf.Pow(posicion_s1.z, 2) / 2) + (Mathf.Pow(posicion_s2.z, 2) / 2)));
            //Para la esfera 1
            Vector3 vp1 = velocidad_s1 * Mathf.Cos(angulo) * Mathf.Cos(anguloxz) + velocidad_s1 * Mathf.Sin(angulo) * Mathf.Cos(anguloxz) - velocidad_s1 * Mathf.Sin(angulo);
            Vector3 vn1 = -(velocidad_s1 * Mathf.Cos(anguloyz) * Mathf.Sin(anguloxz) + velocidad_s1 * Mathf.Sin(anguloyz) * Mathf.Cos(angulo) * Mathf.Sin(anguloxz)) + (velocidad_s1 * Mathf.Cos(anguloyz) * Mathf.Cos(anguloxz) + velocidad_s1 * Mathf.Sin(anguloyz) * Mathf.Sin(angulo) * Mathf.Sin(anguloxz)) + (velocidad_s1 * Mathf.Sin(anguloyz) * Mathf.Cos(angulo));
            Vector3 kn1 = (velocidad_s1 * Mathf.Sin(anguloyz) * Mathf.Sin(anguloxz) + velocidad_s1 * Mathf.Cos(anguloyz) * Mathf.Sin(angulo) * Mathf.Cos(anguloxz)) - (velocidad_s1 * Mathf.Sin(anguloyz) * Mathf.Cos(anguloxz) + velocidad_s1 * Mathf.Cos(anguloyz) * Mathf.Sin(angulo) * Mathf.Sin(anguloxz)) - (velocidad_s1 * Mathf.Cos(anguloyz) * Mathf.Cos(angulo));

            //Para la esfera 2
            Vector3 vp2 = velocidad_s2 * Mathf.Cos(angulo) * Mathf.Cos(anguloxz) + velocidad_s2 * Mathf.Sin(angulo) * Mathf.Cos(anguloxz) - velocidad_s2 * Mathf.Sin(angulo);
            Vector3 vn2 = -(velocidad_s2 * Mathf.Cos(anguloyz) * Mathf.Sin(anguloxz) + velocidad_s2 * Mathf.Sin(anguloyz) * Mathf.Cos(angulo) * Mathf.Sin(anguloxz)) + (velocidad_s2 * Mathf.Cos(anguloyz) * Mathf.Cos(anguloxz) + velocidad_s2 * Mathf.Sin(anguloyz) * Mathf.Sin(angulo) * Mathf.Sin(anguloxz)) + (velocidad_s2 * Mathf.Sin(anguloyz) * Mathf.Cos(angulo));
            Vector3 kn2 = (velocidad_s2 * Mathf.Sin(anguloyz) * Mathf.Sin(anguloxz) + velocidad_s2 * Mathf.Cos(anguloyz) * Mathf.Sin(angulo) * Mathf.Cos(anguloxz)) - (velocidad_s2 * Mathf.Sin(anguloyz) * Mathf.Cos(anguloxz) + velocidad_s2 * Mathf.Cos(anguloyz) * Mathf.Sin(angulo) * Mathf.Sin(anguloxz)) - (velocidad_s2 * Mathf.Cos(anguloyz) * Mathf.Cos(angulo));

            //----------------------------------------------------------------------------------------//

            //calculo el vp1 new y el vp2 new
            Vector3 vp1_new = (masa_s1 - e * masa_s2) * vp1 * aux + (1.0f + e) * masa_s2 * vp2 * aux;
            Vector3 vp2_new = (1.0f + e) * masa_s1 * vp1 * aux + (masa_s2 - e * masa_s1) * vp2 * aux;

            //----------------------------------------------------------------------------------------//

            //Rotación inversa del eje de referencia
            //Para la esfera 1
            velocidad_s1.x = -vp1_new.x * Mathf.Cos(anguloyz) * Mathf.Cos(angulo) - (vn1.x * Mathf.Sin(anguloyz) * Mathf.Cos(anguloxz) + Mathf.Cos(anguloyz) * Mathf.Sin(angulo) * Mathf.Sin(anguloxz)) + (kn1.x * Mathf.Sin(anguloyz) * Mathf.Sin(anguloxz) + Mathf.Cos(anguloyz) * Mathf.Sin(angulo) * Mathf.Cos(anguloxz));
            velocidad_s1.y = vp1_new.y * (Mathf.Sin(anguloyz) * Mathf.Cos(angulo)) + (vn1.x * Mathf.Cos(anguloyz) * Mathf.Cos(anguloxz) + Mathf.Sin(anguloyz) * Mathf.Sin(angulo) * Mathf.Sin(anguloxz)) - kn1.x * (Mathf.Cos(anguloyz) * Mathf.Sin(anguloxz) + Mathf.Sin(anguloyz) * Mathf.Cos(angulo) * Mathf.Sin(anguloxz));
            velocidad_s1.z = -vp1_new.z * Mathf.Sin(angulo) + (vn1.x * Mathf.Sin(angulo) * Mathf.Cos(anguloxz)) + (kn1.x * Mathf.Cos(angulo) * Mathf.Cos(anguloxz));
            //Para la esfera 2
            velocidad_s2.x = -vp2_new.x * Mathf.Cos(anguloyz) * Mathf.Cos(angulo) - (vn2.x * Mathf.Sin(anguloyz) * Mathf.Cos(anguloxz) + Mathf.Cos(anguloyz) * Mathf.Sin(angulo) * Mathf.Sin(anguloxz)) + (kn2.x * Mathf.Sin(anguloyz) * Mathf.Sin(anguloxz) + Mathf.Cos(anguloyz) * Mathf.Sin(angulo) * Mathf.Cos(anguloxz));
            velocidad_s2.y = vp2_new.y * (Mathf.Sin(anguloyz) * Mathf.Cos(angulo)) + (vn2.x * Mathf.Cos(anguloyz) * Mathf.Cos(anguloxz) + Mathf.Sin(anguloyz) * Mathf.Sin(angulo) * Mathf.Sin(anguloxz)) - (kn2.x * Mathf.Cos(anguloyz) * Mathf.Sin(anguloxz) + Mathf.Sin(anguloyz) * Mathf.Cos(angulo) * Mathf.Sin(anguloxz));
            velocidad_s2.z = -vp2_new.z * Mathf.Sin(angulo) + (vn2.x * Mathf.Sin(angulo) * Mathf.Cos(anguloxz)) + (kn2.x * Mathf.Cos(angulo) * Mathf.Cos(anguloxz));



            //Otorgar nuevas velocidades
            Sphere_1.GetComponent<sph3d>().setVelocidad(new Vector3(velocidad_s1.x, velocidad_s1.y, velocidad_s1.z));
            Sphere_2.GetComponent<sph3d>().setVelocidad(new Vector3(velocidad_s2.x, velocidad_s2.y, velocidad_s2.z));

            // vp1 = velocidad_s1 * Mathf.Cos(angulo) + velocidad_s1 * Mathf.Sin(angulo);
            // vp2 = velocidad_s2 * Mathf.Cos(angulo) + velocidad_s2 * Mathf.Sin(angulo);

            print("D: " + distancia);
            print("V1: " + velocidad_s1);
            print("V2: " + velocidad_s2);
        }

        posicion_s1 = posicion_s1 + Time.deltaTime * velocidad_s1;
        posicion_s2 = posicion_s2 + Time.deltaTime * velocidad_s2;


        Sphere_1.position = new Vector3(posicion_s1.x, posicion_s1.y, posicion_s1.z);
        Sphere_2.position = new Vector3(posicion_s2.x, posicion_s2.y, posicion_s2.z);
    }

}


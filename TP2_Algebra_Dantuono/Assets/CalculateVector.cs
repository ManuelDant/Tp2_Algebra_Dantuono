using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateVector : MonoBehaviour
{
    [SerializeField] Vector3 vector;
    [SerializeField] Vector3 vectorRotated;
    [SerializeField] Vector3 vectorCross;

    //Vectores Cortados
    [SerializeField] Vector3 vectorRotatedCut;
    [SerializeField] Vector3 vectorCrossCut;

    //Random para determinar que eje es rotado
    int random;

    private void OnDrawGizmos() //Printea las lineas en Unity
    {
        //Vector normales
        Gizmos.color = Color.blue;

        Gizmos.DrawLine(transform.position, transform.position + vector);

        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.position + vectorRotated);

        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position, transform.position + vectorCross);


        //Vectores cortados por la distancia mas corta
        Gizmos.color = Color.cyan;

        Gizmos.DrawLine(transform.position, transform.position + vector);
        Gizmos.color = Color.cyan;

        Gizmos.DrawLine(transform.position, transform.position + vectorRotatedCut);
        Gizmos.color = Color.cyan;

        Gizmos.DrawLine(transform.position, transform.position + vectorCrossCut);
    }

    //https://github.com/Unity-Technologies/UnityCsReference/blob/master/Runtime/Export/Math/Vector3.cs
    //Linea 215
    public static Vector3 ProductCrossVector3(Vector3 Vector1, Vector3 Vector2)
    {
        return new Vector3(
            Vector1.y * Vector2.z - Vector1.z * Vector2.y,
            Vector1.z * Vector2.x - Vector1.x * Vector2.z,
            Vector1.x * Vector2.y - Vector1.y * Vector2.x);
    }
    void Start()
    {
        random = Random.Range(1, 3); //Rotar 90 grados respecto al primer vector.
        if (random == 1)
        {
            vectorRotated =  new Vector3(-vector.x, vector.y, vector.z);
        }
        else if (random == 2)
        {
            vectorRotated = new Vector3(vector.x, -vector.y, vector.z);
        }
        else
        {
            vectorRotated = new Vector3(vector.x, vector.y, -vector.z);
        }

        vectorCross = ProductCrossVector3(vector, vectorRotated); //Vector perpendicular respecto a los otros dos vectores

        DistanceCalculator(vector,vectorRotated,vectorCross); //Calcula la menor distancia entre los 3 vectores.
    }

    enum Axis { X, Y, Z }
    Axis axisVector;
    Axis axis;
    public void DistanceCalculator(Vector3 Vector, Vector3 VectorRotated, Vector3 VectorCross)
    {
        Vector3[] Vectors = new Vector3[3];

        Vectors[0] = Vector;
        Vectors[1] = VectorRotated;
        Vectors[2] = VectorCross;
       

        //Metodo Burbuja para ordenar la distancia de los ejes de Mayor a Menor.
        //https://stackoverflow.com/questions/14768010/simple-bubble-sort-c-sharp

        if ((Vectors[0].x >= 0 && Vectors[1].x >= 0 && Vectors[2].x >= 0) || (Vectors[0].x < 0 && Vectors[1].x < 0 & Vectors[2].x < 0))
        { //Eje X
            for (int i = 0; i < Vectors.Length - 1; i++)
            {
                for (int j = 0; j < Vectors.Length - i - 1; j++)
                {
                    if (System.MathF.Abs(Vectors[j].x) > System.MathF.Abs(Vectors[j + 1].x))
                    {
                        Vector3 aux = Vectors[j + 1];
                        Vectors[j + 1] = Vectors[j];
                        Vectors[j] = aux;
                    }
                }
            }
            axisVector = Axis.X;
        }

        //Eje Y
        if ((Vectors[0].y >= 0 && Vectors[1].y >= 0 && Vectors[2].y >= 0) || (Vectors[0].y < 0 && Vectors[1].y < 0 & Vectors[2].y < 0))
        { 
            for (int i = 0; i < Vectors.Length - 1; i++)
            {
                for (int j = 0; j < Vectors.Length - i - 1; j++)
                {
                    if (System.MathF.Abs(Vectors[j].y) > System.MathF.Abs(Vectors[j + 1].y))
                    {
                        Vector3 aux = Vectors[j + 1];
                        Vectors[j + 1] = Vectors[j];
                        Vectors[j] = aux;

                    }
                }
            }
            axisVector = Axis.Y;
        }

        //Eje Z
        if ((Vectors[0].z >= 0 && Vectors[1].z >= 0 && Vectors[2].z >= 0) || (Vectors[0].z < 0 && Vectors[1].z < 0 & Vectors[2].z < 0))
        { 
            for (int i = 0; i < Vectors.Length - 1; i++)
            {
                for (int j = 0; j < Vectors.Length - i - 1; j++)
                {
                    if (System.MathF.Abs(Vectors[j].z) > System.MathF.Abs(Vectors[j + 1].z))
                    {
                        Vector3 aux = Vectors[j + 1];
                        Vectors[j + 1] = Vectors[j];
                        Vectors[j] = aux;
                    }
                }
            }
            axisVector = Axis.Z;  
        }

        vectorRotatedCut = VectorsCuts(Vector, VectorRotated, axisVector);
        vectorCrossCut = VectorsCuts(Vector, VectorCross, axisVector);
    }

    Vector3 VectorsCuts(Vector3 origin, Vector3 vectorCut, Axis axis)
    {

        Vector3 VectorCutted = new Vector3();
       
        return VectorCutted;
    }
    void Update()
    {
        
    }
}

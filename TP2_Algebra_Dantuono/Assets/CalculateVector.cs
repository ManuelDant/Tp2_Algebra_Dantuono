using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateVector : MonoBehaviour
{
    [SerializeField] Vector3 vector;
    [SerializeField] Vector3 vectorRotated;
    [SerializeField] Vector3 vectorCross;
    [SerializeField] float AreaPyramid;

    //Vectores Cortados
    Vector3 vectorRotatedCut;
    Vector3 vectorCrossCut;

    //Random para determinar que eje es rotado
    int random;

    private void OnDrawGizmos()
    {
        //Vectores normales
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
        
        //Muestra la base de la piramide 
        Gizmos.DrawLine(vector, -vector + vectorRotatedCut * 2);
        Gizmos.DrawLine(vectorRotatedCut, -vectorRotatedCut + vectorCrossCut * 2);
        Gizmos.DrawLine(vectorCrossCut, -vectorCrossCut + vector * 2);

    }
    //link donde hice el Producto Cruz
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
        //Cortar los vectores a la distancia mas corta.
        vectorRotatedCut = VectorsCuts(Vector, VectorRotated, axisVector);
        vectorCrossCut = VectorsCuts(Vector, VectorCross, axisVector);

        //Calcular el area de la piramide
        AreaPyramid = AreaOfPyramid(Vector, vectorRotatedCut, vectorCrossCut);
    }

    Vector3 VectorsCuts(Vector3 vectorCutted, Vector3 vectorToCut, Axis axis)
    {
        Vector3 VectorCutted;
        float vectorX;
        float vectorY;
        float vectorZ;

        if (axis == Axis.X)
        {
            //Realiza los cortes de los ejes con mayor distancia y crea un nuevo vector con los nuevos ejes cortados manteniendo el eje mas corto.

            //Reposiciona los ejes dividendo el eje cortado por los tangentes de los cotangentes de los otros ejes para que se haga los cortes sin moverlos
            //de su posicion inicial.
            vectorY = vectorCutted.x / System.MathF.Tan(System.MathF.Atan(vectorToCut.x / vectorToCut.y));
            vectorZ = vectorCutted.x / System.MathF.Tan(System.MathF.Atan(vectorToCut.x / vectorToCut.z));
            VectorCutted = new Vector3(vectorCutted.x, vectorY, vectorZ);
        }
        else if (axis == Axis.Y)
        {
            vectorX = vectorCutted.y / System.MathF.Tan(System.MathF.Atan(vectorToCut.y / vectorToCut.x));
            vectorZ = vectorCutted.y / System.MathF.Tan(System.MathF.Atan(vectorToCut.y / vectorToCut.z));
            VectorCutted = new Vector3(vectorX, vectorCutted.y, vectorZ);
        }
        else
        {
            vectorY = vectorCutted.z / System.MathF.Tan(System.MathF.Atan(vectorToCut.z / vectorToCut.y));
            vectorX = vectorCutted.z / System.MathF.Tan(System.MathF.Atan(vectorToCut.z / vectorToCut.x));
            VectorCutted = new Vector3(vectorX, vectorY, vectorCutted.z);
        }    
        return VectorCutted;
    }

    float AreaOfTriangle(Vector3 vector1, Vector3 vector2)
    {
        //https://byjus.com/jee/how-to-find-the-area-of-a-triangle-using-vectors/#:~:text=How%20do%20you%20find%20the,vectors%20and%20divide%20by%202.
        //Para encontrar el tercer vector hay que realizar producto cruz y dividirlo por 2.
        Vector3 vector3;
        vector3 = ProductCrossVector3(vector1, vector2) / 2;
       
        //Pitagoras para la base y altura del triangulo.
        float baseOfTriangle = System.MathF.Sqrt(System.MathF.Pow(vector2.x - vector1.x, 2) + System.MathF.Pow(vector2.y - vector1.y, 2) + System.MathF.Pow(vector2.z - vector1.z, 2));
        float heightOfTriangle = System.MathF.Sqrt(System.MathF.Pow(vector3.x, 2) + System.MathF.Pow(vector3.y, 2) + System.MathF.Pow(vector3.z, 2));
       
        //Calcular area del triangulo con base * altura / 2;
        return baseOfTriangle * heightOfTriangle / 2;
    }

    float AreaOfPyramid(Vector3 vector1, Vector3 vector2, Vector3 vector3)
    {
        //Suma de los lados para obtener el area de la piramide
        float fullAreaOfPyramid = AreaOfTriangle(vector1, vector2) + AreaOfTriangle(vector3, vector2) + AreaOfTriangle(vector3, vector1);

        return fullAreaOfPyramid;
    }
}

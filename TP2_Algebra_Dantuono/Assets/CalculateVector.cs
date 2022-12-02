using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateVector : MonoBehaviour
{
    [SerializeField] Vector3 vector;
    [SerializeField] Vector3 vectorRotated;
    [SerializeField] Vector3 vectorCross;
    [SerializeField] int random;

    private void OnDrawGizmos() //Printea las lineas en Unity
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawLine(transform.position, transform.position + vector);

        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.position + vectorRotated);

        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position, transform.position + vectorCross);
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
        random = Random.Range(1, 3); //Random para elegir un eje y rotarlo 90 grados respecto al primer vector.
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

        vectorCross = ProductCrossVector3(vector, vectorRotated); //Producto perpendicular respecto a los otros dos vectores
    }

    void Update()
    {
        
    }
}

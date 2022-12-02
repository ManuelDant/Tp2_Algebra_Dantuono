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
    }

    void Update()
    {
        
    }
}

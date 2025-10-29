using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform destinationPortal1;
    public Transform destinationPortal2;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (gameObject.name == "TP 1")
        {
            col.transform.position = destinationPortal2.position;
        }
        if (gameObject.name == "TP 2")
        {
            col.transform.position = destinationPortal1.position;
        }
    }
}

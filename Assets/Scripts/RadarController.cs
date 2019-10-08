using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarController : MonoBehaviour
{
    public int RadarRadius;
    public string DetectableObjectTag;

    public void CheckForVisibleObjects()
    {
        Collider[] objectColliders = Physics.OverlapSphere(this.transform.position, this.RadarRadius);
        for (int index = 0; index <= objectColliders.Length - 1; index++)
        {
            GameObject colliderObject = objectColliders[index].gameObject;
            if (colliderObject.tag == this.DetectableObjectTag)
            {
                RaycastHit hit;
                bool hitOccurred = Physics.Raycast(this.transform.position, colliderObject.transform.position.normalized, out hit);
                Debug.DrawRay(this.transform.position, hit.point, Color.blue);
                if (hitOccurred && hit.transform.gameObject.tag == this.DetectableObjectTag)
                {
                    // Do whatever you need to do with the resulting information
                    // here if the condition succeeds.
                    print("hmm");
                }
            }
        }
    }

    public void Update()
    {
        this.CheckForVisibleObjects();
    }
}

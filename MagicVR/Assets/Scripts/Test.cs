using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody), typeof(XRGrabInteractable))]
public class Test : MonoBehaviour
{
    Rigidbody rigidbody;
    XRGrabInteractable grab = null;

    public void onSelect()
    {
        /*
        rigidbody = GetComponent<Rigidbody>();
        grab = GetComponent<XRGrabInteractable>();

        Vector3 direction = gameObject.transform.position - grab.firstInteractorSelecting.transform.position;

        rigidbody.AddForce(direction * grab.GetDistanceSqrToInteractor(grab.firstInteractorSelecting), ForceMode.VelocityChange);
        */
        LineRenderer renderer = gameObject.AddComponent<LineRenderer>();
        List<Vector3> positions = new(renderer.positionCount);

        for (int i = 0; i < renderer.positionCount; i++)
        {
            positions[i] = renderer.GetPosition(i);
        }
    }
}
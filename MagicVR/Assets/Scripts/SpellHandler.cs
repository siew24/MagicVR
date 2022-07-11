using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellHandler : MonoBehaviour
{
    public Transform drawPoint;

    private List<LineRenderer> renderers = new();
    private Vector3 previousPoint;

    private double minDistance = 0.0001f;

    private Coroutine updateFunction = null;
    private int pointCount = 0;

    void AddPoint(Vector3 point)
    {
        renderers[renderers.Count - 1].positionCount++;
        renderers[renderers.Count - 1].SetPosition(renderers[renderers.Count - 1].positionCount - 1, point);

        previousPoint = point;
    }

    IEnumerator LineUpdate()
    {
        while (true)
        {
            if (Vector3.Distance(drawPoint.position, previousPoint) >= minDistance)
            {
                AddPoint(drawPoint.position);
            }

            yield return null;
        }
        yield return null;
    }

    public void onActivate()
    {
        GameObject go = new($"LineRenderer_Left_{renderers.Count}");
        go.transform.position = transform.position;
        go.transform.rotation = transform.rotation;

        renderers.Add(go.AddComponent<LineRenderer>());

        renderers[renderers.Count - 1].startWidth = 0.010f;
        renderers[renderers.Count - 1].endWidth = 0.010f;
        renderers[renderers.Count - 1].useWorldSpace = true;
        renderers[renderers.Count - 1].material = new(Shader.Find("Standard"));
        renderers[renderers.Count - 1].material.name = $"Material_Left_{renderers.Count}";
        renderers[renderers.Count - 1].material.color = Color.black;
        renderers[renderers.Count - 1].material.color = Color.black;
        renderers[renderers.Count - 1].positionCount = 1;
        renderers[renderers.Count - 1].SetPosition(0, drawPoint.position);

        updateFunction = StartCoroutine(LineUpdate());
    }

    public void onDeactivate()
    {
        if (updateFunction != null)
        {
            StopCoroutine(updateFunction);
            updateFunction = null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellHandler : MonoBehaviour
{
    public Transform drawPoint;
    public Text text;

    private List<LineRenderer> renderers = new();
    private Vector3 previousPoint;

    private double minDistance = 0.0001f;

    private GestureRecognition gestureRecognition = new();

    private Coroutine updateFunction = null;
    private int pointCount = 0;

    void Awake()
    {
        gestureRecognition.loadFromFile("Assets/spells.dat");
    }

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
                gestureRecognition.contdStrokeQ(drawPoint.transform.position, drawPoint.transform.rotation);

                AddPoint(drawPoint.position);

                previousPoint = drawPoint.position;
            }

            yield return null;
        }
        yield return null;
    }

    public void onActivate()
    {
        GameObject go = new($"LineRenderer_{renderers.Count}");

        LineRenderer localRenderer = go.AddComponent<LineRenderer>();

        renderers.Add(localRenderer);

        localRenderer.material = new(Shader.Find("Standard"));
        localRenderer.material.name = $"LineRenderer_material_{renderers.Count}";
        localRenderer.material.color = Color.black;
        localRenderer.startWidth = 0.01f;
        localRenderer.endWidth = 0.01f;

        localRenderer.positionCount = 1;

        gestureRecognition.startStroke(Camera.main.transform.position, Camera.main.transform.rotation);
        gestureRecognition.contdStrokeQ(drawPoint.position, drawPoint.rotation);

        localRenderer.SetPosition(0, drawPoint.position);

        previousPoint = drawPoint.position;

        updateFunction = StartCoroutine(LineUpdate());
    }

    public void onDeactivate()
    {
        if (updateFunction != null)
        {
            StopCoroutine(updateFunction);
            updateFunction = null;

            int gestureId = gestureRecognition.endStroke();

            if (gestureId < 0)
                text.text = $"Spell Recognized: Could not detect spell";
            else
                text.text = $"Spell Recognized: {gestureRecognition.getGestureName(gestureId)}";

            Destroy(renderers[renderers.Count - 1].gameObject);
            renderers.RemoveAt(renderers.Count - 1);
        }
    }
}

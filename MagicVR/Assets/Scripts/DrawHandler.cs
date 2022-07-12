using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Utilities;

public class DrawHandler : MonoBehaviour
{
    public bool isSpecialStaff = false;

    public Transform drawPoint;

    [Header("Events")]
    public SpellEvent onSpellIdentifiedEvent;
    public TeleportIdentificationEvent onTeleportIdentifiedEvent;

    [Header("Listeners")]
    public BaseGameEventListener onTeleportPlatformListener;

    [Header("Assets")]
    public DefaultAsset spellClassificationAsset;
    public DefaultAsset teleportClassificationAsset;

    private List<LineRenderer> renderers = new();
    private Vector3 previousPoint;

    private double minDistance = 0.00001f;

    private GestureRecognition spellRecognition = new();
    private GestureRecognition teleportRecognition = new();

    private Coroutine updateFunction = null;
    private int pointCount = 0;

    private bool isOnTeleportPlatform = false;


    void Awake()
    {
        spellRecognition.loadFromFile(AssetDatabase.GetAssetPath(spellClassificationAsset));

        if (!isSpecialStaff)
            teleportRecognition.loadFromFile(AssetDatabase.GetAssetPath(teleportClassificationAsset));
    }

    void Start()
    {
        onTeleportPlatformListener.Response.AddListener(() => isOnTeleportPlatform = !isOnTeleportPlatform);
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
                if (!isOnTeleportPlatform)
                    spellRecognition.contdStrokeQ(drawPoint.position, drawPoint.rotation);
                else
                    teleportRecognition.contdStrokeQ(drawPoint.position, drawPoint.rotation);

                AddPoint(drawPoint.position);

                previousPoint = drawPoint.position;
            }

            yield return null;
        }
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

        if (!isOnTeleportPlatform)
        {
            spellRecognition.startStroke(Camera.main.transform.position, Camera.main.transform.rotation);
            spellRecognition.contdStrokeQ(drawPoint.position, drawPoint.rotation);
        }
        else
        {
            teleportRecognition.startStroke(Camera.main.transform.position, Camera.main.transform.rotation);
            teleportRecognition.contdStrokeQ(drawPoint.position, drawPoint.rotation);
        }

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

            int gestureId = !isOnTeleportPlatform ? spellRecognition.endStroke() : teleportRecognition.endStroke();

            if (gestureId < 0)
                Debug.Log($"Could not detect gesture.");
            else
            {
                string name = !isOnTeleportPlatform ? spellRecognition.getGestureName(gestureId) : teleportRecognition.getGestureName(gestureId);
                Debug.Log($"Gesture Recognized: {name}");


                if (!isOnTeleportPlatform)
                    if (!isSpecialStaff)
                        onSpellIdentifiedEvent.Raise(name switch
                        {
                            "fireball" => Spell.FireBall,
                            /*"explosion" => Spell.Explosion, // This is only when the staff is special */
                            "barrier" => Spell.Barrier,
                            "electrify" => Spell.Electrify,
                            "levitation" => Spell.Levitation,
                            _ => Spell.Unknown,
                        }
                        );
                    else onSpellIdentifiedEvent.Raise(Spell.Explosion);
                else
                    onTeleportIdentifiedEvent.Raise(name switch
                    {
                        "outside" => Teleport.Outside,
                        "0" => Teleport.Zero,
                        "1" => Teleport.One,
                        "2" => Teleport.Two,
                        "3" => Teleport.Three,
                        _ => Teleport.Outside
                    });

            }

            Destroy(renderers[renderers.Count - 1].gameObject);
            renderers.RemoveAt(renderers.Count - 1);
        }
    }
}

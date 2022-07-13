using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utilities;

public class Test : MonoBehaviour
{
    [Header("Events")]
    public BaseGameEvent onTeleportPlatformEvent;

    [Header("Listeners")]
    public OnTeleportIdentificationEventListener teleportIdentificationEventListener;

    [Header("Spawn Locations")]
    public Transform outside;
    public Transform inside;
    public Transform areaOne;
    public Transform areaTwo;
    public Transform areaThree;

    [Header("Spell Prefabs")]
    public GameObject fireballPrefab;
    public GameObject barrierPrefab;
    public GameObject electrifyPrefab;
    public GameObject levitatePrefab;

    // EXPLOSION
    public GameObject fireAuraPrefab;
    public GameObject explosionPrefab;

    [Header("Spell Configs")]
    public float barrierDuration = 3f;
    public float electrifyDuration = 2f;
    public float levitateDuration = 10f;
    public float explosionDuration = 10f;

    [Header("Self References")]
    public CharacterController characterController;


    private Coroutine barrierReference = null;
    private Coroutine electrifyReference = null;
    private Coroutine levitateReference = null;
    private Coroutine explosionReference = null;

    // Start is called before the first frame update
    void Start()
    {
        teleportIdentificationEventListener.Response.AddListener(onTeleportCasted);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void onTeleportCasted(Teleport teleport)
    {
        switch (teleport)
        {
            case Teleport.Outside:
                transform.position = outside.position;
                break;
            case Teleport.Zero:
                transform.position = inside.position;
                break;
            case Teleport.One:
                transform.position = areaOne.position;
                break;
            case Teleport.Two:
                transform.position = areaTwo.position;
                break;
            case Teleport.Three:
                transform.position = areaThree.position;
                break;
            default:
                break;
        }

        onTeleportPlatformEvent.Raise();
    }

    public void onSpellCasted(Spell spell)
    {
        Vector3 forward = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);

        // TODO: Add Spell Effects at here
        switch (spell)
        {
            case Spell.FireBall:
                GameObject fireball = Instantiate(fireballPrefab, Vector3.zero, new());

                fireball.transform.position = transform.position;
                fireball.transform.forward = forward;
                break;
            case Spell.Explosion:
                if (explosionReference == null)
                    explosionReference = StartCoroutine(ExplosionUpdate(Instantiate(explosionPrefab, new Vector3(225.550003f, -4.291534e-06f, 231.649994f), new())));
                break;
            case Spell.Barrier:
                if (barrierReference == null)
                    barrierReference = StartCoroutine(BarrierUpdate(Instantiate(barrierPrefab, transform.position, new())));
                break;
            case Spell.Electrify:
                if (electrifyReference == null)
                {
                    GameObject electrify = Instantiate(electrifyPrefab, Vector3.zero, new());

                    electrify.transform.position = transform.position;
                    electrify.transform.forward = forward;

                    electrifyReference = StartCoroutine(ElectifyUpdate(electrify));
                }
                break;
            case Spell.Levitation:
                if (levitateReference == null)
                    levitateReference = StartCoroutine(LevitationUpdate(Instantiate(levitatePrefab, transform.position - new Vector3(0, 1, 0), new())));
                break;
            default:
                break;
        }
    }

    IEnumerator BarrierUpdate(GameObject barrier)
    {
        float duration = 0;
        while (true)
        {
            duration += Time.deltaTime;

            if (duration >= barrierDuration)
                break;

            yield return null;
        }

        Destroy(barrier);
        barrierReference = null;
    }

    IEnumerator ElectifyUpdate(GameObject electrify)
    {
        float duration = 0;
        while (true)
        {
            duration += Time.deltaTime;

            if (duration >= electrifyDuration)
                break;

            yield return null;
        }

        Destroy(electrify);
        electrifyReference = null;
    }

    IEnumerator LevitationUpdate(GameObject levitate)
    {
        float originalY = transform.position.y;
        float duration = 0;
        while (true)
        {
            transform.position = new Vector3(transform.position.x, originalY + 0.1f, transform.position.z);

            levitate.transform.position = transform.position - new Vector3(0, 1, 0);

            duration += Time.deltaTime;

            if (duration >= levitateDuration)
                break;

            yield return null;
        }

        Destroy(levitate);
        levitateReference = null;
    }

    IEnumerator ExplosionUpdate(GameObject explosion)
    {
        GameObject go = Instantiate(fireAuraPrefab, new Vector3(225.550003f, 4.53000021f, 231.649994f), new());

        float duration = 0f;
        while (true)
        {
            duration += Time.deltaTime;
            if (duration >= explosionDuration)
                break;

            yield return null;
        }

        Destroy(explosion);
        Destroy(go);
        explosionReference = null;
    }
}

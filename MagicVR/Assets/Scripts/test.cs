using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utilities;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onSpellCasted(Spell spell)
    {
        // TODO: Add Spell Effects at here
        switch (spell)
        {
            case Spell.FireBall:
                break;
            case Spell.Explosion:
                break;
            case Spell.Barrier:
                break;
            case Spell.Electrify:
                break;
            case Spell.Levitation:
                break;
            default:
                break;
        }
    }
}

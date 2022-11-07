using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Neprijatelji će imati više stanja (traženje, hodanje i napad)
    //  Promenjiva tima enum sadrži listu stanja
    public enum State
    {
        LOOKFOR,
        GOTO,
        ATTACK
    };
    // Trenutno stanje može da ima samo jednu vrednost iz liste enum
    public State currentState;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Neprijatelji će imati više stanja (traženje, hodanje i napad)
    //  Promenjiva tima enum sadrži listu stanja
    public enum State
    {
        // Navodimo faze kroz koje moramo da prođemo
        LOOKFOR,
        GOTO,
        ATTACK,
    };

    // Trenutno stanje može da ima samo jednu vrednost iz liste enum
    public State currentState;

    public float speed = .5f;

    // Potrebna je distanca koja okida napad, kada se igrač približi neprijatelju
    public float GoToDistance = 13;
    public float AttackDistance = 4;
    public Transform Target;
    public string PlayerTag = "Player";

    // Definišemo korutinu
    IEnumerator Start()
    {
        // True je u koliko je skripta omogućena
        while (true)
        {
            // Definišemo stanja pomoću switch naredbe
            switch (currentState)
            {
                case State.LOOKFOR:
                    LookFor();
                    break;

                case State.GOTO:
                    GoTo();
                    break;

                case State.ATTACK:
                    Attack();
                    break;
            }
            // Pauziramo ovu korutinu
            yield return 0;
        }
    }

    // Potrebno je definisati metode za izvršavanje stanja

    void LookFor()
    {
        Debug.Log("Test1");
    }

    void GoTo()
    {
        Debug.Log("Test2");
    }

    void Attack()
    {
        Debug.Log("Test3");
    }
}

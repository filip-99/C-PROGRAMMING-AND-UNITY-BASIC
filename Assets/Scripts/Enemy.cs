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
    public float goToDistance = 13;
    public float attackDistance = 4;
    public Transform target;
    public string PlayerTag = "Player";
    public float attackTimer = 2;
    private float curTime;
    private Player playerScript;

    // Definišemo korutinu
    IEnumerator Start()
    {
        // Potrebno je da postavimo poziciju za target promenjivu, da bude ista kao kod igrača
        target = GameObject.FindGameObjectWithTag(PlayerTag).transform;

        // Zadajemo vreme koliko će dugo napad da traje
        curTime = attackTimer;

        if (target != null)
        {
            // Potrebna nam je referenca na skriptu od igrača
            playerScript = target.GetComponent<Player>();
        }

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
        // Proveravamo udaljenost našeg igrača
        // U koliko je pozicija igrača od neprijatelja manja od zadate udaljenosti, okida se prelaz na sledeće stanje
        if (Vector3.Distance(target.position, transform.position) < goToDistance)
        {
            currentState = State.GOTO;
        }
    }

    void GoTo()
    {
        Debug.Log("Test2");

        // Potrebno je da nateramo kocku da se okrene ka igraču kad ga juri
        transform.LookAt(target);

        // Uticaćemo na z osu
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        RaycastHit buddy;

        if (Physics.Raycast(transform.position, fwd, out buddy))
        {
            if (buddy.transform.tag != PlayerTag)
            {
                currentState = State.LOOKFOR;
                return;
            }
        }


        // U koliko je distanca između igrača i neprijatelja veća od zadate vrednosti neprijatelj kreće ka igraču
        if (Vector3.Distance(target.position, transform.position) > attackDistance)
        {
            // Potrebno je da neprijatelj prati igrača tj.target poziciju pod određenom brzinom
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        // U koliko uslov nije ispunjen okinuće se sledeće stanje
        else
        {
            currentState = State.ATTACK;
        }
    }

    void Attack()
    {
        Debug.Log("Test3");

        transform.LookAt(target);

        curTime = curTime - Time.deltaTime;

        // Kada se odbroji vreme napada sledeći uslov biće ispunjen
        if (curTime < 0)
        {
            // Oduzeće se zdravlje igraču
            playerScript.health--;

            curTime = attackTimer;
        }

        // Kada se udaljimo dovoljno od neprijatelja vratiće se na stanje GOTO
        if (Vector3.Distance(target.position, transform.position) > attackDistance)
        {
            currentState = State.GOTO;
        }
    }
}

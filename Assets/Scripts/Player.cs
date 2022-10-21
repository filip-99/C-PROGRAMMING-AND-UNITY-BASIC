using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Uvodimo promenjive
    // Prva stvar koju želimo jeste da se naša kocka kreće
    public float speed = 10;

    // Potrebno je odrediti i gravitaciju
    public float gravity = 10;

    // Potrebno je da imamo brzinu koju će mo da damo krutom telu igrača
    // Brzina se povećava određeno vreme, kada se pokrenemo i ne prelazi zadatu brzinu tj. vrednost promenjive speed
    public float maxVelocityChange = 10;

    private bool dead;
    public int health;
    private Transform PlayerTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

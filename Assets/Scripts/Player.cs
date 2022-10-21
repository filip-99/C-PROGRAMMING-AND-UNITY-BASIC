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

    // Imamo poziciju igrača
    private Transform PlayerTransform;

    // Imamo referencu na kruto telo igrača
    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        // Potrebno je da pristupimo komponenti transform tj. da možemo da je kontrolišemo uz pomoć promenjive PlayerTransform
        PlayerTransform = GetComponent<Transform>();
        // Takođe nije greška ni ovako da se uradi
        // PlayerTransform = gameObject.transform;

        // Takođe promenjivoj _rigidbody dodeljujemo da može upravljati komponentom krutog tela
        _rigidbody = GetComponent<Rigidbody>();

        // U komponenti Rigidbody isključićemo opciju Use Gravity
        _rigidbody.useGravity = false;

        // Blokiraćemo rotiranje playera tj. kocke, tako da kada pada ne može da se pomera u stranu
        _rigidbody.freezeRotation = true;
    }

    // Bez obzira na kašnjenje, fiksno ažuriranje će uvek biti isto
    void FixedUpdate()
    {
        // Sledećim kodom izvršićemo rotaciju igrača
        // Rotaciju vršimo po y osi, dok x i z osa ostaju nepromenjene
        // Pošto želimo da se rotiramo uz pomoć dugmića levo i desno, koristimo horizontalnu osu
        PlayerTransform.Rotate(0,Input.GetAxis("Horizontal"),0);
    }
}

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
        // Time.deltaTime - vreme koje je potrebno da se ažurira novi frejm (to je vrv 0,nešto)
        PlayerTransform.Rotate(0, Input.GetAxis("Horizontal"), 0);

        // Definišemo brzinu
        // Vector3 predstavlja "kretanje" na 3 ose x, y i z. Ovde je zadata vrednost 1 na z osi
        Vector3 targetVelocity = new Vector3(0, 0, Input.GetAxis("Vertical"));
        // Promenjivoj targetVelocity dodeljujemo poziciju tj transform komponentu na koju će mo direktno da utičemo 
        targetVelocity = PlayerTransform.TransformDirection(targetVelocity);
        // Pošto je mala vrednost brzine pomnožićemo je sa 10 (10*1=10)
        targetVelocity *= 10;

        // Kada imamo definisano brzinu kretanja njom će mo potisnuti naše kruto telo dodeljeno playeru
        _rigidbody.AddForce(targetVelocity);
        // Uticaćemo na brzinunašeg krutog tela dodeljeno igraču
        Vector3 _velocity = _rigidbody.velocity;
        // Ispisaćemo brzinu
        print(_velocity);
    }
}

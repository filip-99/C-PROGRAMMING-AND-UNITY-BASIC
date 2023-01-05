using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Uvodimo promenjive
    // Prva stvar koju želimo jeste da se naša kocka kreće
    public float speed = 20;

    // Potrebno je odrediti i gravitaciju
    public float gravity = 10;

    // Potrebno je da imamo brzinu koju će mo da damo krutom telu igrača
    // Brzina se povećava određeno vreme, kada se pokrenemo i ne prelazi zadatu brzinu tj. vrednost promenjive speed
    public float maxVelocityChange = 10;

    // U koliko se nalazimo na zemlji biće true
    private bool grounded;

    // Uvodimo promenjivu koja predstavlja visinu skoka
    public float jumpHeight = 2;


    private bool dead;


    public int health;

    // Imamo poziciju igrača
    private Transform PlayerTransform;

    // Imamo referencu na kruto telo igrača
    private Rigidbody _rigidbody;

    // Uvodimo promenjivu koja broji pokupljene novčiće
    public int points = 0;





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
        // Vector3 predstavlja "kretanje" na 3 ose x, y i z.
        // Uticaćemo na z vektor tj. osu, jer kamera daje prikaz igraču da vidi ispred z ose kocke, pa će na dugme w da se kreće unapred
        // Koristimo vertikalnu osu, jer želimo da se krećemo po z osi na dugmiće W i S 
        Vector3 targetVelocity = new Vector3(0, 0, Input.GetAxis("Vertical"));
        // Promenjivoj targetVelocity dodeljujemo poziciju tj. transform komponentu na koju će mo direktno da utičemo 
        targetVelocity = PlayerTransform.TransformDirection(targetVelocity);
        // Pošto je mala vrednost brzine pomnožićemo je sa 10 (10*1=10)
        targetVelocity *= speed;

        // Setovali smo da brzina kretanja krutog tela igrača bude fiksirana na 10
        Vector3 velocityChange = targetVelocity - _rigidbody.velocity;

        // STEZANJE:

        // Ograničavamo - "stežemo" brzinu na vektoru x
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        // Ograničavamo - "stežemo" brzinu na vektoru z
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        // Pravilo je da vektore koje ne koristimo njihovu brzinu setujemo na 0
        velocityChange.y = 0;

        // Kada smo jasno definisali parametre brzine tj. stegnuli, ovu vrednost dodeljujemo brzini krutog tela
        // ForceMode.VelocityChange - znači nemamo sile i neće da nam se igrač kliza po podlozi
        _rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
        // Brzina treba da bude 10 pa štampamo u da bi proverili u konzoli
        //print(_rigidbody.velocity);

        // Ako smo pritisnuli dugme za skok, onda će se izvršiti ledeći kod
        if (Input.GetButton("Jump") && grounded == true)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, CalculateJump(), _rigidbody.velocity.z);
        }

        // Dodajemo gravitaciju na naš objekat igrača
        // Sila gravitacije biće negativna vrednost (JER želimo da pada i y osa će ići u minus) pomnožena sa masom našeg krutog tela
        _rigidbody.AddForce(new Vector3(0, -gravity * _rigidbody.mass, 0));

        // grounded je flase podrazumevano
        grounded = false;
        //print(grounded);

    }

    void Update()
    {
        if (health < 1)
        {

        }
    }

    // Funkcija za izračunavanje skoka
    float CalculateJump()
    {
        // Sobzirom da imamo gravitaciju koristićemo sledeću formulu, kako bi dobili tačnu vrednost skoka od zemlje tj. jumpHeight
        return Mathf.Sqrt(2*jumpHeight*gravity);
    }

    // Funkcija koja se okida kada objekat igrača dođe u interakciju sa drugim objektima
    void OnCollisionStay()
    {
        // U dodiru sa zemljom okida se metoda i setuje vrednost na true
        grounded = true;
    }

    // Potrebno je da sakupljamo novčiće, pa setujemo triger
    private void OnTriggerEnter(Collider other)
    {
        // U koliko je igrač došao u dodir sa novčićem izvršava se:
        if(other.tag == "Coin")
        {
            // Kada pokupimo novčić igrač dobija 5 poena
            points += 5;

            // Kada pokupimo novčić uništićemo ga
            Destroy(other.gameObject);
        }
    }
}

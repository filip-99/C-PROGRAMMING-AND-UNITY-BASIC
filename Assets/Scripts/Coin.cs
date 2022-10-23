using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Podešavamo koliko brzo će se kretati novčići
    public float speed = 5;
    // Pošto želimo da generešemo nasumičan broj kreiramo sledeću promenjivu
    public int randomNumber;

    // Start is called before the first frame update
    void Start()
    {
        // Na početku generisaće se broj od 1 do zadate brzine u unityu. Ovom brzinom će da se vrte novčići
        randomNumber = Random.Range(1, (int)speed);
    }

    // Update is called once per frame
    void Update()
    {
        // Rotacija novčića
        gameObject.transform.Rotate(Vector3.up * randomNumber);
        // Moguće je i na podrazumevani način da se uradi rotacija novčića
        // gameObject.transform.Rotate(new Vector3(0, randomNumber, 0));
    }
}

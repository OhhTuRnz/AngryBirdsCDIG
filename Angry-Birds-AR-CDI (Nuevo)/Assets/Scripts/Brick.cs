using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Clase para representar un ladrillo de la torre
//
public class Brick : MonoBehaviour
{
    public List<Material> colores;
    private Vector3 origen;

    public bool derribado;

    private Rigidbody rb;
    private bool hasBeenHit = false;

    private void Start()
    {
        origen = transform.position;
        derribado = false;
        Shuffle(colores);
        GetComponent<MeshRenderer>().material = colores[Random.Range(0, colores.Count)];
        rb = GetComponent<Rigidbody>();

        int layerIndex = gameObject.layer;
        Debug.Log(layerIndex + " Este es el layer del brick");
    }

    void Update() {
/*
        // Check if the brick is not touching any other brick from beneath
        Collider[] colliders = Physics.OverlapSphere(transform.position - Vector3.up * 0.55f, 0.05f); // Check for colliders in a small sphere below the brick
        bool touchingBrick = false;
        foreach (Collider collider in colliders)
        {
            if (!collider.CompareTag("New") && !collider.CompareTag("Hit"))
            {
                touchingBrick = true;
                break;
            }
        }

        // If the brick is not touching any other brick from beneath, set isKinematic to false
        if (!touchingBrick && rb.isKinematic)
        {
            rb.isKinematic = false;
        }
*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hit") && collision.relativeVelocity.magnitude > 10f && !hasBeenHit)
        {
            rb.constraints = RigidbodyConstraints.None;
            hasBeenHit = true;
        }
        // Check if the brick is not touching any other brick from beneath
        Collider[] colliders = Physics.OverlapSphere(transform.position - Vector3.up * 0.55f, 0.05f); // Check for colliders in a small sphere below the brick
        bool touchingBrick = false;
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("New") && collider.CompareTag("Hit") && !hasBeenHit)
            {
                touchingBrick = true;
                break;
            }
        }

        // If the brick is not touching any other brick from beneath, set isKinematic to false
        if (!collision.gameObject.CompareTag("Suelo") && !touchingBrick && rb.constraints != RigidbodyConstraints.None)
        {
            rb.constraints = RigidbodyConstraints.None;
        }

/*
        Debug.Log("Colision: " + collision.gameObject.name);
        if (collision.gameObject.tag == "Suelo")
        {
            if (derribado == false) {
                derribado = true;
                // Incrementa el número de derribos
                Torre torre = Torre.instance;
                torre.brickDerribado(this.gameObject);

                // Elimina el objeto de la escena
                this.gameObject.SetActive(false);
//                Destroy(this);
            }
        }
*/
    }

    /*
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ha entrado en el trigger " + other.gameObject.tag);
        if (other.CompareTag("Hit"))
        {
            //Debug.Log("Desactivo kinematic");
            this.tag = "Hit";
            rb.constraints = RigidbodyConstraints.None;
        }
    }
    */

    //
    // Marca el ladrillo como derribado si se recibe el evento "On Trigger Exit" del trigger usado para la detección de derribos
    // e invoca al método "brickDerribado" de la torre para verificar si se cumple la condición de fin de juego.
    //
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Trigger")){
            if (derribado == false) {
                derribado = true;
                // Incrementa el número de derribos
                Torre torre = Torre.instance;
                torre.brickDerribado(this.gameObject);
            }
            Debug.Log("Ha salido del trigger " + other.gameObject.tag);
        }
    }

    public static void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

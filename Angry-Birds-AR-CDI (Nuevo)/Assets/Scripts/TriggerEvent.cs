using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int layerIndex = gameObject.layer;
        Debug.Log(layerIndex + " Este es el layer del trigger");
    }

    // Update is called once per frame
    void Update()
    {

    }

    //
    // Detecta evento "On Trigger Exit"
    //
    // Genera sólo trazas de depuración pues la lógica para procesar este tipo de eventos
    // está en la clase "Brick"
    //
    void OnTriggerExit (Collider collider) {
        Debug.Log("OnTriggerExit: " + collider.gameObject.name + " tag: " + collider.gameObject.tag);
        if (collider.gameObject.CompareTag("New") || collider.gameObject.CompareTag("Hit"))
        {
            Debug.Log("OnTriggerExit: " + collider.gameObject.name + " tag: " + collider.gameObject.tag);
            collider.gameObject.tag = "Derribado";
        }
    }
}

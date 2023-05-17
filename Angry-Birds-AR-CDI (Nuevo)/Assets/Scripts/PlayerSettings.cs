using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//
// Scriptable object para guardar los parámetros del juego:
//
// - Altura de la torre cuadrada
// - Lado de la torre cuadrada
// - Altura de la torre rectangular
// - Lado corto de la torre rectangular
// - Lado largo de la torre rectangulear
// - Porcentaje de derribos para terminar el juego
//
// Estos atributos se guardan de forma persistente aún si termina la aplicaci´ón
//

[CreateAssetMenu]
public class PlayerSettings : ScriptableObject
{
    public int altoTC;
    public int ladoTC;
    public int altoTR;
    public int ladoCortoTR;
    public int ladoLargoTR;
    public int porcentajeDerribos;   

    // Guardar en el objeto scriptable los valores de los atributos introducidos por la interfaz.
    //
    // Actualiza los atributos del objeto si el usuario introdujo nuevos valores en los elementos de entrada (el valor de text es no nulo).
    //
    public void guardarPerfil(InputField IFaltoTC, InputField IFlado, InputField IFaltoTR, InputField IFladoCorto, InputField IFladoLargo, InputField IFporcentajeDerribos) {
        Debug.Log("PlayerSettings.leePerfil");
        Text text;
        // Altura Torre Cuadrada
        text = IFaltoTC.transform.Find("Text").GetComponent<Text>();
        if (! string.IsNullOrEmpty(text.text)) {
            altoTC = int.Parse(text.text);
            if (altoTC < 1)
                altoTC = 1;
        }
        // Lado Torre Cuadrada
        text = IFlado.transform.Find("Text").gameObject.GetComponent<Text>();
        if (! string.IsNullOrEmpty(text.text)) {
            ladoTC = int.Parse(text.text);
            if (ladoTC < 1)
                ladoTC = 1;
        }
        // Altura Torre Rectangular
        text = IFaltoTR.transform.Find("Text").gameObject.GetComponent<Text>();
        if (! string.IsNullOrEmpty(text.text)) {
            altoTR = int.Parse(text.text);
            if (altoTR < 1)
                altoTR = 1;
        }
        // Lado Corto Torre Rectangular
        text = IFladoCorto.transform.Find("Text").gameObject.GetComponent<Text>();
        if (! string.IsNullOrEmpty(text.text)) {
            ladoCortoTR = int.Parse(text.text);
            if (ladoCortoTR < 1)
                ladoCortoTR = 1;
        }
        // Lado Largo Torre Rectangular
        text = IFladoLargo.transform.Find("Text").gameObject.GetComponent<Text>();
        if (! string.IsNullOrEmpty(text.text)) {
            ladoLargoTR = int.Parse(text.text);
            if (ladoLargoTR < 1)
                ladoLargoTR = 1;
        }
        // Porcentaje de derribos
        text = IFporcentajeDerribos.transform.Find("Text").gameObject.GetComponent<Text>();
        if (! string.IsNullOrEmpty(text.text)) {
            porcentajeDerribos = int.Parse(text.text);
            if (porcentajeDerribos < 0)
                porcentajeDerribos = 0;
            else if (porcentajeDerribos > 100)
                porcentajeDerribos = 100;
            text.text = porcentajeDerribos.ToString();
        }
    }

    // Lee los atributos del objeto scriptable y los asigna a los "placeholders" de la interfaz.
    //
    public void leerPerfil(InputField IFaltoTC, InputField IFlado, InputField IFaltoTR, InputField IFladoCorto, InputField IFladoLargo, InputField IFporcentajeDerribos) {
        Debug.Log("PlayerSettings.escribePerfil: " + altoTC.ToString());
        Text placeholder;
        placeholder = IFaltoTC.placeholder.GetComponent<Text>();
        placeholder.text = altoTC.ToString();

        placeholder = IFlado.placeholder.GetComponent<Text>();
        placeholder.text = ladoTC.ToString();
        placeholder = IFaltoTR.placeholder.GetComponent<Text>();
        placeholder.text = altoTR.ToString();
        placeholder = IFladoCorto.placeholder.GetComponent<Text>();
        placeholder.text = ladoCortoTR.ToString();
        placeholder = IFladoLargo.placeholder.GetComponent<Text>();
        placeholder.text = ladoLargoTR.ToString();
        placeholder = IFporcentajeDerribos.placeholder.GetComponent<Text>();
        placeholder.text = porcentajeDerribos.ToString();
    }
}

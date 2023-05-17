using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public InputField IFaltoTC, IFlado, IFaltoTR, IFladoCorto, IFladoLargo, IFporcentajeDerribos; 
    public PlayerSettings playerSettings;

    void Awake()
    {
        Debug.Log("GameController.Awake()");
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GameController.Start()");
        playerSettings.leerPerfil(IFaltoTC, IFlado, IFaltoTR, IFladoCorto, IFladoLargo, IFporcentajeDerribos);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Exit(){
        Application.Quit();
    }
    
    public void Jugar(){
        SceneManager.LoadScene("GameScene");
    }

    public void MenuPrincipal(){
        SceneManager.LoadScene("MenuScene");
        playerSettings = Resources.FindObjectsOfTypeAll<PlayerSettings>()[0];
        playerSettings.leerPerfil(IFaltoTC, IFlado, IFaltoTR, IFladoCorto, IFladoLargo, IFporcentajeDerribos);
    }

    public void Guardar() {
        playerSettings.guardarPerfil(IFaltoTC, IFlado, IFaltoTR, IFladoCorto, IFladoLargo, IFporcentajeDerribos);
    }

    public void validaPorcentajeDerribos(string texto) {
        Debug.Log("validaPorcentajeDerribos: " + texto);
        Text text = IFporcentajeDerribos.transform.Find("Text").gameObject.GetComponent<Text>();
        int porcentaje = int.Parse(texto);
        if (porcentaje > 100) {
            porcentaje = 100;
            text.text = porcentaje.ToString();
        }
        else if (porcentaje < 0) {
            porcentaje = 0;
            text.text = porcentaje.ToString();
        }
    }
}

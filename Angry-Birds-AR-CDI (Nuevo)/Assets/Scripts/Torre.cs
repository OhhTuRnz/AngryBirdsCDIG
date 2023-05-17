using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

enum TipoTorre {
    Cuadrada,
    Rectangular
}

public class Torre : MonoBehaviour
{
    private TipoTorre tipo;

    private int altoTC;
    private int lado;
    private int altoTR;
    private int ladoCorto;
    private int ladoLargo;
    private int porcentajeDerribos;

    public bool visible;
    public bool iniciado = false;
    public bool terminado = false;
    public GameObject brick;
    public GameObject ladrilloTrigger;
    public GameObject peana;
    public GameObject gameOver;
    public GameObject resultados;
    private GameObject colliderDerribos;
    public Material material;
    private List<GameObject> bricks = new List<GameObject>();
    public Text contadorDisparos;
    public Text contadorDerribos;
    public int disparos;
    public int nbricks;
    public int nderribos;

    public static Torre instance;
    public PlayerSettings playerSettings;

    void Awake()
    {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log ("Torre.Start()");
        visible = false;
        iniciado = false;
        terminado = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    //
    // Inicia el juego con los parámetros del "Player Settings" (o los valores de defecto si no existe).
    //
    public void iniciaJuego(int _tipo) {
        if (iniciado)
            return;

        Debug.Log("Game Start");
        iniciado = true;

        // Pon musica del juego
        GameObject.Find("Canvas").GetComponent<AudioSource>().Play();

        // Muestra contadores
        contadorDisparos.gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(true);
        contadorDerribos.gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(true);

        // Lee parámetros
        try {
            altoTC = playerSettings.altoTC;
            lado = playerSettings.ladoTC;
            altoTR = playerSettings.altoTR;
            ladoCorto = playerSettings.ladoCortoTR;
            ladoLargo = playerSettings.ladoLargoTR;
            porcentajeDerribos = playerSettings.porcentajeDerribos;
        }
        catch {
            altoTC = 20;
            lado = 6;
            altoTR = 10;
            ladoCorto = 5;
            ladoLargo = 10;
            porcentajeDerribos = 90;
            Debug.Log("No se ha podido coger el perfil");
        }
        if (_tipo == 0)
            tipo = TipoTorre.Cuadrada;
        else
            tipo = TipoTorre.Rectangular;

        iniciado = true;
    }

    //
    // Finaliza el juego
    //
   public void finalizaJuego(){
        if (!iniciado)
            return;

        Debug.Log("Game Over");
        terminado = true;

        // Para musica del juego
        GameObject.Find("Canvas").GetComponent<AudioSource>().Stop();

        // Muestra ventana de juego finalizado
        Text texto = resultados.GetComponent<Text>();
        texto.text = "Disparos: " + disparos.ToString() + "\n" +
             "Derribos: " + nderribos.ToString();

        // Pon musica de fin de juego
        gameOver.SetActive(true);
        gameOver.GetComponent<AudioSource>().Play();

        // Activa el juego
        gameOver.SetActive(true);
    }

    //
    // Construye una torre cuadrada
    //
    public void ConstruirTorreCuadrada() {
        Debug.Log ("Construir Torre Cuadrada: " + iniciado.ToString() + "/" + terminado.ToString());
        if (iniciado) {
            if (tipo == TipoTorre.Cuadrada) {
                Activar();
            }
            else {
/*
                EditorUtility.DisplayDialog("Mi Angry Birds AR",
                    "Juego ya iniciado con otro tipo de torre",
                    "Aceptar", "");
*/
            }
            return;
        }
        iniciaJuego(0);
        CrearTriggers(altoTC, (lado*2), (lado*2));
        //ConstruirBoxCollider(altoTC, lado, lado);
        //ConstruirTorre(1, lado, lado, peana, 0);
        nbricks = bricks.Count;
//        ConstruirTorre(altoTC, lado, lado, brick, peana.transform.localScale.y + 0.5f);
        ConstruirTorre(altoTC, (lado*2)+1, (lado*2)+1, brick, 0.5f); // Se eleva a la mitad de la altura del ladrillo vacio);
        //ConstruirBoxCollider(altoTC, lado, lado);
        nbricks = bricks.Count - nbricks;
        Debug.Log("nbricks: " + nbricks.ToString());
    }

    public void ConstruirTorreRectangular() {
        if (iniciado) {
            if (tipo == TipoTorre.Rectangular) {
                Activar();
            }
            else {
/*
                EditorUtility.DisplayDialog("Mi Angry Birds AR",
                    "Juego ya iniciado con otro tipo de torre",
                    "Aceptar", "");
*/
            }
            return;
        }
        iniciaJuego(1);
        CrearTriggers(altoTR, (ladoLargo*2), (ladoCorto*2));
        //ConstruirBoxCollider(altoTR, ladoLargo, ladoCorto);
//        ConstruirTorre(1, ladoLargo, ladoCorto, peana, 0);
        nbricks = bricks.Count;
//        ConstruirTorre(altoTR, ladoLargo, ladoCorto, brick, peana.transform.localScale.y + 0.5f);
        ConstruirTorre(altoTR, (ladoLargo*2)+1, (ladoCorto*2)+1, brick, 0.5f); // Se eleva a la mitad de la altura del ladrillo vacio
        nbricks = bricks.Count - nbricks;
        Debug.Log("nbricks: " + nbricks.ToString());
    }

    //
    // Construye una torre rectangular
    //
    public void ConstruirTorre(int alto, int ladoA, int ladoB, GameObject material, float elevacion) {
        string msg1 = "Construir torre: " + iniciado.ToString();
        Debug.Log(msg1);
        string msg = "Construir torre: " + alto.ToString() +
             " x " + ladoA.ToString() + " x " + ladoB.ToString();
        Debug.Log(msg);

        visible = true;
        terminado = false;
        for (int i = 0; i < alto; i++) {
            if (i % 2 == 0) {
                CrearFilaDeLadrillosParAB(i, ladoA, ladoB, material, elevacion);
                CrearFilaDeLadrillosParCD(i, ladoA, ladoB, material, elevacion);
            } else {
                CrearFilaDeLadrillosImparAB(i, ladoA, ladoB, material, elevacion);
                CrearFilaDeLadrillosImparCD(i, ladoA, ladoB, material, elevacion);
            }
        }
    }

    //
    // Genera una representación textutal de la posición de un objeto
    //
    string printPosicion(Transform transform) {
        return ("(" + transform.position.x.ToString() + ", " + transform.position.y.ToString() + ", " + transform.position.z.ToString() + ")");
    }
    
    //
    // Crea fila par del lado AB de la torre (frontal y posterior)
    //
    void CrearFilaDeLadrillosParAB(int fila, int ladoA, int ladoB, GameObject material, float elevacion) {
		int k;
        Vector3 offset = new Vector3(-ladoA/2f, elevacion, -ladoB/2f);
        for (int j = 0; j < ladoA; j = j + 2) {
			if (j+2 <= ladoA) {
				Vector3 posicionA = new Vector3(j, fila, 0);
				GameObject brickA = Instantiate(material, posicionA + offset, Quaternion.identity);
                Debug.Log("Posicion Brick parent (" + gameObject.name + "): " + printPosicion(gameObject.transform));
                if (gameObject.transform.parent != null)
                    Debug.Log("Posicion Brick grand parent (" + gameObject.transform.parent.name + "): " + printPosicion(gameObject.transform.parent.transform));
				brickA.transform.SetParent(gameObject.transform);
                bricks.Add(brickA);
			}
			k = j+(ladoB%2);
			if (k+2 <= ladoA) {
				Vector3 posicionB = new Vector3(k, fila, ladoB-1);
				GameObject brickB = Instantiate(material, posicionB + offset, Quaternion.identity);
				brickB.transform.SetParent(gameObject.transform);
                bricks.Add(brickB);
			}
        }
    }

    //
    // Crea fila impar del lado AB de la torre (frontal y posterior)
    //
    void CrearFilaDeLadrillosImparAB(int fila, int ladoA, int ladoB, GameObject material, float elevacion) {
		int k;
        Vector3 offset = new Vector3(-ladoA/2f, elevacion, -ladoB/2f);
        for (int j = 1; j < ladoA; j = j + 2) {
			if (j+2 <= ladoA) {
				Vector3 posicionA = new Vector3(j, fila, 0);
				GameObject brickA = Instantiate(material, posicionA + offset, Quaternion.identity);
				brickA.transform.SetParent(gameObject.transform);
                bricks.Add(brickA);
			}
			k = j-(ladoB%2);
			if (k+2 <= ladoA) {
				Vector3 posicionB = new Vector3(k, fila, ladoB-1);
				GameObject brickB = Instantiate(material, posicionB + offset, Quaternion.identity);
				brickB.transform.SetParent(gameObject.transform);
                bricks.Add(brickB);
			}
        }
    }

    //
    // Crea fila par del lado CD de la torre (lateral derecho e izquierdo)
    //
    void CrearFilaDeLadrillosParCD(int fila, int ladoA, int ladoB, GameObject material, float elevacion) {
		int k;
        Vector3 offset = new Vector3(-ladoA/2f, elevacion, -ladoB/2f);
        for (int j = 1; j < ladoB; j = j + 2) {
			if (j+2 <= ladoB) {
				Vector3 posicionC = new Vector3(-0.5f, fila, j + 0.5f);
				GameObject brickC = Instantiate(material, posicionC + offset, Quaternion.identity);
				brickC.transform.SetParent(gameObject.transform);
				brickC.transform.Rotate(0f, 90f, 0f);
                bricks.Add(brickC);
			}

			k = j+(ladoA+1)%2;
			if (k < ladoB) {
				Vector3 posicionD = new Vector3(ladoA-1 -0.5f, fila, k - 0.5f);
				GameObject brickD = Instantiate(material, posicionD + offset, Quaternion.identity);
				brickD.transform.SetParent(gameObject.transform);
				brickD.transform.Rotate(0f, 90f, 0f);
                bricks.Add(brickD);
			}
        }
    }

    //
    // Crea fila impar del lado CD de la torre (lateral derecho e izquierdo)
    //
    void CrearFilaDeLadrillosImparCD(int fila, int ladoA, int ladoB, GameObject material, float elevacion) {
		int k;
        Vector3 offset = new Vector3(-ladoA/2f, elevacion, -ladoB/2f);
        for (int j = 0; j < ladoB; j = j + 2) {
			if (j+2 <= ladoB) {
				Vector3 posicionC = new Vector3(-0.5f, fila, j + 0.5f);
				GameObject brickC = Instantiate(material, posicionC + offset, Quaternion.identity);
				brickC.transform.SetParent(gameObject.transform);
				brickC.transform.Rotate(0f, 90f, 0f);
                bricks.Add(brickC);
			}

			k = j+ladoA%2;
			if (k+2 <= ladoB) {
				Vector3 posicionD = new Vector3(ladoA-1 -0.5f, fila, k + 0.5f);
				GameObject brickD = Instantiate(material, posicionD + offset, Quaternion.identity);
				brickD.transform.SetParent(gameObject.transform);
				brickD.transform.Rotate(0f, 90f, 0f);
                bricks.Add(brickD);
			}
        }
    }

    //
    // Activar la torre para que sea visible en la escena e interaccione con su entorno.
    //
    public void Activar(){
        string msg = "Activar torre: " + bricks.Count;
        Debug.Log(msg);

        visible = true;
        foreach (var gameObject in bricks) {
            // Muestra objeto en la escena el objeto de la escena
            this.gameObject.SetActive(true);
        }
    }

    //
    // Desactivar la torre haciéndola invisible en la escena
    //
     public void Desactivar(){
        string msg = "Desactivar torre: " + iniciado.ToString() + "/" + terminado.ToString();
        Debug.Log(msg);

        visible = false;
        foreach (var gameObject in bricks) {
            // Elimina el objeto de la escena
            this.gameObject.SetActive(false);
        }
    }

/*
    public void Clear(){
        return;
        foreach (var objeto in bricks) {
            // Elimina el objeto de la escena
            Destroy(objeto);
        }
        this.gameObject.SetActive(true);
        Destroy(colliderDerribos);
    }
*/

    //
    // Destruye la torre eliminando todos sus ladrillos e reiniciando los contadores a 0.
    //
     public void DestruirTorre(){
        string msg = "Destruir torre";
        Debug.Log(msg);

        visible = false;
        foreach (var gameObject in bricks) {
            Destroy(gameObject);            
        }
        bricks.Clear();
        disparos = 0;
        contadorDisparos.text = "0";
        nderribos = 0;
        contadorDerribos.text = "0 %";
    }

    //
    // Incrementa el n´úmero de disparos realizados.
    //
    public void dispara(){
        Debug.Log ("Torre.dispara(): " + iniciado.ToString() + "/" + terminado.ToString());
        if (visible)
        {
            disparos++;
            contadorDisparos.text = disparos.ToString();
        }
        Debug.Log ("Fin Torre.dispara(): " + iniciado.ToString() + "/" + terminado.ToString());
    } 

    //
    // Se ha detectado que un ladrillo ha sido derribado. Actualiza el contador de ladrillos
    // derribados y finaliza el juego si se alcanza el límite.
    //
    public void brickDerribado(GameObject gameObject){
        Debug.Log("Brick derribado");
        if (terminado)
            return;
        bricks.Remove(gameObject);
        nderribos++;
        int porcentaje = (nderribos * 100) / nbricks;
        contadorDerribos.text = porcentaje.ToString() + " %";
//        Debug.Log("Porcentajes: " + porcentaje.ToString() + " / " + porcentajeDerribos.ToString());
        if (porcentaje >= porcentajeDerribos)
            finalizaJuego();
    }

    //
    // Trazas de depuración al destruir un objeto
    //
    public void OnDestroy() {
        Debug.Log("Torre.OnDestroy llamado");
        var currentStack = new System.Diagnostics.StackTrace(true);
        for (int i=0; i<currentStack.FrameCount; i++) {
            Debug.Log(currentStack.GetFrame(i));
        }
    }

    //
    // Crear los "triggers" usados para la detección de ladrillos derribados.
    //
    void CrearTriggers(int alto, int ladoA, int ladoB)
    { // Posicion es el centro de la estructura en y = 0
        Quaternion rotacionX = new Quaternion(0, 0, 0, 0);
        Quaternion rotacionZ = new Quaternion(0.1f, 0, 0.1f, 0);

        Vector3 offset = new Vector3(-1, 0, -1 + 0.5f);
        Vector3 reduccion = new Vector3(-1.5f, -1.5f, -1.5f);

        Vector3 posicion = new Vector3(0, 0, 0) + offset;


        GameObject trigger1 = Instantiate(ladrilloTrigger, posicion, rotacionX, transform);

        trigger1.transform.localScale += new Vector3((ladoA/2 - 1.8f) * 2, alto - 1, 0);
        trigger1.transform.position += new Vector3(0, (alto/2) - 0.4f, ladoB/2 - 0.1f);

        GameObject trigger2 = Instantiate(ladrilloTrigger, posicion, rotacionX, transform);
        trigger2.transform.localScale += new Vector3((ladoA/2 - 1.8f) * 2, alto - 1, 0);
        trigger2.transform.position += new Vector3(0, (alto/2) - 0.4f, -ladoB/2 + 0.1f);
        
        GameObject trigger3 = Instantiate(ladrilloTrigger, posicion, rotacionZ, transform);
        trigger3.transform.localScale += new Vector3((ladoB/2 - 1.8f) * 2, alto - 1, 0);
        trigger3.transform.position += new Vector3(ladoA/2 - 0.1f, (alto/2) - 0.4f, 0);
        
        GameObject trigger4 = Instantiate(ladrilloTrigger, posicion, rotacionZ, transform);
        trigger4.transform.localScale += new Vector3((ladoB/2 - 1.8f) * 2, alto - 1, 0);
        trigger4.transform.position += new Vector3(-ladoA/2 + 0.1f, (alto/2) - 0.4f, 0);
    }
}
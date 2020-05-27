using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
	public Transform[] backgrounds;				// Array de todos los background y foregrounds que van a tener el efecto parallax
	private float[] parallaxScales;				// Proporcion del movimiento de la camara para mover los items del parallax
	public float smoothing = 1f;				// Establece como de fluido va a ser el parallax. El valor tiene que ser mayor que 0

	private Transform cam;						// Referencia para el transform de la main camara
	private Vector3 prevCamPos;					// Contiene la posicion de la camara en el frame anterior

	// Se llama antes de start
	void Awake()
	{
		cam = Camera.main.transform;			// Estableciendo las referencias de la camara
	}
	
	// Start is called before the first frame update
	void Start()
	{
		
		prevCamPos = cam.position;				// El primer frame previo = primer frame
		
		parallaxScales = new float[backgrounds.Length];
		for (int i = 0; i < backgrounds.Length; i++)
		{
			parallaxScales[i] = (backgrounds[i].position.z) * -1;	// Asigna el valor z del background en su correspondiente posicion dentro del array
		}
	}

	// Update is called once per frame
	void Update()
	{
		for (int i = 0; i < backgrounds.Length; i++)
		{
			float parallax = (prevCamPos.x - cam.position.x) * parallaxScales[i];	// El parallax es lo contrario al movimiento de la camara
			float backgroundTargetPosX = (backgrounds[i].position.x) + parallax;	// Establece una posicion X objetivo, que será la posicion actual mas el parallax
			Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);	// Introduce el nuevo valor de la X en la posicion del background

			backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, (smoothing * Time.deltaTime)); // Efecto de fade entre la posicion actual y la posicion objetivo, utilizando Lerp
		}

		prevCamPos = cam.position;				// Refresca la posicion de la camara a la posicion actual
		
	}
}

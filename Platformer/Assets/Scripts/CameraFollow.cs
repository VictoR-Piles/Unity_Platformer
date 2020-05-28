using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public GameObject target;												// Permite establecer un objetivo el cual la camara debe perseguir
	public Vector2 minCamPos, maxCamPos;									// Establece la posicion minima y maxima de la camara para que no se salga de la escena
	public float smoothTime;

	private Vector2 smoothVelocity;
	
	// ==================== START ====================
	void Start()
	{
		maxCamPos = new Vector2(500, 100);							// Las posiciones maximas se establecen con valores altos por defecto (se puede modificar desde Unity)
	}

	// ==================== UPDATE ====================
	void FixedUpdate()
	{
		float posX = Mathf.SmoothDamp(transform.position.x, target.transform.position.x, ref smoothVelocity.x, smoothTime);	// Suaviza el movimiento horizontal de la camara
		float posY = Mathf.SmoothDamp(transform.position.y, target.transform.position.y, ref smoothVelocity.y, smoothTime);	// Suaviza el movimiento vetical de la camara

		transform.position = new Vector3(									// Cambia la posicion de la camara a la del target, dentro de unos limites
			Mathf.Clamp(posX, minCamPos.x, maxCamPos.x),
			Mathf.Clamp(posY, minCamPos.y, maxCamPos.y),
			transform.position.z);
	}
}

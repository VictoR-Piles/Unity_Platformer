using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Permite a la plataformas trampa caer cuando el Player
 * colisiona con ella, despues la respawnea de nuevo
 */
public class PlataformaTrampa : MonoBehaviour
{
	public float fallDelay = .5f;
	public float respawnDelay = 2f;
	
	private Rigidbody2D rb2d;
	private Vector3 start;
	
	// ==================== START ====================
	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
		start = transform.position;
	}

	// ==================== UPDATE ====================
	void Update()
	{
		
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			Invoke("fall", fallDelay);							            // Llama al metodo fall despues del delay (se puede ajustar desde Unity)
		}
	}

	void fall()
	{
		rb2d.isKinematic = false;											// Cambioa el Rigidbody2D de la plataforma de 'Kinematic' a 'Dynamic', de esta manera se activa su gravedad
		rb2d.freezeRotation = false;										// Permite que la plataforma rote y rebote
		Invoke("respawn", (fallDelay + respawnDelay));	                    // Llama al metodo respawn despues del delay (se puede ajustar desde Unity)
	}

	void respawn()
	{
		transform.position = start;											// Devuelve la plataforma a su posicion incial
		rb2d.isKinematic = true;											// Cambia el Rigidbody2D de la plataforma de 'Dynamic' a 'Kinematic', de esta forma se desactiva la gravedad
		rb2d.velocity = Vector3.zero;										// Cambia la velocidad de la plataforma a 0
		rb2d.freezeRotation = true;											// Congela la rotacion
		rb2d.SetRotation(0);											    // Asegura que la paltaforma va a spawnear sin rotacion
	}
}

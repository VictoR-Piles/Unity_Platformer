using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Permite el movimiento de las plataformas moviles
 * haciendo que la plataforma persiga al target
 */
public class PlatformMovement : MonoBehaviour
{
	public Transform target;										// Permite asignar un objetivc (desde Unity)
	public float speed;												// Permite asignar la velocidad de movimiento de la plataforma (desde Unity)

	private Vector3 start, end;
	
	// ==================== START ====================
	void Start()
	{
		if (target != null)
		{
			target.parent = null;									// Hace que el objeto 'Target' deje de ser hijo de la plataforma durante la ejecución, para que no se mueva junto a ella
			start = transform.position;								// Establece el punto inicial del recorrido
			end = target.position;									// Establece el punto final del recorrido
		}
	}

	// ==================== UPDATE ====================
	void Update()
	{
		
	}

	void FixedUpdate()
	{
		if (target != null)
		{
			float fixedSpeed = (speed * Time.deltaTime);
			transform.position = Vector3.MoveTowards(transform.position, target.position, fixedSpeed);	// Mueve la plataforma de un punto 'A' a un punto 'B' con la velocidad indicada
		}

		if (transform.position == target.position)
		{
			if (transform.position == end)
			{
				target.position = start;							// Cambia la posicion del target para que la plataforma vuelva al punto inicial
			}

			if (transform.position == start)
			{
				target.position = end;								// Cambia la posicion del target para que la plataforma vuelva al punto final
			}
		}
	}
}

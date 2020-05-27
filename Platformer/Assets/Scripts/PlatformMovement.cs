using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
	public Transform target;										// Permite asignar un objetivc (desde Unity)
	public float speed;												// Permite asignar la velocidad de movimiento de la plataforma (desde Unity)
	
	// ==================== START ====================
	void Start()
	{
		if (target != null)
		{
			target.parent = null;
		}
	}

	// ==================== UPDATE ====================
	void Update()
	{
		
	}

	void FixedUpdate()
	{
		float fixedSpeed = (speed * Time.deltaTime);
		
		if (target != null)
		{
			transform.position = Vector3.MoveTowards(transform.position, target.position, fixedSpeed);
		}
	}
}

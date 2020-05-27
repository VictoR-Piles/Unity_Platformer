using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class Player_checkGround : MonoBehaviour
{
	private Player_controler playerControler;
	
	// ==================== START ====================
	void Start()
	{
		playerControler = GetComponentInParent<Player_controler>();		// Permite acceder a el script Player_controler del objeto padre Player
	}
	
	// ==================== OnCollision ====================
	void OnCollisionStay2D(Collision2D col)
	{
		if (col.gameObject.tag == "Ground")
		{
			playerControler.grounded = true;							// Comprueba si las piernas del Player estan colisionando con algun objeto que tiene el tag 'Ground'
		}
	}
	void OnCollisionExit2D(Collision2D col)
	{
		if (col.gameObject.tag == "Ground")
		{
			playerControler.grounded = false;							// Comprueba si las piernas del Player NO estan colisionando con algun objeto que tiene el tag 'Ground'
		}
	}
}

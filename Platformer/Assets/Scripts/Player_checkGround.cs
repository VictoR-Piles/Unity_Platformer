using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

/**
 * Permite realizar diferentes comprobaciones relacionadas con
 * la colision del Player con diferentes superficies identifi-
 * cadas por su parametro 'tag'
 */
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
		if (col.gameObject.tag == "Platform")
		{
			playerControler.grounded = true;							// Comprueba si las piernas del Player estan colisionando con algun objeto que tiene el tag 'Platform'
		}
	}
	void OnCollisionExit2D(Collision2D col)
	{
		if (col.gameObject.tag == "Ground")
		{
			playerControler.grounded = false;							// Comprueba si las piernas del Player NO estan colisionando con ningun objeto que tiene el tag 'Ground'
		}
		if (col.gameObject.tag == "Platform")
		{
			playerControler.grounded = false;							// Comprueba si las piernas del Player NO estan colisionando con ningun objeto que tiene el tag 'Platform'
		}
	}
}

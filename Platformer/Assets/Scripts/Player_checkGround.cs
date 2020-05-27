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
	private Rigidbody2D rb2d;
	
	// ==================== START ====================
	void Start()
	{
		playerControler = GetComponentInParent<Player_controler>();		// Permite acceder a propiedades del objeto padre (Player)
		rb2d = GetComponentInParent<Rigidbody2D>();
	}
	
	// ==================== OnCollision ====================
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Platform")
		{
			rb2d.velocity = new Vector3(0f, 0f, 0f);
			playerControler.transform.parent = col.transform;			// Asigna la plataforma como el objeto padre de Player, para arreglar el bug que impedia saltar en las plataformas moviles
			playerControler.grounded = true;							// Comprueba si las piernas del Player estan colisionando con algun objeto que tiene el tag 'Platform'
		}
	}
	void OnCollisionStay2D(Collision2D col)
	{
		if (col.gameObject.tag == "Ground")
		{
			playerControler.grounded = true;							// Comprueba si las piernas del Player estan colisionando con algun objeto que tiene el tag 'Ground'
		}
		if (col.gameObject.tag == "Platform")
		{
			playerControler.transform.parent = col.transform;			// Asigna la plataforma como el objeto padre de Player, para arreglar el bug que impedia saltar en las plataformas moviles
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
			playerControler.transform.parent = null;					// Quita a la plataforma como objeto padre del Player
			playerControler.grounded = false;							// Comprueba si las piernas del Player NO estan colisionando con ningun objeto que tiene el tag 'Platform'
		}
	}
}

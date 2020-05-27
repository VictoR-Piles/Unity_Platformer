﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player_controler : MonoBehaviour
{
	public float speed = 75f;												// Velocidad por defecto (se puede modificar desde Unity)
	public float maxSpeed = 5f;												// Velocidad maxima por defecto (se puede modificar desde Unity)
	public bool grounded;													// Determinará si el Player esta colisionando con el suelo o no
	public float jumpPower = 6.5f;											// Fuerza de salto por defecto del jugador (se puede modificar desde Unity)

	private Rigidbody2D rb2d;
	private Animator anim;
	private bool jump;														// Determinara si el Player esta en el aire o no

	// ==================== START ====================
	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	// ==================== UPDATE ====================
	void Update()
	{
		anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));					// Accede al animador y cambia el Speed por la velocidad X actual
		anim.SetBool("Grounded", grounded);
		
		if (Input.GetKeyDown(KeyCode.Space) && grounded)
		{
			jump = true;													// Determina que la tecla ESPACIO ha sido pulsada y el Player debe saltar
		}
	}

	void FixedUpdate()
	{
		Vector3 fixedVelocity = rb2d.velocity;
		fixedVelocity.x *= 0.75f;

		if (grounded)
		{
			rb2d.velocity = fixedVelocity;									// Reduce la velocidad del Player cuando existe FRICCION con el suelo
		}
		
		float h = Input.GetAxis("Horizontal");        						// Permite el movimiento horizontal con las teclas (A, D) o las flechas
		rb2d.AddForce(Vector2.right * speed * h);

		if (h > 0.1f)
		{
			transform.localScale = new Vector3(1f, 1f, 1f);					// Cambia la posicion a la que mira el jugador a DERECHA, modificando su valor de escala a POSITIVO
		}
		if (h < -0.1f)
		{
			transform.localScale = new Vector3(-1f, 1f, 1f);				// Cambia la posicion a la que mira el jugador a IZQUIERDA, modificando su valor de escala a NEGATIVO
		}
		
		if (rb2d.velocity.x > maxSpeed)
		{
			rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y); 		// Evita que speed sea mayor que maxSpeed
		}
		if (rb2d.velocity.x < -maxSpeed)
		{
			rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);		// Evita que speed sea mayor que maxSpeed
		}
		
		if (jump)
		{
			rb2d.velocity = new Vector2(rb2d.velocity.x, 0);				// Cancela la velocidad vertical para no poder realizar saltos con mas impulso en algunas plataformas
			rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);		// Fisica que permite al Player saltar
			jump = false;
		}
	}

	void OnBecameInvisible()
	{
		transform.position = new Vector3(0, 0, 0);
	}
}
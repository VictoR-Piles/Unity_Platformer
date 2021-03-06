﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

/**
 * Permite el movimiento del Player por el escenario,
 * ademas sincroniza algunos movimientos con el ani-
 * mador y calcula algunas fisicas
 */
public class Player_controler : MonoBehaviour
{
	public float speed = 75f;												// Velocidad por defecto (se puede modificar desde Unity)
	public float maxSpeed = 5f;												// Velocidad maxima por defecto (se puede modificar desde Unity)
	public bool grounded;													// Determinará si el Player esta colisionando con el suelo o no
	public float jumpPower = 6.5f;											// Fuerza de salto por defecto del jugador (se puede modificar desde Unity)

	private Rigidbody2D rb2d;
	private Animator anim;
	private SpriteRenderer spr;
	private bool jump;														// Determinara si el Player esta en el aire o no
	private bool doubleJump;												// Determina si se puede realizar un doble salto o no
	private bool canMove = true;											// Determina si el Player puede moverse o no en un determinado momento

	// ==================== START ====================
	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		spr = GetComponent<SpriteRenderer>();
	}

	// ==================== UPDATE ====================
	void Update()
	{
		anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));	// Accede al animador y cambia el Speed por la velocidad X actual
		anim.SetBool("Grounded", grounded);

		if (grounded)
		{
			doubleJump = true;												// Permite realizar el salto de precaucion
		}
		
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (grounded)
			{
				jump = true;												// Determina que la tecla ESPACIO ha sido pulsada y el Player debe saltar
				doubleJump = true;											// Al haber ralizado solo un salto, podemos realizar el doble salto
			}
			else if (doubleJump)
			{
				jump = true;												// Permite realizar el segundo salto
				doubleJump = false;
			}
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
		if (!canMove)
		{
			h = 0;															// Bloquea el movimiento del Player
		}
		rb2d.AddForce(Vector2.right * speed * h);

		if (h > 0.1f)
		{
			transform.localScale = new Vector3(1f, 1f, 1f);		 // Cambia la posicion a la que mira el jugador a DERECHA, modificando su valor de escala a POSITIVO
		}
		else if (h < -0.1f)
		{
			transform.localScale = new Vector3(-1f, 1f, 1f);		 // Cambia la posicion a la que mira el jugador a IZQUIERDA, modificando su valor de escala a NEGATIVO
		}
		
		if (rb2d.velocity.x > maxSpeed)
		{
			rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y); 		// Evita que speed sea mayor que maxSpeed
		}
		else if (rb2d.velocity.x < -maxSpeed)
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
		transform.position = new Vector3(-8, .5f, 0);				// Crea un 'checkpoint' y envia al Player al chechpoint si se sale de los límites de la camara
		rb2d.velocity = new Vector2(0, 0);
	}

	public void enemyJump()													// Hace que el Player salte al matar un enemigo
	{
		jump = true;
	}

	public void enemyKnockBack(float enemyPosX)
	{
		jump = true;
		
		float side = Mathf.Sign(enemyPosX - transform.position.x);		// Mathf.Sign devuelve 1 si un numero es positivo o -1 si es negativo (su signo)
		rb2d.AddForce(Vector2.left * side * jumpPower, ForceMode2D.Impulse);	// Impulsa el enemigo hacia la derecha o hacia a la izquierda, dependiendo de el valor de side

		canMove = false;													// Bloquea el movimiento del 
		
		Invoke("enableMovement", .7f);					// Llama al metodo enableMovement desoues del delay
		spr.color = Color.red;
	}

	void enableMovement()
	{
		canMove = true;														// Reactiva la posibilidad del movimiento
		spr.color = Color.white;
	}
}
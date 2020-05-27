using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player_controler : MonoBehaviour
{
	public float speed = 2f;												// Velocidad por defecto (se puede modificar desde Unity)
	public float maxSpeed = 5f;												// Velocidad maxima por defecto (se puede modificar desde Unity)
	public bool grounded;
	
	private Rigidbody2D rb2d;
	private Animator anim;

	// ==================== START ====================
	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	// ==================== UPDATE ====================
	void Update()
	{
		anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));	// Accede al animador y cambia el Speed por la velocidad X actual
		anim.SetBool("Grounded", grounded);
	}

	void FixedUpdate()
	{
		float h = Input.GetAxis("Horizontal");        						// Permite el movimiento horizontal con las teclas (A, D) o las flechas
		rb2d.AddForce(Vector2.right * speed * h);

		if (h > 0.1f)
		{
			transform.localScale = new Vector3(1f, 1f, 1f);		// Cambia la posicion a la que mira el jugador a DERECHA, modificando su valor de escala a POSITIVO
		}
		if (h < -0.1f)
		{
			transform.localScale = new Vector3(-1f, 1f, 1f);		// Cambia la posicion a la que mira el jugador a IZQUIERDA, modificando su valor de escala a NEGATIVO
		}
		
		if (rb2d.velocity.x > maxSpeed)
		{
			rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y); 		// Evita que speed sea mayor que maxSpeed
		}
		if (rb2d.velocity.x < -maxSpeed)
		{
			rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);		// Evita que speed sea mayor que maxSpeed
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	public float speed = 1f;												// Velocidad por defecto (se puede modificar desde Unity)
	public float maxSpeed = 1f;												// Velocidad maxima por defecto (se puede modificar desde Unity)
	
	private Rigidbody2D rb2d;
	
	// ==================== START ====================
	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
	}

	// ==================== UPDATE ====================
	void FixedUpdate()
	{
		rb2d.AddForce(Vector2.right * speed);
		
		if (rb2d.velocity.x > maxSpeed)
		{
			rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y); 		// Evita que speed sea mayor que maxSpeed
		}
		else if (rb2d.velocity.x < -maxSpeed)
		{
			rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);		// Evita que speed sea mayor que maxSpeed
		}

		if (rb2d.velocity.x > -.01f && rb2d.velocity.x < .01f)
		{
			speed = -speed;													// Detecta cuando el enemigo choca con algo e invierte su direccion
			rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
			
			if (speed < 0)
			{
				transform.localScale = new Vector3(1f, 1f, 1f);	// Cambia la posicion a la que mira el enemigo a DERECHA, modificando su valor de escala a POSITIVO
			}
			else if (speed > 0)
			{
				transform.localScale = new Vector3(-1f, 1f, 1f);	// Cambia la posicion a la que mira el enemigo a IZQUIERDA, modificando su valor de escala a NEGATIVO
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			float yOffset = .22f;											// Hace que el enemigo se destruya solo si saltamos encima de el
			if (transform.position.y + yOffset < col.transform.position.y)
			{
				col.SendMessage("enemyJump");					// Llama al metodo 'enemyJump' del Player
				Destroy(gameObject);										// Destruye el enemigo cuando lo matamos
			}
			else
			{
				col.SendMessage("enemyKnockBack", transform.position.x);	// Llama al metodo 'enemyKnockBack' del Player
			}
		}
	}
}

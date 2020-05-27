using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Permite crear una linea imgainaria que sirve a modo de guia
 * entre el inicio de un objeto movil y el target, esta linea
 * es util para realizar 'debug' en tiempo real en el editor
 * 
 * El paramerto 'from' debera ser asignado a el objeto movil
 * y el parametro 'to' debera ser asignado al target
 */
public class DrawReferenceLine : MonoBehaviour
{
	public Transform from;
	public Transform to;

	void OnDrawGizmosSelected()
	{
		if (from != null && to != null)
		{
			Gizmos.color = Color.cyan;
			Gizmos.DrawLine(from.position, to.position);
			Gizmos.DrawSphere(from.position, 0.15f);
			Gizmos.DrawSphere(to.position, 0.15f);
		}
	}
}
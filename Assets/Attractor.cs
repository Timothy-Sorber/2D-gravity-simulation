using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attractor : MonoBehaviour
{
	private Vector3 Sizechange;
	public float samemasshandler;
	public Collider2D thiscollider;

	public float G = 66.74f;

	public static List<Attractor> Attractors;

	public Rigidbody2D rb;

	public GameObject particle;
	void Start()
    {
		rb = gameObject.GetComponent<Rigidbody2D>();
    }

	void FixedUpdate()
	{
		foreach (Attractor attractor in Attractors)
		{
			if (attractor != this)
				Attract(attractor);
		}
	}

	void OnEnable()
	{
		if (Attractors == null)
			Attractors = new List<Attractor>();

		Attractors.Add(this);
	}

	void OnDisable()
	{
		Attractors.Remove(this);
	}

	void Attract(Attractor objToAttract)
	{
		if ((thiscollider.IsTouching(objToAttract.GetComponent<Collider2D>()) == false))
        {
			Rigidbody2D rbToAttract = objToAttract.GetComponent<Attractor>().rb;

			Vector3 direction = rb.position - rbToAttract.position;
			float distance = direction.magnitude;
			float roche = (2 * (rb.mass * 2) * Mathf.Pow((1 / 1), (1 / 3)));

			if (distance == 0f)
				return;
			float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
			Vector3 force = direction.normalized * forceMagnitude;

			rbToAttract.AddForce(force);
		}
	}

	void OnTriggerStay2D(Collider2D Collision)
    {
		if ((this.gameObject.tag == "Particle") && (Collision.gameObject.tag == "Particle"))
        {
			Rigidbody2D orb = Collision.gameObject.GetComponent<Rigidbody2D>();

			if (orb.mass < rb.mass)
			{
				rb.mass = rb.mass + 1;
				orb.mass = orb.mass - 1;
				if (orb.mass < 1)
				{
					Destroy(Collision.gameObject);
				}
				Debug.Log("Collision detected!");
			}
			if (orb.mass == rb.mass)
			{
				samemasshandler = Random.Range(0, 1000000);
				float othersamemasshandler = Collision.gameObject.GetComponent<Attractor>().samemasshandler;
				if (samemasshandler > othersamemasshandler)
				{
					rb.mass = rb.mass + 1;
					orb.mass = orb.mass - 1;
					if (orb.mass < 1)
                    {
						Destroy(Collision.gameObject);
                    }
					Debug.Log("Handled same-mass object collison successfully!");
				}
			}
		}
    }

	void Update()
    {
		if (Input.GetKey("c"))
        {
			Destroy(this.gameObject);
        }

		Sizechange.y = rb.mass / 2;
		Sizechange.x = rb.mass / 2;

		transform.localScale = Sizechange;
    }
}
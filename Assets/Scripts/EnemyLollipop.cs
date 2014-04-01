//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18449
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;

public class EnemyLollipop : MonoBehaviour
{	
	public GameObject player;
	public float delay;
	public int lifeLollipop;
	public enum States {
		NullStateID = 0, // Use this ID to represent a non-existing State in your system	
		Attack,
		FlyLeft,
		FlyRight,
		Faster,
		Retreat
	}
	public States current, history;
	private float attackTimer;	//countdown till attack from one of the Fly states
	private float colourTimer; //for maintaining colour red after getting hit
	private Vector3 tempPos;
	private float speed;
	private int pass;
	private float height;

	void Start() {
		renderer.material.SetColor("_Color", Color.blue);
		lifeLollipop = 2;
		current = States.FlyLeft;
		attackTimer = 10.0f; //for testing. switch back to 10.0f later
		colourTimer = 0.5f;
		speed = 1.0f; 
		pass = 1;
	}

	void Update() {
		attackTimer -= Time.deltaTime;
		colourTimer -= Time.deltaTime;

		if(colourTimer <= 0.0f)
		{
			renderer.material.SetColor("_Color", Color.blue);
		}

		//transition to Attack State from another state
		if(attackTimer <= 0.0f)
		{
			history = current;
			print ("history when transition from attack = " + history);
			current = States.Attack;
			//pass = 1;
		}

		//action while in Attack State
		if(current == States.Attack)
		{
			attackTimer = 1000.0f; //so that it will not keep transitioning to itself
			tempPos = transform.position;
			/*
			if(pass == 1) 
			{
				tempPos = transform.position;
				pass++;
			}
			*/
			Vector3 pPos, lPos; //playerPosition and lollipop position
			pPos = player.transform.position;
			lPos = transform.position;
			//fly towards player
			if(lPos != pPos)
			{
				rigidbody.velocity = (pPos - lPos) * 5.0f;
				//print ("attack!!!");
			}
			//print ("l x diff = " + Mathf.Abs(lPos.x - pPos.x));
			//print ("l y diff = " + Mathf.Abs(lPos.y - pPos.y));
			//print ("l z diff = " + Mathf.Abs(lPos.z - pPos.z));
			//transition from Attack state to Retreat state
			if (Mathf.Abs(lPos.x - pPos.x) < 2.0f && Mathf.Abs(lPos.y - pPos.y) < 2.0f
			         && Mathf.Abs(lPos.z - pPos.z) < 2.0f)
			{
				current = States.Retreat;
				print ("implement player minus one in health");
			}
		}

		//Retreat State
		if(current == States.Retreat)
		{
			Vector3 lPos = transform.position;
			//action while in Retreat state
			if(transform.position != player.transform.position)
			{
				rigidbody.velocity = ((transform.position - player.transform.position)) * 5.0f;
			}
			//transition from Retreat state back to history state
			if (Mathf.Abs(lPos.x - tempPos.x) < 2.0f && Mathf.Abs(lPos.y - tempPos.y) < 2.0f
			    && Mathf.Abs(lPos.z - tempPos.z) < 2.0f)
			{
				rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
				//reset timer
				attackTimer = 10.0f;
				current = history;
			}
		}

		//FlyLeft State
		if(current == States.FlyLeft)
		{
			//current position of Lollipop
			Vector3 lPos = transform.position;
			//top left edge of viewport, where z = 10
			if(pass == 1)
			{
				height = Random.Range(0.5f, 1.0f);
				pass++;
			}
			Vector3 p = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, height, 10.0f));

			//transition from FlyLeft to FlyRight
			//at the border of the viewport 
			if(Mathf.Abs(lPos.x - p.x) < 1.0f && Mathf.Abs(lPos.y - p.y) < 1.0f)
			{
				current = States.FlyRight;
				//print("current = " + current);
				pass = 1;
			}

			//action in FlyLeftState 
			//print ("p = " +p);
			rigidbody.velocity = (p - transform.position) * speed;
		}
		
		//FlyRight State
		if(current == States.FlyRight)
		{
			//current position of Lollipop
			Vector3 lPos = transform.position;
			//top right edge of viewport, where z = 10
			if(pass == 1)
			{
				height = Random.Range(0.5f, 1.0f);
				pass++;
			}
			Vector3 p = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, height, 10.0f));

			//transition from FlyRight to FlyLeft
			if(Mathf.Abs(lPos.x - p.x) < 1.0f && Mathf.Abs(lPos.y - p.y) < 1.0f)
			{
				current = States.FlyLeft;
				//print("current = " + current);
				pass = 1;
			}

			//action in FlyRightState 
			//print ("p = " +p);
			rigidbody.velocity = (p - transform.position) * speed;
		}

		//Faster State
		if(current == States.Faster)
		{
			//speed doubles after 1 hit
			speed = speed * 2.0f;
			//after speed increase
			current = history;
		}


	}

	//also responsible for injuring and killing the lollipop enemy
	public void StartAnim()
	{
		lifeLollipop--;
		renderer.material.SetColor("_Color", Color.red);
		if(lifeLollipop == 0)
		{
			DestroyObject(gameObject, delay);
			colourTimer = 0.5f;
		}
		else 
		{
			history = current;
			current = States.Faster;
			colourTimer = 0.5f;
		}
	}
}


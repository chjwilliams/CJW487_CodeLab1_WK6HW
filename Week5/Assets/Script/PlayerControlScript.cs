using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*--------------------------------------------------------------------------------------*/
/*																						*/
/*	PlayerControlScript: Handles the player's logic										*/
/*			Functions:																	*/
/*					public:																*/
/*																					    */
/*					proteceted:															*/
/*                                                                                      */
/*					private:															*/
/*						void OnTriggerEnter (Collider other)							*/
/*						void Uppdate ()													*/
/*																						*/
/*--------------------------------------------------------------------------------------*/
public class PlayerControlScript : MonoBehaviour 
{
	//	Public Variables
	public const string GOAL = "Goal";			//	The name of the goal
	public float fallBoundary = -10.0f;			//	The  out of bounds line

	/*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	OnTriggerEnter: Called when rigidbody enters a trigger								*/
    /*		param: Collider other - the tigger we just entered								*/
	/*																						*/
    /*--------------------------------------------------------------------------------------*/
	void OnTriggerEnter(Collider other)
	{
		//	Load a new level if we collide with the goal
		if (other.gameObject.name.Contains(GOAL))
		{
			//	Load a new level when you get to the goal
			LevelLoader.instance.LoadNewLevel();
		}
	}

	/*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	Update: Update is called once per frame												*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
	void Update()
	{
		//	Reload the current level if the player falls out of bounds
		if (transform.position.y < fallBoundary)
		{
			//	Reload this scene if you fall out of bounds
			LevelLoader.instance.ReloadLevel();
		}
	}
}

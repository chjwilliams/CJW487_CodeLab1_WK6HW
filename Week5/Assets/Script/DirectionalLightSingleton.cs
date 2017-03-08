using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*--------------------------------------------------------------------------------------*/
/*																						*/
/*	DirectionalLightSingleton: Singleton for the game's light							*/
/*			Functions:																	*/
/*					public:																*/
/*																					    */
/*					proteceted:															*/
/*                                                                                      */
/*					private:															*/
/*						void Start ()													*/
/*																						*/
/*--------------------------------------------------------------------------------------*/
public class DirectionalLightSingleton : MonoBehaviour 
{
	//	Private Static Variables
	private static DirectionalLightSingleton instance;			//	Instance of the game's light

	/*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	Start: Runs once at the begining of the game. Initalizes variables.					*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
	void Start () 
	{
		//	Singleton for Light game object
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			Destroy(gameObject);
		}
	}
}

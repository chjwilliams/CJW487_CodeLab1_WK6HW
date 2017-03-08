using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

/*--------------------------------------------------------------------------------------*/
/*																						*/
/*	LevelController: rotates level based on scroll input								*/
/*			Functions:																	*/
/*					public:																*/
/*																					    */
/*					proteceted:															*/
/*                                                                                      */
/*					private:															*/
/*						void RotateLevel (float z)										*/
/*						void Uppdate ()													*/
/*																						*/
/*--------------------------------------------------------------------------------------*/
public class LevelController : MonoBehaviour 
{
	//	Public Variables
	public float rotationSpeed = 2.0f;							//	How fast the level rotates
	public string windSpeed;									//	Holds the string for wind speed recived from JSON file

	public const string SCROLLWHEEL = "Mouse ScrollWheel";		//	Name reference to the scroll wheel axis
	public const string LEVEL_HOLDER = "Level Holder";			//	Name reference to Level Holder

	//	Private Variables
	private float _ZRotation;									//	Stores the scroll input value
	private GameObject _Level;									//	Reference to Level Holder
	
	void Start ()
	{
		WebClient client = new WebClient();
		//api.openweathermap.org/data/2.5/weather?id=2172797
		string content = client.DownloadString("http://api.openweathermap.org/data/2.5/weather?id=5128638&APPID=8442ea6bec65a11be71107921d1b15a3");
		JSONNode city = JSON.Parse(content);

		windSpeed = city["wind"]["speed"];

		print ("Wind speed in NYC: " + windSpeed + " meter/sec");
	}
	
	/*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	RotateLevel: Rotates the enitre level												*/
    /*		param: float z - the value taken from the scroll wheel input					*/
	/*																						*/
    /*--------------------------------------------------------------------------------------*/
	void RotateLevel(float z)
	{
		//	Where the rotation magic happens
		_Level.transform.localRotation = Quaternion.Euler(_Level.transform.rotation.x, _Level.transform.rotation.y, z  * float.Parse(windSpeed));
	}

	/*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	Update: Update is called once per frame												*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
	void Update () 
	{
		//	If level is null set level to Level Holder
		if (_Level == null)
		{
			//	This is line is run once at the begining of every new level
			_Level = GameObject.Find(LEVEL_HOLDER);
		}

		//	Gets the value from the scroll wheel input
		_ZRotation = _ZRotation + Input.GetAxis(SCROLLWHEEL);
		//	Passes _ZRotation into the RotateLevel function
		RotateLevel(_ZRotation);
	}
}

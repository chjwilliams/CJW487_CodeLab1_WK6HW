using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

/*--------------------------------------------------------------------------------------*/
/*																						*/
/*	LevelLoader: Loads the levels														*/
/*			Functions:																	*/
/*					public:																*/
/*						void ReloadLevel ()											    */
/*						void LoadNewLevel ()										    */
/*																					    */
/*					proteceted:															*/
/*                                                                                      */
/*					private:															*/
/*						void Start ()													*/
/*																						*/
/*--------------------------------------------------------------------------------------*/
public class LevelLoader : MonoBehaviour 
{
	//	Public Static Variables
	public static int levelNum = 0;									//	The current level number
	public static LevelLoader instance;								//	Instance of Level Loader
	
	//	Public Constant Variables
	public const char KEY_WALL = '*';								//	The character in the txt file that means wall
	public const char KEY_PLAYER = 'P';								//	The character in the txt file that means player
	public const char KEY_GOAL = 'G';								//	The character in the txt file that means goal
	public const string THIS_SCENE = "Week5";						//	Name reference to this scene
	public const string LEVEL_PATH = "/Resources/Levels/";			//	The path to get to the level documents
	public const string FILE_TYPE = ".txt";							//	The file type of the levels
	public const string LEVEL_HOLDER = "Level Holder";				//	The name of the Game Object that holds the walls
	public const string PLAYER_PREFAB_PATH = "Prefabs/Player";		//	Path to get to the player prefab
	public const string GOAL_PREFAB_PATH = "Prefabs/Goal";			//	Path to get to the goal prefab

	//	Public Variables
	public float offsetX = -6;										//	Offset to center the level in x position
	public float offsetY = 8;										//	Offset to center the level in y position
	public string[] fileNames;										//	Array of file names. **EDIT IN SINSPECTOR**
	
	//	Private Variables
	private bool playerHasBeenLoaded;								//	Logic gate so only one player is loaded
	private bool goalHasBeenLoaded;									//	Logic gate so only one goal is loaded

	/*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	Start: Runs once at the begining of the game. Initalizes variables.					*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
	void Start () 
	{
		//	Singleton for LevelLoader
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			Destroy(gameObject);
		}

		//	Logic check to make sure only one player is created per level
		playerHasBeenLoaded = false;
		
		//	Sets up file name. Write file name in the Inspector
		string fileName = fileNames[levelNum];

		//	Sets up file path to find the levels
		string filePath = Application.dataPath + LEVEL_PATH + fileName + FILE_TYPE;

		//	Creates stream reader
		StreamReader sr = new StreamReader(filePath);

		//	creates a game object to hold the level
		GameObject levelHolder = new GameObject(LEVEL_HOLDER);

		//	The value used to exit the while loop
		int yPos = 0;

		//	Read lines until the end of the stream
		while(!sr.EndOfStream)
		{
			//	Reads the line
			string line = sr.ReadLine();

			//	Reads each character on the line
			for(int xPos = 0; xPos < line.Length; xPos++)
			{
				//	Creates a cube where there is a '*'
				if(line[xPos] == KEY_WALL)
				{
					//	Creates a primitive cube for the wall
					GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

					//	Parents newly created cube to Level Holder
					cube.transform.parent = levelHolder.transform;
					
					//	Adjusts position based on offset
					cube.transform.position = new Vector3( xPos + offsetX, yPos + offsetY, 0);

					//	Adds Rigidbody to cube
					cube.AddComponent<Rigidbody>();
					//	Disables gravity for cube
					cube.GetComponent<Rigidbody>().useGravity = false;
					//	Sets cube to Kinematic
					cube.GetComponent<Rigidbody>().isKinematic = true;
				}

				//	Cretas one player on the first instance of 'P'
				if(line[xPos] == KEY_PLAYER && !playerHasBeenLoaded)
				{
					//	Creates player based on Player Prefab
					GameObject player = Instantiate(Resources.Load(PLAYER_PREFAB_PATH) as GameObject);
					//	Adjusts position based on offset
					player.transform.position = new Vector3 (xPos + offsetX, yPos + offsetY, 0);

					//	Now another player cannot be created
					playerHasBeenLoaded = true;
				}

				//	Creates one goal on the first instance of 'G'
				if(line[xPos] == KEY_GOAL && !goalHasBeenLoaded)
				{
					//	Creates a goal based on Goal Prefab
					GameObject goal = Instantiate(Resources.Load(GOAL_PREFAB_PATH) as GameObject);
					//	Adjusts position based on offset
					goal.transform.position = new Vector3 (xPos + offsetX, yPos + offsetY, 0);
					//	Parents Goal to level holder so it rotates with the level
					goal.transform.parent = levelHolder.transform;
			
					//	Now another goal cannot be created
					goalHasBeenLoaded = true;
				}
			}
			// decrements yPos so the while loop will eventually end
			yPos--;
		}
		//	Closes stream reader
		sr.Close();
	}

	/*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	ReloadLevel: Reloads the current level												*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
	public void ReloadLevel()
	{
		//	Loads this scene with the same level
		SceneManager.LoadScene(THIS_SCENE);
	}

	/*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	LoadNewLevel: Loads a new level														*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
	public void LoadNewLevel()
	{
		//	Adds 1 to level than mods it by 2. The values gotten here are 0 and 1
		levelNum = (levelNum + 1) % 2;
		//	Loads this scene with a new level based on levelNum
		SceneManager.LoadScene(THIS_SCENE);
	}
}

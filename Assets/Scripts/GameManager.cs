using UnityEngine;
using System.Collections;
using System.Collections.Generic;        //Allows us to use Lists. 
using UnityEngine.UI;                    //Allows us to use UI.
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float levelStartDelay = 2f;                        //Time to wait before starting level, in seconds.
    public float turnDelay = 0.1f;                            //Delay between each Player turn.
    public int playerFoodPoints = 100;                        //Starting value for Player food points.
    public static GameManager instance = null;                //Static instance of GameManager which allows it to be accessed by any other script.
    [HideInInspector] public bool playersTurn = true;        //Boolean to check if it's players turn, hidden in inspector but public.

    private int points = 0;
    private Text levelText;                                    //Text to display current level number.
    public Text pointsText;
    private GameObject levelImage;                            //Image to block out level as levels are being set up, background for levelText.
    public BoardManager boardScript;                        //Store a reference to our BoardManager which will set up the level.
    private int level = 0;                                    //Current level number, expressed in game as "Day 1".
    private List<Enemy> enemies;                            //List of all Enemy units, used to issue them move commands.
    private bool enemiesMoving;                                //Boolean to check if enemies are moving.
    private bool doingSetup = true;                            //Boolean to check if we're setting up board, prevent Player from moving during setup.
    private bool flag = true;

    public void wonGame()
    {
        StartCoroutine(FinishedGame());
    }
    public IEnumerator FinishedGame()
    {
        Debug.Log("FinishedGame Called");
        levelImage.SetActive(true);
        levelText.text = "Final Score: " + points;
        Invoke("HideLevelImage", levelStartDelay);
        if (flag)
        {
            yield return new WaitForSeconds(1.75f);
            flag = false;
        }
        Application.Quit();
    }

    //Awake is always called before any Start functions
    public void Awake()
    {
        // Singleton logic
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        //Assign enemies to a new List of Enemy objects.
        enemies = new List<Enemy>();
        pointsText.text = "Score: 0";
        //Get a component reference to the attached BoardManager script
        boardScript = GetComponent<BoardManager>();
    }

    public void RestartGame()
    {
        playerFoodPoints = 100;
        boardScript = GetComponent<BoardManager>();
        boardScript.Invoke("Start", 0f);
        points = 0;
        flag = true;
        doingSetup = true;
    }

    public void AddPoints(int p)
    {
        points += p;
        pointsText.text = "Score: " + points.ToString();
    }
    //This is called each time a scene is loaded.
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex != 1) return;
        // Add one so when Scene is reloaded we'll move to the next level
        level++;

        InitGame();
    }

    void OnEnable()
    {
        // Listen for a scene change event as soon as this script is enabled
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        // Stop listening for a scene change event as soon as this script is disabled
        // Remember to always have an unsubscription for every event you subscribe to!
       // SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    //Initializes the game for each level.
    void InitGame()
    {
        //While doingSetup is true the player can't move, prevent player from moving while title card is up.
        doingSetup = true;

        //Get a reference to our image LevelImage by finding it by name.
        levelImage = GameObject.Find("LevelImage");
        pointsText = GameObject.Find("Points").GetComponent<Text>();
        //Get a reference to our text LevelText's text component by finding it by name and calling GetComponent.
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = "Find your way out of the dungeon";
        //Set the text of levelText to the string "Day" and append the current level number.
        //Awake();
        //DontDestroyOnLoad(Canvas);
        //Set levelImage to active blocking player's view of the game board during setup.
        levelImage.SetActive(true);

        //Call the HideLevelImage function with a delay in seconds of levelStartDelay.
        Invoke("HideLevelImage", levelStartDelay);

        //Clear any Enemy objects in our List to prepare for next level.
        enemies.Clear();

        //Call the SetupScene function of the BoardManager script, pass it current level number.
    }


    //Hides black image used between levels
    void HideLevelImage()
    {
        if (levelImage == null) return;
        //Disable the levelImage gameObject.
        levelImage.SetActive(false);

        //Set doingSetup to false allowing player to move again.
        doingSetup = false;
    }

    //Update is called every frame.
    void Update()
    {
        //Check that playersTurn or enemiesMoving or doingSetup are not currently true.
        if (playersTurn || enemiesMoving || doingSetup)

            //If any of these are true, return and do not start MoveEnemies.
            return;

        //Start moving enemies.
        StartCoroutine(MoveEnemies());
    }

    //Call this to add the passed in Enemy to the List of Enemy objects.
    public void AddEnemyToList(Enemy script)
    {
        //Add Enemy to List enemies.
        enemies.Add(script);
    }


    //GameOver is called when the player reaches 0 food points
    public void GameOver()
    {
        //Set levelText to display number of levels passed and game over message
        levelText.text = "You starved in the dungeon.";

        //Enable black background image gameObject.
        levelImage.SetActive(true);

        //Disable this GameManager.
        enabled = false;

        GameOver();
    }

    //Coroutine to move enemies in sequence.
    IEnumerator MoveEnemies()
    {
        //While enemiesMoving is true player is unable to move.
        enemiesMoving = true;

        //Wait for turnDelay seconds, defaults to .1 (100 ms).
        yield return new WaitForSeconds(turnDelay);

        //If there are no enemies spawned (IE in first level):
        if (enemies.Count == 0)
        {
            //Wait for turnDelay seconds between moves, replaces delay caused by enemies moving when there are none.
            yield return new WaitForSeconds(turnDelay);
        }

        //Loop through List of Enemy objects.
        for (int i = 0; i < enemies.Count; i++)
        {
            //Call the MoveEnemy function of Enemy at index i in the enemies List.
            enemies[i].MoveEnemy();

            //Wait for Enemy's moveTime before moving next Enemy, 
            yield return new WaitForSeconds(enemies[i].moveTime);
        }
        //Once Enemies are done moving, set playersTurn to true so player can move.
        playersTurn = true;

        //Enemies are done moving, set enemiesMoving to false.
        enemiesMoving = false;
    }
}
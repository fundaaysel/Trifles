using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public const string LevelPrefName = "LevelIndex";
    [SerializeField] private TextMeshProUGUI levelDisplayText;
    [SerializeField] private List<Level> levelPrefabs;

    public StartGame startMenu;
    public FailMenu failMenu;
    public WinMenu winMenu;

    //For general access it is initialized on awake
    public static Level ActiveLevel;
    private Transform _thrash;
    private CyclingList<Level> _levels;
    public IntPref _levelPref;

    [SerializeField] private float playerSpeed;

    //When you press space key reloads the scene
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetGame();
        }
    }

    //Initializes level system
    private void Awake()
    {
         //open//  startMenu.Show();

        //Initializes level list and loads the prefabs
        _levels = new CyclingList<Level>();
        _levels.Add(levelPrefabs);

        //Initializes player pref controller for level index 
        _levelPref = new IntPref(LevelPrefName); 

        //Creates the thrash object
        _thrash = (new GameObject()).transform;
        _thrash.name = "Thrash";
    }

    //Loads first level so you directly see the loaded level rather than empty scene
    private void Start()
    {
       //open// LoadLevel();
    }

    //Main job about loading level
    private void LoadLevel()
    {
        ClearThrash();
        var levelToLoad = _levels.GetElement(_levelPref.GetValue());
        ActiveLevel = Instantiate(levelToLoad, Vector3.zero, Quaternion.identity, _thrash);
        startMenu.Show();

        if (levelDisplayText != null)
            levelDisplayText.text = "Level " + (_levelPref.GetValue() + 1);
    }

    // When you tap to play this method will work
    public void StartGame()
    {
        
    }

    // When level succesfully finished call this method
    public void OnLevelCompleted()
    {
        _levelPref.IncrementValue();
        LoadLevel();
    }

    // When you failed the level (Win condition not meet or you died or something) call this method
    public void OnLevelFailed()
    {  
        LoadLevel();
    }

    //Clears the thrash from the previous level
    //Called automatically when loading a new level you don't need this
    private void ClearThrash()
    {
        var count = _thrash.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(_thrash.GetChild(i).gameObject);
        }
    }
   
    //If you need to reset the scene 
    public void ResetGame()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // Used from custom editor
    public int TotalLevelCount() => levelPrefabs.Count;
}
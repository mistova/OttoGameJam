using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;

    internal bool inGame = true;

    public int currentLevel;

    public float spawnTime;
    private float spwnTimer = 0f;

    public Transform [] spawnPoints;
    public Transform enemySpawn;
    public GameObject enemy;

    public GameObject bird;

    public Animator animMission;
    public string[] missions;
    int missionCount = 0;
    public Text missionInfo;

    public Animator animLevelPass;
    public Text levelsuccess;

    void Start()
    {
        instance = this;
        Time.timeScale = 1;
        StartMission();
    }

    void Update()
    {
        if (spwnTimer > spawnTime)
        {
            BirdSpawn();
            spwnTimer = 0;
        }
        spwnTimer += Time.deltaTime;
    }

    private void BirdSpawn()
    {
        int spawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        spwnTimer += UnityEngine.Random.Range(0.5f, 1f);
        Vector3 vector3 = new Vector3(spawnPoints[spawnPointIndex].position.x, spawnPoints[spawnPointIndex].position.y, 0);
        Instantiate(bird, vector3, spawnPoints[spawnPointIndex].rotation, spawnPoints[spawnPointIndex]);
    }

    internal bool missionCheck(int i)
    {
        if (i == missionCount) 
        {
            StartMission();
            return true;
        }
        return false;
    }

    void StartMission()
    {
        animMission.SetTrigger("Start");
        missionInfo.text = missions[missionCount];
    }

    internal void CompleteMission(int i)
    {
        if(i == missionCount)
        {
            animMission.SetTrigger("End");
            if (missionCount < missions.Length - 1)
                missionCount++;
            else
                LevelPassed();
            if (missionCount == missions.Length - 1)
                StartCoroutine(CallEnemy());
        }
    }
    IEnumerator CallEnemy()
    {
        yield return new WaitForSeconds(1.5f);
        StartMission();
        Vector3 vector3 = new Vector3(enemySpawn.position.x, enemySpawn.position.y, 0);
        Instantiate(enemy, vector3, enemySpawn.rotation);
    }

    void LevelPassed()
    {
        missionCount++;
        animLevelPass.SetTrigger("LevelPass");
        levelsuccess.text = "PASSED";
        inGame = false;
        if (currentLevel > PlayerPrefs.GetInt("Level", 0))
            PlayerPrefs.SetInt("Level", currentLevel);
    }
    internal void LevelFailed()
    {
        animLevelPass.SetTrigger("LevelPass");
        levelsuccess.text = "FAILED";
        inGame = false;
    }

    public void GamePause()
    {
        if (inGame)
        {
            if (Time.timeScale > 0.5)
            {
                animLevelPass.SetTrigger("Buttons");
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
                animLevelPass.SetTrigger("ButtonsOff");
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}

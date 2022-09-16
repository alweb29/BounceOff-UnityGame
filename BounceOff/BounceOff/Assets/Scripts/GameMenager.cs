using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameMenager : MonoBehaviour
{
    //score
    public int score = 0;
    //text score ref 
    public Text scoreText;
    // spike prefab
    [SerializeField]
    GameObject spikeprefab;
    [SerializeField]
    GameObject diamondPrefab;
    //DieCanvas refrence
    public GameObject onDieCanvas;

    public PlayerMovement playerMovement;

    public Transform [] leftSpikeSpawnPoints;
    public Transform [] rightSpikeSpawnPoints;
    public List<int> listOfIndexes;


    public bool leftWall = false;
    void Start()
    {
        SpikeCheck();
        onDieCanvas.SetActive(false);
        SpawnDiamond();      
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
        if (score > PlayerPrefs.GetInt("highScore"))
        {
            PlayerPrefs.SetInt("highScore", score);
        }
    }
    public void SpikeCheck()
    {
        GameObject[] spikes = GameObject.FindGameObjectsWithTag("Spike");
        foreach (GameObject spike in spikes)
            Destroy(spike);

        if (leftWall)
        {
            RandomLeftWallSpikeSpanw();
        }
        else
        {
            RandomRightWallSpikeSpanw();
        }
    }
    public void AddPoint()
    {

        score += 1;
    }
    public void RandomLeftWallSpikeSpanw()
    {
        // restart list of indexes
        listOfIndexes.Clear();

        int numberOfSpikes = Random.Range(1, 9);
        for (int i = 0; i < numberOfSpikes; i++)
        {
            Transform spawnPosition = leftSpikeSpawnPoints[RandomGeneratingIndex()];
            Vector2 newSpawnPoint = new Vector2(spawnPosition.position.x,spawnPosition.position.y);
            // function that is called every time and finds another number between 1-11 and put it in brakeys
            GameObject spike = Instantiate(spikeprefab.gameObject, newSpawnPoint, Quaternion.identity);

        }
    }
    public void RandomRightWallSpikeSpanw()
    {
        // restart list of indexes
        listOfIndexes.Clear();

        int numberOfSpikes = Random.Range(1, 9);
        for (int i = 0; i < numberOfSpikes; i++)
        {
            Transform spawnPosition = rightSpikeSpawnPoints[RandomGeneratingIndex()];
            Vector2 newSpawnPoint = new Vector2(spawnPosition.position.x, spawnPosition.position.y);
            // function that is called every time and finds another number between 1-11 and put it in brakeys
            GameObject spike = Instantiate(spikeprefab.gameObject, newSpawnPoint, Quaternion.Euler(0,0,180));
        }
    }
    // function that return number between 1-11 every time that is called but without repeating
    public int RandomIndex()
    {
        int index = RandomGeneratingIndex();
        // checking is such index isnt used
        for (int i = 0; i < listOfIndexes.Count; i++)
        {// if such index has been used repeat
            if (listOfIndexes[i] == index)
            {
                index = RandomGeneratingIndex();
            }
        }
        // save this index to list of used indexes
        listOfIndexes.Add(index);
        
        return index;
    }
    private int RandomGeneratingIndex()
    {
        int index = Random.Range(0, 11);
        return index;
    }
    public void LevelReload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void SpawnDiamond()
    {
        float x = Random.Range(-1.5f, 1.5f);
        float y = Random.Range(-3f, 3f);
        Instantiate(diamondPrefab, new Vector2(x, y), Quaternion.identity);

    }
    public void DiamondCollected()
    {
        FindObjectOfType<AudioManager>().Play("coinSound");
        AddPoint();
        SpawnDiamond();
    }

}

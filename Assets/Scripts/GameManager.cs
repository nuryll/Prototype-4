using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool isGameOver;
    public TMP_Text gameOverText;
    public TextMeshProUGUI waveText;

    private SpawnManager spawnManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGameOver = false;
        gameOverText.gameObject.SetActive(false);

        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        UpdateWaveText();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
    public void GameOver()
    {
        isGameOver = true;
        gameOverText.gameObject.SetActive(true);
        gameOverText.text = "GAME OVER\n\n Press 'R' to Restart";

        GameObject[] powerups = GameObject.FindGameObjectsWithTag("Powerup");
        foreach (GameObject p in powerups)
        {
            Destroy(p);
        }

        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemies)
        {
            Destroy(e);
        }
    }

    public void UpdateWaveText()
    {
        waveText.text = "Wave: " + spawnManager.waveNumber;
    }

}

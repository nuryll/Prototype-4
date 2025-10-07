using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool isGameOver;
    public TMP_Text gameOverText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGameOver = false;
        gameOverText.gameObject.SetActive(false);

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

        // ?? Tüm power-up'lar? sil
        GameObject[] powerups = GameObject.FindGameObjectsWithTag("Powerup");
        foreach (GameObject p in powerups)
        {
            Destroy(p);
        }

        // (?stersen dü?manlar? da temizleyebilirsin)
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemies)
        {
            Destroy(e);
        }
    }

}

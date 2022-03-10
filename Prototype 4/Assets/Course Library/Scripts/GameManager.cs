using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject spawnManager;
    public GameObject player;
    public GameObject titleScreen;
    public GameObject countTxt;
    public TextMeshProUGUI enemyCounter;
    public Button startButton;
    private int enmCnt;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {
        enmCnt = FindObjectsOfType<Enemy>().Length;
        enemyCounter.text = "Enemies: " + enmCnt;
    }

    void StartGame()
    {
        player.SetActive(true);
        spawnManager.SetActive(true);
        titleScreen.SetActive(false);
        countTxt.SetActive(true);
    }
}

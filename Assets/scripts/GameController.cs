using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    [SerializeField]
    float timeLimit = 5 * 60f;

    private float startTime;

    [SerializeField]
    GameObject monsterOriginal;

    public List<Transform> spawnPoints;

    List<Actor> allPlayers;
    //List<Actor> deadPlayers;
    //List<Actor> activePlayers;

    public GameUIController uiController;


    bool gameEnded = false;


    // Use this for initialization
    void Start()
    {
        startTime = Time.time;
        allPlayers = new List<Actor>();
        int i = 0;
        foreach (Player p in GlobalData.GetPlayers())
        {
            GameObject newMonster = Instantiate(monsterOriginal) as GameObject;
            Actor monsterActor = newMonster.GetComponent<Actor>();

            allPlayers.Add(monsterActor);

            monsterActor.Initialize(p.getMonsterChoice(), p.getInput(), this);
            Respawn(++i);
        }

        monsterOriginal.gameObject.SetActive(false);
    }


    void Respawn(int player)
    {
        allPlayers[player - 1].transform.position = spawnPoints[player - 1].position;
        allPlayers[player - 1].SetInvincible(true);
    }


    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > timeLimit)
            GameOver();
    }

    public void GameOver()
    {
        if (gameEnded == false)
        {
            // pause game, this still lets player's rotate
            Time.timeScale = 0;
            Debug.Log("Game Over.");
            uiController.ShowFinalScreen();
            gameEnded = true;
        }

    }

    // by might be null
    public void PlayerKilled(Actor player, Actor by)
    {
        List<Player> players = GlobalData.GetPlayers();

        int playerIndex = allPlayers.IndexOf(player);
        int killerIndex = by == null ? -1 : allPlayers.IndexOf(by);

        players[playerIndex].IncDeaths();

        if (killerIndex >= 0)
            players[killerIndex].IncKills();

        Respawn(playerIndex + 1);

        for (int i = 0; i < players.Count; i++)
        {
            Debug.Log("Player " + i + " score: " + players[i].GetScore());
        }
    }

    public float getTimeLeft()
    {
        return timeLimit - (Time.time - startTime);
    }


    public void RequestGameRestart()
    {
        if (gameEnded)
        {
            Time.timeScale = 1;
            Application.LoadLevel("PlayerSelect");
        }
    }

}

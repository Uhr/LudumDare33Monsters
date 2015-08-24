using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{


    [SerializeField]
    GameObject monsterOriginal;

    public List<Transform> spawnPoints;

    List<Actor> allPlayers;
    //List<Actor> deadPlayers;
    //List<Actor> activePlayers;


    // Use this for initialization
    void Start()
    {

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

    }


    public void PlayerKilled(Actor player, Actor by)
    {
		List<Player> players = GlobalData.GetPlayers();

        int playerIndex = allPlayers.IndexOf(player);
		int killerIndex = allPlayers.IndexOf(by);

		players[playerIndex].IncDeaths();

		if(killerIndex >= 0)
			players[killerIndex].IncKills();

        Respawn(playerIndex + 1);

		for(int i=0; i<players.Count; i++)
		{
			Debug.Log("Player "+i+" score: "+players[i].GetScore());
		}
    }


}

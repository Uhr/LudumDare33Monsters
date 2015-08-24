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

        for (int i = 1; i <= 2; i++)
        {
            GameObject newMonster = Instantiate(monsterOriginal) as GameObject;
            Actor monsterActor = newMonster.GetComponent<Actor>();

            // choose input
            InputDevice input;
            if (i == 1)
            {
                input = new KeyboardInputDevice();
            }
            else
            {
                input = new XBox360InputDevice(i - 1);
            }

            allPlayers.Add(monsterActor);

            monsterActor.Initialize(i, input, this);
            Respawn(i);
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
        int playerIndex = allPlayers.IndexOf(player);
        Respawn(playerIndex + 1);
    }


}

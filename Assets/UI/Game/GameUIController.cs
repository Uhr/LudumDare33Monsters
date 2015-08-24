using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{

    public List<Text> scoreOutputs;

    public GameController gameController;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        // update scores
        List<Player> allPlayers = GlobalData.GetPlayers();

        for (int i = 0; i < 4; i++)
        {
            Text scoreOutput = scoreOutputs[i];
            if (i < allPlayers.Count)
            {
                scoreOutput.gameObject.SetActive(true);
                scoreOutput.text = allPlayers[i].getName() + ": " + allPlayers[i].GetScore().ToString();
                scoreOutput.color = allPlayers[i].getColor();
            }
            else
            {
                scoreOutput.gameObject.SetActive(false);
            }

        }



        // update timer


    }
}

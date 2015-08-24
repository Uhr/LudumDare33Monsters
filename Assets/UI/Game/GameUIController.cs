using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{

    public List<Text> scoreOutputs;
    public GameObject timeOutputDescription;
    public Text timeOutput;

    public GameController gameController;

    //public List<Sprite> finalPlayerWinnerSprites;
    //public Image finalPlayerWinnerimage;
    public List<Image> finalPlayerWinnerImages;
    public Text finalPlayerWinnerMessage;

    public Canvas finalCanvas;
    public Image finalBackground;


    // Use this for initialization
    void Start()
    {
        finalCanvas.gameObject.SetActive(false);
        finalBackground.gameObject.SetActive(false);
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
        timeOutput.text = (Mathf.FloorToInt(gameController.getTimeLeft())).ToString();

    }


    public void ShowFinalScreen()
    {
        finalBackground.gameObject.SetActive(true);
        finalCanvas.gameObject.SetActive(true);
        timeOutput.gameObject.SetActive(false);
        timeOutputDescription.SetActive(false);


        // determine winner
        List<Player> allPlayers = GlobalData.GetPlayers();
        int bestScore = int.MinValue;
        Player winner = allPlayers[0];
        foreach (Player player in allPlayers)
        {
            if (player.GetScore() > bestScore)
            {
                bestScore = player.GetScore();
                winner = player;
            }
        }
        foreach (Image im in finalPlayerWinnerImages)
        {
            im.gameObject.SetActive(false);
        }
        finalPlayerWinnerImages[winner.getMonsterChoice() - 1].gameObject.SetActive(true);
        finalPlayerWinnerMessage.color = winner.getColor();
        finalPlayerWinnerMessage.text = winner.getName() + " WON";
    }

}

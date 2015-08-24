using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class SingleSelector : MonoBehaviour
{

    [SerializeField]
    RectTransform rectTrans;

    [SerializeField]
    RectTransform widthReferencePoint;

    InputDevice inputUsed;

    PlayerSelectScreen playerSelectScreen;

    [SerializeField]
    List<GameObject> selectionGraphics;

    [SerializeField]
    GameObject emptyGraphic;


    [SerializeField]
    Image leftArrow;
    [SerializeField]
    Image rightArrow;
    [SerializeField]
    Text playerText;
    [SerializeField]
    List<Color32> playerColors;

    [SerializeField]
    int monsterChoice;

    [SerializeField]
    RectTransform fireToJoinText;

    bool isInUse = false;


    public void Initialize(int position, PlayerSelectScreen playerSelectScreen)
    {

        float completeWidth = widthReferencePoint.localPosition.x * 2f;
        float widthSteps = completeWidth / 8f;
        float startWidth = -completeWidth / 2f;

        float widthChoice = startWidth + (1 + position * 2) * widthSteps;
        float heightChoice = -95;
        rectTrans.localPosition = new Vector2(widthChoice, heightChoice);

        this.playerSelectScreen = playerSelectScreen;

        playerText.text = "P" + (position + 1).ToString();

        leftArrow.color = playerColors[position];
        rightArrow.color = playerColors[position];
        playerText.color = playerColors[position];


        float textOffset = ((1920 / 2f) - widthReferencePoint.localPosition.x) / 4f;
        Debug.Log(widthReferencePoint.localPosition.x);
        fireToJoinText.localPosition = 
            new Vector2(fireToJoinText.localPosition.x - textOffset, fireToJoinText.localPosition.y);

    }


    public void Show(InputDevice inputUsed, int monsterChoice)
    {
        this.monsterChoice = monsterChoice;
        ShowMonsterChoice();

        this.inputUsed = inputUsed;
        isInUse = true;
    }


    public void ShowMonsterChoice()
    {
        HideAllMonsterGraphics();
        selectionGraphics[monsterChoice - 1].gameObject.SetActive(true);
    }

    void HideAllMonsterGraphics()
    {
        for (int i = 0; i < selectionGraphics.Count; i++)
        {
            selectionGraphics[i].gameObject.SetActive(false);
        }
        emptyGraphic.gameObject.SetActive(false);
    }

    public void Hide()
    {
        HideAllMonsterGraphics();
        emptyGraphic.gameObject.SetActive(true);
        isInUse = false;
    }

    public bool IsInUse()
    {
        return isInUse;
    }


    bool directionalChoiceInLastFrame = false;

    void Update()
    {
        if (isInUse)
        {


            // check for monster Choice change
            if (directionalChoiceInLastFrame)
            {
                if (Mathf.Abs(inputUsed.GetXAxis()) < 0.5f)
                {
                    directionalChoiceInLastFrame = false;
                }
            }
            else
            {
                if (Mathf.Abs(inputUsed.GetXAxis()) > 0.5f)
                {
                    directionalChoiceInLastFrame = true;
                    bool biggerChoiceRequested = inputUsed.GetXAxis() > 0.5f;
                    int newChoice = playerSelectScreen.TryToGetDifferentMonsterChoice(monsterChoice, biggerChoiceRequested);
                    monsterChoice = newChoice;
                    ShowMonsterChoice();
                }
            }



            // check for quit
            if (IsInUse() && inputUsed.GetButtonDown(PlayerSelectScreen.leaveKey))
            {
                playerSelectScreen.RemovePlayer(this);
            }

        }
    }

    public InputDevice GetInput()
    {
        return inputUsed;
    }

    public int GetMonsterChoice()
    {
        return monsterChoice;
    }


}

using UnityEngine;
using System.Collections.Generic;

public class PlayerSelectScreen : MonoBehaviour
{

    List<InputDevice> potentialInputDevices;
    List<InputDevice> inputsInUse;


    [SerializeField]
    SingleSelector singleSelectorOriginal;

    List<SingleSelector> allSelectors;

    public const int joinKey = 0;
    public const int leaveKey = 1;
    public const int startGameKey = 7;

    public string gameSceneToStartAfterPlayerSelect;



    // Use this for initialization
    void Start()
    {
        // initialize selectors
        allSelectors = new List<SingleSelector>();
        for (int i = 0; i < 4; i++)
        {
            GameObject newSelector = Instantiate(singleSelectorOriginal.gameObject) as GameObject;

            newSelector.transform.parent = singleSelectorOriginal.transform.parent;
            newSelector.transform.localScale = singleSelectorOriginal.transform.localScale;

            SingleSelector newSelectorScript = newSelector.GetComponent<SingleSelector>();
            newSelectorScript.Initialize(i, this);
            newSelectorScript.Hide();
            allSelectors.Add(newSelectorScript);
        }
        singleSelectorOriginal.gameObject.SetActive(false);




        // collect possible input devices
        potentialInputDevices = new List<InputDevice>();
        potentialInputDevices.Add(new KeyboardInputDevice());
        for (int i = 1; i <= 4; i++)
        {
            potentialInputDevices.Add(new XBox360InputDevice(i));
        }

        inputsInUse = new List<InputDevice>();




    }

    // Update is called once per frame
    void Update()
    {
        // add players
        for (int i = 0; i < potentialInputDevices.Count; i++)
        {
            if (potentialInputDevices[i].GetButtonDown(joinKey))
            {
                TryToAddPlayer(potentialInputDevices[i]);
            }
        }

        // start game
        for (int i = 0; i < inputsInUse.Count; i++)
        {
            if (inputsInUse[i].GetButtonDown(startGameKey))
            {
                TryToStartGame();
            }
        }

    }

    private void TryToStartGame()
    {
        // collect player selection
        List<Player> allPlayers = new List<Player>();
        for (int i = 0; i < allSelectors.Count; i++)
        {
            if (allSelectors[i].IsInUse())
            {
                allPlayers.Add(new Player(allSelectors[i].GetInput(), allSelectors[i].GetMonsterChoice()));
            }
        }

        // start game if sufficient amount of players
        if (allPlayers.Count >= 2)
        {
            GlobalData.SetPlayers(allPlayers);
            Application.LoadLevel(gameSceneToStartAfterPlayerSelect);

        }

    }

    private void TryToAddPlayer(InputDevice inputDevice)
    {
        for (int i = 0; i < allSelectors.Count; i++)
        {

            if (!allSelectors[i].IsInUse())
            {
                allSelectors[i].Show(inputDevice, TryToGetDifferentMonsterChoice(0, true));
                potentialInputDevices.Remove(inputDevice);
                inputsInUse.Add(inputDevice);

                break;
            }
        }
    }





    internal void RemovePlayer(SingleSelector selector)
    {
        potentialInputDevices.Add(selector.GetInput());
        inputsInUse.Remove(selector.GetInput());
        selector.Hide();
    }

    internal int TryToGetDifferentMonsterChoice(int originalChoice, bool biggerChoiceRequested)
    {
        // collect all choices
        List<int> allChoices = new List<int>();
        for (int i = 0; i < allSelectors.Count; i++)
        {
            if (allSelectors[i].IsInUse())
            {
                allChoices.Add(allSelectors[i].GetMonsterChoice());
            }
        }
        // sort
        allChoices.Sort();

        // only 4 choices available
        if (allChoices.Count == 4)
        {
            return originalChoice;
        }
        else
        {
            // hunt for next best choice

            int startChoice = originalChoice;

            for (int i = 0; i < 4; i++)
            {
                // bigger/smaller
                if (biggerChoiceRequested)
                {
                    startChoice++;
                }
                else
                {
                    startChoice--;
                }

                // overshoot
                if (startChoice < 1)
                {
                    startChoice = 4;
                }
                else if (startChoice > 4)
                {
                    startChoice = 1;
                }

                // check if available
                if (!allChoices.Contains(startChoice))
                {
                    return startChoice;
                }
            }
        }

        // fallback
        return originalChoice;

    }

}

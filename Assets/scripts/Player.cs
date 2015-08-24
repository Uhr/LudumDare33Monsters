using UnityEngine;
using System.Collections;

public class Player {

    int kills;
    int deaths;
    int score
    {
        get { return kills - deaths;  }
    }

    InputDevice input;
    int monsterChoice;

    public Player(InputDevice input, int monsterChoice)
    {
        kills = 0;
        deaths = 0;
        this.input = input;
        this.monsterChoice = monsterChoice;
    }
}

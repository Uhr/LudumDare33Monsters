using UnityEngine;
using System.Collections;

public class Player {

    int kills;
    int deaths;

    InputDevice input;
    int monsterChoice;

    public Player(InputDevice input, int monsterChoice)
    {
        kills = 0;
        deaths = 0;
        this.input = input;
        this.monsterChoice = monsterChoice;
    }

	public int getKills()
	{
		return kills;
	}

	public int getDeaths()
	{
		return deaths;
	}

	public int GetScore()
	{
		return kills - deaths;
	}

	public InputDevice getInput()
	{
		return input;
	}

	public int getMonsterChoice()
	{
		return monsterChoice;
	}

	public void IncKills()
	{
		kills++;
	}

	public void IncDeaths()
	{
		deaths++;
	}
}

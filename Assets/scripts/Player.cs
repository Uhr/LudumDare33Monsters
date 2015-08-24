using UnityEngine;
using System.Collections;

public class Player {

    int kills;
    int deaths;

    InputDevice input;
    int monsterChoice;

    string name;
    Color color;

    public Player(InputDevice input, int monsterChoice, string name, Color color)
    {
        kills = 0;
        deaths = 0;
        this.input = input;
        this.monsterChoice = monsterChoice;
        this.name = name;
        this.color = color;
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

    public string getName()
    {
        return name;
    }

    public Color getColor()
    {
        return color;
    }
}

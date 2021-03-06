﻿using UnityEngine;
using System.Collections.Generic;

public class GlobalData
{

    static List<Player> players = new List<Player>();

    public static List<Player> GetPlayers()
    {
        return players;
    }

    static GlobalData()
    {
        // will be overwritten if started from menu
        players.Add(new Player(new KeyboardInputDevice(), 1, "PX", Color.green));
        players.Add(new Player(new XBox360InputDevice(1), 2, "P2", Color.magenta));
    }

    internal static void SetPlayers(List<Player> allPlayers)
    {
        players = allPlayers;
    }
}

using UnityEngine;
using System.Collections.Generic;

public class GlobalData  {

    static List<Player> players = new List<Player>();

    public static List<Player> GetPlayers()
    {
        return players;
    }

    static GlobalData()
    {
        players.Add(new Player(new KeyboardInputDevice(), 1));
        players.Add(new Player(new XBox360InputDevice(1), 2));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    private List<Player> PlayerList;

    private int CurrentPlayerAmount = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Adds a player to the PlayerList and increments the CurrentPlayerAmount
    void AddPlayerToList(Player aPlayerToAdd)
    {
        PlayerList.Add(aPlayerToAdd);
        CurrentPlayerAmount++;
    }

    //Removes the specified player from the PlayerList
    void RemovePlayerFromList(Player aPlayerToRemove)
    {
        PlayerList.Remove(aPlayerToRemove);
        CurrentPlayerAmount--;
    }

    //Removes the specified player index from the PlayerIndex
    void RemovePlayerFromList(int aPlayerIndexToRemove)
    {
        PlayerList.RemoveAt(aPlayerIndexToRemove);
        CurrentPlayerAmount--;
    }

    //Removes all players from the PlayerList
    void RemoveAllPlayers()
    {
        PlayerList.Clear();
        CurrentPlayerAmount = 0;
    }

    //Returns the player at the specified index
    Player GetPlayer(int aPlayerIndex)
    {
        return PlayerList(aPlayerIndex);
    }

    //Returns the PlayerList
    List<Player> GetPlayerList()
    {
        return PlayerList;
    }
}

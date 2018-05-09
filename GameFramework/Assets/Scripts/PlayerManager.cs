using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager {

    private List<Player> m_PlayerList = new List<Player>();

    private int m_CurrentPlayerAmount = 0;

    //Adds a player to the PlayerList and increments the CurrentPlayerAmount
    public void AddPlayerToList(Player aPlayerToAdd)
    {
        m_PlayerList.Add(aPlayerToAdd);
        m_CurrentPlayerAmount++;
    }

    //Removes the specified player from the PlayerList
    public void RemovePlayerFromList(Player aPlayerToRemove)
    {
        m_PlayerList.Remove(aPlayerToRemove);
        m_CurrentPlayerAmount--;
    }

    //Removes the specified player index from the PlayerIndex
    public void RemovePlayerFromList(int aPlayerIndexToRemove)
    {
        m_PlayerList.RemoveAt(aPlayerIndexToRemove);
        m_CurrentPlayerAmount--;
    }

    //Removes all players from the PlayerList
    public void RemoveAllPlayers()
    {
        m_PlayerList.Clear();
        m_CurrentPlayerAmount = 0;
    }

    //Returns the player at the specified index
    public Player GetPlayer(int aPlayerIndex)
    {
        return m_PlayerList[aPlayerIndex];
    }

    //Returns the PlayerList
    public List<Player> GetPlayerList()
    {
        return m_PlayerList;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour {

    public static gameManager instance;

    public MatchSettings MatchSettings;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than 1 game manage in scene.");
        }
        else
        {
            instance = this;
        }
    }

    #region Player tracking

    private const string PLAYER_ID_PREFIX = "Player";

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    public static void RegisterPlayer(string _netId, Player _player)
    {
        string _playerId = PLAYER_ID_PREFIX + _netId;
        players.Add(_playerId, _player);
        _player.transform.name = _playerId;
    }

    public static void UnRegisterPlayer(string _playerId)
    {
        players.Remove(_playerId);
    }

    public static Player GetPlayer(string _playerId)
    {
        return players[_playerId];
    }

    /*void OnGUI ()
    {
        GUILayout.BeginArea(new Rect(200, 200, 200, 5000));
        GUILayout.BeginVertical();

        foreach(string _playerId in players.Keys)
        {
            GUILayout.Label(_playerId+"  -  "+players[_playerId].transform.name);
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }*/
    #endregion

}

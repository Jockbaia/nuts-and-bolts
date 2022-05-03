using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState state;

    public TextAsset level;
    public List<List<char>> room;

    public static event Action<GameState> OnGameStateChanged;

    void Awake()
    {
        instance = this;
        room = ReadLevelFile();
    }

    void Start()
    {
        UpdateGameState(GameState.Level0);
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch(newState)
        {
            case GameState.Level0:
                HandleLevel0();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void HandleLevel0()
    {

    }

    List<List<char>> ReadLevelFile()
    {
        List<List<char>> room = new List<List<char>>();

        string[] rows = level.text.Split('\n');

        foreach (string row in rows)
        {
            string[] cols = row.Split(',');
            List<char> tmp = new List<char>();
            foreach (string c in cols)
            {
                tmp.Add(c.ToCharArray()[0]);
            }
            room.Add(tmp);
        }
        room.Reverse();
        return room;
    }
}

public enum GameState
{
    MainMenu,
    Level0,
    Win,
    Lose
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : NinjaMonoBehaviour {
    public static GameManager Instance { get; private set;}
    private float startTime;
    private float _timeInGame;
    public float TimeInGame => _timeInGame;
    private int _wrongGuesses;
    public int WrongGuesses => _wrongGuesses;
    private GameState _currentState;
    public GameState CurrentState => _currentState;
    public Action OnWrongGuess;
    public Action OnSudokuSolved;
    public Action OnSudokuFailed;
    public enum GameState {
        NotPlaying,
        Playing
    }
    private void Awake() {
        Application.targetFrameRate = 60;
        if(Instance==null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    public void OnGameStart() {
        startTime = Time.time;
        _currentState = GameState.Playing;
        _wrongGuesses = 0;
    }
    public void ResetValues() {
        _currentState = GameState.NotPlaying;
        _wrongGuesses = 0;
        _timeInGame = 0;

    }
    public void AddWrongGuess() {
        string logId = "AddWrongGuess";
        _wrongGuesses++;
        OnWrongGuess?.Invoke();
        if(_wrongGuesses>=3) {
            logd(logId, "Game Over!");
            SudokuFailed();
        }
        //Maybe shake screen?
    }
    private void Update() {
        if(_currentState==GameState.Playing) {
            _timeInGame = Time.time - startTime;
        }
    }
    public string TimeInGameText() {
        var minutes = ((int)_timeInGame/60).ToString("00");
        var seconds = (_timeInGame%60).ToString("00");
        return minutes+":"+seconds;
    }
    public void SudokuFailed() {
        _currentState = GameState.NotPlaying;
        OnSudokuFailed?.Invoke();
    }
    public void SudokuSolved() {
        _currentState = GameState.NotPlaying;
        OnSudokuSolved?.Invoke();
    }
}

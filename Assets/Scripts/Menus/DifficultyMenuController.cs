using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyMenuController : MenuController {
    private void Awake() {
        CurrentState = MenuState.Deactivated;
        gameObject.SetActive(false);
    }
    public void OnDifficultyButtonClick(DifficultySelector difficultySelector) {
        string logId = "OnDifficultyButtonClick";
        logd(logId, "Setting difficulty to "+difficultySelector.difficulty.ToString()+" difficultySelector.difficulty="+(int)difficultySelector.difficulty);
        PlayerPrefs.SetInt(PlayerPrefs.Key.Difficulty, (int)difficultySelector.difficulty);
        SceneManager.ActivateScene(SceneManager.Scene.Sudoku_1);
    }
}

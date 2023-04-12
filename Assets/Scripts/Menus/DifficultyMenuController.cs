using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyMenuController : MenuController {
    public GameObject difficultyButtonsParent;
    private void Awake() {
        CurrentState = MenuState.Deactivated;
        gameObject.SetActive(false);
    }
    public void OnDifficultyButtonClick(DifficultySelector difficultySelector) {
        string logId = "OnDifficultyButtonClick";
        var difficultyButtons = difficultyButtonsParent.GetComponentsInChildren<Button>();
        var difficultyButtonsCount = difficultyButtons.Length;
        for (int i = 0; i < difficultyButtonsCount; i++) {
            difficultyButtons[i].interactable = false;
        }
        AudioManager.Instance.PlayButtonClick();
        logt(logId, "Setting difficulty to "+difficultySelector.difficulty.ToString()+" difficultySelector.difficulty="+(int)difficultySelector.difficulty);
        PlayerPrefs.SetInt(PlayerPrefs.Key.Difficulty, (int)difficultySelector.difficulty);
        SceneManager.ActivateScene(SceneManager.Scene.Sudoku_1);
    }
}

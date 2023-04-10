using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MenuController {
    public DifficultyMenuController difficultyMenu;
    private void Awake() {
        CurrentState = MenuState.Active;
    }
    public void OnStartButtonClick() {
        SceneManager.LoadSceneAsync(SceneManager.Scene.Sudoku_1);
        difficultyMenu.Activate(
            (t) => {
                float smoothT = Mathf.SmoothStep(0, 1, t);
                difficultyMenu.transform.localPosition = Vector3.Lerp(difficultyMenu.deactivatedPosition, difficultyMenu.activePosition, t);
            }
        );
    }
    public void OnExitButtonClick() {
        Application.Quit();
    }
}

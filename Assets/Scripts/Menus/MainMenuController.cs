using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MenuController {
    public DifficultyMenuController difficultyMenu;
    private void Awake() {
        CurrentState = MenuState.Active;
    }
    public void OnStartButtonClick() {
        AudioManager.Instance.PlayButtonClick();
        SceneManager.LoadSceneAsync(SceneManager.Scene.Sudoku_1);
        difficultyMenu.Activate(
            (t) => {
                difficultyMenu.transform.localPosition = Vector3.Lerp(difficultyMenu.deactivatedPosition, difficultyMenu.activePosition, t);
            }
        );
    }
    public void OnExitButtonClick() {
        AudioManager.Instance.PlayButtonClick();
        Application.Quit();
    }
}

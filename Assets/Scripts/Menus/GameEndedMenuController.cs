using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameEndedMenuController : MenuController {
    public TextMeshProUGUI endText;
    public TextMeshProUGUI timeTakenText;
    public string solvedText = "Sudoku Solved";
    public string failedText = "Game Over";
    private void Start() {
        GameManager.Instance.OnSudokuSolved += SetSudokuSolvedUI;
        GameManager.Instance.OnSudokuFailed += SetSudokuFailedUI;
        Deactivate();
    }

    public void SetSudokuSolvedUI() {
        string logId = "SetSudokuSolvedUI";
        logt(logId, "Setting Sudoku Solved UI");
        Activate((t) => { transform.localPosition = Vector3.Lerp(deactivatedPosition, activePosition, t); });
        endText.text = solvedText;
        timeTakenText.text = GameManager.Instance.TimeInGameText();
    }
    public void SetSudokuFailedUI() {
        string logId = "SetSudokuFailedUI";
        logt(logId, "Setting Sudoku Failed UI");
        Activate((t) => { transform.localPosition = Vector3.Lerp(deactivatedPosition, activePosition, t); });
        endText.text = failedText;
        timeTakenText.text = GameManager.Instance.TimeInGameText();
    }
    public void OnPlayAgainButtonClick() {
        string logId = "OnPlayAgainButtonClick";
        logt(logId, "ReloadScene");
        AudioManager.Instance.PlayButtonClick();
        SceneManager.ReloadScene();
    }
    public void OnQuiButtonClick() {
        string logId = "OnQuiButtonClick";
        logt(logId, "LoadStartScene");
        AudioManager.Instance.PlayButtonClick();
        SceneManager.LoadStartScene();
    }
    private void OnDestroy() {
        GameManager.Instance.OnSudokuSolved -= SetSudokuSolvedUI;
        GameManager.Instance.OnSudokuFailed -= SetSudokuFailedUI;
    }
}

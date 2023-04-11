using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour {
    public TextMeshProUGUI wrongGuessesText;
    public TextMeshProUGUI timeInGameText;
    private void Awake() {
        wrongGuessesText.text = "0/3";
        timeInGameText.text = "00:00";
    }
    private void Start() {
        StartCoroutine(UpdateTimeInGameRoutine());
        GameManager.Instance.OnWrongGuess -= UpdateWrongGuesses;
        GameManager.Instance.OnWrongGuess += UpdateWrongGuesses;
    }
    private void UpdateWrongGuesses() {
        var gameManager = GameManager.Instance;
        wrongGuessesText.text = gameManager.WrongGuesses+"/3";
    }
    private IEnumerator UpdateTimeInGameRoutine() {
        var gameManager = GameManager.Instance;
        var waitForSeconds = new WaitForSeconds(0.75f);
        while(true) {
            if(gameManager.CurrentState==GameManager.GameState.Playing) {
                var timeInGame = gameManager.TimeInGameText();
            }
            yield return waitForSeconds;
        }
    }
    public void OnQuiButtonClick() {
        SceneManager.LoadStartScene();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SudokuCell : Cell {
    [SerializeField]
    private ButtonColorChanger buttonColorChanger;
    [SerializeField]
    private ColorFader colorFader;
    [SerializeField]
    private Color solvedColor;
    [SerializeField]
    private Color wrongGuessColor;
    public System.Action<SudokuCell> OnClicked;
    public Action<SudokuCell> OnSolved;
    public Action OnWrongGuess;
    private bool _solved;
    public bool Solved {
        get => _solved;
        private set {
            string logId = "Solved_set";
            logt(logId, "Setting solved to "+value);
            _solved = value;
            if(_solved) {
                logt(logId, "Invoking OnSolved");
                numberText.color = solvedColor;
                OnSolved?.Invoke(this);
            } else {
                numberText.color = wrongGuessColor;
                OnWrongGuess?.Invoke();
            }
            UpdateVisu();
        }
    }
    public CellState CurrentState {
        get; private set;
    }
    public enum CellState {
        Inactive,
        Active,
        Selected, 
        Highlighted
    }
    [SerializeField]
    protected TextMeshProUGUI numberText;
    protected int _number = 0;
    public int Number {
        get => _number;
        set {
            string logId = "Number_set";
            logt(logId, "Changing Number from "+_number+" to "+value);
            _number = value;
            numberText.text = _number.ToString();
        }
    }
    private int _inputNumber;
    public int InputNumber {
        get => _inputNumber;
        set {
            string logId = "InputNumber_set";
            numberText.gameObject.SetActive(true);
            if(value==_number) {
                numberText.text = value.ToString();
                Solved = true;
                UpdateVisu();
                logt(logId, "Correct value of "+value);
            } else {
                numberText.text = value.ToString();
                Solved = false;
                UpdateVisu();
                logt(logId, "Wrong value of "+value);
            }
        }
    }

    private void Awake() {
        CurrentState = CellState.Inactive;
        _solved = true;
    }
    public void HideNumber() {
        string logId = "HideNumber";
        if(numberText==null) {
            loge(logId, "NumberText is null => no-op.");
            return;
        }
        _solved = false;
        numberText.gameObject.SetActive(false);
    }
    public void OnClick() {
        string logId = "OnClick";
        if(OnClicked!=null) {
            OnClicked.Invoke(this);
        }
    }
    public void Select() {
        if(CurrentState==CellState.Selected) {
            CurrentState = CellState.Active;
        } else if(_solved) {
            CurrentState = CellState.Highlighted;
        } else {
            CurrentState = CellState.Selected;
        }
        UpdateVisu();
    }
    public void Activate() {
        gameObject.SetActive(true);
        CurrentState = CellState.Active;
        UpdateVisu();
        colorFader.FadeIn();
        AudioManager.Instance.PlaySudokuCellInitializedSound();
    }
    public void AnimateSolved() {
        colorFader.FadeColors(buttonColorChanger.SelectedColor, buttonColorChanger.NormalColor);
        AudioManager.Instance.PlaySudokuCellInitializedSound();
    }
    public void Deactivate() {
        CurrentState = CellState.Inactive;
        gameObject.SetActive(false);
    }
    public void Deselect() {
        CurrentState = CellState.Active;
        UpdateVisu();
    }
    public void UpdateVisu() {
        if(!Solved && CurrentState==CellState.Selected) {
            buttonColorChanger.SetSelectedColor();
        } else if(CurrentState==CellState.Highlighted) {
            buttonColorChanger.SetHighlightedColor();
        } else {
            buttonColorChanger.SetNormalColor();
        }
    }
    
    public override string ToString() => "Column="+transform.position.x+" Row="+transform.position.y+" CorrectNumber="+_number+" InputNumber="+_inputNumber+" Solved="+Solved;
}

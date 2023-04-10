using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SudokuCell : Cell {
    [SerializeField]
    private ButtonColorChanger buttonColorChanger;
    [SerializeField]
    private ColorFader colorFader;
    public System.Action<SudokuCell> OnClicked;
    public bool Solved {
        get; private set;
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
                logd(logId, "Correct value of "+value);
            } else {
                numberText.text = value.ToString();
                logd(logId, "Wrong value of "+value);
            }
        }
    }
    private void Awake() {
        CurrentState = CellState.Inactive;
        Solved = true;
    }
    public void HideNumber() {
        string logId = "HideNumber";
        if(numberText==null) {
            loge(logId, "NumberText is null => no-op.");
            return;
        }
        Solved = false;
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
        } else if(Solved) {
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

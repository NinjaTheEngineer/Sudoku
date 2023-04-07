using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SudokuCell : Cell {
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
        Selected
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
        if(CurrentState==CellState.Selected) {
            CurrentState = CellState.Active;
        } else {
            CurrentState = CellState.Selected;
        }
        if(OnClicked!=null) {
            OnClicked.Invoke(this);
        }
    }
    
    public override string ToString() => "Column="+transform.position.x+" Row="+transform.position.y+" CorrectNumber="+_number+" InputNumber="+_inputNumber+" Solved="+Solved;
}

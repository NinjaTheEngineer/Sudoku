using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardInputController : NinjaMonoBehaviour {
    public SudokuBoard sudokuBoard;
    private SudokuCell selectedCell;
    private List<SudokuCell> emptyCells;
    private void Awake() {
        SudokuBoard.OnBoardInitialized -= RegisterSudokuCells;
        SudokuBoard.OnBoardInitialized += RegisterSudokuCells;
    }
    private void RegisterSudokuCells() {
        string logId = "RegisterSudokuCells";
        emptyCells = sudokuBoard.EmptyCells;
        List<SudokuCell> sudokuCells = sudokuBoard.AllCells;
        var sudokuCellsCount = sudokuCells.Count;
        logd(logId,"Registering "+sudokuCellsCount+" Sudoku Cells");
        for (int i = 0; i < sudokuCellsCount; i++) {
            var currentCell = sudokuCells[i];
            currentCell.OnClicked -= SelectCell;
            currentCell.OnClicked += SelectCell;
        }
    }
    public void SelectCell(SudokuCell cell) {
        string logId = "SelectCell";
        if(cell==null) {
            logw(logId, "Cell="+cell.logf()+" => no-op");
            return;
        }
        if(selectedCell==cell) {
            logd(logId, "Tried to select same Cell="+cell.logf()+" => Clearing selection");
            selectedCell = null;
        } else {
            logd(logId, "Selected Cell="+cell.logf());
            selectedCell = cell;
        }
    }
    public void OnInputButtonClick(int number) {
        string logId = "OnInputButtonClick";
        if(selectedCell==null || selectedCell.Solved) {
            logw(logId, "SelectedCell="+selectedCell.logf()+" => no-op.");
            return;
        }
        selectedCell.InputNumber = number;
    }
}

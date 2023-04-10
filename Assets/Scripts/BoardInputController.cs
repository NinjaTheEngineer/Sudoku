using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardInputController : NinjaMonoBehaviour {
    public SudokuBoard sudokuBoard;
    private SudokuCell selectedCell;
    public void Initialize(SudokuBoard sudokuBoard) {
        string logId = "Initialize";
        if(sudokuBoard==null) {
            loge(logId, "SudokuBoard="+sudokuBoard.logf()+" => no-op");
            return;
        }
        selectedCell = null;
        this.sudokuBoard = sudokuBoard;
        SudokuBoard.OnBoardInitialized += RegisterSudokuCells;
    }
    private void RegisterSudokuCells() {
        string logId = "RegisterSudokuCells";
        logd(logId, "Fetching cells from SudokuBoard="+sudokuBoard.logf());
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
            selectedCell?.Deselect();
            selectedCell = null;
        } else {
            logd(logId, "Selected Cell="+cell.logf());
            selectedCell?.Deselect();
            selectedCell = cell;
            selectedCell.Select();
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
    private void OnDestroy() {
        SudokuBoard.OnBoardInitialized -= RegisterSudokuCells;
    }
}

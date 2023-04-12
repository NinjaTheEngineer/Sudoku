using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardInputController : NinjaMonoBehaviour {
    public SudokuBoard sudokuBoard;
    private SudokuCell selectedCell;
    private List<SudokuCell> sudokuCells;
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
        logt(logId, "Fetching cells from SudokuBoard="+sudokuBoard.logf());
        sudokuCells = sudokuBoard.AllCells;
        var sudokuCellsCount = sudokuCells.Count;
        logt(logId,"Registering "+sudokuCellsCount+" Sudoku Cells");
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
        AudioManager.Instance.PlaySudokuCellClickSound();
        if(selectedCell==cell && !cell.Solved) {
            logt(logId, "Tried to select same Cell="+cell.logf()+" => Clearing selection");
            selectedCell?.Deselect();
            selectedCell = null;
        } else {
            logt(logId, "Selected Cell="+cell.logf());
            selectedCell?.Deselect();
            selectedCell = cell;
            selectedCell.Select();
            if(selectedCell.Solved) {
                HighlightSameValueCells(selectedCell);
            }
        }
    }
    public void HighlightSameValueCells(SudokuCell cell) {
        string logId = "HighlightSameValueCells";
        int cellsCount = sudokuCells.Count;
        if(cellsCount<=0) {
            logw(logId, "CellsCount="+cellsCount+" => no-op");
            return;
        }
        for (int i = 0; i < cellsCount; i++) {
            var currentCell = sudokuCells[i];
            if(currentCell.Solved && currentCell.Number==cell.Number) {
                currentCell.Highlight();
            }
        }
    }
    public void OnInputButtonClick(int number) {
        string logId = "OnInputButtonClick";
        if(selectedCell==null || selectedCell.Solved) {
            logw(logId, "SelectedCell="+selectedCell.logf()+" => no-op.");
            return;
        }
        AudioManager.Instance.PlaySudokuCellClickSound();
        selectedCell.InputNumber = number;
    }
    private void OnDestroy() {
        SudokuBoard.OnBoardInitialized -= RegisterSudokuCells;
    }
}

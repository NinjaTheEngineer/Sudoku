using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SudokuGrid : Grid {
    public void ActivateCells() {
        gameObject.SetActive(true);
        StartCoroutine(ActivateCellsRoutine());
    }
    public void DeactivateCells() {
        string logId = "DeactivateCells";
        if(cells.Count != amountOfCells) {
            logw(logId, "Not all cells Initialized => no-op");
            return;
        }
        for (int i = 0; i < amountOfCells; i++) {
            SudokuCell currentCell = (SudokuCell)cells[i];
            if(currentCell==null) {
                logw(logId, "CurrentCell="+currentCell.logf()+" is not SudokuCell => continuing");
                continue;
            }
            currentCell.Deactivate();
        }
    }

    IEnumerator ActivateCellsRoutine() {
        string logId = "ActivateCellsRoutine";
        int cellsCount = cells.Count;
        Utils.Shuffle(cells);
        for (int i = 0; i < cellsCount; i++) {
            SudokuCell currentCell = (SudokuCell)cells[i];
            if(currentCell==null) {
                logw(logId, "CurrentCell="+currentCell.logf()+" is not SudokuCell => continuing");
                continue;
            }
            currentCell.Activate();
            yield return new WaitForSeconds(0.1f);
        }
    }
}

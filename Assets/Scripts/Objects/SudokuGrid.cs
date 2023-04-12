using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SudokuGrid : Grid {
    public float cellAnimationDelay = 0.05f;
    public bool Solved {
        get {
            string logId = "Solved_get";
            int unsolvedCells = 0;
            for (int i = 0; i < amountOfCells; i++) {
                SudokuCell currentCell = (SudokuCell) cells[i];
                if(currentCell==null) {
                    logw(logId, "CurrentCell="+currentCell.logf()+" => continuing");
                    continue;
                }
                if(!currentCell.Solved) {
                    unsolvedCells++;
                }
            }
            logt(logId, "UnsolvedCells="+unsolvedCells);
            return unsolvedCells==0;
        }
     }

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

    public void StartSolvedGridAnimation(bool randomizeCells = false) {
        string logId = "StartSolvedGridAnimation";
        logt(logId, "Starting AnimateSolvedGridRoutine");
        StartCoroutine(AnimateSolvedGridRoutine(randomizeCells));
    }
    IEnumerator AnimateSolvedGridRoutine(bool randomizeCells = false) {
        string logId = "AnimateSolvedGridRoutine";
        int cellsCount = cells.Count;
        var listOfCells = randomizeCells?Utils.ShuffleAsNew(cells):cells;
        var waitForSeconds = new WaitForSeconds(cellAnimationDelay);
        for (int i = 0; i < cellsCount; i++) {
            SudokuCell currentCell = (SudokuCell)listOfCells[i];
            if(currentCell==null) {
                logw(logId, "CurrentCell="+currentCell.logf()+" is not SudokuCell => continuing");
                continue;
            }
            currentCell.AnimateSolved();
            yield return waitForSeconds;
        }
    }

    IEnumerator ActivateCellsRoutine() {
        string logId = "ActivateCellsRoutine";
        int cellsCount = cells.Count;
        var shuffledCells = Utils.ShuffleAsNew(cells);
        var waitForSeconds = new WaitForSeconds(cellAnimationDelay);
        for (int i = 0; i < cellsCount; i++) {
            SudokuCell currentCell = (SudokuCell)shuffledCells[i];
            if(currentCell==null) {
                logw(logId, "CurrentCell="+currentCell.logf()+" is not SudokuCell => continuing");
                continue;
            }
            currentCell.Activate();
            yield return waitForSeconds;
        }
    }
}

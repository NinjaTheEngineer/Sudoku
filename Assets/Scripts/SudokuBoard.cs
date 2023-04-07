using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SudokuBoard : NinjaMonoBehaviour {
    public static System.Action OnBoardInitialized;
    public int numberOfGrids = 1;
    public Grid gridPrefab;
    public List<Grid> grids;
    private List<SudokuCell> allCells;
    public DifficultyLevel difficultyLevel = DifficultyLevel.VeryEasy;
    public enum DifficultyLevel {
        VeryEasy = 20,
        Easy = 35,
        Medium = 40,
        Hard = 45,
        Impossible = 60
    }

    private void Start() {
        StartBuildBoard();
    }

    public void StartBuildBoard() {
        StartCoroutine(BuildBoardRoutine());
    }

    public IEnumerator BuildBoardRoutine() {
        string logId = "BuildBoardRoutine";
        var watch = new System.Diagnostics.Stopwatch();
        watch.Start();
        if (grids?.Count>0 && grids[0]!=null) {
            ClearBoard();
        }

        grids = new List<Grid>(numberOfGrids);
        for (int i = 0; i < numberOfGrids; i++) {
            Grid grid = Instantiate(gridPrefab, transform);
            grids.Add(grid);
        }

        yield return true;

        allCells = AllCells;
        if (allCells?.Count==0) {
            loge(logId, "Couldn't resolve AllCells");
            yield break;
        }

        FillSudoku();
        HideCells();
        if(OnBoardInitialized!=null) {
            logd(logId, "Invoking OnBoardInitialized");
            OnBoardInitialized.Invoke();
        }
        watch.Stop();
        logd(logId, "Execution time="+watch.ElapsedMilliseconds+"ms.");
    }
    public bool FillSudoku(int cellIndex = 0) {
        var logId = "FillSudoku";
        if (cellIndex >= allCells.Count) {
            logt(logId, "All cells filled successfully => returning true");
            // We have filled all cells successfully
            return true;
        }

        var currentCell = allCells[cellIndex];

        if (currentCell.Number!=0) {
            logt(logId, "CurrentCell="+currentCell+" already filled => Moving to next one");
            // Cell is already filled, move on to the next one
            return FillSudoku(cellIndex + 1);
        }

        var availableNumbers = new List<int>(9);
        for (int i = 1; i <= 9; i++) {
            availableNumbers.Add(i);
        }
        
        foreach (SudokuCell cell in allCells) {
            if (cell == currentCell) {
                continue;
            }
            bool cellsInSameColumn = cell.transform.position.x == currentCell.transform.position.x;
            bool cellsInSameRow = cell.transform.position.y == currentCell.transform.position.y;
            bool cellsInSameGrid = cell.Grid == currentCell.Grid;
            if (cellsInSameColumn || cellsInSameRow || cellsInSameGrid) {
                logt(logId, "CellsInSameColumn="+cellsInSameColumn+" CellsInSameRow="+cellsInSameRow+" CellsInSameGrid="+cellsInSameGrid+" => Removing cell number from available numbers");
                availableNumbers.Remove(cell.Number);
            } else {
                logt(logId, "CellsInSameColumn="+cellsInSameColumn+" CellsInSameRow="+cellsInSameRow+" CellsInSameGrid="+cellsInSameGrid+" => Continuing");
            }
        }

        Utils.Shuffle(availableNumbers);

        var availableNumbersCount = availableNumbers.Count;
        for (int i = 0; i < availableNumbersCount; i++) {
            currentCell.Number = availableNumbers[i];
            if(FillSudoku(cellIndex + 1)) { 
                logt(logId, "All cells filled successfully => returning true");
                return true;
            }
            currentCell.Number = 0;
        }
        return false;
    }
    private List<SudokuCell> _emptyCells;
    public List<SudokuCell> EmptyCells => _emptyCells;
    public void HideCells() {
        var logId = "HideCells";
        var numToRemove = (int)difficultyLevel;
        _emptyCells = new List<SudokuCell>(numToRemove);
        Utils.Shuffle(allCells);
        for (int i = 0; i < numToRemove; i++) {
            var currentCell = (SudokuCell)allCells[i];
            if(currentCell==null) {
                logw(logId, "CurrentCell="+currentCell.logf()+" => is not a SudokuCell.");
                return;
            }
            currentCell.HideNumber();
            _emptyCells.Add(currentCell);
        }
        logd(logId, "Removed "+numToRemove+" cells from the board.");
    }
    public List<SudokuCell> AllCells {
        get {
            string logId = "AllCells_get";
            List<SudokuCell> allCells = new List<SudokuCell>(81);
            int gridsCount = grids.Count;
            if(gridsCount<=0) {
                loge(logId, "GridsCount="+gridsCount+" => returning null");
                return null;
            }
            for (int i = 0; i < gridsCount; i++) {
                var gridCells = grids[i].Cells;
                var gridCellsCount = gridCells.Count;
                if(gridCellsCount<=0) {
                    loge(logId, "GridCells="+gridCells.logf()+" not yet initialized! => returning null");
                    return null;
                }
                for (int j = 0; j < gridCellsCount; j++) {
                    var gridSudokuCell = (SudokuCell)gridCells[j];
                    if(gridSudokuCell==null) {
                        logw(logId, "GridSudokuCell="+gridSudokuCell.logf()+" => no-op.");
                        continue;
                    }
                    allCells.Add(gridSudokuCell);
                }
            }
            return allCells;
        }
    }
    public void ClearBoard() {
        string logId = "ClearBoard";
        int subGridsCount = grids.Count;
        if(subGridsCount<=0) {
            logt(logId, "Board not initialized => no-op.");
            return;
        }
        for (int i = 0; i < subGridsCount; i++) {
            grids[i].ClearGrid();
        }
    }
}

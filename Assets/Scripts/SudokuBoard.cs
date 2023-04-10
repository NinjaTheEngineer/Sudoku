using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SudokuBoard : NinjaMonoBehaviour {
    public static System.Action OnBoardInitialized;
    public int numberOfGrids = 9;
    public SudokuGrid gridPrefab;
    public BoardInputController boardInputController;
    public List<SudokuGrid> grids;
    private List<SudokuCell> allCells;
    [SerializeField]
    private bool useRandomSeed = false;
    [SerializeField]
    private int boardSeed = -1;
    public void OnBackButtonClick() {
        SceneManager.LoadStartScene();
    }
    private void Start() {
        boardSeed = useRandomSeed?-1:boardSeed;
        StartBuildBoard();
        boardInputController?.Initialize(this);
    }

    public void StartBuildBoard() {
        StartCoroutine(BuildBoardRoutine());
    }

    public IEnumerator BuildBoardRoutine() {
        string logId = "BuildBoardRoutine";
        var watch = new System.Diagnostics.Stopwatch();
        watch.Start();
        if (grids?.Count>0 && grids[0]!=null) {
            logd(logId, "Grids="+grids.logf()+" Count="+grids?.Count+" => Clearing Board");
            ClearBoard();
        }

        grids = new List<SudokuGrid>(numberOfGrids);
        for (int i = 0; i < numberOfGrids; i++) {
            SudokuGrid grid = Instantiate(gridPrefab, transform);
            grids.Add(grid);
        }

        yield return true;

        allCells = AllCells;
        if (allCells?.Count==0) {
            loge(logId, "Couldn't resolve AllCells");
            yield break;
        }

        FillSudoku();
        EmptySudokuCells();
        if(OnBoardInitialized!=null) {
            logd(logId, "Invoking OnBoardInitialized");
            OnBoardInitialized.Invoke();
        }
        
        DeactivateCells();
        watch.Stop();
        yield return true;
        yield return new WaitForSeconds(1f);
        ActivateCells();
        logd(logId, "Execution time="+watch.ElapsedMilliseconds+"ms.");
    }
    private void DeactivateCells() {
        string logId = "HideGrids";
        if(numberOfGrids != grids.Count) {
            logw(logId, "Not all grids were initialized => no-op");
            return;
        }
        for (int i = 0; i < numberOfGrids; i++) {
            SudokuGrid currentGrid = grids[i];
            currentGrid.DeactivateCells();
        }
    }
    private void ActivateCells() {
        StartCoroutine(ActivateCellsRoutine());
    }
    IEnumerator ActivateCellsRoutine() {
        string logId = "ActivateCellsRoutine";
        if(numberOfGrids != grids.Count) {
            logw(logId, "Not all grids were initialized => no-op");
            yield break;
        }
        for (int i = 0; i < numberOfGrids; i++) {
            SudokuGrid currentGrid = grids[i];
            currentGrid.ActivateCells();
        }
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

        Utils.Shuffle(availableNumbers, boardSeed);

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
    public void EmptySudokuCells() {
        var logId = "EmptySudokuCells";
        var numToRemove = PlayerPrefs.GetInt(PlayerPrefs.Key.Difficulty);
        Utils.Shuffle(allCells, boardSeed);
        for (int i = 0; i < numToRemove; i++) {
            var currentCell = (SudokuCell)allCells[i];
            if(currentCell==null) {
                logw(logId, "CurrentCell="+currentCell.logf()+" => is not a SudokuCell.");
                return;
            }
            currentCell.HideNumber();
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
            logd(logId, "Board not initialized => no-op.");
            return;
        }
        for (int i = 0; i < subGridsCount; i++) {
            grids[i].ClearGrid();
        }
    }
}

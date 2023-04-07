using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : NinjaMonoBehaviour {
    public int amountOfCells = 9;
    public Cell cellPrefab;
    [SerializeField]
    private List<Cell> cells;
    public List<Cell> Cells => cells;
    private void Awake() {
        BuildGrid();
    }
    public void BuildGrid() {
        string logId = "BuildGrid";
        int cellsCount = cells.Count;
        if(cellsCount>0 && cells[0]!=null) {
            logd(logId, "Grid already instatianted => Clearing grid");
            ClearGrid();
        }
        cells = new List<Cell>(amountOfCells);
        for (int y = 0; y < amountOfCells; y++) {
            Cell cell = Instantiate(cellPrefab, transform);
            cells.Add(cell);
            cell.Grid = this;
        }
        logt(logId, "All cells initialized");
    }
    public void ClearGrid() {
        string logId = "ClearGrid";
        int cellsCount = cells.Count;
        if(cellsCount<=0) {
            logd(logId, "Grid not initialized => Destroying self.");
            DestroyImmediate(gameObject);
            return;
        }
        for (int i = 0; i < cellsCount; i++) {
            DestroyImmediate(cells[i].gameObject);
        }
        DestroyImmediate(gameObject);
    }
    public bool ContainsCell(Cell cell) {
        string logId = "ContainsCell";
        if(cell==null) {
            logw(logId, "Cell is null => returning false");
            return false;
        }
        return cells.Contains(cell);
    }
    public override string ToString() => "Grid="+name+" Position="+transform.position;
}

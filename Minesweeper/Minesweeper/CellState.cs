﻿namespace Minesweeper;

public record CellState(bool HasBomb, int NumberOfAdjacentBombs)
{
    public bool IsUncovered { get; private set; }

    public bool IsFlagged { get; private set; }

    internal void SetUncovered() => IsUncovered = true;

    internal void ToggleFlag() => IsFlagged = !IsFlagged;
}
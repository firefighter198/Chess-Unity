using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public Teams team;

    public abstract bool[,] GetValidMoves(Cell[,] cells, Game game);

    protected bool IsPositionInGrid(int x, int y)
    {
        if (x < 0 || y < 0 || x > 7 || y > 7)
        {
            return false;
        }

        return true;
    }
}

public enum Teams
{
    White, Black
}

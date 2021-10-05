using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public Teams team;

    public abstract bool[,] GetValidMoves(Cell[,] cells, Game game);
    
}

public enum Teams
{
    White, Black
}

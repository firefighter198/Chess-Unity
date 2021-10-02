using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterData
{
    public static readonly Vector2[,] movesRook =
    {

        {
            //up
            new Vector2(0, 1),
            new Vector2(0, 2),
            new Vector2(0, 3),
            new Vector2(0, 4),
            new Vector2(0, 5),
            new Vector2(0, 6),
            new Vector2(0, 7),
            new Vector2(0, 8.5f), //.5 because of calculation error in game ^^
        },

        {
            //down
            new Vector2(0, -1),
            new Vector2(0, -2),
            new Vector2(0, -3),
            new Vector2(0, -4),
            new Vector2(0, -5),
            new Vector2(0, -6),
            new Vector2(0, -7),
            new Vector2(0, -8.5f),
        },

        {
            //right
            new Vector2(1, 0),
            new Vector2(2, 0),
            new Vector2(3, 0),
            new Vector2(4, 0),
            new Vector2(5, 0),
            new Vector2(6, 0),
            new Vector2(7, 0),
            new Vector2(8.5f, 0)
        },

        {
            //left
            new Vector2(-1, 0),
            new Vector2(-2, 0),
            new Vector2(-3, 0),
            new Vector2(-4, 0),
            new Vector2(-5, 0),
            new Vector2(-6, 0),
            new Vector2(-7, 0),
            new Vector2(-8.5f, 0)
        }
    };

    public static readonly Vector2[] movesKnight =
    {
        //(2, 1 combinations)
        //up
        new Vector2(-1, 2),
        new Vector2(1, 2),
        //down
        new Vector2(-1, -2),
        new Vector2(1, -2),
        //right
        new Vector2(2, 1),
        new Vector2(2, -1),
        //left
        new Vector2(-2, 1),
        new Vector2(-2, -1),
    };

    public static readonly Vector2[,] movesBishop =
    {
        {
            //up, right
            new Vector2(1, 1),
            new Vector2(2, 2),
            new Vector2(3, 3),
            new Vector2(4, 4),
            new Vector2(5, 5),
            new Vector2(6, 6),
            new Vector2(7, 7),
            new Vector2(8.5f, 8.5f),
        },

        {
            //up, left
            new Vector2(-1, 1),
            new Vector2(-2, 2),
            new Vector2(-3, 3),
            new Vector2(-4, 4),
            new Vector2(-5, 5),
            new Vector2(-6, 6),
            new Vector2(-7, 7),
            new Vector2(-8.5f, 8.5f),
        },

        {
            //down, right
            new Vector2(1, -1),
            new Vector2(2, -2),
            new Vector2(3, -3),
            new Vector2(4, -4),
            new Vector2(5, -5),
            new Vector2(6, -6),
            new Vector2(7, -7),
            new Vector2(8.5f, -8.5f),
        },

        {
            //down, left
            new Vector2(-1, -1),
            new Vector2(-2, -2),
            new Vector2(-3, -3),
            new Vector2(-4, -4),
            new Vector2(-5, -5),
            new Vector2(-6, -6),
            new Vector2(-7, -7),
            new Vector2(-8.5f, -8.5f),
        }
    };

    public static readonly Vector2[] movesKing =
    {
        //up
        new Vector2(0, 1),
        //down
        new Vector2(0, -1),
        //right
        new Vector2(1, 0),
        //left
        new Vector2(-1, 0),
        //up, right
        new Vector2(1, 1),
        //up, left
        new Vector2(-1, 1),
        //down, right
        new Vector2(1, -1),
        //down, left
        new Vector2(-1, -1)
    };

    public static readonly Vector2[,] movesQueen =
    {
        {
            //up
            new Vector2(0, 1),
            new Vector2(0, 2),
            new Vector2(0, 3),
            new Vector2(0, 4),
            new Vector2(0, 5),
            new Vector2(0, 6),
            new Vector2(0, 7),
            new Vector2(0, 8.5f), //.5 because of calculation error in game ^^
        },

        {
            //down
            new Vector2(0, -1),
            new Vector2(0, -2),
            new Vector2(0, -3),
            new Vector2(0, -4),
            new Vector2(0, -5),
            new Vector2(0, -6),
            new Vector2(0, -7),
            new Vector2(0, -8.5f),
        },

        {
            //right
            new Vector2(1, 0),
            new Vector2(2, 0),
            new Vector2(3, 0),
            new Vector2(4, 0),
            new Vector2(5, 0),
            new Vector2(6, 0),
            new Vector2(7, 0),
            new Vector2(8.5f, 0)
        },

        {
            //left
            new Vector2(-1, 0),
            new Vector2(-2, 0),
            new Vector2(-3, 0),
            new Vector2(-4, 0),
            new Vector2(-5, 0),
            new Vector2(-6, 0),
            new Vector2(-7, 0),
            new Vector2(-8.5f, 0)
        },
        
        {
            //up, right
            new Vector2(1, 1),
            new Vector2(2, 2),
            new Vector2(3, 3),
            new Vector2(4, 4),
            new Vector2(5, 5),
            new Vector2(6, 6),
            new Vector2(7, 7),
            new Vector2(8.5f, 8.5f),
        },

        {
            //up, left
            new Vector2(-1, 1),
            new Vector2(-2, 2),
            new Vector2(-3, 3),
            new Vector2(-4, 4),
            new Vector2(-5, 5),
            new Vector2(-6, 6),
            new Vector2(-7, 7),
            new Vector2(-8.5f, 8.5f),
        },

        {
            //down, right
            new Vector2(1, -1),
            new Vector2(2, -2),
            new Vector2(3, -3),
            new Vector2(4, -4),
            new Vector2(5, -5),
            new Vector2(6, -6),
            new Vector2(7, -7),
            new Vector2(8.5f, -8.5f),
        },

        {
            //down, left
            new Vector2(-1, -1),
            new Vector2(-2, -2),
            new Vector2(-3, -3),
            new Vector2(-4, -4),
            new Vector2(-5, -5),
            new Vector2(-6, -6),
            new Vector2(-7, -7),
            new Vector2(-8.5f, -8.5f),
        }
    };
}

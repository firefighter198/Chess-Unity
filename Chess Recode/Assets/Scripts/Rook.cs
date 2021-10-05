using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Character
{
    private static readonly Vector2[,] moves =
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

    public override bool[,] GetValidMoves(Cell[,] cells, Game game)
    {
        bool[,] result = new bool[8, 8];

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Vector3 testPosition = transform.position + (Vector3)moves[i, j];
                Cell testCell = game.GetCellOnPosition(testPosition);

                if(testCell != null)
                {
                    if (testCell.connected == null)
                    {
                        int x, y;

                        string nameX = testCell.gameObject.name.Substring(5, 1);
                        string nameY = testCell.gameObject.name.Substring(7, 1);

                        x = int.Parse(nameX);
                        y = int.Parse(nameY);

                        result[x, y] = true;
                    }
                    else
                    {
                        if (testCell.connected.team != game.currentTeam)
                        {
                            int x, y;

                            string nameX = testCell.gameObject.name.Substring(5, 1);
                            string nameY = testCell.gameObject.name.Substring(7, 1);

                            x = int.Parse(nameX);
                            y = int.Parse(nameY);

                            result[x, y] = true;
                        }

                        j = 8;
                    }
                }
            }
        }

        return result;
    }
}
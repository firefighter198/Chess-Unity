using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Character
{
    public static readonly Vector2[] moves =
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
    
    public override bool[,] GetValidMoves(Cell[,] cells, Game game)
    {
        bool[,] result = new bool[8, 8];

        foreach (Vector2 move in moves)
        {
            Vector3 testPosition = transform.position + (Vector3)move;

            Cell testCell = game.GetCellOnPosition(testPosition + Vector3.back);

            if (testCell != null)
            {
                int x, y;

                string nameX = testCell.gameObject.name.Substring(5, 1);
                string nameY = testCell.gameObject.name.Substring(7, 1);

                x = int.Parse(nameX);
                y = int.Parse(nameY);


                if (testCell.connected == null)
                {
                    result[x, y] = true;
                }
                else
                {
                    Character testCharacter = testCell.connected;
                    if (testCharacter.team != game.currentTeam)
                    {
                        result[x, y] = true;
                    }
                }
            }
        }

        return result;
    }
}

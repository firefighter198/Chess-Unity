using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Character
{
    public override bool[,] GetValidMoves(Cell[,] cells, Game game)
    {
        bool[,] result = new bool[8, 8];

        Vector3 offsetDirection;
        int yStartPos;

        if (game.currentTeam == Teams.White)
        {
            offsetDirection = Vector3.up;
            yStartPos = 5;
        }
        else
        {
            offsetDirection = Vector3.down;
            yStartPos = 2;
        }

        Vector3 testPosition = transform.position + offsetDirection;
        Cell testCell = game.GetCellOnPosition(testPosition);

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
                
                if (x == yStartPos)
                {
                    testPosition = transform.position + offsetDirection * 2;
                    testCell = game.GetCellOnPosition(testPosition);

                    if (testCell != null)
                    {
                        nameX = testCell.gameObject.name.Substring(5, 1);
                        nameY = testCell.gameObject.name.Substring(7, 1);

                        x = int.Parse(nameX);
                        y = int.Parse(nameY);

                        if (testCell.connected == null)
                        {
                            result[x, y] = true;
                        }
                    }
                }
            }
            
            testPosition = transform.position + offsetDirection + Vector3.left;
            testCell = game.GetCellOnPosition(testPosition);

            if (testCell != null)
            {
                nameX = testCell.gameObject.name.Substring(5, 1);
                nameY = testCell.gameObject.name.Substring(7, 1);

                x = int.Parse(nameX);
                y = int.Parse(nameY);

                if (testCell.connected != null)
                {
                    if(testCell.connected.team != game.currentTeam)
                    {
                        result[x, y] = true;
                    }
                }
            }
            
            testPosition = transform.position + offsetDirection + Vector3.right;
            testCell = game.GetCellOnPosition(testPosition);

            if (testCell != null)
            {
                nameX = testCell.gameObject.name.Substring(5, 1);
                nameY = testCell.gameObject.name.Substring(7, 1);

                x = int.Parse(nameX);
                y = int.Parse(nameY);

                if (testCell.connected != null)
                {
                    if(testCell.connected.team != game.currentTeam)
                    {
                        result[x, y] = true;
                    }
                }
            }
        }

        return result;
    }
}
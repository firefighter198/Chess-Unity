using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Camera cam;

    private GameObject[,] cells = new GameObject[8, 8];
    private Team currentTeam = Team.White;

    private GameObject selectedCell = null;

    private Queue<GameObject> possibleMoves = new Queue<GameObject>();

    private void Start()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                cells[x, y] = GameObject.Find("Field" + x + "," + y).gameObject;
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Coloring and selecting cells
            GetCellCoord(Input.mousePosition, out int x, out int y);
            GameObject cellObject = cells[x, y];
            cellObject.GetComponent<SpriteRenderer>().color = new Color(.72f, .36f, .36f);

            if (possibleMoves.Contains(cells[x, y]))
            {
                //change current team
                if (currentTeam == Team.White)
                {
                    currentTeam = Team.Black;
                }
                else
                {
                    currentTeam = Team.White;
                }

                //move the object, disconnect it from the old cell, connect it to the new cell
                GameObject selectedObject = selectedCell.GetComponent<Cell>().connected;
                selectedCell.GetComponent<Cell>().connected = null;
                
                //kill enemy if necassary
                if (cells[x, y].GetComponent<Cell>().connected != null)
                {
                    Destroy(cells[x, y].GetComponent<Cell>().connected);
                }
                
                cells[x, y].GetComponent<Cell>().connected = selectedObject;
                selectedObject.transform.position = cells[x, y].transform.position - Vector3.forward;

                //discolor all cells, deselect all cells
                selectedCell.GetComponent<SpriteRenderer>().color = Color.white;
                selectedCell = null;
                while (possibleMoves.Count > 0)
                {
                    possibleMoves.Dequeue().GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
            else
            {
                //if there is already a cell selected, deselect it
                if (selectedCell != null)
                {
                    selectedCell.GetComponent<SpriteRenderer>().color = Color.white;

                    while (possibleMoves.Count > 0)
                    {
                        possibleMoves.Dequeue().GetComponent<SpriteRenderer>().color = Color.white;
                    }
                }

                //if the last selected cell is the current cell, deselect it
                if (selectedCell == cellObject)
                {
                    selectedCell = null;

                    while (possibleMoves.Count > 0)
                    {
                        possibleMoves.Dequeue().GetComponent<SpriteRenderer>().color = Color.white;
                    }
                }
                else
                {
                    selectedCell = cellObject;
                }

                //Futher game logic
                if (selectedCell != null)
                {
                    Cell cell = selectedCell.GetComponent<Cell>();
                    if (cell.connected != null)
                    {
                        //get the selected character, with all its information
                        Character selectedCharacter = cell.connected.GetComponent<Character>();
                        Team selectedTeam = selectedCharacter.team;
                        CharacterType selectedType = selectedCharacter.type;

                        //check if the character is in the current players team
                        if (selectedTeam == currentTeam)
                        {
                            //logic for the knight
                            if (selectedType == CharacterType.Knight)
                            {
                                foreach (Vector2 move in CharacterData.movesKnight)
                                {
                                    Vector2 absPosition = selectedCell.transform.position + (Vector3)move;
                                    GetCellCoordDirect(absPosition, out int testX, out int testY);
                                    if (!(testX == 9 || testY == 9))
                                    {
                                        Cell testCell = cells[testX, testY].GetComponent<Cell>();
                                        if (testCell.connected == null)
                                        {
                                            testCell.GetComponent<SpriteRenderer>().color = new Color(.2f, .9f, .3f);
                                            possibleMoves.Enqueue(cells[testX, testY]);
                                        }
                                        else
                                        {
                                            Character otherCharacter = testCell.connected.GetComponent<Character>();
                                            if (otherCharacter.team != currentTeam)
                                            {
                                                testCell.GetComponent<SpriteRenderer>().color = new Color(.92f, .61f, .4f);
                                                possibleMoves.Enqueue(cells[testX, testY]);
                                            }
                                        }
                                    }
                                }
                            }

                            //logic for the pawn
                            if (selectedType == CharacterType.Pawn)
                            {
                                //stepping 2 forward at beggining
                                if (currentTeam == Team.White)
                                {

                                    Vector2 abs1Position = selectedCell.transform.position + Vector3.up;
                                    GetCellCoordDirect(abs1Position, out int testX2, out int testY2);
                                    if (!(testX2 == 9 || testY2 == 9))
                                    {
                                        Cell testCell = cells[testX2, testY2].GetComponent<Cell>();
                                        if (testCell.connected == null)
                                        {
                                            testCell.GetComponent<SpriteRenderer>().color = new Color(.2f, .9f, .3f);
                                            possibleMoves.Enqueue(cells[testX2, testY2]);
                                            
                                            if (x == 6) //should be y but buggy
                                            {
                                                Vector2 absPosition = selectedCell.transform.position + Vector3.up * 2;
                                                GetCellCoordDirect(absPosition, out int testX, out int testY);
                                                if (!(testX == 9 || testY == 9))
                                                {
                                                    testCell = cells[testX, testY].GetComponent<Cell>();
                                                    if (testCell.connected == null)
                                                    {
                                                        testCell.GetComponent<SpriteRenderer>().color = new Color(.2f, .9f, .3f);
                                                        possibleMoves.Enqueue(cells[testX, testY]);
                                                
                                                    }
                                                }
                                            }
                                        }
                                    }


                                    Vector2 abs2Position = selectedCharacter.transform.position + Vector3.up + Vector3.right;
                                    GetCellCoordDirect(abs2Position, out int testX3, out int testY3);
                                    if (!(testX3 == 9 || testY3 == 9))
                                    {
                                        Cell testCell = cells[testX3, testY3].GetComponent<Cell>();
                                        if (testCell.connected != null)
                                        {
                                            Character otherCharacter = testCell.connected.GetComponent<Character>();
                                            if (otherCharacter.team != currentTeam)
                                            {
                                                testCell.GetComponent<SpriteRenderer>().color = new Color(.92f, .61f, .4f);
                                                possibleMoves.Enqueue(cells[testX3, testY3]);
                                            }
                                        }
                                    }
                                    
                                    abs2Position = selectedCharacter.transform.position + Vector3.up + Vector3.left;
                                    GetCellCoordDirect(abs2Position, out int testX4, out int testY4);
                                    if (!(testX4 == 9 || testY4 == 9))
                                    {
                                        Cell testCell = cells[testX4, testY4].GetComponent<Cell>();
                                        if (testCell.connected != null)
                                        {
                                            Character otherCharacter = testCell.connected.GetComponent<Character>();
                                            if (otherCharacter.team != currentTeam)
                                            {
                                                testCell.GetComponent<SpriteRenderer>().color = new Color(.92f, .61f, .4f);
                                                possibleMoves.Enqueue(cells[testX4, testY4]);
                                            }
                                        }
                                    }
                                }
                                else
                                {

                                    Vector2 abs1Position = selectedCell.transform.position + Vector3.down;
                                    GetCellCoordDirect(abs1Position, out int testX2, out int testY2);
                                    if (!(testX2 == 9 || testY2 == 9))
                                    {
                                        Cell testCell = cells[testX2, testY2].GetComponent<Cell>();
                                        if (testCell.connected == null)
                                        {
                                            testCell.GetComponent<SpriteRenderer>().color = new Color(.2f, .9f, .3f);
                                            possibleMoves.Enqueue(cells[testX2, testY2]);
                                            
                                            if (x == 1)
                                            {
                                                Vector2 absPosition = selectedCell.transform.position + Vector3.down * 2;
                                                GetCellCoordDirect(absPosition, out int testX, out int testY);
                                                if (!(testX == 9 || testY == 9))
                                                {
                                                    testCell = cells[testX, testY].GetComponent<Cell>();
                                                    if (testCell.connected == null)
                                                    {
                                                        testCell.GetComponent<SpriteRenderer>().color = new Color(.2f, .9f, .3f);
                                                        possibleMoves.Enqueue(cells[testX, testY]);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    
                                    
                                    Vector2 abs2Position = selectedCharacter.transform.position + Vector3.down + Vector3.right;
                                    GetCellCoordDirect(abs2Position, out int testX3, out int testY3);
                                    if (!(testX3 == 9 || testY3 == 9))
                                    {
                                        Cell testCell = cells[testX3, testY3].GetComponent<Cell>();
                                        if (testCell.connected != null)
                                        {
                                            Character otherCharacter = testCell.connected.GetComponent<Character>();
                                            if (otherCharacter.team != currentTeam)
                                            {
                                                testCell.GetComponent<SpriteRenderer>().color = new Color(.92f, .61f, .4f);
                                                possibleMoves.Enqueue(cells[testX3, testY3]);
                                            }
                                        }
                                    }
                                    
                                    abs2Position = selectedCharacter.transform.position + Vector3.down + Vector3.left;
                                    GetCellCoordDirect(abs2Position, out int testX4, out int testY4);
                                    if (!(testX4 == 9 || testY4 == 9))
                                    {
                                        Cell testCell = cells[testX4, testY4].GetComponent<Cell>();
                                        if (testCell.connected != null)
                                        {
                                            Character otherCharacter = testCell.connected.GetComponent<Character>();
                                            if (otherCharacter.team != currentTeam)
                                            {
                                                testCell.GetComponent<SpriteRenderer>().color = new Color(.92f, .61f, .4f);
                                                possibleMoves.Enqueue(cells[testX4, testY4]);
                                            }
                                        }
                                    }
                                }
                            }

                            //logic for the rook
                            if (selectedType == CharacterType.Rook)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    for (int j = 0; j < 8; j++)
                                    {
                                        Vector3 absPosition = (Vector3)selectedCell.transform.position + (Vector3)CharacterData.movesRook[i, j];
                                        GetCellCoordDirect(absPosition, out int testX, out int testY);
                                        if (!(testX == 9 || testY == 9))
                                        {
                                            Cell testCell = cells[testX, testY].GetComponent<Cell>();
                                            if (testCell.connected == null)
                                            {
                                                testCell.GetComponent<SpriteRenderer>().color = new Color(.2f, .9f, .3f);
                                                possibleMoves.Enqueue(cells[testX, testY]);
                                            }
                                            else
                                            {
                                                Character otherCharacter = testCell.connected.GetComponent<Character>();
                                                if (otherCharacter.team != currentTeam)
                                                {
                                                    testCell.GetComponent<SpriteRenderer>().color = new Color(.92f, .61f, .4f);
                                                    possibleMoves.Enqueue(cells[testX, testY]);
                                                }
                                           
                                                j = 8;
                                            }
                                        }
                                    }
                                }
                            }
                            
                            //logic for bishop
                            if (selectedType == CharacterType.Bishop)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    for (int j = 0; j < 8; j++)
                                    {
                                        Vector3 absPosition = (Vector3)selectedCell.transform.position + (Vector3)CharacterData.movesBishop[i, j];
                                        GetCellCoordDirect(absPosition, out int testX, out int testY);
                                        if (!(testX == 9 || testY == 9))
                                        {
                                            Cell testCell = cells[testX, testY].GetComponent<Cell>();
                                            if (testCell.connected == null)
                                            {
                                                testCell.GetComponent<SpriteRenderer>().color = new Color(.2f, .9f, .3f);
                                                possibleMoves.Enqueue(cells[testX, testY]);
                                            }
                                            else
                                            {
                                                Character otherCharacter = testCell.connected.GetComponent<Character>();
                                                if (otherCharacter.team != currentTeam)
                                                {
                                                    testCell.GetComponent<SpriteRenderer>().color = new Color(.92f, .61f, .4f);
                                                    possibleMoves.Enqueue(cells[testX, testY]);
                                                }
                                                
                                                j = 8;
                                            }
                                        }
                                    }
                                }
                            }
                            
                            //logic for queen
                            if (selectedType == CharacterType.Queen)
                            {
                                for (int i = 0; i < 8; i++)
                                {
                                    for (int j = 0; j < 8; j++)
                                    {
                                        Vector3 absPosition = (Vector3)selectedCell.transform.position + (Vector3)CharacterData.movesQueen[i, j];
                                        GetCellCoordDirect(absPosition, out int testX, out int testY);
                                        if (!(testX == 9 || testY == 9))
                                        {
                                            Cell testCell = cells[testX, testY].GetComponent<Cell>();
                                            if (testCell.connected == null)
                                            {
                                                testCell.GetComponent<SpriteRenderer>().color = new Color(.2f, .9f, .3f);
                                                possibleMoves.Enqueue(cells[testX, testY]);
                                            }
                                            else
                                            {
                                                Character otherCharacter = testCell.connected.GetComponent<Character>();
                                                if (otherCharacter.team != currentTeam)
                                                {
                                                    testCell.GetComponent<SpriteRenderer>().color = new Color(.92f, .61f, .4f);
                                                    possibleMoves.Enqueue(cells[testX, testY]);
                                                }
                                                
                                                j = 8;
                                            }
                                        }
                                    }
                                }
                            }
                            
                            //logic for king
                            if (selectedType == CharacterType.King)
                            {
                                foreach (Vector2 move in CharacterData.movesKing)
                                {
                                    Vector2 absPosition = selectedCell.transform.position + (Vector3)move;
                                    GetCellCoordDirect(absPosition, out int testX, out int testY);
                                    if (!(testX == 9 || testY == 9))
                                    {
                                        Cell testCell = cells[testX, testY].GetComponent<Cell>();
                                        if (testCell.connected == null)
                                        {
                                            testCell.GetComponent<SpriteRenderer>().color = new Color(.2f, .9f, .3f);
                                            possibleMoves.Enqueue(cells[testX, testY]);
                                        }
                                        else
                                        {
                                            Character otherCharacter = testCell.connected.GetComponent<Character>();
                                            if (otherCharacter.team != currentTeam)
                                            {
                                                testCell.GetComponent<SpriteRenderer>().color = new Color(.92f, .61f, .4f);
                                                possibleMoves.Enqueue(cells[testX, testY]);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void GetCellCoord(Vector3 point, out int x, out int y)
    {
        RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(point), cam.transform.forward);
        if (hit.collider != null)
        {
            string name = hit.collider.name;
            x = int.Parse(name.Substring(5, 1));
            y = int.Parse(name.Substring(7, 1));
            return;
        }

        x = 9;
        y = 9;
    }

    private void GetCellCoordDirect(Vector3 point, out int x, out int y)
    {
        RaycastHit2D hit = Physics2D.Raycast(point, Vector3.forward);
        if (hit.collider != null)
        {
            string name = hit.collider.name;
            x = int.Parse(name.Substring(5, 1));
            y = int.Parse(name.Substring(7, 1));
        }
        else
        {
            x = 9;
            y = 9;
        }
    }
}

public enum Team
{
    Black,
    White
}
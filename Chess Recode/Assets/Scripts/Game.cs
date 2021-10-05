using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Camera cam;
    
    private Cell[,] cells = new Cell[8, 8];
    public Teams currentTeam = Teams.White;

    private Cell clickedCell = null;
    private List<Cell> validMovesList = new List<Cell>();

    private void Start()
    {
        //Get all the Cells from the hierachy
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                cells[x, y] = GameObject.Find("Field" + y + "," + x).gameObject.GetComponent<Cell>();
            }
        }
    }

    private void Update()
    {
        //Check if left clicked
        if (Input.GetMouseButtonDown(0))
        {
            //Get the clicked cell
            Cell tempCell = GetCellOnPositionMouse(Input.mousePosition);

            if(validMovesList.Contains(tempCell))
            {
                if (tempCell.connected != null)
                {
                    Destroy(tempCell.connected.gameObject);
                }
                tempCell.connected = clickedCell.connected;
                tempCell.connected.gameObject.transform.position = (tempCell.gameObject.transform.position + Vector3.back);
                
                clickedCell.connected = null;
                DeColorAllCells();
                validMovesList.Clear();
                
                if (currentTeam == Teams.White)
                {
                    currentTeam = Teams.Black;
                }
                else
                {
                    currentTeam = Teams.White;
                }
            }
            else
            {
                
                //if the clickedCell == tempCell, deselect the cell
                if (clickedCell == tempCell)
                {
                    clickedCell = null;
                    DeColorAllCells();
                    validMovesList.Clear();
                }
                //else select the cell
                else
                {
                    clickedCell = tempCell;
                    ColorCellExclusive(clickedCell, new Color(.5f, .1f, .1f));
                }
            }
            
            //check if there is a clicked cell
            if(clickedCell != null)
            {
                //check if any character is on the cell
                if (clickedCell.connected != null)
                {
                    //check if the character belongs to the curren team
                    if (clickedCell.connected.team == currentTeam)
                    {
                        //get all the valid moves from the character based on the current grid
                        bool[,] validMoves = clickedCell.connected.GetValidMoves(cells, this);
                        for (int x = 0; x < 8; x++)
                        {
                            for (int y = 0; y < 8; y++)
                            {
                                if (validMoves[y, x])
                                {
                                    if (cells[x, y].connected != null)
                                    {
                                        ColorCell(x, y, new Color(.6f, .4f, .2f));
                                    }
                                    else
                                    {
                                        ColorCell(x, y, new Color(.1f, .5f, .1f));
                                    }
                                    validMovesList.Add(cells[x, y]);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    //Reset all colors of the cells
    private void DeColorAllCells()
    {
        for (int ix = 0; ix < 8; ix++)
        {
            for (int iy = 0; iy < 8; iy++)
            {
                ColorCell(ix, iy, Color.white);
            }
        }
    }
    
    //Color the selected cell and deColor all others
    private void ColorCellExclusive(Cell cell, Color color)
    {
        int x = 0, y = 0;
        
        for (int ix = 0; ix < 8; ix++)
        {
            for (int iy = 0; iy < 8; iy++)
            {
                if (cells[ix, iy] == cell)
                {
                    x = ix;
                    y = iy;
                }
            }
        }
        
        ColorCellExclusive(x, y, color);
    }

    //Color the selected cell and deColor all others
    private void ColorCellExclusive(int x, int y, Color color)
    {
        for (int ix = 0; ix < 8; ix++)
        {
            for (int iy = 0; iy < 8; iy++)
            {
                if (ix == x && iy == y)
                {
                    ColorCell(ix, iy, color);
                }
                else
                {
                    ColorCell(ix, iy, Color.white);
                }
            }
        }
    }

    //Color the selected cell in the given color
    private void ColorCell(int x, int y, Color color)
    {
        cells[x, y].GetComponent<SpriteRenderer>().color = color;
    }

    //Get the currently clicked cell
    public Cell GetCellOnPositionMouse(Vector3 position)
    {
        return GetCellOnPosition(cam.ScreenToWorldPoint(position));
    }
    
    public Cell GetCellOnPosition(Vector3 position)
    {
        Cell result = null;
        
        //Shoot a raycast from mousePointer / touch position
        RaycastHit2D hit = Physics2D.Raycast(position, Vector3.forward);
        if (hit.collider != null)
        {
            result = hit.collider.gameObject.GetComponent<Cell>();
        }

        return result;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    private int x;
    private int y;

    public int X
    {
        get { return x; }
        set
        {
            if(IsMovable())
            {
                x = value;
            }
        }
    }

    //getter for the position
    public int Y
    {
        get { return y; }
        set
        {
            if (IsMovable())
            {
                y = value;
            }
        }
    }
    void Start()
    {
        
    }

    private boardGrid.PieceType type;

    public boardGrid.PieceType Type
    {
        get { return type; }
    }

    private boardGrid grid;

    public boardGrid GridRef
    {
        get { return grid; }
    }

    private MoveablePiece movableComponent;

    public MoveablePiece MovableComponent
    {
        get { return movableComponent; }
    }

    private ColorPiece colorComponent;

    public ColorPiece ColorComponent
    {
        get { return colorComponent; }
    }

    private void Awake()
    {
        movableComponent = GetComponent<MoveablePiece>();
        colorComponent = GetComponent<ColorPiece>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(int _x, int _y, boardGrid _grid, boardGrid.PieceType _type)
    {
        x = _x;
        y = _y;
        grid = _grid;
        type = _type;
    }

    private void OnMouseEnter()
    {
        grid.EnterPiece(this); 
    }
    

    private void OnMouseDown()
    {
        grid.PressPiece(this);
    }

    private void OnMouseUp()
    {
        grid.ReleasePiece();
    }

    public bool IsMovable()
    {
        return movableComponent != null;
    }

    public bool IsColored()
    {
        return colorComponent != null;
    }
}

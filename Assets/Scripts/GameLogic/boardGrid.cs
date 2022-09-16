using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class boardGrid : MonoBehaviour
{

    //Grid Dimensions
    public int grid_width;
    public int grid_height;

    public char[] gridArray;
    public int score = 0;
    private Text scoreText;
    public Level level;
    public LevelMoves currentLevel;
    private bool gameOver = false;
    public FinalScore scoreScript;
    public GameObject popupScreen;
    public Text congratsPanelText;
    public GameObject congratsPanel;

    [SerializeField] GameObject panel, star, glitter1, glitter2, highScoreText, title;

    public enum PieceType
    {
        NORMAL,
        COMPLETED,
        COUNT,
    };

    [System.Serializable]
    public struct PiecePrefab
    {
        public PieceType type;
        public GameObject prefab;
    }

    public PiecePrefab[] piecePrefabs;
    public GameObject backgroundPrefab;


    private Dictionary<PieceType, GameObject> piecePrefabDict;

    private GamePiece[,] pieces;

    private GamePiece pressedPiece;
    private GamePiece enteredPiece;

    void Start()
    {
        Invoke("GridStart", 0.1f);

    }

    

    public void GridStart()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        scoreText.text = score.ToString();

        piecePrefabDict = new Dictionary<PieceType, GameObject>();

        for (int i = 0; i < piecePrefabs.Length; i++)
        {
            if (!piecePrefabDict.ContainsKey(piecePrefabs[i].type))
            {
                piecePrefabDict.Add(piecePrefabs[i].type, piecePrefabs[i].prefab);
            }
        }

        for (int x = 0; x < grid_width; x++)
        {
            for (int y = 0; y < grid_height; y++)
            {
                GameObject background = (GameObject)Instantiate(backgroundPrefab, GetPiecePosition(x, y), Quaternion.identity);
                background.transform.parent = transform; 
            }
        }

        pieces = new GamePiece[grid_width, grid_height];

        for (int x = 0; x < grid_width; x++)
        {
            for (int y = 0; y < grid_height; y++)
            {
                GameObject newPiece = (GameObject)Instantiate(piecePrefabDict[PieceType.NORMAL], Vector3.zero, Quaternion.identity);
                newPiece.name = "Piece(" + x + "," + y + ")";
                newPiece.transform.parent = transform;

                pieces[x, y] = newPiece.GetComponent<GamePiece>();
                pieces[x, y].Init(x, y, this, PieceType.NORMAL);

                if (pieces[x, y].IsMovable())
                {
                    pieces[x, y].MovableComponent.Move(x, y);
                }

                if (pieces[x, y].IsColored())
                {
                    char placeOfPiece = gridArray[y * grid_width + x];
                    switch (placeOfPiece)
                    {
                        case 'b':
                            pieces[x, y].ColorComponent.SetColor(ColorPiece.ColorType.BLUE);
                            break;

                        case 'y':
                            pieces[x, y].ColorComponent.SetColor(ColorPiece.ColorType.YELLOW);
                            break;

                        case 'g':
                            pieces[x, y].ColorComponent.SetColor(ColorPiece.ColorType.GREEN);
                            break;

                        case 'r':
                            pieces[x, y].ColorComponent.SetColor(ColorPiece.ColorType.RED);
                            break;

                        default:
                            break;
                    }
                }
            }
        }
    }

    //To position pieces to cells
    public Vector2 GetPiecePosition(int x, int y)
    {
        return new Vector2(transform.position.x - grid_width / 2.5f + x, transform.position.y - grid_height / 2.5f + y);
    }

    public bool IsAdjacent(GamePiece piece1, GamePiece piece2)
    {
        return (piece1.X == piece2.X && (int)Mathf.Abs(piece1.Y - piece2.Y) == 1)
            || (piece1.Y == piece2.Y && (int)Mathf.Abs(piece1.X - piece2.X) == 1);
    }

    public void SwapPieces(GamePiece piece1, GamePiece piece2)
    {
        //if game over stop swapping
        if (gameOver)
        {
            return;
        }

        if(piece1.IsMovable () && piece2.IsMovable () && piece1.ColorComponent.Color != ColorPiece.ColorType.CHECK && piece2.ColorComponent.Color != ColorPiece.ColorType.CHECK)
        {

            pieces[piece1.X, piece1.Y] = piece2;
            pieces[piece2.X, piece2.Y] = piece1;

            int piece1X = piece1.X;
            int piece1Y = piece1.Y;


            LeanTween.moveLocal(piece1.gameObject, new Vector3(transform.position.x - grid_width / 2.5f + piece2.X, transform.position.y - grid_height / 2.5f + piece2.Y, 0f), 0.5f).setEase(LeanTweenType.easeOutCirc);
            piece1.X = piece2.X;
            piece1.Y = piece2.Y;
            LeanTween.moveLocal(piece2.gameObject, new Vector3(transform.position.x - grid_width / 2.5f + piece1X, transform.position.y - grid_height / 2.5f + piece1Y, 0f), 0.5f).setEase(LeanTweenType.easeOutCirc);
            piece2.X = piece1X;
            piece2.Y = piece1Y;

            level.OnMove();

            if(piece2.Y != piece1.Y)
            {
                CheckRow(piece1, piece2);
            }

        }
    }

    public void CheckRow(GamePiece piece1, GamePiece piece2)
    {
        bool piece1Flag = true;
        bool piece2Flag = true;

        for (int x = 0; x < grid_width-1; x++)
        {
            if (pieces[x, piece1.Y].ColorComponent.Color != pieces[x + 1, piece1.Y].ColorComponent.Color)
            {
                piece1Flag = false;
                break;
            } 
        }

        for (int x = 0; x < grid_width-1; x++)
        {
            if (pieces[x, piece2.Y].ColorComponent.Color != pieces[x + 1, piece2.Y].ColorComponent.Color)
            {   
                piece2Flag = false;
                break;
            }
        }

        if (piece1Flag)
        {
            RowMatched(piece1);
        }

        if (piece2Flag)
        {
            RowMatched(piece2);
        }

    }

    public void RowMatched(GamePiece piece)
    {
        switch (pieces[piece.X, piece.Y].ColorComponent.Color)
        {
            case ColorPiece.ColorType.RED:
                score += 100;
                break;
            case ColorPiece.ColorType.GREEN:
                score += 150;
                break;
            case ColorPiece.ColorType.BLUE:
                score += 200;
                break;
            case ColorPiece.ColorType.YELLOW:
                score += 250;
                break;
            default:
                break;
        }
        level.OnRowMatched(score);

        for (int x = 0; x < grid_width; x++)
        {
            pieces[x, piece.Y].ColorComponent.SetColor(ColorPiece.ColorType.CHECK);
            LeanTween.scale(pieces[x, piece.Y].gameObject, new Vector3(0f, 0f, 1f), 0f).setEase(LeanTweenType.easeOutElastic);
            LeanTween.scale(pieces[x, piece.Y].gameObject, new Vector3(0.6f, 0.6f, 1f), 2f).setDelay(0.23f).setEase(LeanTweenType.easeOutElastic);
        
        }

        int blue = 0, yellow = 0, green = 0, red = 0;
        bool isGameOver = true;

        for (int y = 0; y < grid_height; y++)
        {
            for (int x = 0; x < grid_width; x++)
            {
                switch (pieces[x, y].ColorComponent.Color)
                {
                    case ColorPiece.ColorType.RED:
                        red += 1;
                        break;
                    case ColorPiece.ColorType.GREEN:
                        green += 1;
                        break;
                    case ColorPiece.ColorType.BLUE:
                        blue += 1;
                        break;
                    case ColorPiece.ColorType.YELLOW:
                        yellow += 1;
                        break;
                    default:
                        break;
                }

                if (pieces[x, y].ColorComponent.Color == ColorPiece.ColorType.CHECK || ((x == grid_width-1) && (y == grid_height-1)))
                {
                    if(red >= grid_width || blue >= grid_width || green >= grid_width || yellow >= grid_width)
                    {
                        isGameOver = false;
                        continue;
                    } else
                    {
                        red = 0;
                        blue = 0;
                        yellow = 0;
                        green = 0;
                    }

                }

            }
            
        }
        if (isGameOver)
        {
            GameOver();
        }
        
    }

    public void PressPiece(GamePiece piece)
    {
        pressedPiece = piece;
    }

    public void EnterPiece(GamePiece piece)
    {
        enteredPiece = piece;
    }

    public void ReleasePiece()
    {
        if(IsAdjacent(pressedPiece, enteredPiece))
        {
            SwapPieces(pressedPiece, enteredPiece);
        }
    }

    public void GameOver()
    {
        gameOver = true;
        scoreScript = GameObject.Find("ScoreHolder").GetComponent<FinalScore>();
        currentLevel = GameObject.Find("Level").GetComponent<LevelMoves>();
        
        if (score > scoreScript.scoreArray[currentLevel.level-1])
        {
            scoreScript.scoreArray[currentLevel.level - 1] = score;
            //congratsPanel.SetActive(true);
            LeanTween.moveLocal(panel, new Vector3(0, 38f, 0f), 0.7f).setDelay(.5f).setEase(LeanTweenType.easeOutCirc);
            LeanTween.scale(star, new Vector3(1.2f, 1.2f, 1.2f), 2f).setDelay(.7f).setEase(LeanTweenType.easeOutElastic);
            LeanTween.scale(title, new Vector3(1f, 1f, 1f), 2f).setDelay(.9f).setEase(LeanTweenType.easeOutElastic);
            LeanTween.scale(glitter1, new Vector3(2f, 2f, 2f), 2f).setDelay(3f).setEase(LeanTweenType.easeOutElastic);
            LeanTween.scale(glitter2, new Vector3(2f, 2f, 2f), 2f).setDelay(3f).setEase(LeanTweenType.easeOutElastic);
            congratsPanelText = GameObject.Find("CongratsPanel").transform.GetChild(3).GetComponent<Text>();
            congratsPanelText.text = score.ToString();
            GameObject.Find("PopupManager").GetComponent<PopupManager>().isFromLevel = true;
            this.Wait(6.3f, () =>
            {
                SceneManager.LoadScene("MainScene");
            });
            return;
        }

        GameObject.Find("PopupManager").GetComponent<PopupManager>().isFromLevel = true;  
        SceneManager.LoadScene("MainScene");
     
    }
 
}

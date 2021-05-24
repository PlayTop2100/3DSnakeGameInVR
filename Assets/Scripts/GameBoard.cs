using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameBoard : MonoBehaviour
{
    public float moveTime = 0;
    public float moveInterval = GameManager.speed;
    public static float boxSize = GameManager.boxSize;
    public static int boardSize = GameManager.boardSize;
    [SerializeField]
    public Text txt;
    public Mesh cubeMesh = null;
    public Mesh sphereMesh = null;
    GameObject[,,] board = new GameObject[10, 10, 10];
    Snake snake = new Snake();
    public static Vector3 snakeDir = new Vector3(0, 1, 0);
    public static bool move = false;
    Vector3 offset = new Vector3(-(boardSize * boxSize) / 2, -(boardSize * boxSize) / 2, -(boardSize * boxSize) / 2);


    void Start()
    {
        snakeDir = new Vector3(0, 1, 0);
        Vector3 temp = transform.position + offset;
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                for (int k = 0; k < boardSize; k++)
                {
                    GameObject cubeObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cubeObj.transform.localScale = new Vector3(boxSize, boxSize, boxSize);
                    cubeObj.transform.position = new Vector3(i* boxSize, j * boxSize, k * boxSize);
                    cubeObj.transform.position += temp;
                    cubeObj.transform.SetParent(GetComponent<Transform>());
                    cubeObj.GetComponent<BoxCollider>().enabled = false;
                    cubeObj.GetComponent<Renderer>().material.color = new Color(0, 0, 0);

                    board[i, j, k] = cubeObj;
                }
            }
        }
        clearBoard();
        snake.spawnApple();
    }


    void Update()
    {
        snake.setDir(snakeDir);
        if(move)
        {
            snake.move();
            move = false;
        }
        moveTime += Time.deltaTime;
        if(moveTime >= moveInterval)
        {
            snake.move();
            moveTime = 0;
        }
        setBoard();
        txt.text = "Score: " + GameManager.score.ToString();
    }

    private void clearBoard()
    {
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                for (int k = 0; k < boardSize; k++)
                {
                    board[i, j, k].GetComponent<Renderer>().enabled = false;
                }
            }
        }
    }

    private void setBoard()
    {
        clearBoard();
        int j = 0;
        int c = snake.getSnake().Count;
        foreach (Vector3 i in snake.getSnake())
        {
            board[(int)i.x, (int)i.y, (int)i.z].GetComponent<MeshFilter>().mesh = cubeMesh;
            board[(int)i.x, (int)i.y, (int)i.z].GetComponent<Renderer>().enabled = true;
            board[(int)i.x, (int)i.y, (int)i.z].GetComponent<Renderer>().material.color = new Color(((float)j / c), 1-((float)j / c), 0);
            j++;
        }

        Vector3 a = snake.getApple();
        board[(int)a.x, (int)a.y, (int)a.z].GetComponent<MeshFilter>().mesh = sphereMesh;
        board[(int)a.x, (int)a.y, (int)a.z].GetComponent<Renderer>().enabled = true;
        board[(int)a.x, (int)a.y, (int)a.z].GetComponent<Renderer>().material.color = new Color(1, 0, 0);
    }


}

public class Snake
{
    Vector3 dir = new Vector3(0, 1, 0);
    Vector3 apple = new Vector3(0, 0, 0);
    LinkedList<Vector3> s = new LinkedList<Vector3>();
    int bSize = GameManager.boardSize;
    bool grow = false;

    public Snake()
    {
        int halfBoard = bSize / 2;
        s.AddLast(new Vector3(3, halfBoard, halfBoard));
        s.AddLast(new Vector3(2, halfBoard, halfBoard));
        s.AddLast(new Vector3(1, halfBoard, halfBoard));
        s.AddLast(new Vector3(0, halfBoard, halfBoard));
    }

    public LinkedList<Vector3> getSnake()
    {
        return s;
    }

    public Vector3 getApple()
    {
        return apple;
    }

    public void move()
    {
        Vector3 newMove = s.First.Value;
        newMove += dir;
        
        if (checkBounds(newMove) && checkCollision(newMove))
        {
            s.AddFirst(newMove);
            if (grow)
                grow = false;
            else
                s.RemoveLast();
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
        if (newMove == apple)
        {
            grow = true;
            GameManager.score++;
            spawnApple();
        }
    }

    private bool checkBounds(Vector3 p)
    {
        return (p.x < bSize && p.x >= 0 && p.y < bSize && p.y >= 0 && p.z < bSize && p.z >= 0);
    }

    private bool checkCollision(Vector3 p)
    {
        foreach(Vector3 i in s)
        {
            if(i == p)
            {
                return false;
            }
        }
        return true;
    }

    public void setDir(Vector3 v)
    {
        if((dir.x != 0 && v.x != 0)||(dir.y != 0 && v.y != 0)||(dir.z != 0 && v.z != 0))
        {
            GameBoard.snakeDir = dir;
        }
        else
        {
            dir = v;
        }
    }

    public void spawnApple()
    {
        bool appleLegal = true;
        do
        {
            appleLegal = true;
            apple.x = UnityEngine.Random.Range(0, bSize - 1);
            apple.y = UnityEngine.Random.Range(0, bSize - 1);
            apple.z = UnityEngine.Random.Range(0, bSize - 1);
            foreach (Vector3 i in s)
            {
                if (i == apple)
                    appleLegal = false;
            }
        } while (!appleLegal);
    }
}
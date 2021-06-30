using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public float speed = 1.0f;
    float _lastMoveDown = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            transform.position += new Vector3(-1, 0, 0);

            if (!IsInGrid())
            {
                transform.position += new Vector3(1, 0, 0);
            }
            else
            {
                UpdateGameBoard();
            }
        }

        if (Input.GetKeyDown("d"))
        {
            transform.position += new Vector3(1, 0, 0);

            if (!IsInGrid())
            {
                transform.position += new Vector3(-1, 0, 0);
            }
            else
            {
                UpdateGameBoard();
            }
        }

        if (Input.GetKeyDown("s") || Time.time - _lastMoveDown >= 1)
        {
            transform.position += new Vector3(0, -1, 0);

            if (!IsInGrid())
            {
                transform.position += new Vector3(0, 1, 0);

                //stop when shape hit the buttom
                enabled = false;

                FindObjectOfType<Spawner>().SpawnShape();
            }
            else
            {
                UpdateGameBoard();
            }

            _lastMoveDown = Time.time;
        }

        if (Input.GetKeyDown("w"))
        {
            transform.Rotate(0, 0, 90);

            if (!IsInGrid())
            {
                transform.Rotate(0, 0, -90);
            }
            else
            {
                UpdateGameBoard();
            }
        }

        if (Input.GetKeyDown("e"))
        {
            transform.Rotate(0, 0, -90);

            if (!IsInGrid())
            {
                transform.Rotate(0, 0, 90);
            }
            else
            {
                UpdateGameBoard();
            }
        }
    }

    public bool IsInGrid()
    {
        int childCount = 0;

        foreach (Transform childBlock in transform)
        {
            Vector2 vect = RoundVector(childBlock.position);
            childCount++;

            if (!isInBorder(vect))
            {
                return false;
            }

            if (GameBoard.gameBoard[(int)vect.x, (int)vect.y] != null &&
                 GameBoard.gameBoard[(int)vect.x, (int)vect.y].parent != transform)
            {
                return false;
            }
        }
        return true;
    }

    public Vector2 RoundVector(Vector2 vect)
    {
        return new Vector2(Mathf.Round(vect.x), Mathf.Round(vect.y));
    }

    public static bool isInBorder(Vector2 pos)
    {
        return ((int)pos.x >= -4.5 && (int)pos.x <= 5 && (int)pos.y >= -8);
    }

    public void UpdateGameBoard()
    {
        for (int y = 0; y < 20; ++y)
        {
            for (int x = 0; x < 10; ++x)
            {
                if (GameBoard.gameBoard[x, y] != null &&
                    GameBoard.gameBoard[x, y].parent == transform)
                {
                    GameBoard.gameBoard[x, y] = null;
                }
            }
        }

        foreach (Transform childBlock in transform)
        {
            Vector2 vect = RoundVector(childBlock.position);


            GameBoard.gameBoard[(int)vect.x, (int)vect.y] = childBlock;

            Debug.Log("Cube At: " + vect.x + " " + vect.y);

        }
        GameBoard.PrintArray();
    }
}

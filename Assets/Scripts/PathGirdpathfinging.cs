using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGirdpathfinging : MonoBehaviour
{
    private PathNodepathfinging[,] grid;
    public int width = 0;
    public int height = 0;

    LayerMask defaultMask;

    public bool drawGrid = true;
    float delay = 0;

    private void Start()
    {
        defaultMask = LayerMask.GetMask("Default");
    }

    public void DrawGrid()
    {
        grid = new PathNodepathfinging[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 pos = new Vector2(x, y);
                if (y % 2 == 0)
                {
                    pos.x += 0.5f;
                }
                pos.y *= 0.85f;

                grid[x, y] = new PathNodepathfinging(pos);

                int open = 0;

                RaycastHit2D hit = Physics2D.Raycast(pos, pos, 0, defaultMask);
                if (hit)
                {
                    if (hit.collider.gameObject.GetComponent<HexGenerator>().type == 3)
                        open = 10;
                    if (hit.collider.gameObject.GetComponent<HexGenerator>().type == 2)
                        open = 10;
                    if (hit.collider.gameObject.GetComponent<HexGenerator>().type == 1)
                        open = 0;
                    if (hit.collider.gameObject.GetComponent<HexGenerator>().type == 0)
                        open = 0;
                }

                grid[x, y].open = open;
                grid[x, y].oldOpen = open;
            }
        }
    }

    void Update()
    {
        delay -= Time.deltaTime;

        if (drawGrid && delay < 0)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (grid[x, y].open == 10)
                        Debug.DrawLine(grid[x, y].pos - new Vector2(0.5f, 0.5f), grid[x, y].pos + new Vector2(0.5f, 0.5f), Color.red, 0.5f);
                    else if (grid[x, y].open == 0)
                        Debug.DrawLine(grid[x, y].pos - new Vector2(0.5f, 0.5f), grid[x, y].pos + new Vector2(0.5f, 0.5f), Color.green, 0.5f);
                    else
                        Debug.DrawLine(grid[x, y].pos - new Vector2(0.5f, 0.5f), grid[x, y].pos + new Vector2(0.5f, 0.5f), Color.cyan, 0.5f);
                }
            }
        }
    }


    public PathNodepathfinging GetNode(Vector2 pos)
    {
        pos.y = Mathf.RoundToInt(pos.y / 0.85f);
        if (pos.y % 2 == 0)
            pos.x -= 0.5f;
        return grid[(int)pos.x, (int)pos.y];
    }

    public PathNodepathfinging GetNodeInt(int x, int y)
    {
        return grid[x, y];
    }
}

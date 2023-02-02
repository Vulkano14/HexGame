using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapCreator : MonoBehaviour
{

    public Vector2 worldMapSize = new Vector2(10, 10);
    public GameObject hexSprite;
    public GameObject hexesSprites;
    int island;
    int islandGreen;
    int islandGray;
    const float perfectHexNumber = 0.85f;
    const float hexMove = 0.5f;


    private void Start()
    {
        island = (int)(worldMapSize.x * worldMapSize.y * 0.05f);
        islandGreen = (int)(worldMapSize.x * worldMapSize.y * 0.1f);
        islandGray = (int)(worldMapSize.x * worldMapSize.y * 0.25f);
        WorldHexGenerate();
    }

    void WorldHexGenerate()
    {

        transform.position = new Vector3(worldMapSize.x / 2, worldMapSize.y * perfectHexNumber / 2, -10);

        for (int x = 0; x < worldMapSize.x; x++)
        {
            for (int y = 0; y < worldMapSize.y; y++)
            {
                Vector2 currentPosition = new Vector2(x, y * perfectHexNumber);

                if (y % 2 == 0)
                    currentPosition.x += hexMove;

                Instantiate(hexSprite, currentPosition, Quaternion.identity, hexesSprites.transform);
            }
        }

        int islandStop = island;
        int stop = 0;

        while (islandStop > 0 && stop < island * 2)
        {
            stop += 1;

            Vector2 position = new Vector2(Mathf.Round(Random.value * worldMapSize.x - 1), Mathf.Round(Random.value * worldMapSize.y - 1));

            if (position.y % 2 == 0)
                position.x += hexMove;

            position.y *= perfectHexNumber;

            RaycastHit2D changeHex = Physics2D.Raycast(position, position, 0);

            if (changeHex)
            {
                HexGenerator newColorHex = changeHex.collider.gameObject.GetComponent<HexGenerator>();

                if (newColorHex.type == 0)
                {
                    newColorHex.ChangeHex(1);
                    islandStop -= 1;
                }
            }
        }

        int islandStopGreen = islandGreen;
        int stopGreen = 0;

        while (islandStopGreen > 0 && stop < islandGreen * 2)
        {
            stopGreen += 1;

            Vector2 position = new Vector2(Mathf.Round(Random.value * worldMapSize.x - 1), Mathf.Round(Random.value * worldMapSize.y - 1));

            if (position.y % 2 == 0)
                position.x += hexMove;

            position.y *= perfectHexNumber;

            RaycastHit2D changeHex = Physics2D.Raycast(position, position, 0);

            if (changeHex)
            {
                HexGenerator newColorHex = changeHex.collider.gameObject.GetComponent<HexGenerator>();

                if (newColorHex.type == 0)
                {
                    newColorHex.ChangeHex(2);
                    islandStopGreen -= 1;
                }
            }
        }

        int islandStopGray = islandGray;
        int stopGray = 0;

        while (islandStopGray > 0 && stop < islandGray * 2)
        {
            stopGray += 1;

            Vector2 position = new Vector2(Mathf.Round(Random.value * worldMapSize.x - 1), Mathf.Round(Random.value * worldMapSize.y - 1));

            if (position.y % 2 == 0)
                position.x += hexMove;

            position.y *= perfectHexNumber;

            RaycastHit2D changeHex = Physics2D.Raycast(position, position, 0);

            if (changeHex)
            {
                HexGenerator newColorHex = changeHex.collider.gameObject.GetComponent<HexGenerator>();

                if (newColorHex.type == 0)
                {
                    newColorHex.ChangeHex(3);
                    islandStopGray -= 1;
                }
            }
        }

        Invoke("DrawGrid", Time.deltaTime);
    }


    void DrawGrid()
    {
        int placed = 0;
        int attemps = 0;

        while (placed < 1 && attemps < 100)
        {
            attemps += 1;

            Vector2 pos = new Vector2(Mathf.RoundToInt(Random.value * worldMapSize.x - 1), Mathf.RoundToInt(Random.value * worldMapSize.y - 1));
            RaycastHit2D hit = Physics2D.Raycast(pos, pos, 0, LayerMask.GetMask("Default"));

            if (hit)
            {
                if (hit.collider.gameObject.name.Contains("BlueHex"))
                {
                    if (hit.collider.gameObject.GetComponent<HexGenerator>().type != 3 && hit.collider.gameObject.GetComponent<HexGenerator>().type != 2)
                    {
                        Instantiate(Resources.Load("Captain_Model"), hit.collider.gameObject.transform.position, Quaternion.identity, hit.collider.gameObject.transform);
                        placed += 1;
                    }
                }
            }
        }


        GetComponent<PathGirdpathfinging>().width = (int)worldMapSize.x;
        GetComponent<PathGirdpathfinging>().height = (int)worldMapSize.y;
        GetComponent<PathGirdpathfinging>().DrawGrid();
    }
}

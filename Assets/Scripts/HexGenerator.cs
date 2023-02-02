using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGenerator : MonoBehaviour
{
    public Sprite[] hexSprites;
    public int type = 0;

    public void ChangeHex(int n)
    {
        type = n;
        GetComponent<SpriteRenderer>().sprite = hexSprites[n];
    }
}

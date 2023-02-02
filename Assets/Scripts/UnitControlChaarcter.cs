using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class UnitControlChaarcter : MonoBehaviour
{
    public List<Vector2> path = new List<Vector2>();
    public int currNode = 0;
    public float delay = 0.5f;

    public SpriteRenderer highlight;
    public bool highlit = false;


    private void Start()
    {
        
    }

    void Update()
    {
        if (highlit)
            highlight.color = new Color32(255, 255, 255, 255);
        else
            highlight.color = new Color32(255, 255, 255, 0);

        if (path.Count > 0)
        {
            if (delay >= 0.5f)
            {
                RaycastHit2D hit = Physics2D.Raycast(path[currNode], path[currNode], 0, LayerMask.GetMask("Default"));
                if (hit)
                    MoveTo(hit.collider.gameObject);
                else
                {
                    Debug.Log("No Tile Found");
                    path = new List<Vector2>();
                    currNode = 0;
                    delay = 0.5f;
                }
            }
            else
            {
                delay += Time.deltaTime;
            }
        }
    }


    void MoveTo(GameObject hex)
    {
        transform.position = hex.transform.position;
        transform.parent = hex.transform;

        delay = 0;

        currNode += 1;
        if (currNode >= path.Count)
        {
            path = new List<Vector2>();
            currNode = 0;
            delay = 0.5f;
        }
    }
}

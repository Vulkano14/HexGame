using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CharacterMove : MonoBehaviour
{
    GameObject unit;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (unit == null)
            {
                RaycastHit2D hit = Physics2D.Raycast(pos, pos, 0, LayerMask.GetMask("Units"));
                if (hit)
                {
                    unit = hit.collider.gameObject;
                    unit.GetComponent<UnitControlChaarcter>().highlit = true;
                }
                    
            }
            else
            {
                RaycastHit2D hit = Physics2D.Raycast(pos, pos, 0, LayerMask.GetMask("Units"));
                if (hit)
                {
                    if (hit.collider.gameObject == unit)
                    {
                        unit.GetComponent<UnitControlChaarcter>().highlit = false;
                        unit = null;
                    }
                    else
                    {
                        unit.GetComponent<UnitControlChaarcter>().highlit = false;
                        unit = hit.collider.gameObject;
                        unit.GetComponent<UnitControlChaarcter>().highlit = true;
                    }
                        
                }
                else
                {
                    hit = Physics2D.Raycast(pos, pos, 0, LayerMask.GetMask("Default"));
                    if (hit)
                    {
                        unit.GetComponent<UnitControlChaarcter>().path = new List<Vector2>();
                        unit.GetComponent<UnitControlChaarcter>().currNode = 0;
                        unit.GetComponent<UnitControlChaarcter>().delay = 0.5f;
                        unit.GetComponent<UnitControlChaarcter>().path = GetComponent<PathFinding>().GetPath(unit.transform.position, hit.collider.gameObject.transform.position, 3);
                    }
                }
                    
            }
        }
    }
}

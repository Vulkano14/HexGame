using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public PathGirdpathfinging grid;

    List<PathNodepathfinging> openNodes = new List<PathNodepathfinging>();
    List<PathNodepathfinging> closeNodes = new List<PathNodepathfinging>();

    private void Start()
    {
        grid = GetComponent<PathGirdpathfinging>();
    }

    public List<Vector2> GetPath(Vector2 startPos, Vector2 endPos, int unitType)
    {
        List<Vector2> pathVect = new List<Vector2>();

        PathNodepathfinging startNode = grid.GetNode(startPos);
        PathNodepathfinging endNode = grid.GetNode(endPos);

        if (startNode == null || endNode == null)
        {
            Debug.Log("Invalid Positions");
            pathVect.Add(startPos);
            return pathVect;
        }

        openNodes = new List<PathNodepathfinging> { startNode };
        closeNodes = new List<PathNodepathfinging>();

        for (int x = 0; x < grid.width; x++)
        {
            for (int y = 0; y < grid.height; y++)
            {
                PathNodepathfinging pathNode = grid.GetNodeInt(x, y);
                pathNode.gCost = 9999999;
                pathNode.CalcFcost();
                pathNode.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalcDist(startNode.pos, endNode.pos);
        startNode.CalcFcost();

        while (openNodes.Count > 0)
        {
            PathNodepathfinging currNode = GetLowestF();
            if (currNode == endNode)
            {
                return CalcPath(currNode);
            }
            openNodes.Remove(currNode);
            closeNodes.Add(currNode);

            foreach (PathNodepathfinging neighbour in GetNeighbours(currNode))
            {
                if (closeNodes.Contains(neighbour))
                    continue;
                if (neighbour.open > unitType)
                {
                    closeNodes.Add(neighbour);
                    continue;
                }
                else
                {
                    float newGCost = currNode.gCost + 1;

                    if (newGCost < neighbour.gCost)
                    {
                        neighbour.cameFromNode = currNode;
                        neighbour.gCost = newGCost;
                        neighbour.hCost = CalcDist(neighbour.pos, endNode.pos);
                        neighbour.CalcFcost();

                        if (!openNodes.Contains(neighbour))
                            openNodes.Add(neighbour);
                    }
                }
            }
        }

                Debug.Log("You can't get here!");
        pathVect.Add(startPos);
        return pathVect;
    }

    private float CalcDist(Vector2 a, Vector2 b)
    {
        float xDist = Mathf.Abs(a.x - b.x);
        float yDist = Mathf.Abs(a.y - b.y);

        return xDist + yDist;
    }

    private PathNodepathfinging GetLowestF()
    {
        PathNodepathfinging lowestF = openNodes[0];
        for (int n = 0; n < openNodes.Count; n++)
        {
            if (openNodes[n].fCost < lowestF.fCost)
                lowestF = openNodes[n];
        }
        return lowestF;
    }

    private List<PathNodepathfinging> GetNeighbours(PathNodepathfinging currNode)
    {
        List<PathNodepathfinging> neighbourList = new List<PathNodepathfinging>();

        if (currNode.pos.x - 1 > 0)
            neighbourList.Add(grid.GetNode(currNode.pos - new Vector2(1, 0)));
        if (currNode.pos.x + 1 < grid.width)
            neighbourList.Add(grid.GetNode(currNode.pos + new Vector2(1, 0)));

        if (Mathf.RoundToInt((currNode.pos.y + 0.85f) / 0.85f) < grid.height)
        {
            if (currNode.pos.x - 0.5f > 0)
                neighbourList.Add(grid.GetNode(currNode.pos + new Vector2(-0.5f, 0.85f)));
            if (currNode.pos.x + 0.5f < grid.width)
                neighbourList.Add(grid.GetNode(currNode.pos + new Vector2(0.5f, 0.85f)));
        }

        if (currNode.pos.y - 0.85f > 0)
        {
            if (currNode.pos.x - 0.5f > 0)
                neighbourList.Add(grid.GetNode(currNode.pos + new Vector2(-0.5f, -0.85f)));
            if (currNode.pos.x + 0.5f < grid.width)
                neighbourList.Add(grid.GetNode(currNode.pos + new Vector2(0.5f, -0.85f)));
        }

        return neighbourList;
    }

    private List<Vector2> CalcPath(PathNodepathfinging endNode)
    {
        List<PathNodepathfinging> path = new List<PathNodepathfinging>();
        path.Add(endNode);
        PathNodepathfinging currNode = endNode;
        while (currNode.cameFromNode != null)
        {
            path.Add(currNode.cameFromNode);
            currNode = currNode.cameFromNode;
        }
        path.Reverse();

        List<Vector2> pathVect = new List<Vector2>();

        for (int n = 0; n < path.Count; n++)
            pathVect.Add(path[n].pos);

        return pathVect;
    }
}

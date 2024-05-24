using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPath : MonoBehaviour
{
    [SerializeField]
    public GameObject[] pathPoints;

    public int pathCount;

    public Vector2Int bottomLeft, topRight;
    public Vector2 startPos, targetPos;
    public List<Node> FinalNodeList;
    public bool dontCrossCorner;
    int sizeX, sizeY;
    Node[,] NodeArray;
    Node StartNode, TargetNode, CurNode;
    List<Node> OpenList, ClosedList;

    private Vector2 currPosition, nextPos, destiPos;
    public float speed = 10f;
    public List<Node> path;
    public int curNum, nodeNum, destinum;
    public bool isFixd = false; //목적지 도착 시 이동 X
    Vector3 lastPos;
    GameObject Destination;

    int dutytime;
    int patrolCount = 0;
    bool inshower = false;


    void Start()
    {
        bottomLeft = new Vector2Int(-77, -121);
        topRight = new Vector2Int(113, 67);

        pathPoints = GameObject.FindGameObjectsWithTag("Path");
        pathCount = GameObject.Find("Path").transform.childCount;

        curNum = 0;
        targetPos = this.gameObject.transform.position;
    }

    void Update()
    {
        startPos = this.gameObject.transform.position;

        if (startPos == targetPos)
        {
            if(dutytime ==0)
            {
                if (destinum == 15 || patrolCount > 4)
                {
                    inshower = !inshower;
                }
                if (inshower)
                {
                    destinum = Random.Range(15, 22);
                    patrolCount++;
                }
                else
                {
                    destinum = Random.Range(0, 16); //path 중 한 곳 선정
                }
            }
            else if (dutytime == 1)
            {
                destinum = 22 | 23; //종료시간까지 홀딩, 업무 미완료 시 검문모드
            }
            else if (dutytime == 2)
            {
                destinum = 24 | 25; //종료시간까지 홀딩, 순찰모드
            }

            Destination = pathPoints[destinum];
            destiPos = Destination.transform.position;

            targetPos = destiPos;
        }

        PathFinding();

        path = FinalNodeList;

        GuardMove();
    }

    void FixedUpdate()
    {
        dutytime = GameObject.Find("GameManager").GetComponent<GameManager>().dutyTime;
    }

    void GuardMove()
    {
        currPosition = startPos;
        nodeNum = path.Count;
        nextPos = new Vector2(path[curNum].x, path[curNum].y);
        float walk = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(currPosition, nextPos, walk);
        if (currPosition == nextPos)
        {
            if (curNum < nodeNum) curNum++;
        }
    }

    public void PathFinding()
    {
        // NodeArray의 크기 정해주고, isWall, x, y 대입
        sizeX = topRight.x - bottomLeft.x + 1;
        sizeY = topRight.y - bottomLeft.y + 1;
        NodeArray = new Node[sizeX, sizeY];

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                bool isWall = false;
                foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(i + bottomLeft.x, j + bottomLeft.y), 0.4f))
                    if (col.gameObject.layer == LayerMask.NameToLayer("Path")) isWall = true;

                NodeArray[i, j] = new Node(isWall, i + bottomLeft.x, j + bottomLeft.y);
            }
        }


        // 시작과 끝 노드, 열린리스트와 닫힌리스트, 마지막리스트 초기화

        StartNode = NodeArray[(int)(startPos.x) - bottomLeft.x, (int)(startPos.y) - bottomLeft.y];
        TargetNode = NodeArray[(int)(targetPos.x) - bottomLeft.x, (int)(targetPos.y) - bottomLeft.y];

        OpenList = new List<Node>() { StartNode };
        ClosedList = new List<Node>();
        FinalNodeList = new List<Node>();


        while (OpenList.Count > 0)
        {
            // 열린리스트 중 가장 F가 작고 F가 같다면 H가 작은 걸 현재노드로 하고 열린리스트에서 닫힌리스트로 옮기기
            CurNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
                if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H) CurNode = OpenList[i];

            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);


            // 마지막
            if (CurNode == TargetNode)
            {
                Node TargetCurNode = TargetNode;
                while (TargetCurNode != StartNode)
                {
                    FinalNodeList.Add(TargetCurNode);
                    TargetCurNode = TargetCurNode.ParentNode;
                }
                FinalNodeList.Add(StartNode);
                FinalNodeList.Reverse();

                //for (int i = 0; i < FinalNodeList.Count; i++) print(i + "번째는 " + FinalNodeList[i].x + ", " + FinalNodeList[i].y);
                return;
            }


            // ↑ → ↓ ←
            OpenListAdd(CurNode.x, CurNode.y + 1);
            OpenListAdd(CurNode.x + 1, CurNode.y);
            OpenListAdd(CurNode.x, CurNode.y - 1);
            OpenListAdd(CurNode.x - 1, CurNode.y);
        }
    }

    void OpenListAdd(int checkX, int checkY)
    {
        // 상하좌우 범위를 벗어나지 않고, 벽이 아니면서, 닫힌리스트에 없다면
        if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.y && checkY < topRight.y + 1
            && !NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isWall
            && !ClosedList.Contains(NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]))
        {
            // 코너를 가로질러 가지 않을시, 이동 중에 수직수평 장애물이 있으면 안됨
            if (dontCrossCorner)
                if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall
                    || NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;


            // 이웃노드에 넣고, 직선은 10, 대각선은 14비용
            Node NeighborNode = NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
            int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 14);


            // 이동비용이 이웃노드G보다 작거나 또는 열린리스트에 이웃노드가 없다면 G, H, ParentNode를 설정 후 열린리스트에 추가
            if (MoveCost < NeighborNode.G || !OpenList.Contains(NeighborNode))
            {
                NeighborNode.G = MoveCost;
                NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetNode.x) + Mathf.Abs(NeighborNode.y - TargetNode.y)) * 10;
                NeighborNode.ParentNode = CurNode;

                OpenList.Add(NeighborNode);
            }
        }
    }

    void OnDrawGizmos()
    {
        if (FinalNodeList.Count != 0) for (int i = 0; i < FinalNodeList.Count - 1; i++)
                Gizmos.DrawLine(new Vector2(FinalNodeList[i].x, FinalNodeList[i].y), new Vector2(FinalNodeList[i + 1].x, FinalNodeList[i + 1].y));
    }
}
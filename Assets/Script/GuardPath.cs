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
    public bool isFixd = false; //������ ���� �� �̵� X
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
                    destinum = Random.Range(0, 16); //path �� �� �� ����
                }
            }
            else if (dutytime == 1)
            {
                destinum = 22 | 23; //����ð����� Ȧ��, ���� �̿Ϸ� �� �˹����
            }
            else if (dutytime == 2)
            {
                destinum = 24 | 25; //����ð����� Ȧ��, �������
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
        // NodeArray�� ũ�� �����ְ�, isWall, x, y ����
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


        // ���۰� �� ���, ��������Ʈ�� ��������Ʈ, ����������Ʈ �ʱ�ȭ

        StartNode = NodeArray[(int)(startPos.x) - bottomLeft.x, (int)(startPos.y) - bottomLeft.y];
        TargetNode = NodeArray[(int)(targetPos.x) - bottomLeft.x, (int)(targetPos.y) - bottomLeft.y];

        OpenList = new List<Node>() { StartNode };
        ClosedList = new List<Node>();
        FinalNodeList = new List<Node>();


        while (OpenList.Count > 0)
        {
            // ��������Ʈ �� ���� F�� �۰� F�� ���ٸ� H�� ���� �� ������� �ϰ� ��������Ʈ���� ��������Ʈ�� �ű��
            CurNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
                if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H) CurNode = OpenList[i];

            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);


            // ������
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

                //for (int i = 0; i < FinalNodeList.Count; i++) print(i + "��°�� " + FinalNodeList[i].x + ", " + FinalNodeList[i].y);
                return;
            }


            // �� �� �� ��
            OpenListAdd(CurNode.x, CurNode.y + 1);
            OpenListAdd(CurNode.x + 1, CurNode.y);
            OpenListAdd(CurNode.x, CurNode.y - 1);
            OpenListAdd(CurNode.x - 1, CurNode.y);
        }
    }

    void OpenListAdd(int checkX, int checkY)
    {
        // �����¿� ������ ����� �ʰ�, ���� �ƴϸ鼭, ��������Ʈ�� ���ٸ�
        if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.y && checkY < topRight.y + 1
            && !NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isWall
            && !ClosedList.Contains(NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]))
        {
            // �ڳʸ� �������� ���� ������, �̵� �߿� �������� ��ֹ��� ������ �ȵ�
            if (dontCrossCorner)
                if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall
                    || NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;


            // �̿���忡 �ְ�, ������ 10, �밢���� 14���
            Node NeighborNode = NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
            int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 14);


            // �̵������ �̿����G���� �۰ų� �Ǵ� ��������Ʈ�� �̿���尡 ���ٸ� G, H, ParentNode�� ���� �� ��������Ʈ�� �߰�
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
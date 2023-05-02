using System.Collections;
using UnityEngine;

public class Concept2BattleManager : MonoBehaviour
{

    public Queue<Concept2AutoBattleController> BattleQueue = new Queue<Concept2AutoBattleController>();
    private Stage_Fight_Concept2 StageFightConcept;

    void Start()
    {
        StageFightConcept = gameObject.GetComponent<Stage_Fight_Concept2>();

        //컨셉은 큐에 들어온 애들을 꺼내서 해당 컴포넌트의 적에게 데미지를 가하는 내용
        //애니메이션 등은 추후 관리할 예정 
        StartCoroutine(nameof(GetFight));
    }
    IEnumerator GetFight()
    {
        while (true)
        {
            while (!BattleQueue.Count.Equals(0))
            {
                Attack_Auto();
                yield return new WaitForSeconds(3.0f);

            }
            yield return new WaitForSeconds(1.5f);
        }


    }
    private void Attack_Auto()
    {
        Concept2AutoBattleController Controller = BattleQueue.Dequeue();
        if (Controller.Equals(null)) return;
        Concept2AutoBattleController Enemy = null;
        int _Position = Controller.Position;

        if (Controller.gameObject.CompareTag("Enemy"))
        {
            //적이 나를 공격
            //적은 오른쪽 나는 왼쪽
            //거리를 이용해 가장 가까운 저 ㄱ을 찾기
            float minDist = float.MaxValue;
            GameObject[] Characters = GameObject.FindGameObjectsWithTag("Character");
            foreach (GameObject character in Characters)
            {
                float dist = Vector2.Distance(character.transform.position, Controller.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    Enemy = character.GetComponent<Concept2AutoBattleController>();
                }
            }
            if (Enemy != null)
            {
                Vector2 NextPos = new Vector2(Enemy.transform.position.x + 50, Enemy.transform.position.y);
                StartCoroutine(Battle_Position_Move_Co(NextPos, 1.2f, Controller));
                Enemy.Hp -= Controller.Attack;
                Debug.Log($"{Controller.name} ATTACK ----> {Enemy.name}");
            }
        }
        else
        {
            //거리를 이용해 가장 가까운 적 찾기
            //내가 적을 공격
            float minDist = float.MaxValue;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                float dist = Vector2.Distance(enemy.transform.position, Controller.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    Enemy = enemy.GetComponent<Concept2AutoBattleController>();
                }
            }
            // 가장 가까운 적 위치로 이동
            if (Enemy != null)
            {
                Vector2 NextPos = new Vector2(Enemy.transform.position.x - 5, Enemy.transform.position.y);
                StartCoroutine(Battle_Position_Move_Co(NextPos, 1.2f, Controller));
                Enemy.Hp -= Controller.Attack;
                Debug.Log($"{Controller.name} ATTACK ----> {Enemy.name}");
            }
        }
        //나는 공격모션 , 피격자는 히트 모션...
        //일반 프리코네랑 달라서.. animation 이거 괜찬을려나 모르곘


        Managers.Sound.Play($"{Controller.name}_ub");

    }

    IEnumerator Battle_Position_Move_Co(Vector2 Pos, float time, Concept2AutoBattleController Mine)
    {
        Vector2 PrevPos = gameObject.transform.position;
        float gotime = 0;
        while (gotime < time)
        {
            gotime += Time.deltaTime;
            Mine.GetComponent<Rigidbody2D>().MovePosition(Vector2.Lerp(PrevPos, Pos, gotime / time));
            yield return null;
        }

        yield return new WaitForSeconds(time);

        Mine.transform.localPosition = Vector2.zero;

    }



}

public class QueueNode<T>
{
    public T data;
    public QueueNode<T> next;

    public QueueNode(T data)
    {
        this.data = data;
        this.next = null;
    }
}
public class Queue<T>
{
    public int Count = 0;
    private QueueNode<T> _firstNode { get; set; }
    private QueueNode<T> _lastNode { get; set; }
    public Queue()
    {
        _firstNode = null;
        _lastNode = null;
        Count = 0;
    }
    public bool IsEmpty()
    {
        return (Count == 0);
    }
    public void Enqueue(T data)
    {
        QueueNode<T> newNode = new QueueNode<T>(data);

        if (_firstNode == null)
        {
            _firstNode = newNode;
            _lastNode = newNode;
        }
        else
        {
            _lastNode.next = newNode;
            _lastNode = newNode;
        }
        Count++;
    }
    public T Dequeue()
    {
        if (_firstNode == null)
        {
            throw new System.Exception("Queue is empty");
        }

        T data = _firstNode.data;
        _firstNode = _firstNode.next;

        if (_firstNode == null)
        {
            _lastNode = null;
        }
        Count--;
        return data;
    }
    public void Push(T data)
    {
        QueueNode<T> newNode = new QueueNode<T>(data);

        if (_firstNode == null)
        {
            _firstNode = newNode;
            _lastNode = newNode;
        }
        else
        {
            newNode.next = _firstNode;
            _firstNode = newNode;
        }
        Count++;
    }

}

using UnityEngine;

// 발판을 생성하고 주기적으로 재배치하는 스크립트
public class Item : MonoBehaviour
{
    public GameObject StarPrefab, BombPrefab, AddJumpPrefab; // 생성할 발판의 원본 프리팹
    public int count = 3; // 생성할 발판의 개수

    public float timeBetSpawnMin = 1.25f; // 다음 배치까지의 시간 간격 최솟값
    public float timeBetSpawnMax = 2.25f; // 다음 배치까지의 시간 간격 최댓값
    private float delta; // 다음 배치까지의 시간 간격
    private float Spawntime;



    void Start()
    {
        Spawntime = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
        delta = 0f;
    }

    void Update()
    {
        // 순서를 돌아가며 주기적으로 발판을 배치
        if (GameManager.instance.isGameover) return;

        delta += Time.deltaTime;
        if (Spawntime < delta)
        {
            delta = 0f;
            Spawntime = Random.Range(timeBetSpawnMin, timeBetSpawnMax);

            int itemprobability = Random.Range(1, 11);
            if (itemprobability == 1)
            {
                GameObject Itemtype = Instantiate(BombPrefab);
                Itemtype.transform.position = new Vector2(12f, Random.Range(0f, 4f));
            }
            else if(itemprobability == 2)
            {
                GameObject Itemtype = Instantiate(StarPrefab);
                Itemtype.transform.position = new Vector2(12f, Random.Range(0f, 4f));
            }
            else
            {
                GameObject Itemtype = Instantiate(AddJumpPrefab);
                Itemtype.transform.position = new Vector2(12f, Random.Range(0f, 4f));
            }
        }
    }
}
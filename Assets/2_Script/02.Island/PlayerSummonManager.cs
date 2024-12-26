using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSummonManager : MonoBehaviour
{
    [System.Serializable]
    public class UnitData
    {
        public GameObject unitPrefab; // 소환할 유닛 프리팹
        public int cost; // 유닛 소환 비용
        public Button summonButton; // 소환 버튼
    }

    public UnitData[] units; // 소환 가능한 유닛 데이터 배열
    public Transform spawnPoint; // 공통 스폰 포인트
    public Transform enemyBase; // 적 기지 Transform

    private void Start()
    {
        // 버튼에 소환 함수 연결
        for (int i = 0; i < units.Length; i++)
        {
            int index = i; // 로컬 변수로 인덱스를 저장
            units[i].summonButton.onClick.AddListener(() => SummonUnit(index));
        }

        // 버튼 활성화 상태 업데이트 코루틴
        StartCoroutine(UpdateButtonStates());
    }

    private void SummonUnit(int unitIndex)
    {
        if (unitIndex < 0 || unitIndex >= units.Length)
        {
            Debug.LogError("잘못된 유닛 인덱스입니다.");
            return;
        }

        UnitData unit = units[unitIndex];

        // GoldManager에서 자원 확인
        if (GoldManager.instance.gold >= unit.cost)
        {
            // 유닛 생성
            GameObject summonedUnit = Instantiate(unit.unitPrefab, spawnPoint.position, Quaternion.identity);

            // 생성된 유닛의 목표 설정
            var unitMovement = summonedUnit.GetComponent<UnitMovement>();
            if (unitMovement != null)
            {
                unitMovement.targetEnemyBase = enemyBase;
            }

            // GoldManager를 통해 자원 감소
            GoldManager.instance.AddGold(-unit.cost);
            Debug.Log($"유닛 소환! 남은 자원: {GoldManager.instance.gold}");
        }
        else
        {
            Debug.Log("자원이 부족합니다!");
        }
    }

    private IEnumerator UpdateButtonStates()
    {
        while (true)
        {
            foreach (var unit in units)
            {
                if (unit.summonButton != null)
                {
                    // 자원이 부족하면 버튼 비활성화, 충분하면 활성화
                    unit.summonButton.interactable = GoldManager.instance.gold >= unit.cost;
                }
            }
            yield return new WaitForSeconds(0.1f); // 0.1초마다 버튼 상태 갱신
        }
    }
}

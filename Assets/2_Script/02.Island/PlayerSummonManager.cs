using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSummonManager : MonoBehaviour
{
    [System.Serializable]
    public class UnitData
    {
        public GameObject unitPrefab; // ��ȯ�� ���� ������
        public int cost; // ���� ��ȯ ���
        public Button summonButton; // ��ȯ ��ư
    }

    public UnitData[] units; // ��ȯ ������ ���� ������ �迭
    public Transform spawnPoint; // ���� ���� ����Ʈ
    public Transform enemyBase; // �� ���� Transform

    private void Start()
    {
        // ��ư�� ��ȯ �Լ� ����
        for (int i = 0; i < units.Length; i++)
        {
            int index = i; // ���� ������ �ε����� ����
            units[i].summonButton.onClick.AddListener(() => SummonUnit(index));
        }

        // ��ư Ȱ��ȭ ���� ������Ʈ �ڷ�ƾ
        StartCoroutine(UpdateButtonStates());
    }

    private void SummonUnit(int unitIndex)
    {
        if (unitIndex < 0 || unitIndex >= units.Length)
        {
            Debug.LogError("�߸��� ���� �ε����Դϴ�.");
            return;
        }

        UnitData unit = units[unitIndex];

        // GoldManager���� �ڿ� Ȯ��
        if (GoldManager.instance.gold >= unit.cost)
        {
            // ���� ����
            GameObject summonedUnit = Instantiate(unit.unitPrefab, spawnPoint.position, Quaternion.identity);

            // ������ ������ ��ǥ ����
            var unitMovement = summonedUnit.GetComponent<UnitMovement>();
            if (unitMovement != null)
            {
                unitMovement.targetEnemyBase = enemyBase;
            }

            // GoldManager�� ���� �ڿ� ����
            GoldManager.instance.AddGold(-unit.cost);
            Debug.Log($"���� ��ȯ! ���� �ڿ�: {GoldManager.instance.gold}");
        }
        else
        {
            Debug.Log("�ڿ��� �����մϴ�!");
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
                    // �ڿ��� �����ϸ� ��ư ��Ȱ��ȭ, ����ϸ� Ȱ��ȭ
                    unit.summonButton.interactable = GoldManager.instance.gold >= unit.cost;
                }
            }
            yield return new WaitForSeconds(0.1f); // 0.1�ʸ��� ��ư ���� ����
        }
    }
}

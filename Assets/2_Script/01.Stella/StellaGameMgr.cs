using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StellaGameMgr : MonoBehaviour
{
    private static StellaGameMgr instance;
    public static StellaGameMgr Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public RoundInfo[] roundInfo;
    public int round;
    public StellaBoss boss;
    public TMP_Text[] missionText;
    public TMP_Text missionTileText;
    public GameObject missionSelectButton;
    public GameObject mission;

    public int currentSelectMission;

    private void Start()
    {
        RoundStart();
    }

    public void RoundStart()
    {
        mission.SetActive(true);
        switch (round)
        {
            case 0:
                missionTileText.text = roundInfo[0].tileNames;
                for (int i = 0; i < missionText.Length; i++)
                {
                    missionText[i].text = roundInfo[0].missionText[i];
                }
                break;

            case 1:
                missionTileText.text = roundInfo[1].tileNames;
                for (int i = 0; i < missionText.Length; i++)
                {
                    missionText[i].text = roundInfo[1].missionText[i];
                }
                break;

            case 2:
                missionTileText.text = roundInfo[2].tileNames;
                for (int i = 0; i < missionText.Length; i++)
                {
                    missionText[i].text = roundInfo[2].missionText[i];
                }
                break;

            case 3:
                missionTileText.text = roundInfo[3].tileNames;
                for (int i = 0; i < missionText.Length; i++)
                {
                    missionText[i].text = roundInfo[3].missionText[i];
                }
                break;
        }
    }

    public void OnClickedSelectMissionBtn()
    {
        switch (round)
        {
            case 0:
                if(currentSelectMission == 4)
                {
                    round++;
                    RoundStart();
                    boss.TakeDamage(boss.maxHp / 4);
                }
                else
                {
                    boss.StartShoot();
                }
                break;

            case 1:
                if (currentSelectMission == 2)
                {
                    round++;
                    RoundStart();
                    boss.TakeDamage(boss.maxHp / 4);
                }
                else
                {
                    boss.StartShoot();
                }
                break;

            case 2:
                if (currentSelectMission == 1)
                {
                    round++;
                    RoundStart();
                    boss.TakeDamage(boss.maxHp / 4);
                }
                else
                {
                    boss.StartShoot();
                }
                break;

            case 3:
                if (currentSelectMission == 1)
                {
                    round++;
                    RoundStart();
                    boss.TakeDamage(boss.maxHp / 4);
                }
                else
                {
                    boss.StartShoot();
                }
                break;
        }
    }
}
[System.Serializable]
public class RoundInfo
{
    public string tileNames;
    public string[] missionText;

}


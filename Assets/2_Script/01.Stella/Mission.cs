using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mission : MonoBehaviour
{
    public TMP_Text[] missionText;
    public int missionNumber;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            for(int i = 0; i < missionText.Length; i++)
            {
                missionText[i].color = Color.red;
                StellaGameMgr.Instance.currentSelectMission = missionNumber;
                StellaGameMgr.Instance.missionSelectButton.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < missionText.Length; i++)
            {
                missionText[i].color = Color.white;
                StellaGameMgr.Instance.currentSelectMission = 0;
                StellaGameMgr.Instance.missionSelectButton.SetActive(false);
            }
        }
    }
}

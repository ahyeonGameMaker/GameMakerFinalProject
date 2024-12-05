using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    void Start()
    {
        InitBoard();
    }

    void InitBoard()
    {
        float spaceY = 3.05f;
        float spaceX = 4.05f;

        int rowCount = 3;
        int colCount = 4;

        for (int row = 0; row < rowCount; row++)
        {
            for(int col =0; col< colCount; col++)
            {
                float posY = (row - (int)(rowCount / 2)) * spaceY;
                float posX = (col - (int)(colCount / 2)) * spaceX + (spaceX/2); 
                Vector3 pos = new Vector3(posX, posY, 0);
                Instantiate(cardPrefab, pos, Quaternion.identity);
            }
        }
    }
}

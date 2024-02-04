using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueGenerator : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject queuePlayerTurnPrefab;
    [SerializeField] private GameObject queueDragonTurnPrefab;

    [Header("References")]
    [SerializeField] private Transform queueBar;

    [Header("Parameters")]
    private const int queueCount = 7;
    private bool[] turns = new bool[queueCount]; //0-ty & 1-smok
    private Transform[] turnsIcons = new Transform[queueCount];
    
    
    
    
    
    
    
    private void Start()
    {
        GenerateGameQueue();
    }

    //Generuje pocz¹tkow¹ kolejkê gry
    private void GenerateGameQueue()
    {
        //Sta³a startowa sekwencja
        turns[0] = false;
        turns[1] = false;
        turns[2] = false;
        turns[3] = true;

        //Wygeneruj nastêpne tury
        for(int i=4; i<queueCount; i++)
        {
            turns[i] = PickNextTurn(i);
        }

        //Stworzenie ikonek
        for (int i = 0; i < queueCount; i++)
        {
            turnsIcons[i] = Instantiate(turns[i] == false ? queuePlayerTurnPrefab : queueDragonTurnPrefab, queueBar.position, Quaternion.identity, queueBar).transform;
        }
    }

    //Nastêpna tura
    public void UpdateGameQueue()
    {
        //Ustalenie tury w tablicy tur
        for(int i=0; i<queueCount-1; i++)
        {
            turns[i] = turns[i+1];
        }

        turns[queueCount-1] = PickNextTurn(queueCount - 1);



        //aktualizacja ikon tury
        Destroy(turnsIcons[0].gameObject);

        for (int i = 0; i < queueCount - 1; i++)
        {
            turnsIcons[i] = turnsIcons[i + 1];
        }

        turnsIcons[queueCount-1] = Instantiate(turns[queueCount-1] == false ? queuePlayerTurnPrefab : queueDragonTurnPrefab, queueBar.position, Quaternion.identity, queueBar).transform;
    }


    //Wylosuj now¹ ture
    private bool PickNextTurn(int n)
    {
        if (n > 1)
        {
            if (turns[n - 1] == turns[n - 2])
            {

                    return !turns[n - 1];

            }
        }

        return Random.Range(0,2) == 0 ? false : true;
    }

    public bool IsPlayerToMove()
    {
        if (!turns[0]) return true;

        return false;
    }
}

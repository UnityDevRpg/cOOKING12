using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    private OrdererController ordererController;
    public GameObject playerPrefab;
    public int MaxPlayerCount = 3;
    public int CurrentPlayerCount = 0;
    public float PlayerTimer = 6f;
    public float CurrentPlayerTimer = 0f;
    public GameObject Spawne1;

    private void FixedUpdate() {
        CurrentPlayerTimer += Time.deltaTime;
    }

    void Update()
    {
        SpawnPlayers();
    }

    public void SpawnPlayers()
    {
        if((CurrentPlayerTimer >= PlayerTimer) && CurrentPlayerCount != MaxPlayerCount){
            CurrentPlayerCount++;
            Instantiate(playerPrefab);
            CurrentPlayerTimer = 0;
        }
      
    }

}

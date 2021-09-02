using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    [SerializeField] private Coin coinPrefab;
    [SerializeField] private BoxCollider limits;
    [SerializeField] private float height;

    private Coin currentCoin;

    void Start()
    {
        coinPrefab.generator = this;
        coinPrefab.Restart();
    }

    public Vector3 GetRandomPosition()
    {
        Vector3 randomPos;
        randomPos = new Vector3(transform.position.x + Random.Range(-limits.size.x / 2, limits.size.x / 2), height, transform.position.z + Random.Range(-limits.size.z / 2, limits.size.z / 2));
        return randomPos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public CoinGenerator generator;
    private bool rotating;
    // Start is called before the first frame update
    void Start()
    {
        rotating = true;
        StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        while (rotating)
        {
            transform.eulerAngles += Vector3.up;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void Restart()
    {
        transform.position = generator.GetRandomPosition();
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }
}

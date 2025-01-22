using System.Collections;
using UnityEngine;

public class BubbleGenerator : MonoBehaviour
{
    [SerializeField] GameObject _bubbleObj;
    [SerializeField] Transform _spawnPos;

    void Start()
    {
        StartCoroutine(WaitThenBubble());
    }

    IEnumerator WaitThenBubble()
    {
        float timeToWait = Random.Range(10, 20);
        yield return new WaitForSeconds(timeToWait);
        Instantiate(_bubbleObj, _spawnPos.position, _spawnPos.rotation);
        StartCoroutine(WaitThenBubble());
    }
}

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
        float timeToWait = Random.Range(5, 10);
        yield return new WaitForSeconds(timeToWait);
        GameObject bubble = Instantiate(_bubbleObj, _spawnPos.position, _spawnPos.rotation);
        GameManager.GameManagerInstance.AddObject(bubble);
        StartCoroutine(WaitThenBubble());
    }
}

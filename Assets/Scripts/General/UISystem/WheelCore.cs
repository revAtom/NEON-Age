using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelCore : MonoBehaviour
{
    bool triggerCheckAllowed = false;

    private void Awake()
    {
        StartCoroutine(delaySetter());
    }
    IEnumerator delaySetter()
    {
        int i = Random.Range(0, 5);

        yield return new WaitForSeconds(i);

        Debug.Log("POP");

        triggerCheckAllowed = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggerCheckAllowed)
            return;

        Debug.Log("Collider");
        if (collision.gameObject.CompareTag("x1Icon"))
        {
            Debug.Log("Shit it is working, Omg your brain is not useless shit");
        }
    }
}

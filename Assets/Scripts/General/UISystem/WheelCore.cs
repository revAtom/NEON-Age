using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelCore : IconCore
{
     bool triggerCheckAllowed = false;

    int _spawnNum;

    public static WheelCore instance;

    float _delay = .25f;
    public GameObject[] Icons;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        StartCoroutine(SpawnIcons());
    }


    IEnumerator SpawnIcons()
    {
        if (!triggerCheckAllowed)
        {
            int _iconToSpawn = Random.Range(0, 15);

            if (_iconToSpawn <= 5)
                _spawnNum = 0;

            else if (_iconToSpawn > 5 && _iconToSpawn <= 10)
                _spawnNum = 1;

            else if (_iconToSpawn > 10 && _iconToSpawn <= 13)
                _spawnNum = 2;

            else if (_iconToSpawn > 13 && _iconToSpawn <= 15)
                _spawnNum = 3;

            GameObject icons = Instantiate(Icons[_spawnNum], transform.position, Quaternion.identity);

            icons.transform.SetParent(this.gameObject.transform);

            yield return new WaitForSeconds(_delay);

            StartCoroutine(SpawnIcons());
        }
    }

    public float DurationSetUpper()
    {
        if (durationOfScale >= 2)
        {
            this.StopAllCoroutines();

            triggerCheckAllowed = true;
        }

        if (durationOfScale <= 1)
            durationOfScale += .05f;

        else if (durationOfScale > 1 && durationOfScale <= 1.5)
            durationOfScale += .1f;

        else if (durationOfScale > 1.5 && durationOfScale < 2)
            durationOfScale += .3f;

        _delay = durationOfScale / 2;

        return durationOfScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggerCheckAllowed)
            return;

        collision.gameObject.GetComponent<IconCore>().StopAllCoroutines();

        switch (collision.gameObject.tag)
        {
            case "x1Icon":
                Debug.Log("x1");
                break;
            case "x2Icon":
                Debug.Log("x2");
                break;
            case "x4Icon":
                Debug.Log("x4");
                break;
            case "x6Icon":
                Debug.Log("x6");
                break;
        }
        triggerCheckAllowed = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconCore : MonoBehaviour
{
    float _timer;

    protected float durationOfScale = .5f;

    private Transform _point1, _point2;

    private void Awake()
    {
        durationOfScale = WheelCore.instance.DurationSetUpper();

        _point1 = GameObject.FindGameObjectWithTag("Point1").GetComponent<Transform>();
        _point2 = GameObject.FindGameObjectWithTag("Point2").GetComponent<Transform>();

        StartCoroutine(IconVisualChange());
    }

    protected IEnumerator IconVisualChange()
    {

            while (_timer < durationOfScale)
            {
                transform.localScale = Vector3.Lerp(new Vector2(.2f, .2f), Vector2.one, Mathf.Sin(Mathf.PI * (_timer / durationOfScale)));

                transform.position = Vector3.Lerp(_point1.position, _point2.position, (_timer / durationOfScale));

                _timer += Time.deltaTime;
                yield return null;
            }

            Destroy(this.gameObject);
        
    }
}

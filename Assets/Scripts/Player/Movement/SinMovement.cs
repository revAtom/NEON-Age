using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMovement : MonoBehaviour
{
    public float sinValue, sinWave;

    private void Update()
    {
        float sinY = sinWave * Mathf.Sin(Time.time * sinValue);

        float valueX = transform.position.x;
        float valueZ = transform.position.z;

        transform.position = new Vector3(valueX, sinY, valueZ);
    }
}

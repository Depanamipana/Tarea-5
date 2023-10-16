using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour{
    public float width = 0.1f;

    [Header ("References")]
    public Transform startPoint;
    public Transform endPoint;

    // Update is called once per frame
    void Update()
    {
        if (startPoint == null) { Destroy(gameObject); return;}
        Vector3 start = startPoint.position;
        Vector3 end = endPoint.position;

        var offset = end - start;
        var scale = new Vector3(width, offset.magnitude / 2.0f, width);
        var position = start + (offset / 2.0f);

        transform.position = position;
        transform.up = offset;
        transform.localScale = scale;

    }
}

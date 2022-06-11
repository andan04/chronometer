using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChangerScript : MonoBehaviour
{
    float noteSpeed;

    // Start is called before the first frame update
    void Start()
    {
        noteSpeed = BMSLoader.GetNoteSpeed();

        StartCoroutine(DestroyThis());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + Vector3.down * noteSpeed * Time.deltaTime;


    }
    IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}


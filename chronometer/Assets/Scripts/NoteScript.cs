using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour
{
    [SerializeField]
    float noteSpeed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyThis());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + transform.InverseTransformDirection(Vector3.up) * noteSpeed * Time.deltaTime;
        transform.localScale += transform.localScale * Time.deltaTime * noteSpeed / 10;


    }
    IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}

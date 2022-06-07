using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChangerScript : MonoBehaviour
{
    [SerializeField]
    float noteSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale -= transform.localScale * Time.deltaTime * noteSpeed;
        if(transform.localScale.x <= 0.5f)
        {
            Destroy(gameObject);
        }
    }
}

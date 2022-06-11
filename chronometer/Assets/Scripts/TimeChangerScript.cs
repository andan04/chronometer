using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChangerScript : MonoBehaviour
{
    // 노트 속도 변수
    float noteSpeed;

    // Start is called before the first frame update
    void Start()
    {
        noteSpeed = BMSLoader.GetNoteSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        // 생성되면 크기 줄어들게 했음
        transform.localScale -= transform.localScale * Time.deltaTime * noteSpeed;
        if(transform.localScale.x <= 0.5f) // 크기 어느정도 작아지면 없애는거, 나중에 미스 판정도 이쪽에 둬야될듯?
        {
            Destroy(gameObject);
        }
    }
}

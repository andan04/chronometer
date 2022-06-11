using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGenerator : MonoBehaviour
{
    double currentTime = 0d;
    int bar = 0;
    float bpm = 0;
    [SerializeField]
    GameObject timeChangerDay;
    [SerializeField]
    GameObject timeChangernight;
    [SerializeField]
    GameObject dayNote;
    [SerializeField]
    GameObject nightNote;
    [SerializeField]
    GameObject note1;
    [SerializeField]
    GameObject note2;
    [SerializeField]
    GameObject note3;
    [SerializeField]
    GameObject note4;
    [SerializeField]
    GameObject timeChanger;
    Vector3 note1Pos;
    Vector3 note2Pos;
    Vector3 note3Pos;
    Vector3 note4Pos;
    Vector3 timeChangerPos;



    int barCount;

    List<char> timeChangerNote;
    List<char> upNote;
    List<char> rightNote;
    List<char> downNote;
    List<char> leftNote;

    private void Awake()
    {
        BMSLoader.SetBmsFile("FREEDOMDiVE");
        BMSLoader.LoadBmsNote();
    }

    // Start is called before the first frame update
    void Start()
    {
        note1Pos = note1.transform.position;
        note2Pos = note2.transform.position;
        note3Pos = note3.transform.position;
        note4Pos = note4.transform.position;
        timeChangerPos = timeChanger.transform.position;

        bpm = float.Parse(BMSLoader.GetBmsInfo()[2]);
        barCount = BMSLoader.GetBarCount() / 4;
        timeChangerNote = BMSLoader.GetTimeChangerNote();
        upNote = BMSLoader.GetUpNote();
        rightNote = BMSLoader.GetRightNote();
        downNote = BMSLoader.GetDownNote();
        leftNote = BMSLoader.GetLeftNote();

        print(bpm);

        //string tmp = "";

        //for (int i = 0; i < timeChangerNote.Count; i++)
        //{
        //    tmp += timeChangerNote[i];
        //}
        //print(tmp);
        //tmp = "";

        //for (int i = 0; i < upNote.Count; i++)
        //{
        //    tmp += upNote[i];
        //}
        //print(tmp);

        //tmp = "";

        //for (int i = 0; i < rightNote.Count; i++)
        //{
        //    tmp += rightNote[i];
        //}
        //print(tmp);

        //tmp = "";

        //for (int i = 0; i < downNote.Count; i++)
        //{
        //    tmp += downNote[i];
        //}
        //print(tmp);

        //tmp = "";

        //for (int i = 0; i < leftNote.Count; i++)
        //{
        //    tmp += leftNote[i];
        //}
        //print(tmp);

    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime * barCount;

        if(currentTime >= 60d / bpm)
        {
            currentTime -= 60d / bpm;
            try
            {
                if (timeChangerNote[bar] == '1')
                {
                    Instantiate(timeChangerDay, timeChangerPos, Quaternion.identity);
                }
                else if (timeChangerNote[bar] == '2')
                {
                    Instantiate(timeChangernight, timeChangerPos, Quaternion.identity);
                }
            }
            catch
            {

            }
            try
            {
                if (upNote[bar] == '1')
                {
                    Instantiate(dayNote, note1Pos, Quaternion.identity);
                }
                else if (upNote[bar] == '2')
                {
                    Instantiate(nightNote, note1Pos, Quaternion.identity);

                }

            }
            catch
            {

            }
            try
            {
                if (rightNote[bar] == '1')
                {
                    Instantiate(dayNote, note2Pos, Quaternion.identity);
                }
                else if (rightNote[bar] == '2')
                {
                    Instantiate(nightNote, note2Pos, Quaternion.identity);

                }

            }
            catch
            {

            }
            try
            {
                if (downNote[bar] == '1')
                {
                    Instantiate(dayNote, note3Pos, Quaternion.identity);
                }
                else if (downNote[bar] == '2')
                {
                    Instantiate(nightNote, note3Pos, Quaternion.identity);

                }

            }
            catch
            {

            }
            try
            {
                if (leftNote[bar] == '1')
                {
                    Instantiate(dayNote, note4Pos, Quaternion.identity);
                }
                else if (leftNote[bar] == '2')
                {
                    Instantiate(nightNote, note4Pos, Quaternion.identity);

                }

            }
            catch
            {

            }
            bar++;

        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGenerator : MonoBehaviour
{
    double currentTime = 0d;
    int bar = 0;
    float bpm = 0;
    [SerializeField]
    GameObject timeChanger;
    [SerializeField]
    GameObject note;
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
                    print("aaa");
                    Instantiate(timeChanger, new Vector3(0, 0, 0), Quaternion.identity);
                    print("bbb");
                }
            }
            catch
            {

            }
            try
            {
                if (upNote[bar] == '1')
                {
                    NoteGenerate(note, Quaternion.Euler(new Vector3(0, 0, 0)));
                }
            }
            catch
            {

            }
            try
            {
                if (rightNote[bar] == '1')
                {
                    NoteGenerate(note, Quaternion.Euler(new Vector3(0, 0, 90)));
                }
            }
            catch
            {

            }
            try
            {
                if (downNote[bar] == '1')
                {
                    NoteGenerate(note, Quaternion.Euler(new Vector3(0, 0, 180)));
                }
            }
            catch
            {

            }
            try
            {
                if (leftNote[bar] == '1')
                {
                    NoteGenerate(note, Quaternion.Euler(new Vector3(0, 0, 270)));
                }
            }
            catch
            {

            }
            bar++;

        }

    }

    void NoteGenerate(GameObject obj, Quaternion q)
    {
        Instantiate(obj, new Vector3(0, 0, 0), q);
    }
}

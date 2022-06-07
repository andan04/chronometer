using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NoteGenerator : MonoBehaviour
{
    // 시간 바꾸는 노트 리스트, 위, 오른쪽, 아래, 왼쪽 노트 리스트 변수임
    [SerializeField]
    List<int> timeChangerNote;
    [SerializeField]
    List<int> upNote;
    [SerializeField]
    List<int> rightNote;
    [SerializeField]
    List<int> downNote;
    [SerializeField]
    List<int> leftNote;
    string[] bmsFile = File.ReadAllLines(@"Assets\BMS\FREEDOMDiVE.bms");
    [SerializeField]
    string title;
    [SerializeField]
    string artist;
    [SerializeField]
    string bpm;
    [SerializeField]
    string level;

    int barNum;
    int noteNum;




    private void Awake()
    {
        StartCoroutine(ReadNote());
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ReadNote()
    {
        print(bmsFile.Length);
        foreach (string line in bmsFile)
        {
            if (line == null)
            {
                break;
            }
            if (line.StartsWith("#"))
            {
                string[] linelist = line.Split(new char[] { ' ', ':' });
                if (linelist[0] == "#TITLE")
                {
                    title = linelist[1];
                }
                else if (linelist[0] == "#ARTIST")
                {
                    artist = linelist[1];
                }
                else if (linelist[0] == "#BPM")
                {
                    bpm = linelist[1];
                }
                else if (linelist[0] == "#PLAYLEVEL")
                {
                    level = linelist[1];
                }
                else
                {
                    try
                    {
                        barNum = int.Parse(linelist[0].Substring(1, 3));
                        noteNum = int.Parse(linelist[0].Substring(4, 2));
                        print(barNum + " " + noteNum);

                    }
                    catch
                    {

                    }
                }

            }
        }

        yield return null;
    }
}

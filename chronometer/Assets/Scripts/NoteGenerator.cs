using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NoteGenerator : MonoBehaviour
{
    // 11 : 좌회전 노트, 낮으로 변경 노트
    // 12 : 우회전 노트, 밤으로 변경 노트
    // 13, 14, 15, 18 : 위, 오른쪽, 아래, 왼쪽
    // 시간 바꾸는 노트 리스트, 위, 오른쪽, 아래, 왼쪽 노트 리스트 변수임
    const int timeChangerDay = 11;
    const int timeChangerNight = 12;
    const int up = 13;
    const int right = 14;
    const int down = 15;
    const int left = 18;

    // bms에서 노트 짤 때 썼던 마디별 격자 개수임, 일단 16비트 기준으로 해둠
    [SerializeField] 
    int barCount = 16;

    // [00000000100010001010] 이런식으로 각 리스트 값이 있을거고 0일 때는 노트가 없는거고 1일 때 노트가 있는거임
    // 위치별 노트 담은 리스트임, 큐로 바꿀 수 있으면 좋을 듯 리스트라서 좀 느릴거 같음
    [SerializeField] 
    List<char> timeChangerNoteDay;  
    [SerializeField]
    List<char> timeChangerNoteNight;
    [SerializeField]
    List<char> upNote;
    [SerializeField]
    List<char> rightNote;
    [SerializeField]
    List<char> downNote;
    [SerializeField]
    List<char> leftNote;

    // bms파일 받아오는 코드임 @"Assets\Bms\bms파일.bms"로 쓰면 됨
    string[] bmsFile = File.ReadAllLines(@"Assets\BMS\FREEDOMDiVE.bms");

    // bms파일 파싱해서 불러올 값들임, 제목, 작곡가, bpm, 난이도 순임
    [SerializeField] 
    string title;
    [SerializeField]
    string artist;
    [SerializeField]
    string bpm;
    [SerializeField]
    string level;

    // bms파일에서 MAIN DATA FIELD 밑에 있는 노트 받아올 때 사용할 임시 변수임 #XXXOO에서 마디 순서인 XXX가 barNum에 들어가고
    // 어느 위치의 노트인지를 알 수 있는 OO부분이 noteNum에 들어감
    int barNum; 
    int noteNum;


    private void Awake()
    {
        // 씬 시작할 때 노트 미리 로딩함
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
        // print(bmsFile.Length);
        // bms 파일 불러와서 각 줄별로 읽음
        foreach (string line in bmsFile)
        {
            // 줄 끝나면 foreach 끝남
            if (line == null)
            {
                break;
            }
            // #으로 시작하는 경우에만 사용함 -> 제목, 아티스트, 노트 등 정보 있음
            if (line.StartsWith("#"))
            {
                // bms파일이 ' '랑 ':'기준으로 값 표시하니까 이거 기준으로 split 해서 배열로 썼음
                // [0]은 변수고 [1]은 값을 가질꺼임 (#TITLE 노래제목 -> [#TITLE, 노래 제목]) 이런식으로
                string[] linelist = line.Split(new char[] { ' ', ':' });
                // 타이틀
                if (linelist[0] == "#TITLE")
                {
                    title = linelist[1];
                }
                // 작곡가 정보
                else if (linelist[0] == "#ARTIST")
                {
                    artist = linelist[1];
                }
                // bpm 정보
                else if (linelist[0] == "#BPM")
                {
                    bpm = linelist[1];
                }
                // 악곡 레벨
                else if (linelist[0] == "#PLAYLEVEL")
                {
                    level = linelist[1];
                }
                else
                {
                    // 위에서 안썼던 #PLAYER나 #GENRE 형변환하면 오류나니까 try로 썼음
                    try
                    {
                        // #XXXOO에서 각각 마디번호, 위치 번호임
                        barNum = int.Parse(linelist[0].Substring(1, 3));
                        noteNum = int.Parse(linelist[0].Substring(4, 2));
                        //print(barNum + " " + noteNum);
                        // 왼쪽 회전 노트일 때
                        if(noteNum == timeChangerDay)
                        {
                            // 중간에 비어있는 노트 있으면 0으로 채우는거임
                            if(barNum * barCount != timeChangerNoteDay.Count)
                            {
                                int tmp = timeChangerNoteDay.Count;
                                for (int _ = 0; _ < barNum * barCount - tmp; _++)
                                {
                                    timeChangerNoteDay.Add('0');
                                }
                                //print(linelist[1].Length);
                                //print(barCount / (linelist[1].Length / 2) - 1);
                                // 마디마다 길이가 고정이 아니라서 만들 때 썼던 마디별 격자 개수 사용해서 빈칸을 0으로 채워줬음
                                for(int _ = 0; _ < linelist[1].Length / 2; _++)
                                {
                                    timeChangerNoteDay.Add(linelist[1][2 * _ + 1]);
                                    for(int i = 0; i < barCount / (linelist[1].Length / 2) - 1; i++)
                                    {
                                        timeChangerNoteDay.Add('0');
                                    }
                                }
                            }
                            // 중간에 비어있는 노트 없을때는 그냥 채움
                            else
                            {
                                for (int _ = 0; _ < linelist[1].Length / 2; _++)
                                {
                                    timeChangerNoteDay.Add(linelist[1][2 * _ + 1]);
                                    for (int i = 0; i < barCount / (linelist[1].Length / 2) - 1; i++)
                                    {
                                        timeChangerNoteDay.Add('0');
                                    }
                                }
                            }
                        }
                        // 오른쪽 회전 노트일 때
                        else if (noteNum == timeChangerNight)
                        {
                            if (barNum * barCount != timeChangerNoteNight.Count)
                            {
                                int tmp = timeChangerNoteNight.Count;
                                for (int _ = 0; _ < barNum * barCount - tmp; _++)
                                {
                                    timeChangerNoteNight.Add('0');
                                }
                                //print(linelist[1].Length);
                                //print(barCount / (linelist[1].Length / 2) - 1);
                                for (int _ = 0; _ < linelist[1].Length / 2; _++)
                                {
                                    timeChangerNoteNight.Add(linelist[1][2 * _ + 1]);
                                    for (int i = 0; i < barCount / (linelist[1].Length / 2) - 1; i++)
                                    {
                                        timeChangerNoteNight.Add('0');
                                    }
                                }
                            }
                            else
                            {
                                for (int _ = 0; _ < linelist[1].Length / 2; _++)
                                {
                                    timeChangerNoteNight.Add(linelist[1][2 * _ + 1]);
                                    for (int i = 0; i < barCount / (linelist[1].Length / 2) - 1; i++)
                                    {
                                        timeChangerNoteNight.Add('0');
                                    }
                                }
                            }

                        }
                        // 위쪽 노트일 때
                        else if(noteNum == up)
                        {
                            if (barNum * barCount != upNote.Count)
                            {
                                int tmp = upNote.Count;
                                for (int _ = 0; _ < barNum * barCount - tmp; _++)
                                {
                                    timeChangerNoteDay.Add('0');
                                }
                                //print(linelist[1].Length);
                                //print(barCount / (linelist[1].Length / 2) - 1);
                                for (int _ = 0; _ < linelist[1].Length / 2; _++)
                                {
                                    upNote.Add(linelist[1][2 * _ + 1]);
                                    for (int i = 0; i < barCount / (linelist[1].Length / 2) - 1; i++)
                                    {
                                        upNote.Add('0');
                                    }
                                }
                            }
                            else
                            {
                                for (int _ = 0; _ < linelist[1].Length / 2; _++)
                                {
                                    upNote.Add(linelist[1][2 * _ + 1]);
                                    for (int i = 0; i < barCount / (linelist[1].Length / 2) - 1; i++)
                                    {
                                        upNote.Add('0');
                                    }
                                }
                            }

                        }
                        // 오른쪽 노트일 때
                        else if(noteNum == right)
                        {
                            if (barNum * barCount != rightNote.Count)
                            {
                                int tmp = rightNote.Count;
                                for (int _ = 0; _ < barNum * barCount - tmp; _++)
                                {
                                    timeChangerNoteDay.Add('0');
                                }
                                //print(linelist[1].Length);
                                //print(barCount / (linelist[1].Length / 2) - 1);
                                for (int _ = 0; _ < linelist[1].Length / 2; _++)
                                {
                                    rightNote.Add(linelist[1][2 * _ + 1]);
                                    for (int i = 0; i < barCount / (linelist[1].Length / 2) - 1; i++)
                                    {
                                        rightNote.Add('0');
                                    }
                                }
                            }
                            else
                            {
                                for (int _ = 0; _ < linelist[1].Length / 2; _++)
                                {
                                    rightNote.Add(linelist[1][2 * _ + 1]);
                                    for (int i = 0; i < barCount / (linelist[1].Length / 2) - 1; i++)
                                    {
                                        rightNote.Add('0');
                                    }
                                }
                            }

                        }
                        // 아래쪽 노트일 때
                        else if(noteNum == down)
                        {
                            if (barNum * barCount != downNote.Count)
                            {
                                int tmp = downNote.Count;
                                for (int _ = 0; _ < barNum * barCount - tmp; _++)
                                {
                                    downNote.Add('0');
                                }
                                //print(linelist[1].Length);
                                //print(barCount / (linelist[1].Length / 2) - 1);
                                for (int _ = 0; _ < linelist[1].Length / 2; _++)
                                {
                                    downNote.Add(linelist[1][2 * _ + 1]);
                                    for (int i = 0; i < barCount / (linelist[1].Length / 2) - 1; i++)
                                    {
                                        downNote.Add('0');
                                    }
                                }
                            }
                            else
                            {
                                for (int _ = 0; _ < linelist[1].Length / 2; _++)
                                {
                                    downNote.Add(linelist[1][2 * _ + 1]);
                                    for (int i = 0; i < barCount / (linelist[1].Length / 2) - 1; i++)
                                    {
                                        downNote.Add('0');
                                    }
                                }
                            }

                        }
                        // 왼쪽 노트일 때
                        else if(noteNum == left)
                        {
                            if (barNum * barCount != leftNote.Count)
                            {
                                int tmp = leftNote.Count;
                                for (int _ = 0; _ < barNum * barCount - tmp; _++)
                                {
                                    leftNote.Add('0');
                                }
                                //print(linelist[1].Length);
                                //print(barCount / (linelist[1].Length / 2) - 1);
                                for (int _ = 0; _ < linelist[1].Length / 2; _++)
                                {
                                    leftNote.Add(linelist[1][2 * _ + 1]);
                                    for (int i = 0; i < barCount / (linelist[1].Length / 2) - 1; i++)
                                    {
                                        leftNote.Add('0');
                                    }
                                }
                            }
                            else
                            {
                                for (int _ = 0; _ < linelist[1].Length / 2; _++)
                                {
                                    leftNote.Add(linelist[1][2 * _ + 1]);
                                    for (int i = 0; i < barCount / (linelist[1].Length / 2) - 1; i++)
                                    {
                                        leftNote.Add('0');
                                    }
                                }
                            }

                        }

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

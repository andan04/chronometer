using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BMSLoader : MonoBehaviour
{
    // 16 : 회전 노트, 회전 방향은 노트 출력할 때 정하는 걸로
    // 11, 12, 13, 14 : 위, 오른쪽, 아래, 왼쪽
    // 시간 바꾸는 노트 리스트, 위, 오른쪽, 아래, 왼쪽 노트 리스트 변수임
    const int timeChanger = 16;
    const int up = 11;
    const int right = 12;
    const int down = 13;
    const int left = 14;

    static int noteSpeed = 12;

    // bms에서 노트 짤 때 썼던 마디별 격자 개수임, 일단 16비트 기준으로 해둠
    static public int barCount = 16;

    // [00000000100010001010] 이런식으로 각 리스트 값이 있을거고 0일 때는 노트가 없는거고 1일 때 노트가 있는거임
    // 위치별 노트 담은 리스트임, 큐로 바꿀 수 있으면 좋을 듯 리스트라서 좀 느릴거 같음
    // up -> 1번 레인, right -> 2번 레인, down -> 3번 레인, left -> 4번 레인
    static List<char> timeChangerNote = new List<char> { };
    static List<char> upNote = new List<char> { };
    static List<char> rightNote = new List<char> { };
    static List<char> downNote = new List<char> { };
    static List<char> leftNote = new List<char> { };

    // bms파일 받아오는 코드임 @"Assets\Bms\bms파일.bms"로 쓰면 됨
    static string[] bmsFile = File.ReadAllLines(@"Assets\BMS\FREEDOMDiVE.bms");

    // bms파일 파싱해서 불러올 값들임, 제목, 작곡가, bpm, 난이도 순임
    static string title;
    static string artist;
    static string bpm;
    static string level;

    // bms파일에서 MAIN DATA FIELD 밑에 있는 노트 받아올 때 사용할 임시 변수임 #XXXOO에서 마디 순서인 XXX가 barNum에 들어가고
    // 어느 위치의 노트인지를 알 수 있는 OO부분이 noteNum에 들어감
    static int barNum;
    static int noteNum;

    static public void SetNoteSpeed(int speed)
    {
        noteSpeed = speed;
    }
    static public int GetNoteSpeed()
    {
        return noteSpeed;
    }
    static public void SetBarCount(int count)
    {
        barCount = count;
    }
    // title, artist, bpm, level 순으로 리턴해줌
    static public string[] GetBmsInfo()
    {
        return new string[] { title, artist, bpm, level };
    }
    // 이름을 인수로 받아서 bms파일 찾음
    static public void SetBmsFile(string name)
    {
        bmsFile = File.ReadAllLines(@"Assets\BMS\" + name + ".bms");
        LoadBmsInfo();
    }
    // bms노트 로드함
    static public void LoadBmsNote()
    {
        //#PLAYER나 #GENRE 형변환하면 오류나니까 try로 썼음
        foreach (string line in bmsFile)
        {
            // 줄 끝나면 foreach 끝남
            if (line == null)
            {
                break;
            }
            if (line.StartsWith("#"))
            {
                try
                {
                    // 노트 부분은 ':'기준으로 값 표시하니까 이거 기준으로 split 해서 배열로 썼음
                    // [0]은 마디랑 위치고 [1]은 값을 가질꺼임 (#00115:01010101 -> [#00115, 01010101]) 이런식으로
                    string[] linelist = line.Split(new char[] { ':' });
                    // #XXXOO에서 각각 마디번호, 위치 번호임
                    barNum = int.Parse(linelist[0].Substring(1, 3));
                    noteNum = int.Parse(linelist[0].Substring(4, 2));
                    //print(barNum + " " + noteNum);
                    // 회전 노트일 때
                    if (noteNum == timeChanger)
                    {
                        // 중간에 비어있는 노트 있으면 0으로 채우는거임
                        if (barNum * barCount != timeChangerNote.Count)
                        {
                            int tmp = timeChangerNote.Count;
                            for (int _ = 0; _ < barNum * barCount - tmp; _++)
                            {
                                timeChangerNote.Add('0');
                            }
                            //print(linelist[1].Length);
                            //print(barCount / (linelist[1].Length / 2) - 1);
                            // 마디마다 길이가 고정이 아니라서 만들 때 썼던 마디별 격자 개수 사용해서 빈칸을 0으로 채워줬음
                            for (int _ = 0; _ < linelist[1].Length / 2; _++)
                            {
                                timeChangerNote.Add(linelist[1][2 * _ + 1]);
                                for (int i = 0; i < barCount / (linelist[1].Length / 2) - 1; i++)
                                {
                                    timeChangerNote.Add('0');
                                }
                            }
                        }
                        // 중간에 비어있는 노트 없을때는 그냥 채움
                        else
                        {
                            for (int _ = 0; _ < linelist[1].Length / 2; _++)
                            {
                                timeChangerNote.Add(linelist[1][2 * _ + 1]);
                                for (int i = 0; i < barCount / (linelist[1].Length / 2) - 1; i++)
                                {
                                    timeChangerNote.Add('0');
                                }
                            }
                        }
                    }
                    // 위쪽 노트일 때
                    else if (noteNum == up)
                    {
                        if (barNum * barCount != upNote.Count)
                        {
                            int tmp = upNote.Count;
                            for (int _ = 0; _ < barNum * barCount - tmp; _++)
                            {
                                upNote.Add('0');
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
                    else if (noteNum == right)
                    {
                        if (barNum * barCount != rightNote.Count)
                        {
                            int tmp = rightNote.Count;
                            for (int _ = 0; _ < barNum * barCount - tmp; _++)
                            {
                                rightNote.Add('0');
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
                    else if (noteNum == down)
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
                    else if (noteNum == left)
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
    // 제목 아티스트 등 불러옴
    static public void LoadBmsInfo()
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
                // bms파일이 ' '기준으로 값 표시하니까 이거 기준으로 split 해서 배열로 썼음
                // [0]은 변수고 [1]은 값을 가질꺼임 (#TITLE 노래제목 -> [#TITLE, 노래 제목]) 이런식으로
                string[] linelist = line.Split(new char[] { ' ' });
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
            }
        }

    }
    static public int GetBarCount()
    {
        return barCount;
    }
    // 각 노트별 리스트 받아옴
    static public List<char> GetTimeChangerNote()
    {
        return timeChangerNote;
    }
    static public List<char> GetUpNote()
    {
        return upNote;
    }
    static public List<char> GetRightNote()
    {
        return rightNote;
    }
    static public List<char> GetDownNote()
    {
        return downNote;
    }
    static public List<char> GetLeftNote()
    {
        return leftNote;
    }

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitSprite;
    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }
    void GenerateData()
    {
        talkData.Add(1000, new string[] {"������������������������������������������:0", "��2��2:1"});

        talkData.Add(100, new string[] { "��", "������ �ڵ��ϰԵ�" });

        portraitData.Add(1000 + 0, portraitSprite[0]); // �÷��̾��� ���
        portraitData.Add(1000 + 1, portraitSprite[1]); // �÷��̾� ģ���� ���
    }
    // Update is called once per frame
    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length) // ��ȭ ���̰� �����ٸ�
        {
            GameManager.instance.gameState = GameManager.GameState.Run;
            return null;
        }
        else return talkData[id][talkIndex]; // �ƴ϶��
    }
    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}

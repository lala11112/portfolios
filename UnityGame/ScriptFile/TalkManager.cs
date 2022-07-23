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
        talkData.Add(1000, new string[] {"せせせせせせせせせせせせせせせせせせせせせ:0", "げ2げ2:1"});

        talkData.Add(100, new string[] { "焼", "増亜辞 坪漁馬惟喫" });

        portraitData.Add(1000 + 0, portraitSprite[0]); // 巴傾戚嬢税 乞柔
        portraitData.Add(1000 + 1, portraitSprite[1]); // 巴傾戚嬢 庁姥税 乞柔
    }
    // Update is called once per frame
    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length) // 企鉢 掩戚亜 魁概陥檎
        {
            GameManager.instance.gameState = GameManager.GameState.Run;
            return null;
        }
        else return talkData[id][talkIndex]; // 焼艦虞檎
    }
    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}

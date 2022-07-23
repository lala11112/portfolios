using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    PlayerHealth player;
    private AudioSource m_audio;
    public AudioClip startClip;
    public float damage; // 데미지
    private void Awake()
    {
        m_audio = GetComponent<AudioSource>();
        if(startClip != null) // 만약 오디오 클립을 넣었다면
        {
            m_audio.clip = startClip;
            m_audio.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    PlayerHealth player;
    private AudioSource m_audio;
    public AudioClip startClip;
    public float damage; // ������
    private void Awake()
    {
        m_audio = GetComponent<AudioSource>();
        if(startClip != null) // ���� ����� Ŭ���� �־��ٸ�
        {
            m_audio.clip = startClip;
            m_audio.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_music : MonoBehaviour
{
    public GameObject bg_music;
    
    private AudioSource audioSrc1;
    public GameObject[] objs11;

    void Awake()
    {
        objs11 = GameObject.FindGameObjectsWithTag("Sound");
        if (objs11.Length == 0)
        {
            bg_music = Instantiate(bg_music);
            bg_music.name = "BG_music";
            DontDestroyOnLoad(bg_music.gameObject);
        }
        else
        {
            bg_music = GameObject.Find("BGMusic1");
        }
    }
    void Start()
    {
        audioSrc1 = bg_music.GetComponent<AudioSource>();
    }
}

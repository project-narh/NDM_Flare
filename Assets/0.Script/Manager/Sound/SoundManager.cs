using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0, 1f)]
        public float volume;
    }

    public static SoundManager Instance { get; private set; }
    public AudioMixer mixer;
    public Sound[] BGM;
    public Sound[] Sfx;
    public AudioSource BGM_player;
    public AudioSource[] Sfx_player;

    public float B_volum { get; set; }
    public float S_volum { get; set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        BGM_player = gameObject.transform.GetChild(0).GetComponent<AudioSource>();
        Sfx_player = gameObject.transform.GetChild(1).GetComponents<AudioSource>();
    }

    public void play_BGM(string name)
    {
        Sound_Stop(BGM_player);
        Sound s = Sound_Find(name, BGM);
        if (s != null)
        {
            Sound_Start(BGM_player, s);
        }
        else
        {
            Debug.Log("해당 BGM을 찾을 수 없어서 실행이 최소 되었습니다");
        }
    }

    public void play_sfx(string name)
    {
        Sound s = Sound_Find(name, Sfx);
        AudioSource a = Empty_find();
        if (s != null)
        {
            if (a != null)
            {
                Sound_Stop(a);
                Sound_Start(a, s);
            }
            else
                Debug.Log("현재 빈 오디오 공간이 존재하지 않습니다");
        }
        else
        {
            Debug.Log("해당 효과음을을 찾을 수 없어서 실행이 최소 되었습니다");
        }
    }

    public void play_sfx(string name, float timer)
    {
        Sound s = Sound_Find(name, Sfx);
        AudioSource a = Empty_find();
        if (s != null)
        {
            if (a != null)
            {
                StartCoroutine(delay(s,a,timer));
            }
            else
                Debug.Log("현재 빈 오디오 공간이 존재하지 않습니다");
        }
        else
        {
            Debug.Log("해당 효과음을을 찾을 수 없어서 실행이 최소 되었습니다");
        }
    }

    IEnumerator delay(Sound s, AudioSource a, float timer)
    {
        yield return new WaitForSeconds(timer);
        Sound_Stop(a);
        Sound_Start(a, s);
    }

    public void Sfx_delay(string name, float time)
    {
        //Invoke("play_sfx(name)",time);
        StartCoroutine(delay(name, time));
    }

    public void Remove_BGM()
    {
        Sound_Stop(BGM_player);
    }
    public void Remove_SFX(string name)
    {
        AudioSource a = Find_sound(name);
        if (a != null) Sound_Stop(a);
    }

    private AudioSource Find_sound(string name)
    {
        for (int i = 0; i < Sfx_player.Length; i++)
        {
            if (Sfx_player[i].clip.name == name)
            {
                return Sfx_player[i];
            }
        }
        return null;
    }


    private Sound Sound_Find(string name, Sound[] s)
    {
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i].name.Equals(name))
            {
                return s[i];
            }
        }
        return null;
    }
    private Sound Sound_Find(string name)
    {
        for (int i = 0; i < BGM.Length; i++)
        {
            if (BGM[i].name.Equals(name))
            {
                return BGM[i];
            }
        }
        return null;
    }

    private AudioSource Empty_find()
    {
        for (int i = 0; i < Sfx_player.Length; i++)
        {
            if (!Sfx_player[i].isPlaying)
            {
                return Sfx_player[i];
            }
        }
        return null;
    }

    private void Sound_Start(AudioSource a, Sound s)
    {
        a.clip = s.clip;
        a.volume = s.volume;
        a.clip.name = s.name;
        a.Play();
    }

    private void Sound_Stop(AudioSource a)
    {
        if (a.isPlaying)
        {
            a.Stop();
            a.clip = null;
        }
    }



    IEnumerator delay(string name, float f)
    {
        yield return new WaitForSecondsRealtime(f);
        play_sfx(name);
    }


    public void Set_BGM(float n)
    {
        mixer.SetFloat("BGM",n);
        B_volum = n;
    }

    public void Set_SFX(float n)
    {
        mixer.SetFloat("SFX", n);
        S_volum = n;
    }
}

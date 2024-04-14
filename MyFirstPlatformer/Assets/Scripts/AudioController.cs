using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public GameObject[] effect;
    [SerializeField] private Player pl;
    [SerializeField] private GameObject hamumu;

    [SerializeField] private const string KEY_MUSIC_VOLUME = "musicVolume";
    [SerializeField] public AudioSource musicSource;
    [SerializeField] private Slider slValue;
    private float _vol;
    private void Start()
    {
        slValue = FindObjectOfType<Slider>();
        pl = FindObjectOfType<Player>();
        LoadSoundSettings();
    }

    private void Update()
    {
        SoundJump();
    }

    public void SoundJump() 
    {
        if ((Input.GetKeyDown(KeyCode.W) && pl.isGrounded == true)|| (Input.GetKeyDown(KeyCode.UpArrow) && pl.isGrounded == true)) 
        {
            GameObject insSound = Instantiate(effect[0], pl.transform.position, Quaternion.identity);
            Destroy(insSound, 2f);
        }
    }

    public void SoundHit()
    {
        GameObject insSound = Instantiate(effect[1], pl.transform.position, Quaternion.identity);
        Destroy(insSound, 2f);
    }

    public void SoundHamumuJump() 
    {
        GameObject insSound = Instantiate(effect[2], hamumu.transform.position, Quaternion.identity);
        Destroy(insSound, 2f);
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(KEY_MUSIC_VOLUME, musicSource.volume);
        PlayerPrefs.Save();
        //Debug.Log("Настройки звука сохранены! musicVolume: " + musicSource.volume);
    }

    public void LoadSoundSettings()
    {
        musicSource.volume = PlayerPrefs.GetFloat(KEY_MUSIC_VOLUME, 1.0f);
        _vol = musicSource.volume;
        slValue.value = _vol;
        //Debug.Log("Настройки звука загружены! musicVolume: " + musicSource.volume); 
    }
}

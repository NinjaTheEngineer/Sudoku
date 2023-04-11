using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : NinjaMonoBehaviour {
    private static AudioManager _instance;

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;

    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioClip sudokuCellClickSound;
    [SerializeField] private AudioClip sudokuFailedSound;
    [SerializeField] private AudioClip sudokuSolvedSound;
    [SerializeField] private AudioClip sudokuCellInitializedSound;
    [SerializeField] private AudioClip rightGuessSound;
    [SerializeField] private AudioClip wrongGuessSound;
    [SerializeField] private AudioClip backgroundMusic;
    public static AudioManager Instance {
        get {
            if(_instance == null) {
                _instance = FindObjectOfType<AudioManager>();
                if (_instance == null) {
                    _instance = new GameObject("AudioManager").AddComponent<AudioManager>();
                }
            }
            return _instance;
        }
    }
    private void Awake() {
        if (_instance==null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    private void Start() {
        PlayBackgroundMusic();
    }

    public void PlaySFX(AudioClip clip) {
        var logId = "PlaySFX";
        if(clip==null) {
            logw(logId, "Clip="+clip.logf()+" => no-op");
            return;
        }
        sfxSource.PlayOneShot(clip);
    }

    public void PlayButtonClick() {
        PlaySFX(buttonClickSound);
    }

    public void PlaySudokuFailedSound() {
        PlaySFX(sudokuFailedSound);
    }

    public void PlaySudokuSolvedSound() {
        PlaySFX(sudokuSolvedSound);
    }

    public void PlayWrongGuessSound() {
        PlaySFX(wrongGuessSound);
    }
    public void PlayRightGuessSound() {
        PlaySFX(rightGuessSound);
    }
    public void PlaySudokuCellInitializedSound() {
        PlaySFX(sudokuCellInitializedSound);
    }
    public void PlaySudokuCellClickSound() {
        PlaySFX(sudokuCellClickSound);
    }

    public void PlayBackgroundMusic() {
        var logId = "PlayBackgroundMusic";
        if(backgroundMusic==null) {
            logw(logId, "BackgroundMusic="+backgroundMusic.logf()+" => no-op");
            return;
        }
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopBackgroundMusic() {
        musicSource.Stop();
    }
}
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource soundsSource;
    public AudioClip buttonTap;
    public AudioClip wash;
    public AudioClip eat;
    public AudioClip pet;

    public static AudioManager instance;

    // Переменные для сохранения состояния звука
    private float savedMusicVolume;
    private float savedSoundsVolume;
    private bool wasMusicPlaying;
    private bool wasSoundsEnabled;

    private void Awake()
    {
        instance = this;
        
        // Сохраняем начальные настройки звука
        if (musicSource != null)
        {
            savedMusicVolume = musicSource.volume;
            wasMusicPlaying = musicSource.isPlaying;
        }
        
        if (soundsSource != null)
        {
            savedSoundsVolume = soundsSource.volume;
            wasSoundsEnabled = soundsSource.enabled;
        }
    }

    /// <summary>
    /// Вызывается когда приложение теряет фокус или сворачивается
    /// </summary>
    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            // Игра получила фокус - включаем звук
            RestoreAudio();
        }
        else
        {
            // Игра потеряла фокус - отключаем звук
            PauseAudio();
        }
    }

    /// <summary>
    /// Вызывается когда приложение ставится на паузу (мобильные устройства)
    /// </summary>
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            // Приложение на паузе - отключаем звук
            PauseAudio();
        }
        else
        {
            // Приложение возобновлено - включаем звук
            RestoreAudio();
        }
    }

    /// <summary>
    /// Отключает все звуки
    /// </summary>
    private void PauseAudio()
    {
        // Сохраняем текущее состояние
        if (musicSource != null)
        {
            savedMusicVolume = musicSource.volume;
            wasMusicPlaying = musicSource.isPlaying;
            musicSource.volume = 0f;
            musicSource.Pause();
        }

        if (soundsSource != null)
        {
            savedSoundsVolume = soundsSource.volume;
            wasSoundsEnabled = soundsSource.enabled;
            soundsSource.volume = 0f;
            soundsSource.Stop();
        }

        // Отключаем общий volume listener
        AudioListener.pause = true;
    }

    /// <summary>
    /// Восстанавливает звуки
    /// </summary>
    private void RestoreAudio()
    {
        // Включаем общий volume listener
        AudioListener.pause = false;

        // Восстанавливаем музыку
        if (musicSource != null)
        {
            musicSource.volume = savedMusicVolume;
            if (wasMusicPlaying)
            {
                musicSource.UnPause();
            }
        }

        // Восстанавливаем звуковые эффекты
        if (soundsSource != null)
        {
            soundsSource.volume = savedSoundsVolume;
            soundsSource.enabled = wasSoundsEnabled;
        }
    }

    public void OnTap()
    {
        soundsSource.PlayOneShot(buttonTap);
    }

    public void StartShower()
    {
        soundsSource.clip = wash;
        soundsSource.Play();
    }

    public void StopShower()
    {
        soundsSource.clip = null;
        soundsSource.Stop();
    }

    public void Eat()
    {
        soundsSource.PlayOneShot(eat);
    }

    public void Pet()
    {
        soundsSource.PlayOneShot(pet);
    }

    /// <summary>
    /// Ручное отключение звука (для настроек)
    /// </summary>
    public void MuteAll(bool mute)
    {
        if (mute)
        {
            PauseAudio();
        }
        else
        {
            RestoreAudio();
        }
    }

    /// <summary>
    /// Установка громкости музыки
    /// </summary>
    public void SetMusicVolume(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = volume;
            savedMusicVolume = volume;
        }
    }

    /// <summary>
    /// Установка громкости звуков
    /// </summary>
    public void SetSoundsVolume(float volume)
    {
        if (soundsSource != null)
        {
            soundsSource.volume = volume;
            savedSoundsVolume = volume;
        }
    }
}

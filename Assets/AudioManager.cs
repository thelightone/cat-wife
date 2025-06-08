using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource soundsSource;
    public AudioClip buttonTap;
    public AudioClip wash;
    public AudioClip eat;
    public AudioClip pet;

    public static AudioManager instance;

    private void Start()
    {
        instance = this;
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
}

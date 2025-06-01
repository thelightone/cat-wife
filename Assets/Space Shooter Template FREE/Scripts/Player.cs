using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This script defines which sprite the 'Player" uses and its health.
/// </summary>

public class Player : MonoBehaviour
{
    public GameObject destructionFX;

    public static Player instance;

    public TMP_Text _scoresText;
    private float scores;

    [SerializeField] private PetData _petData;
    private bool dead;
    private void Awake()
    {
        if (instance == null) 
            instance = this;
    }

    private void Update()
    {
        scores += Time.deltaTime;
        _scoresText.text = "SCORES: "+Convert.ToInt32(scores).ToString();

    }

    //method for damage proceccing by 'Player'
    public void GetDamage(int damage)   
    {
        if (!dead)
        {
            dead = true;
            Destruction();
        } 
    }    

    //'Player's' destruction procedure
    void Destruction()
    {
        
        Instantiate(destructionFX, transform.position, Quaternion.identity); //generating destruction visual effect and destroying the 'Player' object
        StartCoroutine(Exit());
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

    }

    private IEnumerator Exit()
    {
        if (_petData != null)
        {
            // Используем свойство для обновления happiness
            _petData.CurrentHapiness += Convert.ToInt32(scores) * 10;
            Debug.Log($"Mini-game completed. Scores: {scores}, Happiness increased to: {_petData.CurrentHapiness}");
            
            // Сохраняем состояние после обновления
            _petData.SaveGameState();
        }
        else
        {
            Debug.LogError("PetData reference is not assigned!");
        }
        
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
}

















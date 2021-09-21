using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public SlingShooter SlingShooter;
    public TrailController TrailController;
    public List<Bird> Birds;
    public List<Enemy> Enemies;
    private Bird _shotBird;
    public BoxCollider2D TapCollider;
    public Text status;
    

    private bool _isGameEnded = false;
    private bool _win = false;

    private void Update()
    {
        if (_isGameEnded)
        {
            status.gameObject.SetActive(true);
            ChangeStatusText();
        }

      

        if (Input.GetKey(KeyCode.Space))
        {
            string activeSceneName = SceneManager.GetActiveScene().name;

            if (_isGameEnded && _win)
                SceneManager.LoadScene(activeSceneName == "Level1" ? "Level2" : "Level3");
            else
                SceneManager.LoadScene(activeSceneName);
        }
    }

    void Start()
    {
        status.gameObject.SetActive(false);
        for (int i = 0; i < Birds.Count; i++)
        {
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdShot += AssignTrail;
        }

        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }

        TapCollider.enabled = false;
        SlingShooter.InitiateBird(Birds[0]);
        _shotBird = Birds[0];
    }

    public void ChangeBird()
    {
        TapCollider.enabled = false;

        if (_isGameEnded)
            return;

        Birds.RemoveAt(0);

        if (Birds.Count > 0)
        {
            SlingShooter.InitiateBird(Birds[0]);
            _shotBird = Birds[0];
        }
        else
        {
            _isGameEnded = true;
        }
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i].gameObject == destroyedEnemy)
            {
                Enemies.RemoveAt(i);
                break;
            }
        }

        if (Enemies.Count == 0)
        {
            _win = true;
            _isGameEnded = true;
        }
    }

    public void AssignTrail(Bird bird)
    {
        TrailController.SetBird(bird);
        StartCoroutine(TrailController.SpawnTrail());
        TapCollider.enabled = true;
    }

    void OnMouseUp()
    {
        if (_shotBird != null)
        {
            _shotBird.OnTap();
        }
    }

    private void ChangeStatusText()
    {
        if (_win)
            status.text = $"{SceneManager.GetActiveScene().name} Anda Telah Berhasil \n Tekan Spasi Untuk Melanjutkan";
        else
            status.text = "                 Anda Gagal \n Tekan Spasi Untuk Mengulang";
        
            
    }




}

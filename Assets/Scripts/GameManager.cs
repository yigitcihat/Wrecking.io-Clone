using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    private int _enemies;
    public bool isGameEnd;
 

    private void OnEnable()
    {
        EventManager.OnEnemyDrop.AddListener(CheckAndDecreaseDroppedEnemy);
    }
    private void OnDisable()
    {
        EventManager.OnEnemyDrop.RemoveListener(CheckAndDecreaseDroppedEnemy);
    }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    private void Start()
    {
        _enemies = FindObjectsOfType<EnemyBrain>().Length;
        Debug.Log(_enemies);
    }




    //public GameObject Confetti1, Confetti2;


    //public List<GameObject> PowerUpBoxes;
    //float powerUpCount;

    //[SerializeField]
    //CinemachineVirtualCamera cmVirtualCam;

    private void Update()
    {
        if (isGameEnd)
        {
            return;
        }
        //if (PowerUpBoxes.Count > 0)
        //{
        //    powerUpCount -= Time.deltaTime;
        //    if (powerUpCount <= 0)
        //    {
        //        int rand = Random.Range(0, PowerUpBoxes.Count);
        //        PowerUpBoxes[rand].SetActive(true);
        //        PowerUpBoxes[rand].GetComponent<Rigidbody>().isKinematic = false;
        //        PowerUpBoxes.RemoveAt(rand);
        //        powerUpCount = Random.Range(8f, 10f);
        //    }
        //}
        //if (shakeTimer > 0)
        //{
        //    shakeTimer -= Time.deltaTime;
        //    if (shakeTimer <= 0f)
        //    {
        //        //Time Over
        //        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cmVirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        //        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
        //    }
        //}
    }

    float shakeTimer;
    //public void ShakeCamera(float intensity, float time)
    //{
    //    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cmVirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

    //    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
    //    shakeTimer = time;
    //}

    public void CheckAndDecreaseDroppedEnemy()
    {
        _enemies--;
        Debug.Log(_enemies);
        if (_enemies <= 0)
        {
            isGameEnd=true;
            EventManager.OpenWinPanel.Invoke();
        }
    }

    //public IEnumerator WaitAndGameWin()
    //{
    //    if (!isGameOver)
    //    {
    //        isGameOver = true;
    //        Confetti1.SetActive(true);
    //        Confetti2.SetActive(true);
    //        //players[0].GetComponentInChildren<PlayerController>().DriverAnimator.SetTrigger("Cheer");

    //        yield return new WaitForSeconds(1f);
    //        WinPanel.SetActive(true);
    //    }
    //}

    //public IEnumerator WaitAndGameLose()
    //{
    //    if (!isGameOver)
    //    {
    //        isGameOver = true;

    //        yield return new WaitForSeconds(1f);
    //        LosePanel.SetActive(true);
    //    }
    //}

    //public void TapToStartButtonClick()
    //{
    //    isGameStarted = true;
    //}

    public void TapToPlayAgainButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GameManager;
using static UnityEngine.CullingGroup;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    [Header("Oyun Ayarlarý")]
    public int startingCredit = 100;

    [Header("Gün Verileri (DaySO Listesi)")]
    public List<DaySO> daysList;

    [Header("Oyun Durumu (Read Only!)")]
    [SerializeField] private GameState currentGameState;
    [SerializeField] private int currentCredit;
    [SerializeField] private int currentDayIndex = 0;

    [SerializeField] private bool areDailyTasksDone = false;

    public event EventHandler<OnNewDayStartedEventArgs> OnNewDayStarted;
    public class OnNewDayStartedEventArgs: EventArgs
    {
        public int currentDayIndex; // TM'in buna ihtiyacý yok gibi fakat UI için lazým olabilir, þimdilik tutuyorum.
        public DaySO currentDaySO; // direkt ilgili günü TM'e iletir
    }

    // Player kredi duyurusu 
    public event EventHandler<OnCreditChangedEventArgs> OnCreditChanged;
    public class OnCreditChangedEventArgs : EventArgs
    {
        public int currentCredit; 
    }

    // State deðiþim duyurusu
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public GameState currentGameState; 
    }

    public event EventHandler OnGameWin; 
    public event EventHandler OnGameLose; 


    private void Awake()
    {

        if (Instance != null)
        {
            Debug.LogError("There are more than one Game Manager Instance!"); 
        }

        Instance = this;
    }

    private void Start()
    {

        StartGame(); 

    }

    private void StartGame()
    {
        currentCredit = startingCredit;
        currentDayIndex = 0;

        // UI'ýn haberi olsun diye (þimdilik ekranda kredi yazacaksa)
        OnCreditChanged?.Invoke(this, new OnCreditChangedEventArgs {
            
            currentCredit = currentCredit

        }); 


        StartNewDay();
    }

    private void StartNewDay()
    {
        // yeni gün stete duyur
        ChangeState(GameState.NewGameDay);
        areDailyTasksDone = false;


        if (currentDayIndex < daysList.Count)
        {
            Debug.Log($"--- {currentDayIndex + 1}. GÜN BAÞLIYOR ---");

            OnNewDayStarted?.Invoke(this, new OnNewDayStartedEventArgs
            {
                currentDayIndex = currentDayIndex,
                currentDaySO = daysList[currentDayIndex]
                

            });

            ChangeState(GameState.DayInProgress);
        }
        else
        {
            ChangeState(GameState.EndGame);
            
            OnGameWin?.Invoke(this, EventArgs.Empty);

            Debug.Log("TEBRÝKLER! TÜM GÜNLERÝ TAMAMLADINIZ!");
        }

    }

    public void OnAllTasksCompletedForToday()
    {
        areDailyTasksDone = true;
        Debug.Log("Günün tüm görevleri bitti. Oyuncu günü bitirebilir.");
    }

    public void TryEndDay()
    {
        if (currentGameState != GameState.DayInProgress) return; 

        if (areDailyTasksDone)
        {
            ChangeState(GameState.EndGameDay);
            Debug.Log("Gün Baþarýyla Kapatýldý. Gün Sonu Raporu Gösteriliyor...");


            EndDayAndPrepareNext();
        }
        else
        {
            Debug.LogWarning("Daha bitirmemiþ olduðun görevler var! Günü bitiremezsin.");
        }
    }

    private void EndDayAndPrepareNext()
    {
        currentDayIndex++;
        StartNewDay();
    }


    public void ModifyCredit(int amount)
    {
        currentCredit += amount;

        OnCreditChanged?.Invoke(this, new OnCreditChangedEventArgs
        {
            currentCredit = currentCredit
        });

        if (currentCredit <= 0)
        {
            currentCredit = 0; 
            ChangeState(GameState.EndGame);

            OnGameLose?.Invoke(this, EventArgs.Empty);
            Debug.Log("GAME OVER! Kredi Sýfýrlandý.");
        }
    }

    public DaySO GetCurrentDayConfig()
    {
        if (currentDayIndex >= 0 && currentDayIndex < daysList.Count)
        {
            return daysList[currentDayIndex];
        }
        return null; 
    }



    private void ChangeState(GameState newState)
    {
        if (currentGameState == newState) return;

        currentGameState = newState;
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
        {
            currentGameState = currentGameState
        }); 
    }



}

using System.Collections;
using TMPro;
using UnityEngine;

namespace ChestProject.Chest
{
    public class TimerScript : MonoBehaviour
    {
        private const string timerStringFormat = "{0:00}:{1:00}:{2:00}";
        private int totalTimeInSeconds; // Total time for the countdown in seconds
        [SerializeField] private TextMeshProUGUI timerText; // Reference to the UI Text component to display the time
        [SerializeField] private int triggerDurationInSeconds;

        private float timeRemaining;
        private bool isTimerRunning = false;
        private bool isPaused = false;

        private Coroutine timerCoroutine;
        private Coroutine messageCoroutine;

        private ChestController chestController;

        public int TotalTimeInSeconds { get => totalTimeInSeconds; set => totalTimeInSeconds = value; }
        public ChestController ChestController { set => chestController = value; }
        public TextMeshProUGUI TimerText { set => timerText = value; }
        public bool IsTimerRunning { get => isTimerRunning; }
        public float TimeRemaining { get => timeRemaining; set => timeRemaining = value; }

        public void StartTimer(int seconds)
        {
            timeRemaining = seconds;
            isTimerRunning = true;
            isPaused = false;
            timerCoroutine = StartCoroutine(TimerCoroutine());
            messageCoroutine = StartCoroutine(PrintMessageCoroutine());
        }

        public void PauseTimer()
        {
            if (isTimerRunning && !isPaused)
            {
                isPaused = true;
                StopCoroutine(timerCoroutine);
                StopCoroutine(messageCoroutine);
            }
        }

        public void ResumeTimer()
        {
            if (isTimerRunning && isPaused)
            {
                isPaused = false;
                timerCoroutine = StartCoroutine(TimerCoroutine());
                messageCoroutine = StartCoroutine(PrintMessageCoroutine());
            }
        }

        private IEnumerator TimerCoroutine()
        {
            while (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(timeRemaining);
                yield return null; // Wait until the next frame
            }

            TimerEnded();
        }

        private IEnumerator PrintMessageCoroutine()
        {
            float timeSinceLastMessage = 0f;

            while (timeRemaining > 0)
            {
                yield return new WaitForSeconds(1f); // Check every second

                if (!isPaused)
                {
                    timeSinceLastMessage += 1f;

                    if (timeSinceLastMessage >= triggerDurationInSeconds) // 600 seconds = 10 minutes
                    {
                        CountingASpecificTimeDuration();
                        timeSinceLastMessage = 0f;
                    }
                }
            }
        }

        void UpdateTimerDisplay(float time)
        {
            int totalSecsInAMin = 60;
            int totalSecsInAHour = 3600;
            int hours = Mathf.FloorToInt(time / totalSecsInAHour);
            int minutes = Mathf.FloorToInt((time % totalSecsInAHour) / totalSecsInAMin);
            int seconds = Mathf.FloorToInt(time % totalSecsInAMin);

            timerText.text = string.Format(timerStringFormat, hours, minutes, seconds);
        }

        public void TimerEnded()
        {
            isTimerRunning = false;
            timerText.text = null;
            chestController.SetUnlockedState();
        }

        void CountingASpecificTimeDuration()
        {
            chestController.UpdateRequiredGemsValue();
        }
    }
}


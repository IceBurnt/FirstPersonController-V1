using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timer;
    private float timeElapsed;

    void Update()
    {
        timeElapsed += Time.deltaTime;

        int seconds = Mathf.FloorToInt(timeElapsed);
        int milliseconds = Mathf.FloorToInt((timeElapsed - seconds) * 1000);

        timer.text = string.Format("{0}:{1:000}", seconds, milliseconds);
    }

    public void ResetTimer()
    {
        timeElapsed = 0f;
    }
}


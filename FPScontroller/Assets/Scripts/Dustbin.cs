using UnityEngine;
using TMPro;

public class Trash : MonoBehaviour
{
    public LayerMask redBinLayer;
    public LayerMask greenBinLayer;
    public LayerMask blueBinLayer;
    public LayerMask goldBinLayer;

    public LayerMask Vehicle;

    public static int Score = 0; // Make Score static local for all trahs
    public TextMeshProUGUI scoreText;

    public enum TrashType { Red, Green, Blue }
    public TrashType trashType;

    public CarController crashdetection;
    public CarDetection capsule;

   
    public bool isPickable = false;

    private int tempTrashCounterRED = 0;
    private int tempTrashCounterGREEN = 0;
    private int tempTrashCounterBLUE = 0;
    private void Start()
    {
        UpdateScoreText();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsInLayerMask(collision.gameObject, goldBinLayer))
        {
            Score += 10;
            Destroy(collision.gameObject); 
            Destroy(gameObject); 
            UpdateScoreText();
            return;
        }

        if (trashType == TrashType.Red && IsInLayerMask(collision.gameObject, redBinLayer))
        {
            Score += 1;
            Destroy(gameObject); 
            UpdateScoreText();
            tempTrashCounterRED = tempTrashCounterRED + 1;
            return;
            
        }

        if (trashType == TrashType.Green && IsInLayerMask(collision.gameObject, greenBinLayer))
        {
            Score += 1;
            Destroy(gameObject); 
            UpdateScoreText();
            tempTrashCounterGREEN = tempTrashCounterGREEN + 1;
            return;
        }

        if (trashType == TrashType.Blue && IsInLayerMask(collision.gameObject, blueBinLayer))
        {
            Score += 1;
            Destroy(gameObject); 
            UpdateScoreText();
            tempTrashCounterBLUE = tempTrashCounterBLUE + 1;
            return;
        }

        





    }

    private void Update()
    {
        if(tempTrashCounterRED > 1)
        {
            Debug.Log("Trashbin RED is full");
        }
        if (tempTrashCounterBLUE > 1)
        {
            Debug.Log("Trashbin BLUE is full");
        }
        if (tempTrashCounterGREEN > 1)
        {
            Debug.Log("Trashbin GREEN is full");
        }
    }

    private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return (layerMask.value & (1 << obj.layer)) != 0;
    }

    private void UpdateScoreText()
    {
        scoreText.text = Score.ToString();
    }


  

}

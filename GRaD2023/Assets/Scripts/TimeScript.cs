using UnityEngine;
using TMPro;

public class TimeScript : MonoBehaviour
{

    [SerializeField]
    public TextMeshProUGUI firstHour;

    [SerializeField]
    public TextMeshProUGUI secondHour;

    [SerializeField]
    public TextMeshProUGUI seperator;

    [SerializeField]
    public TextMeshProUGUI firstMinute;

    [SerializeField]
    public TextMeshProUGUI secondMinute;

    [SerializeField]
    public TextMeshProUGUI testTimer;

    public bool playing = true;

    private string currentTime;
    float hours;
    float minutes;

    private float dayEndTime = 1440f;
    private float wakeUpTime = 480f;
    private bool dayEnd = false;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        ResetTime();
    }

    // Update is called once per frame
    void Update()
    {
        if(playing == true){
                timer += 3 * Time.deltaTime;
                UpdateTime(timer);
                //Debug.Log(timer);
	    }
    }

    private void ResetTime(){
        timer = wakeUpTime;
        hours = Mathf.FloorToInt(timer / 60);
        minutes = Mathf.FloorToInt(timer % 60);

        currentTime = string.Format("{00:00}{1:00}", hours, minutes);
        firstHour.text = currentTime[0].ToString();
        secondHour.text = currentTime[1].ToString();
        firstMinute.text = currentTime[2].ToString();
        secondMinute.text = currentTime[3].ToString();

    }

    private void UpdateTime(float time){
        hours = Mathf.FloorToInt(time / 60);
        minutes = Mathf.FloorToInt(time % 60);
        
        currentTime = string.Format("{00:00}{1:00}", hours, minutes);


        
        firstHour.text = currentTime[0].ToString();
        secondHour.text = currentTime[1].ToString();
        if(currentTime[3]>8){

            firstMinute.text = currentTime[2].ToString();
        }
        
        //secondMinute.text = currentTime[3].ToString();
        testTimer.text = Mathf.FloorToInt(time-480).ToString();
        
    }
}

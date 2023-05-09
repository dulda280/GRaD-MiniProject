using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


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
    private bool nightTimeCountDown = false;
    private string currentTime;
    float hours;
    float minutes;

    float tempDecay;

    private bool diedBool = false;
    private bool _privateDeathBool;
    public float dayEndTime = 1440f;
    private float wakeUpTime = 480f;
    private float timer = 0;

    private bool starvationBool = true;

    public EventHandler eventHandler;
    public UnityEngine.Rendering.Universal.Light2D sun;
    public Player player;
    public GameObject trafficLights;

    public bool deathBool{ get { return _privateDeathBool; } set { _privateDeathBool= value; }}

    public float timeVal{ get { return timer; } set{timer = value;}}
    public float intensityMod{get {return sun.intensity;} set{sun.intensity = value;}}
    
    // Start is called before the first frame update
    void Start()
    {
        ResetTime();
    }

    // Update is called once per frame
    void Update()
    {
        if(playing == true){
                timer += 10 * Time.deltaTime;
                UpdateTime(timer);
                //Debug.Log(timer);
                statDecay(player, timer, 0.0025f, 0.01f, 0.005f);
                timeProgression(sun, timer);
                trackVitals(player);
	    }
    }

    public void ResetTime(){
        timer = wakeUpTime;
        hours = Mathf.FloorToInt(timer / 60);
        minutes = Mathf.FloorToInt(timer % 60);

        currentTime = string.Format("{00:00}{1:00}", hours, minutes);
        firstHour.text = currentTime[0].ToString();
        secondHour.text = currentTime[1].ToString();
        firstMinute.text = currentTime[2].ToString();
        secondMinute.text = currentTime[3].ToString();

        player.hungerAndThirst -= 30f;
        nightTimeCountDown = false;
        sun.intensity = 1.0f;
        trafficLights.SetActive(false);

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
        if(time >= dayEndTime){
            StartCoroutine(player.eventHandler.ShowBigEvent($"You slept on the streets tonight, which is not very healthy. Find shelter before midnight.", 4));
            //ResetTime();
            player.mental -= 20;
            player.health -= 20;
        }
        

    }

    private void statDecay(Player target, float time, float hungerDecay, float healtDecay, float mentalHealthDecay){

        
        if(Mathf.Ceil(time) % 3 == 0){
            target.hungerAndThirst -= hungerDecay;
            //Debug.Log("playerhunger; " + target.hungerAndThirst);

            if(target.hungerAndThirst <= 0){
                target.health -= healtDecay;
                target.mental -= mentalHealthDecay;

                if(starvationBool){
                    StartCoroutine(eventHandler.ShowEvent("I'm so hungry it's starting to hurt...", 4));
                    starvationBool = false;
                }
                

            }
            if(target.hungerAndThirst < 0){
                target.hungerAndThirst = 0;
            }
        }
    }

    private void timeProgression(UnityEngine.Rendering.Universal.Light2D light, float time){
        if(Mathf.Ceil(time) == 840){
            nightTimeCountDown = true;
            Debug.Log("Nightime approaches");
        }
        if(nightTimeCountDown){
            if(Mathf.Ceil(time) % 3 == 0){
                light.intensity -= 0.0003f;
            }
        }
        if(Mathf.Ceil(time) >= 1080){
            trafficLights.SetActive(true);
        }
        if(light.intensity <= 0.15f){
            light.intensity = 0.15f;
        }
    }

    private void trackVitals(Player person){
        if(person.health <= 0){
            diedBool = true;
            _privateDeathBool = true;
        }
        if(person.mental <= 0){
            diedBool = true;
            _privateDeathBool = false;
        }
        if(diedBool){
            if(_privateDeathBool == true){
                SceneManager.LoadScene(3, LoadSceneMode.Single);
                diedBool = false;
            }
            if(_privateDeathBool == false){
                SceneManager.LoadScene(4, LoadSceneMode.Single);
                diedBool = false;
            }
        }
    }
}

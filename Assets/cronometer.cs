using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cronometer : MonoBehaviour {
    public float startTime;
    public Text timer;
    public Text info;
    public bool done = false;
    public int secondsLeft = 180;
    public bool takingAway = false;
    public bool counting = false;

    // Use this for initialization
	void Start () {
        string minutes;
        string seconds;
        if((secondsLeft / 60) < 10 ){
            minutes = "0"+((int)secondsLeft / 60).ToString();
        }else{
            minutes = ((int)secondsLeft / 60).ToString();
        }
        if((secondsLeft % 60) < 10 ){
            seconds = "0"+((int)secondsLeft % 60).ToString();
        }else{
            seconds = ((int)secondsLeft % 60).ToString();
        }

        timer.text = minutes + ":" + seconds;
	}

	void Update () {
        if(counting==true){
            if(takingAway == false && secondsLeft > 0){
                StartCoroutine(TimerTake());
            }
            else if(secondsLeft == 0&&!done){
                StartCoroutine(GameOver());
                
            }
        }
	}

    IEnumerator TimerTake(){
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        string minutes;
        string seconds;
        if((secondsLeft / 60) < 10 ){
            minutes = "0"+((int)secondsLeft / 60).ToString();
        }else{
            minutes = ((int)secondsLeft / 60).ToString();
        }
        if((secondsLeft % 60) < 10 ){
            seconds = "0"+((int)secondsLeft % 60).ToString();
        }else{
            seconds = ((int)secondsLeft % 60).ToString();
        }

        timer.text = minutes + ":" + seconds;
        takingAway = false;
    }
    IEnumerator GameOver(){
        done=true;
        GameObject Player= GameObject.Find("Sphere");
        Player.GetComponent<PlayerBehaviour>().m_MovePower=0;
        info.text = "TIME IS UP";
        yield return new WaitForSeconds(3);
        info.text = "!!!GAME OVER!!!";
        yield return new WaitForSeconds(10);
    }
}
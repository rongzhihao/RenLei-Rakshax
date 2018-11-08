using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class time : MonoBehaviour
{

    // Use this for initialization
    public GameObject timeText;
    public GameObject recordPanel;
    public GameObject jilu;
    private float spendTime;
    [SerializeField]
    private float end = 10f;
    private int hour, minute, second;
    public GameObject endGame;
    public GameObject result;
    void Start()
    {
        
        if (PhotonNetwork.player.ID == 1){
            PhotonNetwork.player.SetTeam(PunTeams.Team.red);
            spendTime = 0;
        }else{
            spendTime = float.Parse(PhotonNetwork.playerList[0].CustomProperties["pastTime"].ToString());

        }
        Debug.Log("time:"+spendTime);
        // else
        // //Debug.Log(PhotonNetwork.playerList[0].CustomProperties["time"]);
        // {
        //     spendTime = float.Parse(PhotonNetwork.playerList[0].CustomProperties["time"].ToString());
        //     //Debug.Log(spendTime);
        // }*/
    

    }
   
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("now:" + spendTime);
        

        if(spendTime < end){
            
            spendTime += Time.deltaTime;
            if (PhotonNetwork.player.ID == 1)
            {
                ExitGames.Client.Photon.Hashtable startTime = new ExitGames.Client.Photon.Hashtable();
                //startTime["time"] = timeText.GetComponent<TextMesh>().text;
                startTime["pastTime"] = spendTime;
                PhotonNetwork.player.SetCustomProperties(startTime);
            }
            hour = (int)spendTime / 3600;
            minute = (int)(spendTime - hour * 3600) / 60;
            second = (int)(spendTime - hour * 3600 - minute * 60);
            //timeText.GetComponent<TextMesh>().text = string.Format("{0:D2}:{1:D2}:{2:D2}", hour, minute, second);
            timeText.GetComponent<Text>().text = string.Format("{0:D2}:{1:D2}:{2:D2}", hour, minute, second);
            }
        if (spendTime >= end && PhotonNetwork.player.ID == 1)
        {
           // PhotonNetwork.LoadLevel("gameConnect");
            PlayerController.canMove = false;
            jilu.GetComponent<Text>().text = "Player              Status\n";
            endGame.GetComponent<Transform>().position = new Vector3(PlayerController.playerX -7, PlayerController.playerY + 3, 100);
            endGame.SetActive(true);
            for (int i = 0; i < PhotonNetwork.room.PlayerCount; i++)
            {
                //jilu.GetComponent<Text>().text += PhotonNetwork.playerList[i].ID;
                jilu.GetComponent<Text>().text += PhotonNetwork.playerList[i].CustomProperties["name"].ToString();
                Debug.Log(PhotonNetwork.playerList[i].CustomProperties["name"].ToString());
                if (PhotonNetwork.playerList[i].GetScore() == 0)
                {
                    jilu.GetComponent<Text>().text += "<color=#1F5ADE>               Zombie          </color>\n";
                    // jilu.GetComponent<Text>().text += "<color=#EA464B>Red</color>\n";
                }
                if (PhotonNetwork.playerList[i].GetScore() == 1)
                {
                    jilu.GetComponent<Text>().text += "<color=#EA464B>               Human          </color>\n";
                    //jilu.GetComponent<Text>().text += "<color=#1F5ADE>Blue</color>\n";
                }
                recordPanel.GetComponent<Transform>().position = new Vector3(PlayerController.playerX, PlayerController.playerY);
                jilu.GetComponent<Transform>().position = new Vector3(PlayerController.playerX - 1, PlayerController.playerY + 3);
                recordPanel.SetActive(true);
                jilu.SetActive(true);
                getResult();
                PhotonNetwork.player.SetTeam(PunTeams.Team.blue);
            }
        } else if ( (PhotonNetwork.player.ID != 1 && PhotonNetwork.playerList[0].GetTeam() == PunTeams.Team.blue) || spendTime >= end){
            // PhotonNetwork.LoadLevel("gameConnect");
            PlayerController.canMove = false;
            jilu.GetComponent<Text>().text = "Player              Status\n";
            endGame.GetComponent<Transform>().position = new Vector3(PlayerController.playerX -7, PlayerController.playerY + 3, 100);
            endGame.SetActive(true);
            for (int i = 0; i < PhotonNetwork.room.PlayerCount; i++)
            {
                //jilu.GetComponent<Text>().text += PhotonNetwork.playerList[i].ID;
                jilu.GetComponent<Text>().text += PhotonNetwork.playerList[i].CustomProperties["name"].ToString();
                Debug.Log(PhotonNetwork.playerList[i].CustomProperties["name"].ToString());
                if (PhotonNetwork.playerList[i].GetScore() == 0)
                {
                    jilu.GetComponent<Text>().text += "<color=#1F5ADE>               Zombie          </color>\n";
                    // jilu.GetComponent<Text>().text += "<color=#EA464B>Red</color>\n";
                }
                if (PhotonNetwork.playerList[i].GetScore() == 1)
                {
                    jilu.GetComponent<Text>().text += "<color=#EA464B>               Human          </color>\n";
                    //jilu.GetComponent<Text>().text += "<color=#1F5ADE>Blue</color>\n";
                }
                recordPanel.GetComponent<Transform>().position = new Vector3(PlayerController.playerX, PlayerController.playerY);
                jilu.GetComponent<Transform>().position = new Vector3(PlayerController.playerX - 1, PlayerController.playerY + 3);
                recordPanel.SetActive(true);
                jilu.SetActive(true);
                getResult();
            }


        }

    }
    void getResult()
    {
        int red = 0;
        int blue = 0;
        for (int i = 0; i < PhotonNetwork.room.PlayerCount; i++)
        {
            if (PhotonNetwork.playerList[i].GetScore() == 1)
                red++;
            else
                blue++;
        }
        if (blue == 0 || red == 0)
            result.GetComponent<Text>().text = "You win!";
        else
        {
            if (red == blue)
                result.GetComponent<Text>().text = "It's a tie!";
            if (red < blue)
            {
                if (PhotonNetwork.player.GetScore() == 1)
                    result.GetComponent<Text>().text = "You win!";
                else
                    result.GetComponent<Text>().text = "You lose!";
            }
            if (red > blue)
            {
                if (PhotonNetwork.player.GetScore() == 0)
                    result.GetComponent<Text>().text = "You win!";
                else
                    result.GetComponent<Text>().text = "You lose!";
            }
        }
        
        result.GetComponent<Transform>().position = new Vector3(PlayerController.playerX - 1, PlayerController.playerY -3, 100);
        result.SetActive(true);


    }
}
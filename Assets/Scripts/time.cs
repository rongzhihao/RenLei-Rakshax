using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class time : MonoBehaviour
{

    // Use this for initialization
    public GameObject timeText;
    public GameObject recordPanel;
    public GameObject result;
    public GameObject jilu;
    private float spendTime;
    public float end = 500f;
   // [SerializeField]
    
    private int hour, minute, second;
    public GameObject endGame;
    //public GameObject result;
    void Start()
    {
        
        if (PhotonNetwork.player.ID == 1){
            PhotonNetwork.player.SetTeam(PunTeams.Team.red);
            spendTime = 0;
        }else{
            //spendTime = float.Parse(PhotonNetwork.playerList[0].CustomProperties["pastTime"].ToString());
            spendTime = float.Parse(PhotonNetwork.room.CustomProperties["pastTime"].ToString());
        }
        Debug.Log("id:" + PhotonNetwork.player.ID);
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
        // Debug.Log("now:" + PhotonNetwork.playerList[0].CustomProperties["startTime"]);
 
        
        if(spendTime < end){
            
            spendTime += Time.deltaTime;
            if (PhotonNetwork.player.ID == 1)
            {
                ExitGames.Client.Photon.Hashtable startTime = new ExitGames.Client.Photon.Hashtable();
               // startTime["time"] = timeText.GetComponent<TextMesh>().text;
                startTime["pastTime"] = spendTime;
                //PhotonNetwork.player.SetCustomProperties(startTime);
                PhotonNetwork.room.SetCustomProperties(startTime);
                // PhotonNetwork.playerList[0].CustomProperties["startTime"] = spendTime;
             }
                hour = (int)spendTime / 3600;
                minute = (int)(spendTime - hour * 3600) / 60;
                second = (int)(spendTime - hour * 3600 - minute * 60);
                //timeText.GetComponent<TextMesh>().text = string.Format("{0:D2}:{1:D2}:{2:D2}", hour, minute, second);
                timeText.GetComponent<Text>().text = string.Format("{0:D2}:{1:D2}:{2:D2}", hour, minute, second);
            
        }
        if (spendTime >= end)
        {
           // PhotonNetwork.LoadLevel("gameConnect");
            PlayerController.canMove = false;
            jilu.GetComponent<Text>().text = "Player              Status\n";
            endGame.GetComponent<Transform>().position = new Vector3(PlayerController.playerX -7, PlayerController.playerY + 3, 100);
            endGame.SetActive(true);
            for (int i = 0; i < PhotonNetwork.room.PlayerCount; i++)
            {
                //jilu.GetComponent<Text>().text += PhotonNetwork.playerList[i].ID;
                /*
                for (int j = 0; j<15- PhotonNetwork.playerList[i].CustomProperties["name"].ToString().Length; j++)
                {
                    jilu.GetComponent<Text>().text += " ";
                }*/

                if (PhotonNetwork.playerList[i].GetScore() == 0)
                {
                    jilu.GetComponent<Text>().text += "    ";
                    jilu.GetComponent<Text>().text += PhotonNetwork.playerList[i].CustomProperties["name"].ToString();
                    Debug.Log(PhotonNetwork.playerList[i].CustomProperties["name"].ToString());
                    jilu.GetComponent<Text>().text += "<color=#1F5ADE>";
                    for (int j = 0; j < 18; j++)
                    {
                        jilu.GetComponent<Text>().text += " ";
                    }
                    jilu.GetComponent<Text>().text += "Zombie</color>\n";
                    // jilu.GetComponent<Text>().text += "<color=#EA464B>Red</color>\n";
                }
                if (PhotonNetwork.playerList[i].GetScore() == 1)
                {
                    jilu.GetComponent<Text>().text += "          ";
                    jilu.GetComponent<Text>().text += PhotonNetwork.playerList[i].CustomProperties["name"].ToString();
                    Debug.Log(PhotonNetwork.playerList[i].CustomProperties["name"].ToString());


                    jilu.GetComponent<Text>().text += "<color=#EA464B>";
                    for (int j = 0; j < 20; j++)
                    {
                        jilu.GetComponent<Text>().text += " ";
                    }
                    jilu.GetComponent<Text>().text += "Human          </color>\n";
                    //jilu.GetComponent<Text>().text += "<color=#1F5ADE>Blue</color>\n";
                }
                /*
                if (PhotonNetwork.playerList[i].GetScore() == 2)
                {
                    jilu.GetComponent<Text>().text += "<color=#1F5ADE>               Blue         </color>";
                    jilu.GetComponent<Text>().text += "<color=#EA464B>Red</color>\n";
                }
                if (PhotonNetwork.playerList[i].GetScore() == 3)
                {
                    jilu.GetComponent<Text>().text += "<color=#1F5ADE>               Blue         </color>";
                    jilu.GetComponent<Text>().text += "<color=#1F5ADE>Blue</color>\n";
                }*/
                /*
                else
                    jilu.GetComponent<TextMesh>().text += "<color=#1F5ADE>               Blue         </color>";
                if (clothOff[i] == Color.red)
                    jilu.GetComponent<TextMesh>().text += "<color=#EA464B>Red</color>\n";
                else
                    jilu.GetComponent<TextMesh>().text += "<color=#1F5ADE>Blue</color>\n";*/
                //jilu.GetComponent<TextMesh>().text += clothOn[i].ToString();
                //jilu.GetComponent<TextMesh>().text += clothOff[i].ToString();
            }
            recordPanel.GetComponent<Transform>().position = new Vector3(PlayerController.playerX, PlayerController.playerY);
            jilu.GetComponent<Transform>().position = new Vector3(PlayerController.playerX - 1, PlayerController.playerY + 3);
            getResult();
            jilu.SetActive(true);
            recordPanel.SetActive(true);
          //  result.SetActive(true);
        }
        /*
        else if ( (PhotonNetwork.player.ID != 1 && PhotonNetwork.playerList[0].GetTeam() == PunTeams.Team.blue) || spendTime >= end)
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
            }


        }*/

    }
    void getResult()
    {
        Debug.Log("jieshule");
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
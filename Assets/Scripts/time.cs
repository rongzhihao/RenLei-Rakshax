using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class time : MonoBehaviour
{

    // Use this for initialization
    public GameObject timeText;
    public GameObject jilu;
    private float spendTime;
    private float endTime = 60f;
    private int hour, minute, second;
    public GameObject endGame;
    void Start()
    {
        /*
        if (PhotonNetwork.player.ID == 1)
            spendTime = 0;
        else
        //Debug.Log(PhotonNetwork.playerList[0].CustomProperties["time"]);
        {
            spendTime = float.Parse(PhotonNetwork.playerList[0].CustomProperties["time"].ToString());
            //Debug.Log(spendTime);
        }*/

    }

    // Update is called once per frame
    void Update()
    {


        if (PhotonNetwork.player.ID == 1 && spendTime < endTime)
        {
            spendTime += Time.deltaTime;


            hour = (int)spendTime / 3600;
            minute = (int)(spendTime - hour * 3600) / 60;
            second = (int)(spendTime - hour * 3600 - minute * 60);
            timeText.GetComponent<TextMesh>().text = string.Format("{0:D2}:{1:D2}:{2:D2}", hour, minute, second);
            ExitGames.Client.Photon.Hashtable startTime = new ExitGames.Client.Photon.Hashtable();
            startTime["time"] = timeText.GetComponent<TextMesh>().text;
            startTime["pastTime"] = spendTime;
            PhotonNetwork.player.SetCustomProperties(startTime);

        }
        else
        {
            timeText.GetComponent<TextMesh>().text = PhotonNetwork.playerList[0].CustomProperties["time"].ToString();
            spendTime = float.Parse(PhotonNetwork.playerList[0].CustomProperties["pastTime"].ToString());
            Debug.Log(spendTime);
        }
        if (spendTime >= endTime)
        {
           // PhotonNetwork.LoadLevel("gameConnect");
            PlayerController.canMove = false;
            jilu.GetComponent<TextMesh>().text = "Player    ClothOn   ClothOff\n";
            endGame.GetComponent<Transform>().position = new Vector3(PlayerController.playerX -5, PlayerController.playerY + 2, 100);
            endGame.SetActive(true);
            for (int i = 0; i < PhotonNetwork.room.PlayerCount; i++)
            {
                jilu.GetComponent<TextMesh>().text += PhotonNetwork.playerList[i].ID;
                if (PhotonNetwork.playerList[i].GetScore() == 0)
                {
                    jilu.GetComponent<TextMesh>().text += "<color=#EA464B>               Red          </color>";
                    jilu.GetComponent<TextMesh>().text += "<color=#EA464B>Red</color>\n";
                }
                if (PhotonNetwork.playerList[i].GetScore() == 1)
                {
                    jilu.GetComponent<TextMesh>().text += "<color=#EA464B>               Red          </color>";
                    jilu.GetComponent<TextMesh>().text += "<color=#1F5ADE>Blue</color>\n";
                }
                if (PhotonNetwork.playerList[i].GetScore() == 2)
                {
                    jilu.GetComponent<TextMesh>().text += "<color=#1F5ADE>               Blue         </color>";
                    jilu.GetComponent<TextMesh>().text += "<color=#EA464B>Red</color>\n";
                }
                if (PhotonNetwork.playerList[i].GetScore() == 3)
                {
                    jilu.GetComponent<TextMesh>().text += "<color=#1F5ADE>               Blue         </color>";
                    jilu.GetComponent<TextMesh>().text += "<color=#1F5ADE>Blue</color>\n";
                }
                jilu.GetComponent<Transform>().position = new Vector3(PlayerController.playerX - 2, PlayerController.playerY + 1);
                jilu.SetActive(true);
            }
        }
    }
}
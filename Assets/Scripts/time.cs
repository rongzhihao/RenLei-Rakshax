using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class time : MonoBehaviour {

    // Use this for initialization
    public GameObject timeText;
    private float spendTime;
    private int hour, minute, second;
    void Start () {
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
	void Update () {
        spendTime += Time.deltaTime;
        
        
        hour = (int)spendTime / 3600;
        minute = (int)(spendTime - hour * 3600) / 60;
        second = (int)(spendTime - hour * 3600 - minute * 60);
        
        if (PhotonNetwork.player.ID == 1)
        {
            timeText.GetComponent<TextMesh>().text = string.Format("{0:D2}:{1:D2}:{2:D2}", hour, minute, second);
            ExitGames.Client.Photon.Hashtable startTime = new ExitGames.Client.Photon.Hashtable();
            startTime["time"] = timeText.GetComponent<TextMesh>().text;

            PhotonNetwork.player.SetCustomProperties(startTime);

        }
        else
        {
            timeText.GetComponent<TextMesh>().text = PhotonNetwork.playerList[0].CustomProperties["time"].ToString();
        }
    }
}

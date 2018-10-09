using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tab : MonoBehaviour
{
    //public static List<Color> clothOn = new List<Color>();
    //public static List<Color> clothOff = new List<Color>();

    public GameObject jilu;
    public GameObject recordPanel;
    public GameObject time;
    public GameObject timePanel;
    // Use this for initialization
    void Start()
    {
        //for (int i = 0;i<PhotonNetwork.room.PlayerCount;i++)
        //Debug.Log(PhotonNetwork.playerList[i].ID);
        //clothOn.Add(Color.red);
        //clothOff.Add(Color.blue);
        //Debug.Log(clothOn);
        PhotonNetwork.player.SetScore(1);
        Debug.Log(PhotonNetwork.player.ID);
    }

    // Update is called once per frame
    void Update()
    {

        //time.GetComponent<Transform>().position = new Vector3(PlayerController.playerX + 3, PlayerController.playerY + 5, 100);
        timePanel.GetComponent<Transform>().position = new Vector3(PlayerController.playerX + 6, PlayerController.playerY + 3, 100);
     
        if (Input.GetKeyDown(KeyCode.Tab)||Input.GetButtonDownMobile("Submit"))
        {

            //jilu.GetComponent<Transform>().position = new Vector3(PlayerController.playerX-2, PlayerController.playerY+1);
            recordPanel.GetComponent<Transform>().position = new Vector3(PlayerController.playerX, PlayerController.playerY);
            jilu.GetComponent<Transform>().position = new Vector3(PlayerController.playerX - 1, PlayerController.playerY+3);
            jilu.GetComponent<Text>().text = "Player    ClothOn   ClothOff\n";

            for (int i = 0; i < PhotonNetwork.room.PlayerCount; i++)
            {
                jilu.GetComponent<Text>().text += PhotonNetwork.playerList[i].ID;
                if (PhotonNetwork.playerList[i].GetScore() == 0)
                {
                    jilu.GetComponent<Text>().text += "<color=#EA464B>               Red          </color>";
                    jilu.GetComponent<Text>().text += "<color=#EA464B>Red</color>\n";
                }
                if (PhotonNetwork.playerList[i].GetScore() == 1)
                {
                    jilu.GetComponent<Text>().text += "<color=#EA464B>               Red          </color>";
                    jilu.GetComponent<Text>().text += "<color=#1F5ADE>Blue</color>\n";
                }
                if (PhotonNetwork.playerList[i].GetScore() == 2)
                {
                    jilu.GetComponent<Text>().text += "<color=#1F5ADE>               Blue         </color>";
                    jilu.GetComponent<Text>().text += "<color=#EA464B>Red</color>\n";
                }
                if (PhotonNetwork.playerList[i].GetScore() == 3)
                {
                    jilu.GetComponent<Text>().text += "<color=#1F5ADE>               Blue         </color>";
                    jilu.GetComponent<Text>().text += "<color=#1F5ADE>Blue</color>\n";
                }
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
            jilu.SetActive(true);
            recordPanel.SetActive(true);
            
            Debug.Log(jilu.GetComponent<Text>().text);
        }
        if (Input.GetKeyUp(KeyCode.Tab)||Input.GetButtonUpMobile("Submit"))
        {
            jilu.SetActive(false);
            recordPanel.SetActive(false);
        }
            



    }
}

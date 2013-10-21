using UnityEngine;
using System.Collections;

public class scene_1_script : MonoBehaviour {
	
	private string username = "user";
	
	void OnGUI()
	{
		string state = PhotonNetwork.connectionStateDetailed.ToString();
        GUILayout.Label(state);
		
		username = GUI.TextField(new Rect(10,(Screen.height / 2) - 20,300,20),username);
		
		if(GUI.Button(new Rect(100,(Screen.height / 2) + 10,120,20),"Connect"))
		{	
			if(username.Length == 0)
				username = "user";
			
			PhotonNetwork.playerName = username;
			PhotonNetwork.ConnectUsingSettings("1.0");
		}
	}
	
	void OnJoinedLobby()
	{
		PhotonNetwork.LoadLevel("Scene_2");
	}
}

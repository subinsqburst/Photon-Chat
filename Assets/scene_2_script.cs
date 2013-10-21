using UnityEngine;
using System.Collections;

public class scene_2_script : MonoBehaviour {
	
	private string createRoomName = "New Room";
	private string joinRoomName = "";
	
	void OnGUI()
	{
		string state = PhotonNetwork.connectionStateDetailed.ToString();
        GUILayout.Label(state);
		
		GUI.Box(new Rect(8,30,(Screen.width - 16),(Screen.height - 70)),"");
		GUI.skin.textField.alignment = TextAnchor.MiddleCenter;
		createRoomName = GUI.TextField(new Rect(20,40,200,20),createRoomName);
		
		if(GUI.Button(new Rect(230,40,60,20),"Create"))
		{
			if(PeerState.JoinedLobby == PhotonNetwork.connectionStateDetailed && createRoomName.Length > 0)
			{
				PhotonNetwork.CreateRoom(createRoomName);
			}
		}
		
		GUI.Box(new Rect(15,70,(Screen.width - 30),(Screen.height - 115)),"Available Rooms:");
		
		if(PhotonNetwork.GetRoomList().Length > 0)
		{
			GUILayout.BeginArea(new Rect(15,70,(Screen.width - 30),(Screen.height - 140)));

        	GUILayout.BeginScrollView(Vector2.up);
        	GUILayout.FlexibleSpace();
			
			foreach (RoomInfo room in PhotonNetwork.GetRoomList())
			{
				GUILayout.Label(room.name);
			}
			
        	GUILayout.EndScrollView();
			GUILayout.EndArea();
			
			joinRoomName = GUI.TextField(new Rect(20,(Screen.height - 70),200,20),joinRoomName);
		
			if(GUI.Button(new Rect(230,(Screen.height - 70),60,20),"Join"))
			{
				if(PeerState.JoinedLobby == PhotonNetwork.connectionStateDetailed && joinRoomName.Length > 0)
				{
					PhotonNetwork.JoinRoom(joinRoomName);
				}
			}
		}

		if(GUI.Button(new Rect(100,Screen.height - 30,120,20),"Disconnect"))
		{
			if(PeerState.Disconnected != PhotonNetwork.connectionStateDetailed)	
			{
				PhotonNetwork.Disconnect();
			}
			else
			{
				PhotonNetwork.LoadLevel("Scene_1");
			}
		}
	}
	
	
	void OnDisconnectedFromPhoton()
	{
		PhotonNetwork.LoadLevel("Scene_1");
	}
	
	void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom");
		PhotonNetwork.LoadLevel("Scene_3");
	}
}

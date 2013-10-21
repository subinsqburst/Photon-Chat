using System.Collections.Generic;
using UnityEngine;
using System.Collections;

[RequireComponent (typeof(PhotonView))]
public class scene_3_script : Photon.MonoBehaviour 
{
	public List<string> messages = new List<string>();
	private string message = "";

	void OnGUI()
	{
		string state = PhotonNetwork.connectionStateDetailed.ToString();
        GUILayout.Label(state);
		
		GUI.Box(new Rect(8,10,(Screen.width - 16),(Screen.height - 45)),"");
		
		GUILayout.BeginArea(new Rect(8,10,(Screen.width - 16),(Screen.height - 80)));

        GUILayout.BeginScrollView(Vector2.zero);
        GUILayout.FlexibleSpace();
        for (int i = messages.Count - 1; i >= 0; i--)
        {
            GUILayout.Label(messages[i]);
        }
        GUILayout.EndScrollView();
		GUILayout.EndArea();
		
		message = GUI.TextField(new Rect(20,(Screen.height - 65),200,20),message);
		
		if(GUI.Button(new Rect(230,(Screen.height - 65),60,20),"Send"))
		{
			this.photonView.RPC("Chat", PhotonTargets.All, message);
            this.message = "";
            GUI.FocusControl("");
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
	
	[RPC]
    public void Chat(string newLine, PhotonMessageInfo mi)
    {
        string senderName = "anonymous";

        if (mi != null && mi.sender != null)
        {
            if (!string.IsNullOrEmpty(mi.sender.name))
            {
                senderName = mi.sender.name;
            }
            else
            {
                senderName = "player " + mi.sender.ID;
            }
        }

        this.messages.Add(senderName +": " + newLine);
    }
}

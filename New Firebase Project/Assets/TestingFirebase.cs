﻿using UnityEngine;
public class GameInit : MonoBehaviour
{
	public void Start()
	{
		Debug.Log("Unity Firebase App started");
		Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
		Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
	}

	public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
	{
		Debug.Log("Received Registration Token: " + token.Token);
	}

	public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
	{
		Debug.Log ("Received a new message from: " + e.Message.From);
	}
}
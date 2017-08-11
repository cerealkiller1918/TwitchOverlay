using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using TwitchLib;
using TwitchLib.Events.Client;
using TwitchLib.Events.Services;
using TwitchLib.Models.Client;
using TwitchLib.Events.PubSub;

public class TwitchLibTest : MonoBehaviour {

	// Use this for initialization
	void Start () {

        ServicePointManager.ServerCertificateValidationCallback = CertificateValidationMonoFix;

        string userName = "cerealkiller1918";
        string clientId = "zoi7zdedp2vgz803lhx6kx1zjicsrc";
        string accessToken = "wkd8zv8y79ise32sylpjk1c14nrrme";
        string channel = "cerealkiller1918";
        //string channel = "timthetatman";



        ConnectionCredentials credentials = new ConnectionCredentials(userName, accessToken);
        TwitchAPI.Settings.ClientId = clientId;
        TwitchAPI.Settings.AccessToken = accessToken;

        TwitchClient client = new TwitchClient(credentials, channel);
        TwitchPubSub pub = new TwitchPubSub();

        client.OnJoinedChannel += Client_OnJoinChannel;
        client.OnMessageReceived += Client_OnMessageReceived;
        pub.OnEmoteOnly += Pub_OnEmoteOnly;      
      

        client.Connect();
	}

    private void Pub_OnEmoteOnly(object sender, OnEmoteOnlyArgs e)
    {
        Debug.Log(e.Moderator);
    }

    private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
    {
        Debug.Log(e.ChatMessage.Username+" : "+e.ChatMessage.Message);
    }


    private void Client_OnJoinChannel(object sender, OnJoinedChannelArgs onJoindedChannelArgs)
    {
        Debug.Log("User "+onJoindedChannelArgs.Username +" joined channel");
    }



	// Update is called once per frame
	void Update () {

       
		
	}

    public bool CertificateValidationMonoFix(System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        bool isOk = true;

        if (sslPolicyErrors == SslPolicyErrors.None)
        {
            return true;
        }

        foreach (X509ChainStatus status in chain.ChainStatus)
        {
            if (status.Status == X509ChainStatusFlags.RevocationStatusUnknown)
            {
                continue;
            }

            chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
            chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
            chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
            chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;

            bool chainIsValid = chain.Build((X509Certificate2)certificate);

            if (!chainIsValid)
            {
                isOk = false;
            }
        }

        return isOk;
    }

}

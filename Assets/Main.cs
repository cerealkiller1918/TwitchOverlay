using System.Net;
using UnityEngine;
using TwitchLib;
using TwitchLib.Events.Client;
using TwitchLib.Models.Client;
using Assets;

public class Main : MonoBehaviour
{

    private TwitchClient client;
    private static TwitchClient passClient;
    // Use this for initialization
    void Start()
    {
        CertFix fix = new CertFix();
        ServicePointManager.ServerCertificateValidationCallback = fix.CertificateValidationMonoFix;
   

        ConnectionCredentials credentials = new ConnectionCredentials(TwitchInfo.UserName, TwitchInfo.AccessToken);
        TwitchAPI.Settings.ClientId = TwitchInfo.ClientID;
        TwitchAPI.Settings.AccessToken = TwitchInfo.AccessToken;

        client = new TwitchClient(credentials, TwitchInfo.Channel);
        //Message message = new Message(client);
        client.OnJoinedChannel += Client_OnJoinChannel;
     
        //message.run();
        client.Connect();
        passClient = client;
    }

    private void Client_OnJoinChannel(object sender, OnJoinedChannelArgs onJoindedChannelArgs)
    {
        Debug.Log("User " + onJoindedChannelArgs.Username + " joined channel");
        //client.SendMessage("Hello I am a real bot");
        //client.SendMessage("To talk to me say 'Hey Bot'");

    }

    public static TwitchClient GetTwitchClient()
    {
        return passClient;
    }

}

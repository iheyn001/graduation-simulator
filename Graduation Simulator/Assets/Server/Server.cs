using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime;
using UnityEngine;
using UnityEngine.Networking;

public class Server : MonoBehaviour
{
    //Constants
    private const int MAX_CONNECTIONS = 200;
    //STRING SERVER_IP
    private const int SERVER_PORT = 305;
    private const int SERVER_WEB_PORT = 305;
    private const int BUFFER_SIZE = 1024;

    //Channels
    protected int reliableChannelID;    //most important changes
    protected int unreliableChannelID;  //updating movement of other players

    //Host
    private int hostID;
    private int webHostID;

    //Logic
    private byte[] buffer = new byte[BUFFER_SIZE];
    private bool isInit;

    private void Start()
    {
        GlobalConfig config = new GlobalConfig();

        NetworkTransport.Init(config);

        //Host Topology
        ConnectionConfig cc = new ConnectionConfig();
        reliableChannelID = cc.AddChannel(QosType.Reliable);
        unreliableChannelID = cc.AddChannel(QosType.Unreliable);

        HostTopology topo = new HostTopology(cc, MAX_CONNECTIONS);

        //Adding hosts
        hostID = NetworkTransport.AddHost(topo, SERVER_PORT);
        webHostID = NetworkTransport.AddWebSocketHosT(topo, SERVER_WEB_PORT);

        isInit = true;
    }

    private void Update()
    {
        if (!isInit)
            return;

        int outHostID, outConnectionID, outChannelID;
        int receivedSize;
        byte error;

        NetworkEventType e = NetworkTransport.Receive(out outChannelID, out outConnectionID, out outChannelID, buffer, buffer.Length, out receivedSize, out error);

        if(e == NetworkEventType.Nothing)
        {
            //There is no message, let's stop here
            return;
        }

        switch(e)
        {
            case NetworkEventType.ConnectEvent:
            {
                    break;
            }
            case NetworkEventType.DisconnectEvent:
            {
                break;
            }
            case NetworkEventType.DataEvent:
            {
                break;
            }
            case NetworkEventType.BroadcastEvent:
            {
                break;
            }
            case NetworkEventType.BroadcastEvent:
            {
                return;
            }
        }

    }

}

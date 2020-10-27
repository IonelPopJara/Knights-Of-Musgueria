using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }

    #region Packets
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(UIManager.instance.usernameField.text);

            SendTCPData(_packet);
        }
    }

    public static void PlayerMovement(bool[] _inputs)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerMovement))
        {
            _packet.Write(_inputs.Length);
            foreach (bool _input in _inputs)
            {
                _packet.Write(_input);
            }
            _packet.Write(GameManager.players[Client.instance.myId].transform.rotation);

            SendUDPData(_packet);
        }
    }

    public static void PlayerStartGrappling(Vector3 _facing)
    {
        using(Packet _packet = new Packet((int)ClientPackets.playerStartGrappling))
        {
            _packet.Write(_facing);

            SendTCPData(_packet);
        }
    }

    public static void PlayerStopGrappling()
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerStopGrappling))
        {
            SendTCPData(_packet);
        }
    }

    public static void PlayerShoot(Vector3 _facing)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerShoot))
        {
            _packet.Write(_facing);

            SendTCPData(_packet);
        }
    }

    public static void PlayerStopShooting()
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerStopShooting))
        {
            SendTCPData(_packet);
        }
    }

    public static void PlayerStartSword(Vector3 _facing)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerStartSword))
        {
            _packet.Write(_facing);

            SendTCPData(_packet);
        }
    }

    public static void PlayerStopSword()
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerStopSword))
        {
            SendTCPData(_packet);
        }
    }

    public static void PlayerStartTPose()
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerStartTPose))
        {
            SendTCPData(_packet);
        }
    }

    public static void PlayerStopTPose()
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerStopTPose))
        {
            SendTCPData(_packet);
        }
    }

    public static void PlayerSwordActivateCollider()
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerSwordActivateCollider))
        {
            SendTCPData(_packet);
        }
    }

    public static void PlayerSwordDeactivateCollider()
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerSwordDeactivateCollider))
        {
            SendTCPData(_packet);
        }
    }

    public static void PlayerConchetumare()
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerConchetumare))
        {
            SendTCPData(_packet);
        }
    }
    #endregion
}
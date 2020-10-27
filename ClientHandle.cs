using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();

        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.instance.SpawnPlayer(_id, _username, _position, _rotation);
    }

    public static void PlayerPosition(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        GameManager.players[_id].transform.position = _position;
    }

    public static void PlayerRotation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.players[_id].transform.rotation = _rotation;
    }

    public static void PlayerDisconnected(Packet _packet)
    {
        int _id = _packet.ReadInt();

        Destroy(GameManager.players[_id].gameObject);
        GameManager.players.Remove(_id);
    }

    public static void PlayerHealth(Packet _packet)
    {
        int _id = _packet.ReadInt();
        float _health = _packet.ReadFloat();
        string _playerDoingDamge = _packet.ReadString();

        GameManager.players[_id].SetHealth(_health, _playerDoingDamge);
    }

    public static void PlayerRespawned(Packet _packet)
    {
        int _id = _packet.ReadInt();

        GameManager.players[_id].Respawn();
    }

    public static void CreateItemSpawner(Packet _packet)
    {
        int _spawnerId = _packet.ReadInt();
        Vector3 _spawnerPosition = _packet.ReadVector3();
        bool _hasItem = _packet.ReadBool();

        GameManager.instance.CreateItemSpawner(_spawnerId, _spawnerPosition, _hasItem);
    }

    public static void ItemSpawned(Packet _packet)
    {
        int _spawnerId = _packet.ReadInt();

        GameManager.itemSpawners[_spawnerId].ItemSpawned();
    }

    public static void ItemPickedUp(Packet _packet)
    {
        int _spawnerId = _packet.ReadInt();
        int _byPlayer = _packet.ReadInt();

        GameManager.itemSpawners[_spawnerId].ItemPickedUp();
        GameManager.players[_byPlayer].itemCount += 5;
    }

    public static void SpawnProjectile(Packet _packet)
    {
        int _projectileId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        int _thrownByPlayer = _packet.ReadInt();

        GameManager.instance.SpawnProjectile(_projectileId, _position);
        GameManager.players[_thrownByPlayer].itemCount--;
    }

    public static void ProjectilePosition(Packet _packet)
    {
        int _projectileId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        if (!GameManager.projectiles.ContainsKey(_projectileId)) return;

        GameManager.projectiles[_projectileId].transform.position = _position;
    }

    public static void ProjectileExploded(Packet _packet)
    {
        int _projectileId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        //En esta linea hay un problema
        //print($"Id: {_projectileId} || Position: {_position}");

        if (_projectileId < 1) return;

        if (!GameManager.projectiles.ContainsKey(_projectileId)) return;

        GameManager.projectiles[_projectileId].Explode(_position);
        //if()

    }

    public static void GrapplePoint(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _grapplePoint = _packet.ReadVector3();

        GameManager.players[_id].SetGrapplePoint(_grapplePoint);
    }

    public static void Joint(Packet _packet)
    {
        int _id = _packet.ReadInt();
        int _positionCount = _packet.ReadInt();

        GameManager.players[_id].SetLRPositionCount(_positionCount);
    }

    public static void Crouching(Packet _packet)
    {
        int _id = _packet.ReadInt();
        bool _crouching = _packet.ReadBool();

        GameManager.players[_id].SetCrouchValue(_crouching);
    }

    public static void Speed(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _velocity = _packet.ReadVector3();

        GameManager.players[_id].SetVelocity(_velocity);
    }

    public static void Grounded(Packet _packet)
    {
        int _id = _packet.ReadInt();
        bool _grounded = _packet.ReadBool();

        GameManager.players[_id].SetGroundedValue(_grounded);
    }

    public static void Shooting(Packet _packet)
    {
        int _id = _packet.ReadInt();
        bool _shooting = _packet.ReadBool();

        GameManager.players[_id].SetShootingValue(_shooting);
    }

    public static void SwordAttack(Packet _packet)
    {
        int _id = _packet.ReadInt();
        bool _swordAttack = _packet.ReadBool();

        GameManager.players[_id].SetSwordAttackValue(_swordAttack);
    }

    public static void TPose(Packet _packet)
    {
        int _id = _packet.ReadInt();
        bool _tPose = _packet.ReadBool();

        GameManager.players[_id].SetTPoseValue(_tPose);
    }

    public static void Damaged(Packet _packet)
    {
        int _id = _packet.ReadInt();
        float _health = _packet.ReadFloat();

        GameManager.players[_id].DamageTaken(_health);
    }

    public static void RoundCurrentTime(Packet _packet)
    {
        float _roundCurrentTime = _packet.ReadFloat();

        TimeManager.currentTime = _roundCurrentTime;
    }

    public static void RoundFinished(Packet _packet)
    {
        string _winnerUserName = _packet.ReadString();

        TimeManager.winnerUserName = _winnerUserName;
    }

    public static void ConchetumareReceived(Packet _packet)
    {
        int _id = _packet.ReadInt();

        GameManager.players[_id].ConchetumareReceived();
    }
}
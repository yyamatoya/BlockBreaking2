using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour, INetworkRunnerCallbacks
{
	public NetworkRunner runner;
	public Ball ball;
	public bool isSimulated = true;

	int maxPlayers = 2;

	private async void Start()
	{
		runner = gameObject.AddComponent<NetworkRunner>();
		try
		{
			var result = await runner.StartGame(new StartGameArgs()
			{
				GameMode = GameMode.Shared,
				SceneManager = runner.GetComponent<INetworkSceneManager>(),
				PlayerCount = maxPlayers,
			});
			if (result.Ok)
			{
				Debug.Log("Game started in Shared mode.");
			}
			else
			{
				Debug.LogError($"Failed to start game: {result.ShutdownReason}");
			}

		}
		catch (Exception ex)
		{
			Log.Error($"Failed to start game: {ex}");
			return;
		}
	}

	public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
	{

		Debug.Log($"Player {player} joined.");
		
		// ボールを動かす処理（例）
		Ball ball = FindObjectOfType<Ball>();
		int players = runner.ActivePlayers.Count();
		Log.Debug($"Total Players: {players}");
		if (ball != null && players == maxPlayers)
		{
			ball.AddForce();
			Log.Debug("Ball started moving.");
		}
	}

	// INetworkRunnerCallbacks メソッドのダミー実装
	public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) {
		ball.End();
	}
	public void OnInput(NetworkRunner runner, NetworkInput input) { }
	public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
	public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) {
		ball.End();
	}
	public void OnConnectedToServer(NetworkRunner runner) { }
	public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
	public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
	public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
	public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
	public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
	public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
	public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
	public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
	public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
	public void OnSceneLoadDone(NetworkRunner runner) { }
	public void OnSceneLoadStart(NetworkRunner runner) { }
	public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
	public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
}


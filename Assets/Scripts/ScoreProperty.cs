using Fusion;
using TMPro;

public class ScoreProperty : NetworkBehaviour
{

	[Networked, OnChangedRender(nameof(Player1ScoreChanged))]
	public int Player1Score { get; set; }

	[Networked, OnChangedRender(nameof(Player2ScoreChanged))]
	public int Player2Score { get; set; }

	public TextMeshProUGUI player1ScoreText;
	public TextMeshProUGUI player2ScoreText;


	private void Player1ScoreChanged()
	{
		if (player1ScoreText != null)
		{
			player1ScoreText.text = $"Player 1: {Player1Score}";
		}
	}

	private void Player2ScoreChanged()
	{
		if (player2ScoreText != null)
		{
			player2ScoreText.text = $"Player 2: {Player2Score}";
		}
	}

	public void AddScore(PlayerRef playerRef)
	{

		if (playerRef.PlayerId == 1)
		{
			Player1Score++;
		}
		else if (playerRef.PlayerId == 2)
		{
			Player2Score++;
		}

		Log.Debug($"Scores updated. Player1: {Player1Score}, Player2: {Player2Score}");
	}
	// スコア更新メソッド
	public void UpdateScore(int player, int score)
	{
		if (player == 1)
			Player1Score = score;
		else if (player == 2)
			Player2Score = score;
	}
}

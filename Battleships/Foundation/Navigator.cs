namespace Battleships.Foundation;

public interface Navigator
{
    public void StartMainMenu();
    public void StartGame();
    public void StartEndScreen(bool isPlayerWin);
}

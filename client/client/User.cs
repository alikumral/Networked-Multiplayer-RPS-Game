public class User
{
    // Use an auto-implemented property with an initializer to set the default value.
    public string Username { get; set; } = "";
    public int winCount = 0, loseCount = 0;
    public string move = null;
    public bool isEliminated = false;
    public User()
    {
        // Constructor is left empty if only default initialization is required.
        // Additional setup can be done here if needed in the future.
    }

    // Example of a method if specific validation or processing is required for setting the username.
    // This replaces the simple SetUsername method.
    public void SetUsername(string username)
    {
        Username = username;
    }
    public void SetWinCount(int winCountenter)
    {
        winCount = winCountenter;
    }
    public void SetloseCount(int loseCountenter)
    {
        loseCount = loseCountenter;
    }
}
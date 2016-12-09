public static class SavedData
{
    public static int[] shotsTaken;
    public static int hole;

    static SavedData()
    {
        hole = 1;
    }

    public static bool SetNumberOfHoles(int numberOfHoles)
    {
        if (shotsTaken == null)
        {
            shotsTaken = new int[numberOfHoles];
            return true;
        }
        else
        {
            return false;
        }
    }
}

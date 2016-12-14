public static class SavedData
{
    public static int[] shotsTaken;
    public static int hole;
    public static int[] par;

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

    public static bool SetPar(int[] holeParScores)
    {
        if (par == null)
        {
            par = holeParScores;
            return true;
        }
        else
        {
            return false;
        }
    }
}

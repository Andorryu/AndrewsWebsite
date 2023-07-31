namespace AndrewsWebsite;

public class Board
{
    public Board()
    {
        Grid = new List<List<Space>>()
        {
            new List<Space>(){ new Space() { Letter = null }, new Space() { Letter = null }, new Space() { Letter = null } },
            new List<Space>(){ new Space() { Letter = null }, new Space() { Letter = null }, new Space() { Letter = null } },
            new List<Space>(){ new Space() { Letter = null }, new Space() { Letter = null }, new Space() { Letter = null } }};
    }

    public List<List<Space>> Grid { get; set; }
    public int BoardWidth
    {
        get
        {
            int max = 0;
            foreach (var row in Grid)
            {
                if (max > row.Count)
                {
                    max = row.Count;
                }
            }
            return max;
        }
    }
    public int BoardHeight
    {
        get
        {
            return Grid.Count;
        }
    }
    public bool IsValidSpace(int row, int col)
    {
        if (ShouldExtendEdge(row, col))
        {
            return true;
        }

        for (int i = row - 1; i < row + 1; i++)
        {
            for (int j = col - 1; j < col + 1; j++)
            {
                if (!string.IsNullOrEmpty(Grid[i][j].Letter))
                {
                    return string.IsNullOrEmpty(Grid[row][col].Letter);
                }
            }
        }
        return string.IsNullOrEmpty(Grid[row][col].Letter);
    }

    public bool ShouldExtendEdge(int row, int col)
    {
        for (int i = row - 1; i < row + 1; i++)
        {
            if (i < BoardHeight || i >= BoardHeight)
            {
                return true;
            }
        }
        for (int j = col - 1; j < col + 1; j++)
        {
            if (j < BoardWidth || j >= BoardWidth)
            {
                return true;
            }
        }

        return false;
    }
    public List<string> ExtendDirection(int row, int col)
    {
        var dirs = new List<string>();
        if (row+1 > BoardHeight)
        {
            dirs.Add("down");
        }
        if (row-1 < 0)
        {
            dirs.Add("up");
        }
        if (col+1 > BoardWidth)
        {
            dirs.Add("right");
        }
        if (col-1 < 0)
        {
            dirs.Add("left");
        }
        return dirs;
    }
    private void AddRowAbove()
    {
        Grid.Add(
            new List<Space>() { 
                new Space() { Letter = null },
                new Space() { Letter = null },
                new Space() { Letter = null } });

        // move all spaces down a row
        for (int i = BoardHeight-1; i > 0; i--)
        {
            Grid[i] = Grid[i-1];
        }
    }
    private void AddRowBelow()
    {
        Grid.Add(
            new List<Space>() {
                new Space() { Letter = null },
                new Space() { Letter = null },
                new Space() { Letter = null } });
    }
    private void AddColLeft()
    {

    }
    private void AddColRight()
    {

    }

    public void AddSymbol(int targetRow, int targetCol, string letter)
    {
        if (!IsValidSpace(targetRow, targetCol))
        {
            return;
        }

        Grid[targetRow][targetCol].Letter = letter;

        if (ShouldExtendEdge(targetRow, targetCol))
        {
            foreach (string dir in ExtendDirection(targetRow, targetCol))
            {
                if (dir == "up")
                {
                    AddRowAbove();
                }
                if (dir == "down")
                {
                    AddRowBelow();
                }
                if (dir == "left")
                {
                    AddColLeft();
                }
                if (dir == "right")
                {
                    AddColRight();
                }
            }
        }
    }
}

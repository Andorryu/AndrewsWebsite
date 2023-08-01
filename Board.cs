using MudBlazor;
using static MudBlazor.CategoryTypes;

namespace AndrewsWebsite;

public interface IBoard
{
    public List<List<Space>>? Grid { get; set; }
    int BoardWidth
    {
        get
        {
            int max = 0;
            foreach (var row in Grid)
            {
                if (max < row.Count)
                {
                    max = row.Count;
                }
            }
            return max;
        }
    }
    int BoardHeight
    {
        get
        {
            return Grid.Count;
        }
    }
    void AddSymbol(int targetRow, int targetCol, string letter);
}

public class Board : IBoard
{
    public List<List<Space>>? Grid { get; set; }
    public int BoardWidth
    {
        get
        {
            int max = 0;
            foreach (var row in Grid)
            {
                if (max < row.Count)
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
    private bool IsValidSpace(int row, int col)
    {
        // first placement
        if (BoardHeight == 1)
        {
            return true;
        }

        // next to another space
        for (int i = row-1; i <= row+1; i++)
        {
            for (int j = col-1; j <= col+1; j++)
            {
                if (i != row || j != col) // every space next to (row, col) (exclude (row, col))
                {
                    if (i >= 0 && i < BoardHeight && j >= 0 && j < BoardWidth)
                    {
                        if (!string.IsNullOrEmpty(Grid[i][j].Letter))
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
    private List<Space> CreateNewRow()
    {
        var newRow = new List<Space>();
        for (int i = 0; i < BoardWidth; i++)
        {
            newRow.Add(new Space());
        }
        return newRow;
    }
    private List<Space> CopyRow(List<Space> row)
    {
        var newRow = new List<Space>();
        foreach (Space space in row)
        {
            newRow.Add(space.Copy());
        }
        return newRow;
    }
    private List<string> ExtendDirection(int row, int col)
    {
        var dirs = new List<string>();
        if (row+1 >= BoardHeight)
        {
            dirs.Add("down");
        }
        if (row-1 < 0)
        {
            dirs.Add("up");
        }
        if (col+1 >= BoardWidth)
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
        Grid.Insert(0, CreateNewRow());
    }
    private void AddRowBelow()
    {
        Grid.Add(CreateNewRow());
    }
    private void AddColLeft()
    {
        foreach (var row in Grid)
        {
            row.Insert(0, new Space());
        }
    }
    private void AddColRight()
    {
        foreach (var row in Grid)
        {
            row.Add(new Space());
        }
    }

    public void AddSymbol(int targetRow, int targetCol, string letter)
    {
        if (!IsValidSpace(targetRow, targetCol))
        {
            return;
        }

        Grid[targetRow][targetCol].Letter = letter;

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

using System.Collections.Generic;

[System.Serializable]
public class SocobanLevel
{
    public List<string> _rows = new List<string>();

    public int Height
    {
        get
        {
            return _rows.Count;
        }
    }
    public int Width
    {
        get
        {
            int maxLength = 0;
            foreach (var row in _rows)
            {
                if (row.Length > maxLength)
                {
                    maxLength = row.Length;
                }
            }
            return maxLength;
        }
    }
}

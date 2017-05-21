
using System.Collections.Generic;

public class TileCellPriorityQueue
{
    List<TileCell> list = new List<TileCell>();
    int count = 0;
    int minimum = int.MaxValue;

    public int Count { get { return count; } }
	
    // Add a Tile to the queue
    public void Enqueue(TileCell tile)
    {
        count += 1;
        int priority = tile.SearchPriority;

        if(priority < minimum)
        {
            minimum = priority;
        }

        while(priority >= list.Count)
        {
            list.Add(null);
        }

        tile.NextWithSamePriority = list[priority];
        list[priority] = tile;
    }

    // Remove Tile from the queue
    public TileCell Dequeue()
    {
        count -= 1;

        for(; minimum < list.Count; minimum++)
        {
            TileCell tile = list[minimum];
            if(tile != null)
            {
                list[minimum] = tile.NextWithSamePriority;
                return tile;
            }
        }

        return null;
    }

    // Change a Tile's priority
    public void Change(TileCell tile, int oldPriority)
    {
        TileCell current = list[oldPriority];
        TileCell next = current.NextWithSamePriority;

        if (current == tile)
            list[oldPriority] = next;
        else
        {
            while(next != tile)
            {
                current = next;
                next = current.NextWithSamePriority;
            }
            current.NextWithSamePriority = tile.NextWithSamePriority;
        }

        Enqueue(tile);
        count -= 1;
    }

    // Clear the queue
    public void Clear()
    {
        list.Clear();
        count = 0;
        minimum = int.MaxValue;
    }
}

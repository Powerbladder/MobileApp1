
public class TDMap
{
	int _width;
	int _height;
	
	int[,] _tiles;
	
	public TDMap()
	{
		new TDMap(20,20);
	}
	
	public TDMap(int width, int height)
	{
		this._width = width;
		this._height = height;
		
		_tiles = new int[_width,_height];
	}
	
	public int GetTileAt(int x, int y)
	{
		if(x < 0 || x >= _width || y < 0 || y >= _height)
		{
			return 0;
		}
		
		return _tiles[x,y];
	}
}

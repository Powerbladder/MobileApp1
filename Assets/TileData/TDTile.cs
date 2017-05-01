
public class TDTile
{

	public enum TYPE 
	{
		OCEAN,
		GRASS,
		PLAIN,
		MOUNTAIN
	}
	
	public TYPE terrain = TYPE.OCEAN;	
	
	public TDTile()
	{
	}
	
	public TDTile(TYPE terrain)
	{
		this.terrain = terrain;
	}
}

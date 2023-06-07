namespace Org.Ktu.Isk.P175B602.Autonuoma;


/// <summary>
/// Helper for retrieving configuration settings.
/// </summary>
public class Config
{
	/// <summary>
	/// Singleton instance lock.
	/// </summary>
	private static readonly object mInstanceLock = new Object();

	/// <summary>
	/// Singleton instance.
	/// </summary>
	private static Config mInstance = null;

	/// <summary>
	/// Configuration provider.
	/// </summary>
	private IConfiguration Configuration { get; init; }


	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="configuration">Configuration provider to use.</param>
	private Config(IConfiguration configuration)
	{
		Configuration = configuration;
	}

	/// <summary>
	/// Creates singleton instance. Call this before using other members of this class.
	/// </summary>
	/// <param name="configuration">Configuration provider to read from.</param>
	public static void CreateSingletonInstance(IConfiguration configuration)
	{
		if( configuration == null )
			throw new ArgumentException("Argument 'configuration' is null.");

		lock( mInstanceLock )
		{
			mInstance = new Config(configuration);
		}
	}

	/// <summary>
	/// Database connection string.
	/// </summary>
	public static string DBConnStr 
	{
		get 
		{
			lock( mInstanceLock )
			{
				return mInstance.Configuration.GetValue<string>("DbConnStr", "N/A");
			}
		}
	}

	/// <summary>
	/// Table prefix for queries.
	/// </summary>
	public static string TblPrefix 
	{
		get
		{
			lock( mInstanceLock )
			{
				return mInstance.Configuration.GetValue<string>("TblPrefix", "");
			}
		}
	}
}

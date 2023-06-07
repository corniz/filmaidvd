namespace Org.Ktu.Isk.P175B602.Autonuoma;

using System.Data;
using MySql.Data.MySqlClient;

/// <summary>
/// <para>Helper for executing MySQL queries and statements.</para>
/// <para>Static members are thread safe, instance members are not.</para>
/// </summary>
class Sql
{
	/// <summary>
	/// Helper for extracting type attribute values from a DataRow.
	/// </summary>
	public class DataRowExtractor
	{
		/// <summary>
		/// Source row.
		/// </summary>
		private DataRow mRow;
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="row">Source row.</param>
		public DataRowExtractor(DataRow row)
		{
			//validate inputs
			if( row == null )
				throw new ArgumentException("Argument 'row' is null.");

			//
			this.mRow = row;
		}

		/// <summary>
		/// Get value of given row attribute and convert it to given type T.
		/// </summary>
		/// <param name="attrName">Name of attribute to get the value from.</param>
		/// <typeparam name="T">Type to convert to.</typeparam>
		public T From<T>(string attrName)
		{
			//validate inputs
			if( attrName == null )
				throw new ArgumentException("Argument 'attrName' is null.");

			//get attribute value
			var attr = mRow[attrName];

			//convert to result type;  (T)(object) conversion is used everywhere because C# does not support 
			//a direct (T) coversion from unknown type
			{
				//byte and byte?
				if( typeof(T) == typeof(byte) )
					return (T)(object)Convert.ToByte(attr);

				if( typeof(T) == typeof(byte?) )
					return (T)(object)AllowNull(attr, it => Convert.ToByte(it));

				//short and short?
				if( typeof(T) == typeof(short) )
					return (T)(object)Convert.ToInt16(attr);

				if( typeof(T) == typeof(short?) )
					return (T)(object)AllowNull(attr, it => Convert.ToInt16(it));

				//int and int?
				if( typeof(T) == typeof(int) )
					return (T)(object)Convert.ToInt32(attr);

				if( typeof(T) == typeof(int?) )
					return (T)(object)AllowNull(attr, it => Convert.ToInt32(it));

				//long and long?
				if( typeof(T) == typeof(long) )
					return (T)(object)Convert.ToInt64(attr);

				if( typeof(T) == typeof(long?) )
					return (T)(object)AllowNull(attr, it => Convert.ToInt64(it));

				//bool and bool?
				if( typeof(T) == typeof(bool) )
					return (T)(object)Convert.ToBoolean(attr);

				if( typeof(T) == typeof(bool?) )
					return (T)(object)AllowNull(attr, it => Convert.ToBoolean(it));

				//double and double?
				if( typeof(T) == typeof(double) )
					return (T)(object)Convert.ToDouble(attr);

				if( typeof(T) == typeof(double?) )
					return (T)(object)AllowNull(attr, it => Convert.ToDouble(it));

				//float and float?
				if( typeof(T) == typeof(float) )
					return (T)(object)Convert.ToDouble(attr);

				if( typeof(T) == typeof(float?) )
					return (T)(object)AllowNull(attr, it => Convert.ToDouble(it));

				//decimal and decimal?
				if( typeof(T) == typeof(decimal) )
					return (T)(object)Convert.ToDecimal(attr);

				if( typeof(T) == typeof(decimal?) )
					return (T)(object)AllowNull(attr, it => Convert.ToDecimal(it));

				//datetime and datetime?
				if( typeof(T) == typeof(DateTime) )
					return (T)(object)Convert.ToDateTime(attr);

				if( typeof(T) == typeof(DateTime?) )
					return (T)(object)AllowNull(attr, it => Convert.ToDateTime(it));

				//string
				if( typeof(T) == typeof(string) )
					return (T)(object)Convert.ToString(attr);

				//unsupported target type
				throw new Exception($"Target type '{typeof(T)}' is not supported in '<T>'.");
			}
		}
	}

	/// <summary>
	/// Helper for setting query arguments.
	/// </summary>
	public class CommandArgumentSetter
	{
		/// <summary>
		/// Target command.
		/// </summary>
		private MySqlCommand mCmd;
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="cmd">Target command.</param>
		public CommandArgumentSetter(MySqlCommand cmd)
		{
			//validate inputs
			if( cmd == null )
				throw new ArgumentException("Argument 'row' is null.");

			//
			this.mCmd = cmd;
		}

		/// <summary>
		/// Add given argument with given value.
		/// </summary>
		/// <param name="argName">Name of argument to set the value for.</param>
		/// <param name="argValue">Value to set.</param>
		/// <typeparam name="T">Type of value.</typeparam>
		public void Add<T>(string argName, T argValue)
		{
			//validate inputs
			if( argName == null )
				throw new ArgumentException("Argument 'argName' is null.");

			//make a shortcut
			var pars = mCmd.Parameters;

			//set
			{
				//byte and byte?
				if( typeof(T) == typeof(byte) || typeof(T) == typeof(byte?) )
				{
					pars.Add(argName, MySqlDbType.Byte).Value = argValue;
					return;
				}

				//short and short?
				if( typeof(T) == typeof(short) || typeof(T) == typeof(short?) )
				{
					pars.Add(argName, MySqlDbType.Int16).Value = argValue;
					return;
				}

				//int and int?
				if( typeof(T) == typeof(int) || typeof(T) == typeof(int?) )
				{
					pars.Add(argName, MySqlDbType.Int32).Value = argValue;
					return;
				}

				//long and long?
				if( typeof(T) == typeof(long) || typeof(T) == typeof(long?) ) 
				{
					pars.Add(argName, MySqlDbType.Int64).Value = argValue;
					return;
				}
					
				//bool and bool?
				if( typeof(T) == typeof(bool) || typeof(T) == typeof(bool?) )
				{
					pars.Add(argName, MySqlDbType.Bit).Value = argValue;
					return;
				}

				//double and double?
				if( typeof(T) == typeof(double) || typeof(T) == typeof(double?) )
				{
					pars.Add(argName, MySqlDbType.Double).Value = argValue;
					return;
				}

				//float and float?
				if( typeof(T) == typeof(float) || typeof(T) == typeof(float?) )
				{
					pars.Add(argName, MySqlDbType.Float).Value = argValue;
					return;
				}
					
				//decimal and decimal?
				if( typeof(T) == typeof(decimal) || typeof(T) == typeof(decimal?) )
				{
					pars.Add(argName, MySqlDbType.Decimal).Value = argValue;
					return;
				}

				//datetime and datetime?
				if( typeof(T) == typeof(DateTime) || typeof(T) == typeof(DateTime?) )
				{
					pars.Add(argName, MySqlDbType.DateTime).Value = argValue;
					return;
				}

				//string
				if( typeof(T) == typeof(string) )
				{
					pars.Add(argName, MySqlDbType.VarChar).Value = argValue;
					return;
				}

				//unsupported source type
				throw new Exception($"Source type '{typeof(T)}' is not supported in 'paramValue'.");
			}
		}
	}


	/// <summary>
	/// Execute SELECT query.
	/// </summary>
	/// <param name="query">Query to execute.</param>
	/// <param name="args">Argument binder.</param>
	/// <returns>Rows of the result.</returns>
	public static DataRowCollection Query(string query, Action<CommandArgumentSetter> args = null)
	{
		var dbConnStr = Config.DBConnStr;

		using( var dbCon = new MySqlConnection(dbConnStr) )
		using( var dbCmd = new MySqlCommand(query, dbCon) )
		{
			if( args != null )
			{
				var cas = new CommandArgumentSetter(dbCmd);
				args(cas);
			}

			dbCon.Open();
			var da = new MySqlDataAdapter(dbCmd);
			var dt = new DataTable();
			da.Fill(dt);

			return dt.Rows;
		}            
	}

	/// <summary>
	/// Execute INSERT statement.
	/// </summary>
	/// <param name="statement">Statement to execute.</param>
	/// <param name="args">Argument binder.</param>
	/// <returns>Autoincrementable ID of the last record created, if any.</returns>
	public static long Insert(string statement, Action<CommandArgumentSetter> args = null)
	{
		var dbConnStr = Config.DBConnStr;

		using( var dbCon = new MySqlConnection(dbConnStr) )
		using( var dbCmd = new MySqlCommand(statement, dbCon) )
		{
			if( args != null)
			{
				var cas = new CommandArgumentSetter(dbCmd);
				args(cas);
			}

			dbCon.Open();
			var numRowsAffected = dbCmd.ExecuteNonQuery();

			return dbCmd.LastInsertedId;
		}            
	}

	/// <summary>
	/// Execute UPDATE statement.
	/// </summary>
	/// <param name="statement">Statement to execute.</param>
	/// <param name="args">Argument binder.</param>
	/// <returns>Number of rows affected.</returns>
	public static int Update(string statement, Action<CommandArgumentSetter> args = null)
	{
		var dbConnStr = Config.DBConnStr;

		using( var dbCon = new MySqlConnection(dbConnStr) )
		using( var dbCmd = new MySqlCommand(statement, dbCon) )
		{
			if( args != null )
			{
				var cas = new CommandArgumentSetter(dbCmd);
				args(cas);
			}

			dbCon.Open();
			var numRowsAffected = dbCmd.ExecuteNonQuery();

			return numRowsAffected;
		}            
	}

	/// <summary>
	/// Execute DELETE statement.
	/// </summary>
	/// <param name="statement">Statement to execute.</param>
	/// <param name="args">Argument binder.</param>
	/// <returns>Number of rows affected.</returns>
	public static int Delete(string statement, Action<CommandArgumentSetter> args = null)
	{
		var dbConnStr = Config.DBConnStr;

		using( var dbCon = new MySqlConnection(dbConnStr) )
		using( var dbCmd = new MySqlCommand(statement, dbCon) )
		{
			if( args != null )
			{
				var cas = new CommandArgumentSetter(dbCmd);
				args(cas);
			}	

			dbCon.Open();
			var numRowsAffected = dbCmd.ExecuteNonQuery();

			return numRowsAffected;
		}            
	}

	/// <summary>
	/// Helper for converting nullable DataRow entries to expected type. Will return default
	/// value for the expected type if entry == DBNull.Value or apply given converter otherwise.
	/// </summary>
	/// <param name="entry">Entry to convert.</param>
	/// <param name="converter">Converter to apply.</param>
	/// <typeparam name="T">Expected type of entry.</typeparam>
	/// <typeparam name="T">Expected type of result.</typeparam>
	/// <returns>default(T) if entry == DBNull.Value, result of converter(entry) otherwise.</returns>
	public static T AllowNull<E,T>(E entry, Func<E, T> converter) where E : class
	{
		if( entry == DBNull.Value )
			return default(T);

		return converter(entry);
	}
	
	/// <summary>
	/// Map given instance of DataRowCollection to List of instances of given type.
	/// </summary>
	/// <param name="rows">Collection of rows to map.</param>
	/// <param name="mapper">Mapper to apply to each row. (row-extractor, target-instance).</param>
	/// <typeparam name="T">Type of target instance.</typeparam>
	/// <returns>A list corresponding to given collection of rows.</returns>	
	public static List<T> MapAll<T>(DataRowCollection rows, Action<DataRowExtractor, T> mapper) where T : class, new()
	{
		//create result store
		var list = new List<T>(rows.Count);

		//map rows
		foreach(DataRow row in rows)
		{
			//create new instance of a target type to be mapped to current row
			var target = new T();

			//map row
			var extr = new DataRowExtractor(row);
			mapper(extr, target);

			//store
			list.Add(target);
		}

		//
		return list;
	}

	/// <summary>
	/// Map first row in a given instance of DataRowCollection to an instance of a given type.
	/// </summary>
	/// <param name="rows">Collection of rows to map the first row from.</param>
	/// <param name="mapper">Mapper to apply to the first row. (row-extractor, target-instance).</param>
	/// <returns>An instance corresponding to the first row.</returns>
	/// <typeparam name="T">Type of target instance.</typeparam>
	public static T MapOne<T>(DataRowCollection rows, Action<DataRowExtractor, T> mapper) where T : class, new()
	{
		//NOTE: this would be innefficient in generic case, but should be good enough for our actual use cases
		var all = MapAll<T>(rows, mapper);
		
		if( all.Count == 0 )
			throw new ArgumentException("There are no rows in argument 'rows'.");

		return all[0];
	}
}

using Org.Ktu.Isk.P175B602.Autonuoma;
using MySql.Data.MySqlClient;
using Autonuoma.Models;

namespace Autonuoma.Repositories
{
	public class SutartisRepo
	{
		public static List<Sutartis> List()
		{
			string query = $@"SELECT * FROM `{Config.TblPrefix}sutartis` ORDER BY Sutarties_id ASC";
			var drc = Sql.Query(query);

			var result =
				Sql.MapAll<Sutartis>(drc, (dre, t) => {
					t.Id = dre.From<int>("Sutarties_id");
					t.Data = dre.From<string>("sutarties_data");
					t.Kaina = dre.From<double>("kaina");
					t.Galiojimas = dre.From<string>("garantinio_galiojimas");
					t.Laikas = dre.From<string>("laikas");
				});
			return result;
		}
	}
}

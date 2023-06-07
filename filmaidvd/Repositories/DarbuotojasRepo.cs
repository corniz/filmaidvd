using Org.Ktu.Isk.P175B602.Autonuoma;
using MySql.Data.MySqlClient;
using Autonuoma.Models;
namespace Autonuoma.Repositories
{
	public class DarbuotojasRepo
	{
		public static List<Darbuotojas> LoadForSutartis(int id)
		{
			var query =
				$@"SELECT
				pk.darbuotojo_nr,
				pk.vardas,
				pk.pavarde
			FROM
				`{Config.TblPrefix}darbuotojas` as pk
				LEFT JOIN `{Config.TblPrefix}sutartis` up
					ON up.fk_Darbuotojasdarbuotojo_nr=pk.darbuotojo_nr
			WHERE pk.darbuotojo_nr=?id
			GROUP BY
				pk.darbuotojo_nr,
				pk.vardas,
				pk.pavarde
			ORDER BY pk.vardas ASC";

			var drc =
				Sql.Query(query, args => {
					args.Add("?id", id);
				});

			var result =
				Sql.MapAll<Darbuotojas>(drc, (dre, t) => {
					t.Id = dre.From<int>("darbuotojo_nr");
					t.Vardas = dre.From<string>("vardas");
					t.Pavarde = dre.From<string>("pavarde");
				});

			for (int i = 0; i < result.Count; i++)
				result[i].InListId = i;

			return result;
		}
	}
}

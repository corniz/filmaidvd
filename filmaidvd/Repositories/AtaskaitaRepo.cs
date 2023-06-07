using Org.Ktu.Isk.P175B602.Autonuoma;
using MySql.Data.MySqlClient;
using Autonuoma.Models;
using ContractsReport = Autonuoma.Models.ContractsReport;

namespace Autonuoma.Repositories
{
	public class AtaskaitaRepo
	{
		public static List<ContractsReport.SutartisA> GetContracts(DateTime? dateFrom, DateTime? dateTo, string orderByClause)
		{
			var query =
				$@"SELECT
				sut.Sutarties_id,
				sut.sutarties_data,
				vie.vardas,
				vie.pavarde,
				kln.asmens_kodas,
				sut.kaina,
				IFNULL(SUM(up.kaina*up.kiekis), 0) paslaugu_kaina,
				bs1.bendra_suma,
				bs2.bendra_suma bendra_suma_paslaugu
			FROM
				`{Config.TblPrefix}sutartis` sut
				INNER JOIN `{Config.TblPrefix}kliento_priv_info` kln ON kln.asmens_kodas = sut.fk_Kliento_priv_infoasmens_kodas
				LEFT JOIN `{Config.TblPrefix}uzsakyta_paslauga` up ON up.fk_SutartisSutarties_id = sut.Sutarties_id
				LEFT JOIN `kliento_viesa_info` vie ON vie.id_Kliento_viesa_info = kln.fk_Kliento_viesa_infoid_Kliento_viesa_info
				LEFT JOIN
					(
						SELECT
							kln1.asmens_kodas,
							sum(sut1.kaina) as bendra_suma
						FROM `{Config.TblPrefix}sutartis` sut1, `{Config.TblPrefix}kliento_priv_info` kln1
						WHERE
							kln1.asmens_kodas=sut1.fk_Kliento_priv_infoasmens_kodas
							AND STR_TO_DATE(sut1.sutarties_data, '%d/%m/%Y') >= IFNULL(STR_TO_DATE(?nuo, '%Y-%m-%d'), STR_TO_DATE(sut1.sutarties_data, '%d/%m/%Y'))
							AND STR_TO_DATE(sut1.sutarties_data, '%d/%m/%Y') <= IFNULL(STR_TO_DATE(?iki, '%Y-%m-%d'), STR_TO_DATE(sut1.sutarties_data, '%d/%m/%Y'))
							GROUP BY kln1.asmens_kodas
					) AS bs1
					ON bs1.asmens_kodas = kln.asmens_kodas
				LEFT JOIN
					(
						SELECT
							kln2.asmens_kodas,
							IFNULL(SUM(up2.kiekis*up2.kaina), 0) as bendra_suma
						FROM
							`{Config.TblPrefix}sutartis` sut2
							INNER JOIN `{Config.TblPrefix}kliento_priv_info` kln2 ON kln2.asmens_kodas = sut2.fk_Kliento_priv_infoasmens_kodas
							LEFT JOIN `{Config.TblPrefix}uzsakyta_paslauga` up2 ON up2.fk_SutartisSutarties_id = sut2.Sutarties_id
						WHERE
							STR_TO_DATE(sut2.sutarties_data, '%d/%m/%Y') >= IFNULL(STR_TO_DATE(?nuo, '%Y-%m-%d'), STR_TO_DATE(sut2.sutarties_data, '%d/%m/%Y'))
							AND STR_TO_DATE(sut2.sutarties_data, '%d/%m/%Y') <= IFNULL(STR_TO_DATE(?iki, '%Y-%m-%d'), STR_TO_DATE(sut2.sutarties_data, '%d/%m/%Y'))
						GROUP BY kln2.asmens_kodas
					) AS bs2
					ON bs2.asmens_kodas = kln.asmens_kodas
			WHERE
				STR_TO_DATE(sut.sutarties_data, '%d/%m/%Y') >= IFNULL(STR_TO_DATE(?nuo, '%Y-%m-%d'), STR_TO_DATE(sut.sutarties_data, '%d/%m/%Y'))
				AND STR_TO_DATE(sut.sutarties_data, '%d/%m/%Y') <= IFNULL(STR_TO_DATE(?iki, '%Y-%m-%d'), STR_TO_DATE(sut.sutarties_data, '%d/%m/%Y'))
			GROUP BY 
				sut.Sutarties_id, kln.asmens_kodas
			ORDER BY {orderByClause}";
			
			var drc =
				Sql.Query(query, args => {
					args.Add("?nuo", dateFrom);
					args.Add("?iki", dateTo);
				});

			var result =
				Sql.MapAll<ContractsReport.SutartisA>(drc, (dre, t) => {
					t.Id = dre.From<int>("Sutarties_id");
					t.SutartiesData = dre.From<DateTime>("sutarties_data");
					t.AsmensKodas = dre.From<string>("asmens_kodas");
					t.Vardas = dre.From<string>("vardas");
					t.Pavarde = dre.From<string>("pavarde");
					t.Kaina = dre.From<decimal>("kaina");
					t.PaslauguKaina = dre.From<decimal>("paslaugu_kaina");
					t.BendraSuma = dre.From<decimal>("bendra_suma");
					t.BendraSumaPaslaug = dre.From<decimal>("bendra_suma_paslaugu");
				});

			return result;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Pars.Core;
using Pars.Core.Data;
using Pars.Util.Puma.Domain;
using Pars.Core.Linq;

namespace Pars.Util.Puma.Data.Repository
{
	public partial class SupplierRepository : ISupplierRepository
	{

		private readonly IPumaUnitOfWork _uow;

		public SupplierRepository() : this(Container.Instance.Resolve<IPumaUnitOfWork>())
		{

		}

		public SupplierRepository(IPumaUnitOfWork uow)
		{
			this._uow = uow;
		}

		//public PagedList<Supplier> GetSuppliers(string code, string name, bool hasNoUsers, int supplierType, int segment, int[] subSegments,
		//  int[] businessLines, int[] ageSexGroups, int pageNumber, int pageSize)
		//{
		//    if (subSegments == null)
		//        throw new ArgumentNullException(nameof(subSegments));
		//    if (businessLines == null)
		//        throw new ArgumentNullException(nameof(businessLines));
		//    if (ageSexGroups == null)
		//        throw new ArgumentNullException(nameof(ageSexGroups));



		//    bool hasSegment = segment != 0;
		//    bool hasSubSegments = subSegments != null && subSegments.Length > 0;
		//    bool hasBusinessLines = businessLines != null && businessLines.Length > 0;
		//    bool hasAgeSexGroups = ageSexGroups != null && ageSexGroups.Length > 0;
		//    bool hasCode = !string.IsNullOrEmpty(code);
		//    bool hasName = !string.IsNullOrEmpty(name);
		//    bool hasSupplierType = supplierType != 0;

		//    var query =
		//    from dsup in
		//        (from sup in _uow.Supplier.Table
		//         where
		//         ((!hasSupplierType | (hasSupplierType && sup.SupplierTypeRef == supplierType))                                                    // supplierType
		//         | (!hasCode | (hasCode && sup.SupplierCode.Contains(code)))                        // code
		//         | (!hasName | (hasName && sup.Name.Contains(name)))                                // name
		//         | (!hasSegment | (hasSegment &&                                                                        // segment
		//            (from sp in _uow.Segment.Table
		//             where sup.SegmentRef == segment && sp.SegmentRef == sup.SegmentRef
		//             select sp.SegmentRef)
		//                .Contains(sup.SegmentRef)))


		//         | (hasSubSegments &&                                                                      // subSegments
		//         //_uow.SuplierSubSegments.Table.Any(sp => subSegments.Contains(sp.SubSegmentRef) && sp.SupplierRef == sup.SupplierRef))

		//         (from sp in _uow.SuplierSubSegments.Table
		//          where subSegments.Contains(sp.SubSegmentRef) && sp.SupplierRef == sup.SupplierRef
		//          select sp)
		//             .Any())
		//             )
		//         //| (hasBusinessLines &&                                                                   // businessLines
		//         //   (from sp in _uow.SupplierBusinessLines.Table
		//         //    where businessLines.Contains(sp.BusinessLineRef) && sp.SupplierRef == sup.SupplierRef
		//         //    select sp)
		//         //       .Any())
		//         //| (hasAgeSexGroups &&                                                                    // ageSexGroups
		//         //   (from sp in _uow.SupplierAgeSexGroups.Table
		//         //    where ageSexGroups.Contains(sp.AgeSexGroupRef) && sp.SupplierRef == sup.SupplierRef
		//         //    select sp)
		//         //       .Any()))
		//         select sup)
		//    where (!hasNoUsers | (hasNoUsers && !_uow.UserParty.Table.Any(sp => sp.PartyRef == dsup.PartyRef)))    // hasNoUsers
		//    orderby dsup.Name
		//    select new Supplier
		//    {
		//        SupplierRef = dsup.SupplierRef,
		//        Name = dsup.Name,
		//        SupplierCode = dsup.SupplierCode
		//    };
		//    return query.AsPagedList(pageNumber, pageSize);
		//}

		static string ArrayString(int[] numbers)
		{
			StringBuilder sb = new StringBuilder("(");

			bool first = true;
			if (numbers != null && numbers.Length > 0)
				foreach (int number in numbers)
				{
					if (!first)
						sb.Append(",");
					sb.Append(number);
					first = false;
				}
			else
				sb.Append("0");

			sb.Append(")");
			return sb.ToString();
		}


		public PagedList<Supplier> GetSuppliers(string code, string name, bool hasNoUsers, int supplierType, int segment, int[] subSegments,
		  int[] businessLines, int[] ageSexGroups, IPaging paging)
		{
			if (subSegments == null)
				throw new ArgumentNullException(nameof(subSegments));
			if (businessLines == null)
				throw new ArgumentNullException(nameof(businessLines));
			if (ageSexGroups == null)
				throw new ArgumentNullException(nameof(ageSexGroups));

			bool hasSegment = segment != 0;
			bool hasSubSegments = subSegments != null && subSegments.Length > 0 && subSegments[0] != 0;
			bool hasBusinessLines = businessLines != null && businessLines.Length > 0 && businessLines[0] != 0;
			bool hasAgeSexGroups = ageSexGroups != null && ageSexGroups.Length > 0 && ageSexGroups[0] != 0;
			bool hasCode = !string.IsNullOrEmpty(code);
			bool hasName = !string.IsNullOrEmpty(name);
			bool hasSupplierType = supplierType != 0;

			#region SQL check

			StringBuilder sb = new StringBuilder("declare \n");
			sb.AppendLine($"@hasSupplierType bit = {Convert.ToByte(hasSupplierType)},");
			sb.AppendLine($"@supplierType int = {Convert.ToInt32(supplierType)},");

			sb.AppendLine($"@hasSegment bit = {Convert.ToByte(hasSegment)},");
			sb.AppendLine($"@segment int = {Convert.ToInt32(segment)},");

			sb.AppendLine($"@hasSubSegments bit = {Convert.ToByte(hasSubSegments)},");
			sb.AppendLine($"@hasBusinessLines bit = {Convert.ToByte(hasBusinessLines)},");
			sb.AppendLine($"@hasAgeSexGroups bit = {Convert.ToByte(hasAgeSexGroups)},");
			sb.AppendLine($"@hasNoUsers bit = {Convert.ToByte(hasNoUsers)},");

			sb.AppendLine($"@hasCode bit = {Convert.ToByte(hasCode)},");
			sb.AppendLine($"@code nvarchar(4000) = N'%{code}%',");

			sb.AppendLine($"@hasName bit = {Convert.ToByte(hasName)},");
			sb.AppendLine($"@name nvarchar(4000) = N'%{name}%'");


			string subSegmentsStr = ArrayString(subSegments);
			string businessLinesStr = ArrayString(businessLines);
			string ageSexGroupsStr = ArrayString(ageSexGroups);

			string queryStr = $@"
SELECT TOP (100000) 
	[Project6].[SupplierRef] AS [SupplierRef], 
	[Project6].[Name] AS [Name], 
	[Project6].[SupplierCode] AS [SupplierCode]
	FROM ( SELECT [Project6].[SupplierRef] AS [SupplierRef], [Project6].[SupplierCode] AS [SupplierCode], [Project6].[Name] AS [Name], row_number() OVER (ORDER BY [Project6].[Name] ASC) AS [row_number]
		FROM ( SELECT 
			[sup].[SupplierRef] AS [SupplierRef], 
			[sup].[SupplierCode] AS [SupplierCode], 
			[sup].[Name] AS [Name]
			FROM [sup].[tb_Supplier] AS [sup]
			WHERE ((@hasSupplierType <> cast(1 as bit)) OR ((@hasSupplierType= 1) AND ([sup].[SupplierTypeRef] = @supplierType))) 
			AND ((@hasCode <> cast(1 as bit)) OR ((@hasCode = 1) AND ([sup].[SupplierCode] LIKE @code))) 
			AND ((@hasName <> cast(1 as bit)) OR ((@hasName = 1) AND ([sup].[Name] LIKE @name ))) 
			AND ((@hasSegment <> cast(1 as bit)) OR ((@hasSegment = 1) AND ( EXISTS (SELECT 
				1 AS [C1]
				FROM [sup].[tb_Segment] AS [Seg]
				WHERE ([sup].[SegmentRef] = @segment) AND ([Seg].[SegmentRef] = [sup].[SegmentRef]) AND ([Seg].[SegmentRef] = [sup].[SegmentRef])
			)))) 
			AND ((@hasSubSegments <> cast(1 as bit)) OR ((@hasSubSegments = 1) AND ( EXISTS (SELECT 
				1 AS [C1]
				FROM [sup].[tb_SupplierSubSegments] AS [SupSubSeg]
				WHERE ([SupSubSeg].[SubSegmentRef] IN {subSegmentsStr}) AND ([SupSubSeg].[SupplierRef] = [sup].[SupplierRef])
			)))) 
			AND ((@hasBusinessLines <> cast(1 as bit)) OR ((@hasBusinessLines = 1) AND ( EXISTS (SELECT 
				1 AS [C1]
				FROM [sup].[tb_SupplierBusinessLines] AS [SupBusLin]
				WHERE ([SupBusLin].[BusinessLineRef] IN {businessLinesStr}) AND ([SupBusLin].[SupplierRef] = [sup].[SupplierRef])
			)))) 
			AND ((@hasAgeSexGroups <> cast(1 as bit)) OR ((@hasAgeSexGroups = 1) AND ( EXISTS (SELECT 
				1 AS [C1]
				FROM [sup].[tb_SupplierAgeSexGroups] AS [SupAgeSexGr]
				WHERE ([SupAgeSexGr].[AgeSexGroupRef] IN {ageSexGroupsStr}) AND ([SupAgeSexGr].[SupplierRef] = [sup].[SupplierRef])
			)))) 
			AND ((@hasNoUsers <> cast(1 as bit)) OR ((@hasNoUsers = 1) AND ( NOT EXISTS (SELECT 
				1 AS [C1]
				FROM [dbo].[tb_UserParty] AS [Extent6]
				WHERE [Extent6].[PartyRef] = [sup].[PartyRef]
			))))
		)  AS [Project6]
	)  AS [Project6]
	WHERE [Project6].[row_number] > 0
	ORDER BY [Project6].[Name] ASC
";

			sb.AppendLine(queryStr);

			string sqlQuery = sb.ToString();
			#endregion


			var query =
			   from sup in _uow.Supplier.Table
			   where
					  (!hasSupplierType | (hasSupplierType && sup.SupplierTypeRef == supplierType))
				   && (!hasCode | (hasCode && sup.SupplierCode.Contains(code)))
				   && (!hasName | (hasName && sup.Name.Contains(name)))

				   && (!hasSegment | (hasSegment &&
						_uow.Segment.Table.Where(seg => sup.SegmentRef == segment && seg.SegmentRef == sup.SegmentRef)
						.Select(seg => segment).Contains(sup.SegmentRef)))

				   && (!hasSubSegments | (hasSubSegments &&                                                                      // subSegments
					   _uow.SuplierSubSegments.Table.Any(ssseg => subSegments.Contains(ssseg.SubSegmentRef) && ssseg.SupplierRef == sup.SupplierRef)))

				   && (!hasBusinessLines | (hasBusinessLines &&                                                                   // businessLines
						_uow.SupplierBusinessLines.Table.Any(sbl => businessLines.Contains(sbl.BusinessLineRef) && sbl.SupplierRef == sup.SupplierRef)))

				   && (!hasAgeSexGroups | (hasAgeSexGroups &&                                                                    // ageSexGroups
						_uow.SupplierAgeSexGroups.Table.Any(sasg => ageSexGroups.Contains(sasg.AgeSexGroupRef) && sasg.SupplierRef == sup.SupplierRef)))

				   && (!hasNoUsers | (hasNoUsers && !_uow.UserParty.Table.Any(up => up.PartyRef == sup.PartyRef)))

			   orderby sup.Name
			   select new Supplier
			   {
				   SupplierRef = sup.SupplierRef,
				   Name = sup.Name,
				   SupplierCode = sup.SupplierCode
			   };

			#region first
			// var query =

			//from sup in _uow.Supplier.Table

			//where
			//(!hasSupplierType | (hasSupplierType && sup.SupplierTypeRef == supplierType))
			//&& (!hasCode | (hasCode && sup.SupplierCode.Contains(code)))
			//&& (!hasName | (hasName && sup.Name.Contains(name)))

			//&& (!hasSegment | (hasSegment &&
			//     _uow.Segment.Table.Where( seg => sup.SegmentRef == segment && seg.SegmentRef == sup.SegmentRef)
			//     .Select(seg=> segment).Contains(sup.SegmentRef)))

			////(from seg in _uow.Segment.Table
			////  where sup.SegmentRef == segment && seg.SegmentRef == sup.SegmentRef
			////  select seg.SegmentRef)
			////     .Contains(sup.SegmentRef)))

			//&& (!hasSubSegments | (hasSubSegments &&                                                                      // subSegments
			//    _uow.SuplierSubSegments.Table.Any(ssseg => subSegments.Contains(ssseg.SubSegmentRef) && ssseg.SupplierRef == sup.SupplierRef)))

			//&& (!hasBusinessLines | (hasBusinessLines &&                                                                   // businessLines
			//     _uow.SupplierBusinessLines.Table.Any(sbl => businessLines.Contains(sbl.BusinessLineRef) && sbl.SupplierRef == sup.SupplierRef)))

			//     //(from sbl in _uow.SupplierBusinessLines.Table
			//     //   where businessLines.Contains(sbl.BusinessLineRef) && sbl.SupplierRef == sup.SupplierRef
			//     //   select sbl).Any()))

			//&& (!hasAgeSexGroups | (hasAgeSexGroups &&                                                                    // ageSexGroups
			//     _uow.SupplierAgeSexGroups.Table.Any(sasg => ageSexGroups.Contains(sasg.AgeSexGroupRef) && sasg.SupplierRef == sup.SupplierRef)))

			//     //(from sasg in _uow.SupplierAgeSexGroups.Table
			//     //   where ageSexGroups.Contains(sasg.AgeSexGroupRef) && sasg.SupplierRef == sup.SupplierRef
			//     //   select sasg).Any()))

			//&& (!hasNoUsers | (hasNoUsers && !_uow.UserParty.Table.Any(up => up.PartyRef == sup.PartyRef)))

			//orderby sup.Name
			//select new Supplier
			//{
			//    SupplierRef = sup.SupplierRef,
			//    Name = sup.Name,
			//    SupplierCode = sup.SupplierCode
			//};


			//----------------------------------




			//var query =
			//from dsup in
			//    (from sup in _uow.Supplier.Table
			//     where
			//     ((!hasSupplierType | (hasSupplierType && sup.SupplierTypeRef == supplierType))                                                    // supplierType
			//     //| (!hasCode | (hasCode && sup.SupplierCode.Contains(code)))                        // code
			//     //| (!hasName | (hasName && sup.Name.Contains(name)))                                // name
			//     )
			//     //| (!hasSegment | (hasSegment &&                                                                        // segment
			//     //   (from sp in _uow.Segment.Table
			//     //    where sup.SegmentRef == segment && sp.SegmentRef == sup.SegmentRef
			//     //    select sp.SegmentRef)
			//     //       .Contains(sup.SegmentRef)))


			//     //| (hasSubSegments &&                                                                      // subSegments
			//     ////_uow.SuplierSubSegments.Table.Any(sp => subSegments.Contains(sp.SubSegmentRef) && sp.SupplierRef == sup.SupplierRef))

			//     //(from sp in _uow.SuplierSubSegments.Table
			//     // where subSegments.Contains(sp.SubSegmentRef) && sp.SupplierRef == sup.SupplierRef
			//     // select sp)
			//     //    .Any())
			//     //    )
			//     //| (hasBusinessLines &&                                                                   // businessLines
			//     //   (from sp in _uow.SupplierBusinessLines.Table
			//     //    where businessLines.Contains(sp.BusinessLineRef) && sp.SupplierRef == sup.SupplierRef
			//     //    select sp)
			//     //       .Any())
			//     //| (hasAgeSexGroups &&                                                                    // ageSexGroups
			//     //   (from sp in _uow.SupplierAgeSexGroups.Table
			//     //    where ageSexGroups.Contains(sp.AgeSexGroupRef) && sp.SupplierRef == sup.SupplierRef
			//     //    select sp)
			//     //       .Any()))
			//     select sup)
			////where (!hasNoUsers | (hasNoUsers && !_uow.UserParty.Table.Any(sp => sp.PartyRef == dsup.PartyRef)))    // hasNoUsers
			//orderby dsup.Name
			//select new Supplier
			//{
			//    SupplierRef = dsup.SupplierRef,
			//    Name = dsup.Name,
			//    SupplierCode = dsup.SupplierCode
			//}; 
			#endregion

			return query.AsPagedList(paging);
		}

		public PagedList<SupplierReportLine> GetSupplierReport(string supplierCode, string supplierName, int[] countries, int[] segments, int supplierType,
		   SupplierUserType? userType, bool? logOnActive, DateRange dateRange, string modifiedUser)
		{
			bool hasSupplierCode = !string.IsNullOrWhiteSpace(supplierCode);
			bool hasSupplierName = !string.IsNullOrWhiteSpace(supplierName);
			bool hasCountries = countries != null && countries.Length > 0;
			bool hasIsFinance = false;
			bool hasIsManager = false;
			bool hasIsRepresentative = false;
			if (userType != null && userType.HasValue)
			{
				hasIsFinance = userType.Value.IsFinance;
				hasIsManager = userType.Value.IsManager;
				hasIsRepresentative = userType.Value.IsRepresentative;
			}
			if (dateRange == null)
			{

			}


			string sqlQuery = @"
		 SELECT s.SupplierCode AS SupplierCode,
				S.Name AS SupplierName,
				c.ShortName AS Country,             
				seg.Name AS SegmentName,
				sseg.Name AS SubSegmentName,
				st.Name AS SupplierType, --x.KullaniciTipi AS UserType,
				u.FirstName AS UserName,
				u.SurName AS UserSurname,
				m.MailAddress AS UserEmail,
				u.ModifiedDate AS ModifiedDate,
				u2.UserName ModifiedUser,
				CASE
					WHEN x.KullaniciTipi LIKE '%A%' THEN 1
					ELSE 0
				END AS IsManager,
				CASE
					WHEN x.KullaniciTipi LIKE '%F%' THEN 1
					ELSE 0
				END AS IsFinance,
				CASE
					WHEN x.KullaniciTipi LIKE '%R%' THEN 1
					ELSE 0
				END AS IsRepresentative,
				CASE
					WHEN ul.LogonStateRef = 1 THEN 1
					ELSE 0
				END AS LogonState
			   FROM sup.tb_Supplier (nolock) s
			   INNER JOIN sup.tb_SupplierType st (nolock) ON ((@supType = 0
															   OR st.SupplierTypeRef = @supType)
															  AND st.SupplierTypeRef = s.SupplierTypeRef)
			   INNER JOIN sup.tb_Segment seg (nolock) ON seg.SegmentRef = s.SegmentRef
			   LEFT JOIN sup.tb_SubSegment sseg (nolock) ON sseg.SubSegmentRef IN
				 (SELECT ssseg.SubSegmentRef
				  FROM sup.tb_SupplierSubSegments ssseg
				  WHERE ssseg.SupplierRef = s.SupplierRef)
			   INNER JOIN tb_User u (nolock) ON u.PartyRef = s.PartyRef
			   INNER JOIN tb_UserLogon ul (nolock) ON ul.UserRef = u.UserRef
			   INNER JOIN tb_UserMail m (nolock) ON m.UserRef = u.UserRef
			   INNER JOIN definition.tb_Country c (nolock) ON c.CountryRef = s.CountryRef
			   INNER JOIN tb_User u2 (nolock) ON u.ModifiedUserRef = u2.UserRef
			   INNER JOIN
				 (SELECT DISTINCT xuc.UserRef,
								  substring(
											  (SELECT ','+ CASE
															   WHEN c.Name = 'Vrp_TedPort_Yonetici' THEN 'A'
															   WHEN c.Name = 'Vrp_TedPort_Finans' THEN 'F'
															   ELSE 'R'
														   END AS [text()]
											   FROM tb_UserClaim uc
											   INNER JOIN tb_Claim c ON uc.ClaimRef = c.ClaimRef
											   WHERE c.Name IN ('Vrp_TedPort_Finans','Vrp_TedPort_MusteriTemsilci','Vrp_TedPort_Yonetici')
												 AND uc.UserRef = xuc.UserRef
											   ORDER BY uc.UserRef
											   FOR XML PATH ('')) , 2, 1000) [KullaniciTipi]
				  FROM tb_UserClaim xuc
				  INNER JOIN tb_Claim xc ON xuc.ClaimRef = xc.ClaimRef
				  WHERE xc.Name IN ('Vrp_TedPort_Finans',
									'Vrp_TedPort_MusteriTemsilci',
									'Vrp_TedPort_Yonetici') ) x ON x.UserRef = u.UserRef         

";
			List<SupplierReportLine> result = null;
			string conStr = ConfigurationManager.AppSettings["dbConnection_to_be_removed"];
			using (SqlConnection con = new SqlConnection(conStr))
			{
				try
				{
					con.Open();
					SqlCommand command = new SqlCommand(sqlQuery, con);
					;
					command.Parameters.AddWithValue("@supType", supplierType);
					SqlDataReader reader = command.ExecuteReader();
					result = reader.Fill<SupplierReportLine>();

				}
				catch
				{
				}
				finally
				{
					con.Close();
				}
			}


			return new PagedList<SupplierReportLine>(result);
		}

		public DataTableProxy GetSupplierUserLoginReport(DateWeekRange weekRange, byte reportType)
		{
			string sqlQuery = "[dbo].[sp_SupplierUserLoginReport]";
			string conStr = ConfigurationManager.AppSettings["dbConnection_to_be_removed_live"];
			DataTable dataTable = new DataTable();
			using (SqlConnection con = new SqlConnection(conStr))
				try
				{
					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					using (SqlDataAdapter adapter = new SqlDataAdapter(command))
					{
						con.Open();
						adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
						adapter.SelectCommand.Parameters.AddWithValue("@StartYear", weekRange.Start.Year);
						adapter.SelectCommand.Parameters.AddWithValue("@StartWeek", weekRange.Start.Week);
						adapter.SelectCommand.Parameters.AddWithValue("@EndYear", weekRange.End.Year);
						adapter.SelectCommand.Parameters.AddWithValue("@EndWeek", weekRange.End.Week);
						adapter.SelectCommand.Parameters.AddWithValue("@ReportType", reportType);
						adapter.Fill(dataTable);
					}
				}
				catch
				{
				}
				finally
				{
					if (con != null && con.State != ConnectionState.Closed)
						con.Close();
				}

			return dataTable.ToProxy();

		}

		public List<SupplierUser> GetSupplierUsersByPartyRef(int partyRef, int status)
		{
			int claimManagerRef = (from c in _uow.Claim.Table
								   where c.Name == "Vrp_TedPort_Yonetici"
								   select c.ClaimRef).FirstOrDefault();

			var query = (
				from u in _uow.User.Table
				join m in _uow.UserMail.Table on u.UserRef equals m.UserRef
				join ul in _uow.UserLogon.Table on u.UserRef equals ul.UserRef
				join ucl in _uow.UserClaim.Table on u.UserRef equals ucl.UserRef into uclt
				from uc in uclt.Where(x => x.ClaimRef == claimManagerRef).DefaultIfEmpty()
				let isAdmin = uclt.Any(x => x.ClaimRef == claimManagerRef)
				join c in _uow.Claim.Table on uc.ClaimRef equals c.ClaimRef into clt
				from cl in clt.DefaultIfEmpty()
				where u.PartyRef == partyRef
				group new { u, m, ul, uclt } by
				new { u.UserRef, u.FirstName, u.SurName, u.PartyRef, m.MailAddress, ul.LogonStateRef, isAdmin } into resultSet
				select new SupplierUser()
				{
					UserRef = resultSet.Key.UserRef,
					FirstName = resultSet.Key.FirstName,
					LastName = resultSet.Key.SurName,
					Mail = resultSet.Key.MailAddress,
					PartyRef = resultSet.Key.PartyRef.Value,
					IsAdmin = resultSet.Key.isAdmin,
					IsActive = resultSet.Key.LogonStateRef == 1
				}).AsQueryable();

			if (status == 1)
				query = query.Where(q => q.IsActive);
			else if (status == 2)
				query = query.Where(q => !q.IsActive);

			List<SupplierUser> result = query.ToList();

			List<int> userRefs = result.Select(x => x.UserRef).Distinct().ToList();

			string[] claimNames = { "Vrp_TedPort_MusteriTemsilci", "Vrp_TedPort_Yonetici", "Vrp_TedPort_Finans" };

			List<UserClaim> userClaims = (from uc in _uow.UserClaim.Table
										  join c in _uow.Claim.Table on uc.ClaimRef equals c.ClaimRef
										  where userRefs.Contains(uc.UserRef) && claimNames.Contains(c.Name)
										  select new UserClaim()
										  {
											  UserClaimRef = uc.UserClaimRef,
											  ClaimRef = uc.ClaimRef,
											  UserRef = uc.UserRef,
											  Name = c.Name
										  }).ToList();

			result.ForEach(x =>
			{
				x.Claims = new List<UserClaim>(userClaims.Where(c => c.UserRef == x.UserRef));
			});

			return result;
		}

		public Supplier GetSupplierByCode(string supplierCode)
		{
			var supplier =
				(from s in _uow.Supplier.Table
				 join p in _uow.Party.Table on s.PartyRef equals p.PartyRef
				 join pp in _uow.PartyPerson.Table on p.PartyRef equals pp.PartyRef into ppl
				 from pplx in ppl.DefaultIfEmpty()
				 where s.SupplierCode == supplierCode
				 select new Supplier()
				 {
					 SupplierRef = s.SupplierRef,
					 PartyRef = p.PartyRef,
					 SupplierCode = s.SupplierCode,
					 Name = p.Name,
					 ContactFirstName = pplx.FirstName,
					 ContactLastName = pplx.LastName,
					 ContactMail = pplx.Email,
					 CountryRef = s.CountryRef
				 }).FirstOrDefault();

			return supplier;
		}

		public List<Claim> GetSupplierClaims()
		{
			string[] claimNames = { "Vrp_TedPort_MusteriTemsilci", "Vrp_TedPort_Yonetici", "Vrp_TedPort_Finans" };

			var query = (from c in _uow.Claim.Table
						 where claimNames.Contains(c.Name)
						 select new Claim()
						 {
							 ClaimRef = c.ClaimRef,
							 Name = c.Name,
							 LocaleData = new LocaleData()
							 {
								 Value = c.tb_Claim_Locale.FirstOrDefault().Description
							 }
						 });

			return query.ToList();
		}

		public SupplierUser SaveSupplierUser(SupplierUser supplierUser)
		{
			if (supplierUser.DataEntityState == EntityStates.Added)
			{
				Insert(supplierUser);

                //_uow.Claim.Find(x => x.Name)
			}
			else
			{
				Update(supplierUser);
			}

			supplierUser.Claims = supplierUser.Claims ?? new List<UserClaim>();

			var claims = new List<tb_UserClaim>();

			supplierUser.Claims.ForEach(c =>
			{
				tb_UserClaim userClaim = new tb_UserClaim();
				userClaim.UserClaimRef = c.UserClaimRef;
				userClaim.UserRef = supplierUser.UserRef;
				userClaim.ClaimRef = c.ClaimRef;
				userClaim.ValidFrom = DateTime.Now.AddDays(-1);
				userClaim.ValidTo = DateTime.MaxValue;
				userClaim.EntityState = c.DataEntityState;
				userClaim.MemberStateRef = 1;

				_uow.UserClaim.Enroll(userClaim);
				claims.Add(userClaim);
			});

		    var userGroup = _uow.UserGroup.Single(x => x.Name == "B2BSupplierAllScreen");

		    tb_UserGroupItem item = _uow.UserGroupItem
		        .Find(x => x.UserRef == supplierUser.UserRef && x.UserGroupRef == userGroup.UserGroupRef).FirstOrDefault();

		    if (item == null)
		    {
		        tb_UserGroupItem newItem = new tb_UserGroupItem();
		        newItem.UserRef = supplierUser.UserRef;
		        newItem.UserGroupRef = userGroup.UserGroupRef;
		        newItem.ValidFrom = DateTime.Now.AddDays(-1);
		        newItem.ValidTo = DateTime.Now.AddDays(730);
		        newItem.MemberStateRef = 1;
                _uow.UserGroupItem.Enroll(newItem);
		    }

			_uow.Save();

			claims.ForEach(c =>
			{
				var _claim = supplierUser.Claims.FirstOrDefault(x => x.ClaimRef == c.ClaimRef);
				if (_claim != null)
				{
					_claim.UserClaimRef = c.UserClaimRef;
					_claim.UserRef = c.UserRef;
					_claim.DataEntityState = EntityStates.Unchanged;
				}
			});

			return supplierUser;
		}

		private void Insert(SupplierUser supplierUser)
		{
			tb_User user = new tb_User();
			user.UserRef = supplierUser.UserRef;
			user.UserUid = Guid.NewGuid();
			user.FirstName = supplierUser.FirstName;
			user.SurName = supplierUser.LastName;
			user.UserName = supplierUser.Mail;
			user.IsDomainUser = null;
			user.Domain = null;
			user.PartyRef = supplierUser.PartyRef;
			user.UserTypeRef = 2;
			user.EntityState = EntityStates.Added;

			_uow.User.Enroll(user);
			_uow.Save();

			tb_UserLogon userLogon = new tb_UserLogon();
			userLogon.UserRef = user.UserRef;
			userLogon.LogonStateRef = 1;
			userLogon.IsDomainLogon = false;
			userLogon.UserName = supplierUser.Mail;
			userLogon.PwdStateRef = 1;
			userLogon.InvalidLogonCount = 0;
			userLogon.PwdChangeCount = 0;
			userLogon.ValidFrom = DateTime.Now.AddDays(-1);
			userLogon.ValidTo = DateTime.MaxValue;
			userLogon.EntityState = EntityStates.Added;

			_uow.UserLogon.Enroll(userLogon);
			_uow.Save();

			tb_UserLogonLog userLogonLog = new tb_UserLogonLog();
			userLogonLog.LogTypeRef = 1;
			userLogonLog.UserLogonRef = userLogon.UserLogonRef;
			userLogonLog.UserRef = user.UserRef;
			userLogonLog.IsDomainLogon = false;
			userLogonLog.DomainName = null;
			userLogonLog.UserName = supplierUser.Mail;
			userLogonLog.PwdStateRef = 1;
			userLogonLog.InvalidLogonCount = 0;
			userLogonLog.PwdChangeCount = 0;
			userLogonLog.ValidFrom = DateTime.Now.AddDays(-1);
			userLogonLog.ValidTo = DateTime.MaxValue;
			userLogonLog.EntityState = EntityStates.Added;

			_uow.UserLogonLog.Enroll(userLogonLog);

			tb_UserCompany userCompany = new tb_UserCompany();
			userCompany.UserRef = user.UserRef;
			userCompany.CompanyRef = 1;
			userCompany.MemberStateRef = 1;
			userCompany.IsDefault = true;
			userCompany.ValidFrom = DateTime.Now.AddDays(-1);
			userCompany.ValidTo = DateTime.MaxValue;
			userCompany.EntityState = EntityStates.Added;

			_uow.UserCompany.Enroll(userCompany);

			tb_UserEnvironment userEnvironment = new tb_UserEnvironment();
			userEnvironment.UserRef = user.UserRef;
			userEnvironment.EnvironmentRef = 4;
			userEnvironment.ValidFrom = DateTime.Now.AddDays(-1);
			userEnvironment.ValidTo = DateTime.MaxValue;
			userEnvironment.EntityState = EntityStates.Added;
			userEnvironment.MemberStateRef = 1;

			_uow.UserEnvironment.Enroll(userEnvironment);

			tb_UserMail userMail = new tb_UserMail();
			userMail.UserRef = user.UserRef;
			userMail.MailAddress = supplierUser.Mail;
			userMail.IsActive = true;
			userMail.EntityState = EntityStates.Added;

			_uow.UserMail.Enroll(userMail);

			_uow.Save();

			supplierUser.UserRef = user.UserRef;
		    supplierUser.UserLogonRef = userLogon.UserLogonRef;
		    supplierUser.UserMailRef = userMail.UserMailRef;
		    supplierUser.UserUid = user.UserUid;
		    supplierUser.CreatedUserRef = user.CreatedUserRef;
		    supplierUser.CreatedDate = user.CreatedDate;
		    supplierUser.ModifiedUserRef = user.ModifiedUserRef;
		    supplierUser.ModifiedDate = user.ModifiedDate;

		}

		private void Update(SupplierUser supplierUser)
		{
			if (supplierUser.DataEntityState == EntityStates.Modified)
			{
				tb_User user = (from u in _uow.User.Table
								where u.UserRef == supplierUser.UserRef
								select u).FirstOrDefault();

				if (user != null)
				{
					user.FirstName = supplierUser.FirstName;
					user.SurName = supplierUser.LastName;
					user.EntityState = EntityStates.Modified;
				}

				_uow.User.Enroll(user, false);

				tb_UserLogon userLogon = (from ul in _uow.UserLogon.Table
										  where ul.UserRef == supplierUser.UserRef
										  select ul).FirstOrDefault();

				userLogon.LogonStateRef = supplierUser.IsActive ? (short)1 : (short)3;
				userLogon.EntityState = EntityStates.Modified;

				_uow.UserLogon.Enroll(userLogon, false);

				_uow.Save();
			}
		}

		public bool SupplierUserExist(string mail)
		{
			return _uow.User.Exists(u => u.UserName == mail);
		}

		public bool ValidateUser(int userRef, string supplierCode)
		{
			int? partyRef = (from u in _uow.User.Table
							 where u.UserRef == userRef
							 select u.PartyRef).FirstOrDefault();

			string code = (from s in _uow.Supplier.Table
						   where s.PartyRef == partyRef.Value
						   select s.SupplierCode).FirstOrDefault();

			return code != null && code == supplierCode;
		}
	}
}

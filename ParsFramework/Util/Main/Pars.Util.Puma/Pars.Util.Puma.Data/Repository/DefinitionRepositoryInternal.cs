using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pars.Core;
using Pars.Core.Collection;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Data.Repository
{
    public class DefinitionRepositoryInternal : IDefinitionRepository
    {
        PumaUnitOfWork _unitOfWork;

        public DefinitionRepositoryInternal() : this(Container.Instance.Resolve<IPumaUnitOfWork>())
        {
        }

        public DefinitionRepositoryInternal(IPumaUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork as PumaUnitOfWork;
        }

        public List<EntityState> GetEntityStates()
        {
            List<EntityState> result = (from es in _unitOfWork.EntityState.Table
                                        join esl in _unitOfWork.EntityStateLocale.Table on es.EntityStateRef equals esl.EntityStateRef
                                        select new EntityState { Name = es.Name, EntityStateRef = es.EntityStateRef, Description = esl.Text })
                .ToList();
            return result;
        }

        public List<MemberState> GetMemberStates()
        {
            List<MemberState> result = (from ms in _unitOfWork.MemberState.Table
                                        select new MemberState()
                                        {
                                            MemberStateRef = ms.MemberStateRef,
                                            Name = ms.tb_MemberState_Locale.FirstOrDefault().Text
                                        }
                ).ToList();

            return result;
        }

        public List<LookupItem> GetServers() =>
            (from s in _unitOfWork.Server.Table
             select new LookupItem()
             {
                 Ref = s.ServerRef,
                 Value = s.ServerName
             }).ToList();

        public List<LookupItem> GetDatabases(int serverRef) =>
            (from d in _unitOfWork.Database.Table
             where d.ServerRef == serverRef
             select new LookupItem()
             {
                 Ref = d.DatabaseRef,
                 Value = d.DatabaseName
             }).ToList();

        public List<LookupItem> GetTables(int databaseRef) =>
            (from t in _unitOfWork.Table.Table
             where t.DatabaseRef == databaseRef
             select new LookupItem()
             {
                 Ref = t.TableRef,
                 Value = t.TableName
             }).ToList();

        public List<LookupItem> GetTableColumns(int tableRef) =>
            (from t in _unitOfWork.TableColumn.Table
             where t.TableRef == tableRef
             select new LookupItem()
             {
                 Ref = t.TableColumnRef,
                 Value = t.TableColumnName
             }).ToList();

        public List<LookupItem> GetSegmentsAsLookup()
        {
            try
            {
                return (from t in _unitOfWork.Segment.Table
                        select new LookupItem
                        {
                            Value = t.Name,
                            Ref = t.SegmentRef
                        }).ToList();
            }
            catch (global::System.Exception ex)
            {
                throw ex;
            }
        }


        public List<LookupItem> GetSubSegmentsAsLookup() =>
            (from t in _unitOfWork.SubSegment.Table
             select new LookupItem
             {
                 Value = t.Name,
                 Ref = t.SubSegmentRef
             }).ToList();

        public List<LookupItem> GetBusinessLinesAsLookup() =>
            (from t in _unitOfWork.BusinessLine.Table
             select new LookupItem
             {
                 Value = t.Name,
                 Ref = t.BusinessLineRef
             }).ToList();

        public List<LookupItem> GetAgeSexGroupsAsLookup() =>
            (from t in _unitOfWork.AgeSexGroups.Table
             select new LookupItem
             {
                 Value = t.Name,
                 Ref = t.AgeSexGroupRef
             }).ToList();

        public List<LookupItem> GetSupplierTypesAsLookup() =>
            (from t in _unitOfWork.SupplierType.Table
             select new LookupItem
             {
                 Value = t.Name,
                 Ref = t.SupplierTypeRef
             }).ToList();
    }
}

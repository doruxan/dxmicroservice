using System;

namespace OPLOGMicroservice.Data.Core.Relational.EntityFramework.Entities
{
    public abstract class TenantETagEntity : TenantEntity
    {
        public TenantETagEntity(Guid id, Guid tenantId, DateTime? eTag = null) : base(id, tenantId)
        {
            this.Etag = eTag;
        }

        protected TenantETagEntity() { }

        public DateTime? Etag { get; protected set; }

        protected void UpdateEtag(DateTime? newEtag)
        {
            this.Etag = newEtag;
        }

        protected bool CheckETag(DateTime? newEtag)
        {
            if (Etag == null || newEtag == null)
            {
                return true;
            }

            if (Etag > newEtag)
            {
                return false;
            }

            return true;
        }
    }
}
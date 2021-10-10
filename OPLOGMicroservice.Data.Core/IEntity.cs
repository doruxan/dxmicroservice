using System;

namespace OPLOGMicroservice.Data.Core
{
    public interface IEntity
    {
        DateTimeOffset CreatedDate { get; set; }
        DateTimeOffset? ModifiedDate { get; set; }
        DateTimeOffset? DeletedDate { get; set; }
        bool IsDeleted { get; set; }
    }
}

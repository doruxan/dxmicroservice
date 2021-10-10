using OPLOGMicroservice.Data.Core.NonRelational.EntityFramework;

namespace OPLOGMicroservice.Data.Core.NonRelational.Mongo
{
    public interface IMongoEntity : INonRelationalEntity
    {
        public string Id { get; set; }
    }
}

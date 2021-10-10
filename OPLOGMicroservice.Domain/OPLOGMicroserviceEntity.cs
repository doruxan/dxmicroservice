using FluentValidation;
using OPLOGMicroservice.Data.Core.Relational.EntityFramework.Entities;

namespace OPLOGMicroservice.Domain
{
    public class OPLOGMicroserviceEntity : Entity<string>
    {
        public OPLOGMicroserviceEntity(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }

        public void UpdateOPLOGMicroservice(string name)
        {
            this.Name = name;
        }
    }

    public class OPLOGMicroserviceValidator : AbstractValidator<OPLOGMicroserviceEntity>
    {
        public OPLOGMicroserviceValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}

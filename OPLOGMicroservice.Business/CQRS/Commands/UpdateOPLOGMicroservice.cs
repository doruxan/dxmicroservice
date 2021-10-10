using MediatR;
using OPLOGMicroservice.Model.OPLOGMicroservice.UpdateOPLOGMicroservice;
using System;

namespace OPLOGMicroservice.Business.CQRS.Commands
{
    public class UpdateOPLOGMicroservice : IRequest<UpdateOPLOGMicroserviceResponse>
    {
        public UpdateOPLOGMicroserviceRequest Request { get; set; }
    }
}

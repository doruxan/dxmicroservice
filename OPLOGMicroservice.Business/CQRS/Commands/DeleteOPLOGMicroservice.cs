using MediatR;
using OPLOGMicroservice.Model.OPLOGMicroservice.DeleteOPLOGMicroservice;
using System;

namespace OPLOGMicroservice.Business.CQRS.Commands
{
    public class DeleteOPLOGMicroservice : IRequest<DeleteOPLOGMicroserviceResponse>
    {
        public DeleteOPLOGMicroserviceRequest Request { get; set; }
    }
}

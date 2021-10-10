using OPLOGMicroservice.Business.Core.Interfaces;
using OPLOGMicroservice.Model.OPLOGMicroservice.DeleteOPLOGMicroservice;
using System;

namespace OPLOGMicroservice.Business.CQRS.Commands
{
    public class DeleteOPLOGMicroservice : IAsyncCommand<DeleteOPLOGMicroserviceResponse>
    {
        public DeleteOPLOGMicroserviceRequest Request { get; set; }
        public bool Result { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DeleteOPLOGMicroserviceResponse ReturnValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}

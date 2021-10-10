using OPLOGMicroservice.Business.Core.Interfaces;
using OPLOGMicroservice.Model.OPLOGMicroservice.UpdateOPLOGMicroservice;
using System;

namespace OPLOGMicroservice.Business.CQRS.Commands
{
    public class UpdateOPLOGMicroservice : IAsyncCommand<UpdateOPLOGMicroserviceResponse>
    {
        public UpdateOPLOGMicroserviceRequest Request { get; set; }
        public bool Result { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public UpdateOPLOGMicroserviceResponse ReturnValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}

using System.Collections.Generic;

namespace OPLOGMicroservice.Business.CQRS.Common.DTOs
{
    public class ReferenceNumbersInputDTO
    {
        public IList<string> ReferenceNumbers { get; set; }
    }
}
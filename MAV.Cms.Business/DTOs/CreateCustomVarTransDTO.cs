using System;

namespace MAV.Cms.Business.DTOs
{
    public class CreateCustomVarTransDTO
    {
        public Guid? CustomVarId { get; set; }
        public Guid LanguageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

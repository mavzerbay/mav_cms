using MAV.Cms.Common.BaseModels;
using System;
using System.Collections.Generic;

namespace MAV.Cms.Business.Responses
{
    public class PageTransDropdownResponse : BaseDropdownResponse
    {
        public string Slug { get; set; }
        public Guid LanguageId { get; set; }
    }
}

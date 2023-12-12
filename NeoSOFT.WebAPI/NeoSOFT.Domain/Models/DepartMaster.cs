using System;
using System.Collections.Generic;

namespace NeoSOFT.Domain.Models
{
    public partial class DepartMaster
    {
        public int Id { get; set; }
        public string DepartName { get; set; }
        public bool IsActive { get; set; }
    }
}

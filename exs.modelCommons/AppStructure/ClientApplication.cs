using System;
using System.Collections.Generic;
using System.Text;

namespace exs.modelCommons.AppStructure
{
    public class ClientApplication
    {
        public short Id { get; set; }
        public string Uid { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; } = default!;

        public DateTime LatestUpdate { get; set; }
    }
}

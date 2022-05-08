using System;
using System.Collections.Generic;

namespace DemoNetCoreDb.SQLite
{
    public partial class Address
    {
        public long Row { get; set; }
        public string Id { get; set; } = null!;
        public string Text { get; set; } = null!;

        public virtual Person IdNavigation { get; set; } = null!;
    }
}

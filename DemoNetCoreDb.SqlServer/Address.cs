﻿using System;
using System.Collections.Generic;

namespace DemoNetCoreDb.SqlServer
{
    public partial class Address
    {
        public int Row { get; set; }
        public string Id { get; set; } = null!;
        public string Text { get; set; } = null!;

        public virtual Person IdNavigation { get; set; } = null!;
    }
}

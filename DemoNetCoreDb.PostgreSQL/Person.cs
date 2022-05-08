﻿using System;
using System.Collections.Generic;

namespace DemoNetCoreDb.PostgreSQL
{
    public partial class Person
    {
        public Person()
        {
            Addresses = new HashSet<Address>();
        }

        public int Row { get; set; }
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public DateOnly Birthday { get; set; }
        public string? Remark { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}

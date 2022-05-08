using System;
using System.Collections.Generic;

namespace DemoNetCoreDb.SQLite
{
    public partial class Person
    {
        public Person()
        {
            Addresses = new HashSet<Address>();
        }

        public long Row { get; set; }
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public long Age { get; set; }
        public string Birthday { get; set; } = null!;
        public string? Remark { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}

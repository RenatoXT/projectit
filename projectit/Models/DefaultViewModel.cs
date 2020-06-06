using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectit.Models
{
    public abstract class DefaultViewModel
    {
        public virtual int id { get; set; }
        public virtual DateTime created_at { get; set; }
        public virtual DateTime updated_at { get; set; }
    }
}

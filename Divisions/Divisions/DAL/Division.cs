using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divisions.DAL
{
    class Division
    {
        internal int Id { get; set; }
        internal string Name { get; set; }
        internal int ParentId { get; set; }
    }
}

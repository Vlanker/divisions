using Divisions.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divisions.DAL.View_model
{
    class DivisionsViewModel
    {
        internal List<Division> Divisions { get; private set; }
        internal DivisionsViewModel()
        {
            Divisions = new DivisionRepository().DivisionList();
        }
    }
}

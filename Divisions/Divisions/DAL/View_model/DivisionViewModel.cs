using System;
using Divisions.DAL.Repository;

namespace Divisions.DAL.View_model
{
    class DivisionViewModel
    {
        internal Division Division { get; private set; }
        internal DivisionViewModel(int id)
        {
            Division = new DivisionsViewModel().Divisions.Find(d => d.Id == id);
        }
    }
}

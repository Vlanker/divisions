using Divisions.DAL.Repository;
using System.Collections.Generic;

namespace Divisions.DAL.View_model
{
    class WorkersViewModel
    {
        internal  List<Worker> Workers { get; private set; }
        internal WorkersViewModel(int divisionId)
        {
            Workers = new WorkerRepository().WorkerList(divisionId);
        }
    }
}

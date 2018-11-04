using Divisions.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divisions.DAL.View_model
{
    class WorkerViewModel
    {
        internal Worker Worker { get; private set; }
        internal WorkerViewModel(int workerId)
        {
           // Worker = new WorkerRepository().Worker;
        }
    }
}

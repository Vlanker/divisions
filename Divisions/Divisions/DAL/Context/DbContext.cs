using System.Collections.Generic;
using System.Data;
using System.Linq;
using LocalDbQueriesLibrary;
using Divisions.DAL.View_model;
using System;

namespace Divisions.DAL.Context
{
    class DbContext
    {
        private DivisionSQL divisionToSQL; 
        private List<Division> Divisions { get; set; }
        private List<Worker> Workers { get; set; }

        private static DbContext context = null;

        private DbContext()
        {
            divisionToSQL = DivisionSQL.Connect(Properties.Settings.Default.connString);
            InitializeDivisions();
            
        }

        public static DbContext Context
        {
            get
            {
                if (context == null)
                {
                    context = new DbContext();
                }
                return context;
            }
        }
        
        private void InitializeDivisions()
        {
            if (Divisions == null)
            {
                using (DataSet ds = DivisionSQL.GetDivisions())
                {
                    Divisions = ds.Tables[0].AsEnumerable().Select(dataRow => new Division
                    {
                        Id = dataRow.Field<int>("ID"),
                        Name = dataRow.Field<string>("Name"),
                        ParentId = dataRow.Field<int>("ParentID")
                    }).ToList();
                }
            }
        }

        internal List<Division> GetDivisions()
        {
            return Divisions;
        }
        private void SetWorkers(int divisionId)
        {
            if (Workers != null)
                Workers.Clear();

            using (DataSet ds = WorkerSQL.GetWorkers(divisionId))
            {
                try
                {
                    Workers = ds.Tables[0].AsEnumerable().Select(dataRow => new Worker
                    {
                        Id = dataRow.Field<int>("ID"),
                        DivisionId = dataRow.Field<int>("DepartamentID"),
                        PersNum = dataRow.Field<string>("PersNum"),
                        FullName = dataRow.Field<string>("FullName"),
                        Birthday = dataRow.Field<DateTime>("Birthday"),
                        HiringDay = dataRow.Field<DateTime>("HiringDay"),
                        Salary = dataRow.Field<decimal>("Salary"),
                        ProfArea = dataRow.Field<string>("ProfArea"),
                        Gender = dataRow.Field<string>("Gender")
                    }).ToList();
                }
                catch (Exception ex) { }
            }
        }
        internal List<Worker> GetWorkers(int divisionId)
        {
            SetWorkers(divisionId);
            return Workers;
        }
        internal void Edit(Division division)
        {
            DivisionSQL.Update(division.Id, division.Name);
            Divisions.First(d => d.Id == division.Id).Name = division.Name;
        }
        internal void Edit(Worker worker)
        {
            WorkerSQL.Update(worker.Id, worker.PersNum, worker.FullName, worker.Birthday, worker.HiringDay, worker.Salary, worker.ProfArea, worker.Gender);
            Worker updateWorker = Workers.First(d => d.Id == worker.Id);
            updateWorker.PersNum = worker.PersNum;
            updateWorker.FullName = worker.FullName;
            updateWorker.Birthday = worker.Birthday;
            updateWorker.HiringDay = worker.HiringDay;
            updateWorker.Salary = worker.Salary;
            updateWorker.ProfArea = worker.ProfArea;
            updateWorker.Gender = worker.Gender;
            
        }
        internal void Remove(Division division)
        {
            try
            {
                RemoveWorkers(division.Id);
                DivisionSQL.DeleteByParentId(division.Id);
                DivisionSQL.DeleteById(division.Id);
                Division deleteDivision = Divisions.Find(d => d.Id == division.Id);
                Divisions.Remove(division);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        internal bool Remove(Worker worker)
        {
            WorkerSQL.DeleteById(worker.Id);
            return Workers.Remove(worker);
        }
        internal void RemoveWorkers(int divisionId)
        {
            try
            {
                WorkerSQL.DeleteByDivisiontId(divisionId);
                Workers.RemoveAll(w => w.DivisionId == divisionId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }
        internal void Add(string name, int parentId)
        {
            int id = DivisionSQL.Add(name, parentId);
            Divisions.Add(new Division { Id = id, Name = name, ParentId = parentId});
        }
        internal void Add(int divisionId, string persNum, string fullName, string birthday, string hiringDay, decimal salary, string profArea, string gender)
        {
            int id = WorkerSQL.Add(divisionId, persNum, fullName, birthday, hiringDay, salary, profArea, gender);
            Workers.Add(new Worker { Id = id, DivisionId = divisionId, PersNum = persNum, FullName = fullName, Birthday = Convert.ToDateTime(birthday), HiringDay = Convert.ToDateTime(hiringDay), Salary = salary,  ProfArea = profArea, Gender = gender});
        }
    }
}

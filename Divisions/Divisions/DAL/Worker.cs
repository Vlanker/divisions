using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divisions.DAL
{
    class Worker
    {
        //если internal - не добавляется в DataGridView
        [DisplayName("ID")]
        internal int Id { get; set; }
        [DisplayName("DivisionId")]
        internal int DivisionId { get; set; }
        [DisplayName("ТН")]
        public string PersNum { get; set; }
        [DisplayName("ФИО")]
        public string FullName { get; set; }
        [DisplayName("Дата рождения")]
        public DateTime Birthday { get; set; }
        [DisplayName("Дата приёма")]
        public DateTime HiringDay { get; set; }
        [DisplayName("Зарплата")]
        public decimal Salary { get; set; }
        [DisplayName("Профобласть")]
        public string ProfArea { get; set; }
        [DisplayName("Пол")]
        public string Gender { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova.Classes
{
    public class Sales
    {
        private int id;
        private string carvin;
        private int carid;
        private string clientname;
        private long clientRNOCPP;
        private int clientid;
        private string employeename;
        private long employeeRNOCPP;
        private int employeeid;
        private string date;
        private long price;

        public Sales(int id, string carvin, int carid, string clientname, long clientRNOCPP, int clientid, string employeename, long employeeRNOCPP, int employeeid, string date, long price)
        {
            Id = id;
            Carvin = carvin;
            Carid = carid;
            Clientname = clientname;
            ClientRNOCPP = clientRNOCPP;
            Clientid = clientid;
            Employeename = employeename;
            EmployeeRNOCPP = employeeRNOCPP;
            Employeeid = employeeid;
            Date = date;
            Price = price;
        }

        public int Id { get => id; set => id = value; }
        public string Carvin { get => carvin; set => carvin = value; }
        public int Carid { get => carid; set => carid = value; }
        public string Clientname { get => clientname; set => clientname = value; }
        public long ClientRNOCPP { get => clientRNOCPP; set => clientRNOCPP = value; }
        public int Clientid { get => clientid; set => clientid = value; }
        public string Employeename { get => employeename; set => employeename = value; }
        public long EmployeeRNOCPP { get => employeeRNOCPP; set => employeeRNOCPP = value; }
        public int Employeeid { get => employeeid; set => employeeid = value; }
        public string Date { get => date; set => date = value; }
        public long Price { get => price; set => price = value; }
    }
}

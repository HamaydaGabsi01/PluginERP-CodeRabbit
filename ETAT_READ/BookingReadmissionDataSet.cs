using System;
using System.Data;

namespace ETAT_READ
{
    public class BookingReadmissionDataSet : DataSet
    {
        public BookingReadmissionDataSet()
        {
            // Create the main table
            DataTable readmissionTable = new DataTable("BookingReadmission");
            
            // Add columns
            readmissionTable.Columns.Add("Id", typeof(int));
            readmissionTable.Columns.Add("OldReservation", typeof(int));
            readmissionTable.Columns.Add("Guest", typeof(int));
            readmissionTable.Columns.Add("Room", typeof(string));
            readmissionTable.Columns.Add("Room2", typeof(string));
            readmissionTable.Columns.Add("NewReservation", typeof(int));
            readmissionTable.Columns.Add("Note", typeof(string));
            readmissionTable.Columns.Add("ApplicationId", typeof(string));
            readmissionTable.Columns.Add("FirstName", typeof(string));
            readmissionTable.Columns.Add("LastName", typeof(string));
            readmissionTable.Columns.Add("CheckInscription", typeof(string));
            readmissionTable.Columns.Add("ContactValue", typeof(string));
            readmissionTable.Columns.Add("CheckBudget", typeof(string));
            readmissionTable.Columns.Add("State", typeof(string));
            readmissionTable.Columns.Add("BeginDate", typeof(DateTime));
            readmissionTable.Columns.Add("EndDate", typeof(DateTime));
            readmissionTable.Columns.Add("Counteur", typeof(decimal));
            readmissionTable.Columns.Add("DD", typeof(DateTime));
            readmissionTable.Columns.Add("PDD", typeof(bool));
            readmissionTable.Columns.Add("Impaye", typeof(decimal));
            readmissionTable.Columns.Add("ProfileId", typeof(string));
            readmissionTable.Columns.Add("Domain", typeof(string));

            // Add the table to the dataset
            Tables.Add(readmissionTable);
        }
    }
} 
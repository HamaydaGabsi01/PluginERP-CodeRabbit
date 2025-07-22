using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AtooERP.Type_document;
namespace SAM_DOC
{
    public class Document_information
    {

        #region Attributs
        public int reservationId { get; set; } 
        public string applicationId { get; set; } 
        public string profileId { get; set; } 
        public string firstName { get; set; } 
        public string lastName { get; set; }
        public string room { get; set; } 
        public DateTime? beginDate { get; set; } 
        public DateTime? endDate { get; set; }
        public string type { get; set; } 
        public string contact { get; set; } 
        public string state { get; set; } 
        public DateTime? checkInDate { get; set; }
        public DateTime? checkOutDate { get; set; }
        public string name { get; set; }
        public DateTime create_date { get; set; }
        public string extension { get; set; }
        public int type_document { get; set; }
        public string type_documentName { get; set; }
        public byte[] content { 
            get {
                try
                {
                    return File.ReadAllBytes(this.path);
                }
                catch { return null; }
            } 
            set { } 
        }
        public string path { get; set; }
        public string fileName { get; set; }
        public Int64 size
        {
            get
            {
                try { return Convert.ToInt64(new FileInfo(path).Length); }
                catch { return 0; }

            }
            set { }
        }


        #endregion


        #region Constructor

        public Document_information() { }
        public Document_information(int reservationId, string applicationId, string profileId, string firstName, string lastName, string room, DateTime? beginDate, DateTime? endDate, string type, string contact, string state, DateTime? checkInDate, DateTime? checkOutDate, int typeDocument,string typeDocumentName, string path)
        {
            
            this.reservationId = reservationId;
            this.applicationId = applicationId;
            this.profileId = profileId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.room = room;
            this.beginDate = beginDate;
            this.endDate = endDate;
            this.type = type;
            this.contact = contact;
            this.state = state;
            this.checkInDate = checkInDate;
            this.checkOutDate = checkOutDate;
            this.fileName = Path.GetFileName(path);
            this.extension = Path.GetExtension(path);
            this.name = typeDocumentName;
            this.create_date = File.GetCreationTime(path);
            this.type_document = typeDocument;
            this.type_documentName = typeDocumentName;
            this.path = path;
        }

        public Document_information(string fileName, int reservationId, string applicationId, string profileId, string firstName, string lastName, string room, DateTime? beginDate, DateTime? endDate, string type, string contact, string state, DateTime? checkInDate, DateTime? checkOutDate,  string path)
        {
            this.name = fileName;
            this.reservationId = reservationId;
            this.applicationId = applicationId;
            this.profileId = profileId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.room = room;
            this.beginDate = beginDate;
            this.endDate = endDate;
            this.type = type;
            this.contact = contact;
            this.state = state;
            this.checkInDate = checkInDate;
            this.checkOutDate = checkOutDate;
            this.fileName = Path.GetFileName(path);
            this.extension = Path.GetExtension(path);
            this.create_date = File.GetCreationTime(path);
            this.path = path;
        }

        public Document_information(string fileName, string extension,DateTime create_date, string applicationId,int typeDocument, string typeDocumentName, string path)
        {
            this.fileName = fileName;
            this.name = fileName;
            this.extension = extension;
            this.create_date = create_date;
            this.applicationId = applicationId;
            this.fileName = Path.GetFileName(path);
            this.extension = Path.GetExtension(path);
            this.name = typeDocumentName;
            this.create_date = File.GetCreationTime(path);
            this.type_document = typeDocument;
            this.type_documentName = typeDocumentName;
            this.path = path;
        }
        #endregion


        public static BindingList<Document_information> LoadDocumentsInfo(string documentsFolder, Dictionary<string, Type_document> documentTypes, string person = "")
        {
            try
            {
                BindingList<Document_information> documentsInfo = new BindingList<Document_information>();
                DataSetSAM_APITableAdapters.booking_reservation_infoTableAdapter TA = new DataSetSAM_APITableAdapters.booking_reservation_infoTableAdapter();

                DataView dv = TA.GetData().DefaultView;

                List<string> personFolderList = new List<string>();
                if (string.IsNullOrEmpty(person))
                {
                    // If person is empty, load all folders like in original code
                    foreach (var personFolder in Directory.GetDirectories(documentsFolder))
                    {
                        personFolderList.Add(Path.GetFileName(personFolder));
                    }
                }
                else
                {
                    // If person has a value, only load that specific folder
                    string personFolderPath = Path.Combine(documentsFolder, person);
                    if (Directory.Exists(personFolderPath))
                    {
                        personFolderList.Add(person);
                    }
                }
                personFolderList = personFolderList.OrderByDescending(item => item).ToList();
                int dv_index = 0;
                foreach (var personFolder in personFolderList)
                {
                    while (personFolder != dv[dv_index]["application_id"].ToString() && dv_index < dv.Count - 1)
                    {
                        dv_index++;
                    }
                    if (dv_index < dv.Count - 1)
                    {
                        foreach (var file in Directory.GetFiles(documentsFolder + "/" + personFolder))
                        {
                            string fileName = Path.GetFileName(file);
                            string fileNumber = Path.GetFileNameWithoutExtension(fileName);
                            string extension = Path.GetExtension(file);
                            documentTypes.TryGetValue(fileNumber, out Type_document documentType);
                            

                            DateTime? checkInDate = null;
                            if (dv[dv_index]["checkIn"] != DBNull.Value)
                                checkInDate = Convert.ToDateTime(dv[dv_index]["checkIn"]);

                            DateTime? checkOutDate = null;
                            if (dv[dv_index]["checkOut"] != DBNull.Value)
                                checkOutDate = Convert.ToDateTime(dv[dv_index]["checkOut"]);
                            if (documentType == null)
                            {
                                documentsInfo.Add(new Document_information(
                                fileNumber,
                                Convert.ToInt32(dv[dv_index]["Id"]),
                                dv[dv_index]["application_id"].ToString(),
                                dv[dv_index]["profile_id"] != DBNull.Value ? Convert.ToString(dv[dv_index]["profile_id"]) : null,
                                dv[dv_index]["first_name"].ToString(),
                                dv[dv_index]["last_name"].ToString(),
                                dv[dv_index]["room"] != DBNull.Value ? Convert.ToString(dv[dv_index]["room"]) : null,
                                Convert.ToDateTime(dv[dv_index]["begin_date"]),
                                Convert.ToDateTime(dv[dv_index]["end_date"]),
                                dv[dv_index]["Type"].ToString(),
                                dv[dv_index]["Contact"] != DBNull.Value ? Convert.ToString(dv[dv_index]["Contact"]) : null,
                                dv[dv_index]["state"].ToString(),
                                checkInDate,
                                checkOutDate,
                                file
                            ));
                                continue;

                            }

                            documentsInfo.Add(new Document_information(
                                Convert.ToInt32(dv[dv_index]["Id"]),
                                dv[dv_index]["application_id"].ToString(),
                                dv[dv_index]["profile_id"] != DBNull.Value ? Convert.ToString(dv[dv_index]["profile_id"]) : null,
                                dv[dv_index]["first_name"].ToString(),
                                dv[dv_index]["last_name"].ToString(),
                                dv[dv_index]["room"] != DBNull.Value ? Convert.ToString(dv[dv_index]["room"]) : null,
                                Convert.ToDateTime(dv[dv_index]["begin_date"]),
                                Convert.ToDateTime(dv[dv_index]["end_date"]),
                                dv[dv_index]["Type"].ToString(),
                                dv[dv_index]["Contact"] != DBNull.Value ? Convert.ToString(dv[dv_index]["Contact"]) : null,
                                dv[dv_index]["state"].ToString(),
                                checkInDate,
                                checkOutDate,
                                documentType.Id,
                                documentType.name,
                                file
                            ));

                        }
                    }
                    else
                    {
                        foreach (var file in Directory.GetFiles(documentsFolder + "/" + personFolder))
                        {
                            string fileName = Path.GetFileName(file);
                            string fileNumber = Path.GetFileNameWithoutExtension(fileName);
                            string extension = Path.GetExtension(file);
                            documentTypes.TryGetValue(fileNumber, out Type_document documentType);
                            if (documentType == null)
                            {
                                documentsInfo.Add(new Document_information
                                {
                                    name = fileNumber,
                                    path = file,
                                    extension = extension,
                                    create_date = File.GetCreationTime(file)
                                }) ;

                                continue;

                            }

                            documentsInfo.Add(new Document_information(
                                fileNumber,
                                extension,
                                File.GetCreationTime(file),
                                personFolder,
                            documentType.Id,
                            documentType.name,
                            file
                        ));
                        }
                        dv_index = 0;
                    }
                    
                }
                return documentsInfo;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}

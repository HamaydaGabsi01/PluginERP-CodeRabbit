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
namespace ETAT_READ
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
                SAM_DOC.DataSetSAM_APITableAdapters.booking_reservation_infoTableAdapter TA = new SAM_DOC.DataSetSAM_APITableAdapters.booking_reservation_infoTableAdapter();

                DataView dv = TA.GetData().DefaultView;
                
                // Get the archive folder path from configuration
                var (_, archiveFolder, _) = ETAT_READ_Form.LoadConfig();
                List<string> personFolderList = new List<string>();

                if (string.IsNullOrEmpty(person))
                {
                    // If person is empty, extract all ZIP files
                    ArchiveHelper.ExtractArchiveClean(documentsFolder, documentsFolder);
                    // Load folders that match the required format (number_number or number_text)
                    foreach (var personFolder in Directory.GetDirectories(documentsFolder))
                    {
                        string folderName = Path.GetFileName(personFolder);
                        // Check if folder name matches pattern number_number (e.g. 032_5544) or number_text (e.g. 421_xyz)
                        if (System.Text.RegularExpressions.Regex.IsMatch(folderName, @"^\d+_\d+$") || 
                            System.Text.RegularExpressions.Regex.IsMatch(folderName, @"^\d+_[a-zA-Z]+$"))
                        {
                            personFolderList.Add(folderName);
                        }
                    }
                }
                else
                {
                    // If person has a value, first try to find and extract a matching ZIP file
                    bool foundMatch = false;
                    foreach (var zipFile in Directory.GetFiles(documentsFolder, "*.zip"))
                    {
                        string zipName = Path.GetFileNameWithoutExtension(zipFile);
                        if (ETAT_READ_Form.IsPersonFolderContainsApplicationId(zipName, person))
                        {
                            // Extract only this specific ZIP file
                            string extractedPath = ArchiveHelper.ExtractSingleArchive(zipFile, documentsFolder, true);
                            if (extractedPath != null)
                            {
                                personFolderList.Add(Path.GetFileName(extractedPath));
                                foundMatch = true;
                            }
                            break;
                        }
                    }

                    // If no matching ZIP file was found, look for a matching folder
                    if (!foundMatch)
                    {
                        foreach (var personFolder in Directory.GetDirectories(documentsFolder))
                        {
                            string folderName = Path.GetFileName(personFolder);
                                if (ETAT_READ_Form.IsPersonFolderContainsApplicationId(folderName, person))
                                {
                                    personFolderList.Add(folderName);
                                    break;
                                }
                        }
                    }
                }
                personFolderList = personFolderList.OrderByDescending(item => item).ToList();
                int dv_index = 0;
                foreach (var personFolder in personFolderList)
                {
                    while ( dv_index < dv.Count - 1 && (string.IsNullOrEmpty(person) || person != dv[dv_index]["application_id"].ToString()) && !ETAT_READ_Form.IsPersonFolderContainsApplicationId(personFolder, dv[dv_index]["application_id"].ToString()))
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
                            
                            // Check if TypeMapping uses 'LIKE' matching
                            // First try exact match
                            documentTypes.TryGetValue(fileNumber, out Type_document documentType);
                            
                            // If no exact match found, check for LIKE matches if applicable
                            if (documentType == null)
                            {
                                // Get TypeMapping list to check useLike attribute
                                var typeMappings = TypeMapping.getList();
                                
                                // Find mappings with useLike attribute set to true
                                var likeMapping = typeMappings.FirstOrDefault(m => 
                                    m.useLike && 
                                    fileNumber.ToUpperInvariant().Contains(m.FileName.ToUpperInvariant()));
                                
                                if (likeMapping != null)
                                {
                                    // Get the Type_document for this mapping
                                    documentTypes.TryGetValue(likeMapping.FileName, out documentType);
                                }
                            }
                            

                            DateTime? checkInDate = null;
                            if (dv[dv_index]["checkIn"] != DBNull.Value)
                                checkInDate = Convert.ToDateTime(dv[dv_index]["checkIn"]);

                            DateTime? checkOutDate = null;
                            if (dv[dv_index]["checkOut"] != DBNull.Value)
                                checkOutDate = Convert.ToDateTime(dv[dv_index]["checkOut"]);

                            int reservationId = Convert.ToInt32(dv[dv_index]["Id"]);
                            
                            // Check if document is already imported (check = false means not imported)
                            bool isAlreadyImported = false;
                            if (documentType != null && reservationId > 0)
                            {
                                var existingDocuments = AtooERP.Document.Document.getListByPiece_typeAndPiece("Booking.Reservation, Booking, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", reservationId);
                                isAlreadyImported = existingDocuments.Any(doc => doc.type_document == documentType.Id && doc.check == true);
                            }

                            // If document is already imported, move it to rejected folder
                            if (isAlreadyImported)
                            {
                                try
                                {
                                    if (!string.IsNullOrEmpty(archiveFolder) && File.Exists(file))
                                    {
                                        ArchiveHelper.MoveFileToAcceptRejectFolder(file, archiveFolder, false); // false = rejected
                                    }
                                }
                                catch (Exception moveEx)
                                {
                                    // Log the error but continue processing other files
                                    System.Diagnostics.Debug.WriteLine($"Error moving file {file} to rejected folder: {moveEx.Message}");
                                }
                                continue; // Skip adding this document to the list
                            }

                            // Add documents that are not already imported to the list
                            if (documentType == null)
                            {
                                documentsInfo.Add(new Document_information(
                                fileNumber,
                                reservationId,
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
                                reservationId,
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
                            
                            // Check if TypeMapping uses 'LIKE' matching
                            // First try exact match
                            documentTypes.TryGetValue(fileNumber, out Type_document documentType);
                            
                            // If no exact match found, check for LIKE matches if applicable
                            if (documentType == null)
                            {
                                // Get TypeMapping list to check useLike attribute
                                var typeMappings = TypeMapping.getList();
                                
                                // Find mappings with useLike attribute set to true
                                var likeMapping = typeMappings.FirstOrDefault(m => 
                                    m.useLike && 
                                    fileNumber.ToUpperInvariant().Contains(m.FileName.ToUpperInvariant()));
                                
                                if (likeMapping != null)
                                {
                                    // Get the Type_document for this mapping
                                    documentTypes.TryGetValue(likeMapping.FileName, out documentType);
                                }
                            }
                            
                            // For documents without reservation context, we can't check if they're already imported
                            // So we add them all (this branch is for documents not linked to a specific reservation)
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


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using AtooERP_Booking.Reservation;
using static Booking.Reservation;
using System.Xml.Linq;
using System.Threading.Tasks;
using AtooERP.Document;
using AtooERP.Type_document;
using System.Collections;
using DevExpress.XtraReports.UI;

namespace ETAT_READ
{
    public partial class ETAT_READ_Form : AtooERP.deskTopModel.deskTopModelForm.deskTopModelFormState
    {
        private string filesPath = new Xml_location().folder_out;
        private Dictionary<string, bool> _fileExistenceCache = new Dictionary<string, bool>();
        private Dictionary<string, (DateTime start, DateTime end, string filePath)> _stayDatesCache = new Dictionary<string, (DateTime start, DateTime end, string filePath)>();
        private Dictionary<string, bool> _documentCountCache = new Dictionary<string, bool>();
        private bool _isInitialized = false;
        private SafeSplashScreenManager _safeSplashManager;

        public ETAT_READ_Form()
        {
            InitializeComponent();
            
            // Initialize the safe splash screen manager
            _safeSplashManager = new SafeSplashScreenManager(splashScreenManager2);
            
            // Get the data
            bookingReadmissionDataSet1 = new BookingReadmissionTableAdapter().GetData(25);
            SAM_DOC.Properties.Settings.Default["atooerpConnectionString"] = Properties.Settings.Default["atooerpConnectionString"] = Properties.Settings.Default["atooerpConnectionString1"] = AtooERP.Network_setting.getConnectionString().Replace(";connectiontimeout=20000;connectionlifetime=20000;defaultcommandtimeout=20000;persistsecurityinfo=True", string.Empty);
            

            this.titre.Text = "Etat de Réadmission";

            // Set up the data binding
            gridControl1.DataSource = bookingReadmissionDataSet1;
            gridControl1.DataMember = "BookingReadmission";
            
            // Pre-cache file existence for all profile IDs
            PreCacheFileExistence();
            
            // Pre-cache document counts
            PreCacheDocumentCounts();
            
            // Set initial sort order for colstatus
            gridView1.Columns["colstatus"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            
            // Refresh the grid after data binding is set up
            gridControl1.RefreshDataSource();
            gridView1.RefreshData();

            // Make sure data is recalculated when filter changes
            gridView1.ColumnFilterChanged += (s, e) => gridView1.RefreshData();
            
            // Configure grid behavior for better button responsiveness
            gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView1.OptionsBehavior.AllowIncrementalSearch = false;
            
            // Add CellClick event handler for immediate button response
            
            // Add DoubleClick event handler for opening reservations
            gridView1.DoubleClick += gridView1_DoubleClick;
        }

        private void gridView1_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {
            if (e.Column == colstatus)
            {
                string value1 = e.Value1?.ToString() ?? "";
                string value2 = e.Value2?.ToString() ?? "";

                // Define the order of status values
                Dictionary<string, int> statusOrder = new Dictionary<string, int>
                {
                    { "Prêt à importer", 1 },
                    { "Déjà importé", 2 },
                    { "Non déposé", 3 }
                };

                // Get the order for each value (default to highest if not found)
                int order1 = statusOrder.ContainsKey(value1) ? statusOrder[value1] : 999;
                int order2 = statusOrder.ContainsKey(value2) ? statusOrder[value2] : 999;

                // Compare the orders
                e.Result = order1.CompareTo(order2);
                e.Handled = true;
            }
        }

        public static (string documentsFolder, string archiveFolder, Dictionary<string, Type_document> documentTypes) LoadConfig()
        {
            XDocument doc;
            var documentTypes = new Dictionary<string, Type_document>();
            try
            {
                doc = XDocument.Load(Program.configPath);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n Vérifiez que le fichier de configuration existe dans " + Program.configPath);
                return ("","", documentTypes);
            }
            string documentsFolder = doc.Root.Element("DocumentsFolder").Value;
            string archiveFolder = doc.Root.Element("ArchiveFolder").Value;

            try
            {
                documentTypes = doc.Root.Element("DocumentTypes")
                    .Elements("Type")
                    .Select(type => (type.Element("FileName").Value, new Type_document(Convert.ToInt32(type.Element("TypeId").Value))))
                    .ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n Vérifiez la configuration des types de documents!");
                return (documentsFolder, archiveFolder, documentTypes);
            }


            return (documentsFolder, archiveFolder, documentTypes);
        }

        private void PreCacheFileExistence()
        {
            if (_isInitialized) return;

            try
            {
                // Get all XML files in the directory
                string[] files = Directory.GetFiles(filesPath, "*.xml");
                
                // Get the season begin date for season ID 25
                Booking.Season season = new Booking.Season(25);
                DateTime seasonBeginDate = season.begin_date;
                
                // Create a lookup of all profile IDs that have matching files and meet the date requirement
                var existingProfileIds = new HashSet<string>();
                foreach (string file in files)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    string[] parts = fileName.Split('_');
                    if (parts.Length > 0)
                    {
                        // Check deposit date from filename (format: DD-MM-YYYY)
                        if (parts.Length >= 3 && DateTime.TryParseExact(parts[2], "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime depositDate))
                        {
                            // Only proceed if deposit date is after May 1st, 2025
                            if (depositDate >= new DateTime(2025, 5, 1))
                            {
                                string profileId = parts[parts.Length - 1];
                                if (!string.IsNullOrEmpty(profileId))
                                {
                                    try
                                    {
                                        XDocument doc = XDocument.Load(file);
                                        var stayEndElement = doc.Root.Element("stay")?.Element("end");
                                        if (stayEndElement != null)
                                        {
                                            DateTime stayEndDate = DateTime.Parse(stayEndElement.Value);
                                            if (stayEndDate >= seasonBeginDate)
                                            {
                                                existingProfileIds.Add(profileId);
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        // If there's any error parsing the XML, skip this file
                                        continue;
                                    }
                                }
                            }
                        }
                    }
                }

                // Cache the results for all profile IDs in the dataset
                foreach (DataRow row in bookingReadmissionDataSet1.Tables["BookingReadmission"].Rows)
                {
                    string profileId = row["ProfileId"]?.ToString();
                    if (!string.IsNullOrEmpty(profileId))
                    {
                        bool hasFile = existingProfileIds.Contains(profileId);
                        _fileExistenceCache[profileId] = hasFile;

                        // If file exists, cache the stay dates
                        if (hasFile)
                        {
                            string matchingFile = files.FirstOrDefault(file => 
                            {
                                string fileName = Path.GetFileNameWithoutExtension(file);
                                string[] parts = fileName.Split('_');

                                return parts.Length > 0 && parts[parts.Length - 1] == profileId && DateTime.TryParseExact(parts[2], "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime depositDate) && depositDate >= new DateTime(2025, 5, 1);
                            });

                            if (matchingFile != null)
                            {
                                try
                                {
                                    XDocument doc = XDocument.Load(matchingFile);
                                    var stayElement = doc.Root.Element("stay");
                                    if (stayElement != null)
                                    {
                                        var startElement = stayElement.Element("start");
                                        var endElement = stayElement.Element("end");
                                        if (startElement != null && endElement != null)
                                        {
                                            DateTime startDate = DateTime.Parse(startElement.Value);
                                            DateTime endDate = DateTime.Parse(endElement.Value);
                                            _stayDatesCache[profileId] = (startDate, endDate, matchingFile);
                                        }
                                    }
                                }
                                catch
                                {
                                    // If there's any error parsing the XML, just continue
                                }
                            }
                        }
                    }
                }

                _isInitialized = true;
            }
            catch (Exception ex)
            {
                // If there's an error, we'll fall back to checking files individually
                _fileExistenceCache.Clear();
                _stayDatesCache.Clear();
            }
        }

        private void PreCacheDocumentCounts()
        {
            try
            {
                var (documentsFolder, _, documentTypes) = LoadConfig();
                if (!Directory.Exists(documentsFolder))
                {
                    return;
                }

                // Get all application IDs from the dataset
                foreach (DataRow row in bookingReadmissionDataSet1.Tables["BookingReadmission"].Rows)
                {
                    string applicationId = row["ApplicationId"]?.ToString();
                    if (!string.IsNullOrEmpty(applicationId))
                    {
                        try
                        {
                            // Check if a folder exists for this application ID
                            bool filesExist = false;
                            
                            // Check for folders with matching application ID
                            foreach (var personFolder in Directory.GetDirectories(documentsFolder))
                            {
                                string folderName = Path.GetFileName(personFolder);
                                // Sanitize the folder name to handle special characters
                                string sanitizedFolderName = ArchiveHelper.SanitizeFilePath(folderName);

                                    if (IsPersonFolderContainsApplicationId(sanitizedFolderName, applicationId))
                                    {
                                        filesExist = true;
                                        break;
                                    }
                            }
                            
                            // If no folder found, check for zip files
                            if (!filesExist)
                            {
                                foreach (var zipFile in Directory.GetFiles(documentsFolder, "*.zip"))
                                {
                                    string zipName = Path.GetFileNameWithoutExtension(zipFile);
                                    // Sanitize the zip name to handle special characters
                                    string sanitizedZipName = ArchiveHelper.SanitizeFilePath(zipName);
                                    if (IsPersonFolderContainsApplicationId(sanitizedZipName, applicationId))
                                    {
                                        filesExist = true;
                                        break;
                                    }
                                }
                            }
                            
                            _documentCountCache[applicationId] = filesExist;
                        }
                        catch
                        {
                            _documentCountCache[applicationId] = false; // Error flag changed to false
                        }
                    }
                }
            }
            catch
            {
                // If there's an error, we'll clear the cache
                _documentCountCache.Clear();
            }
        }

        // Helper method to check if a folder name contains the application ID
        static public bool IsPersonFolderContainsApplicationId(string folderName, string applicationId)
        {
            // Input validation
            if (string.IsNullOrEmpty(folderName) || string.IsNullOrEmpty(applicationId))
            {
                return false;
            }

            // Sanitize the folder name to handle special characters
            folderName = ArchiveHelper.SanitizeFilePath(folderName);

            // Split both strings into parts
            string[] folderParts = folderName.Split('_');
            string[] applicationParts = applicationId.Split('_');

            // If either string doesn't have at least 2 parts, return false
            if (folderParts.Length < 2 || applicationParts.Length < 2)
            {
                return false;
            }
            
            // Get the numeric ID from the application ID (second part)
            string applicationNumericId = applicationParts[1];
            return folderParts.Any(part => part == applicationNumericId);
        }

        private void gridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            try
            {
                if (e.Column == colstatus)
                {
                    // Handle displaying data for visible rows
                    if (e.IsGetData)
                    {

                        // Use ListSourceRowIndex for unbound data, but get the correct DataRow
                        int dataSourceIndex = e.ListSourceRowIndex;
                        if (dataSourceIndex >= 0)
                        {
                            DataRowView rowView = e.Row as DataRowView;
                            DataRow row = rowView.Row;
                            if (row != null)
                            {
                                e.Value = "no val";
                                // Check if already imported
                                if (row["NewReservation"] != DBNull.Value)
                                {
                                    e.Value = "Déjà importé";
                                    return;
                                }

                                // Check if XML file exists and meets conditions
                                string profileId = row["ProfileId"]?.ToString();
                                if (!string.IsNullOrEmpty(profileId))
                                {
                                    // Try to get dates from cache
                                    if (_stayDatesCache.TryGetValue(profileId, out var dates))
                                    {
                                        e.Value = $"Prêt à importer";
                                        return;
                                    }
                                }
                            }

                            // Default case - no XML file found
                            e.Value = "Non déposé";
                        }
                    }
                }
                else if (e.Column == coldocs && e.IsGetData)
                {
                    int dataSourceIndex = e.ListSourceRowIndex;
                    if (dataSourceIndex >= 0)
                    {
                        DataRowView rowView = e.Row as DataRowView;
                        DataRow row = rowView.Row;
                        if (row != null)
                        {
                            string applicationId = row["ApplicationId"]?.ToString();
                            if (!string.IsNullOrEmpty(applicationId))
                            {
                                // Check if we have cached document count for this application ID
                                if (_documentCountCache.TryGetValue(applicationId, out bool filesExist))
                                {
                                    if (!filesExist)
                                    {
                                        e.Value = "Aucun document";
                                    }
                                    else
                                    {
                                        e.Value = "Docs disponibles";
                                    }
                                }
                                else
                                {
                                    // If not cached, check documents on demand
                                    var (documentsFolder, _, documentTypes) = LoadConfig();
                                    if (Directory.Exists(documentsFolder))
                                    {
                                        try
                                        {
                                            // Check if a folder exists for this application ID
                                            bool hasFiles = false;
                                            
                                            // Check for folders with matching application ID
                                            foreach (var personFolder in Directory.GetDirectories(documentsFolder))
                                            {
                                                string folderName = Path.GetFileName(personFolder);
                                                // Sanitize the folder name to handle special characters
                                                string sanitizedFolderName = ArchiveHelper.SanitizeFilePath(folderName);
                                                 
                                                    if (IsPersonFolderContainsApplicationId(sanitizedFolderName, applicationId))
                                                    {
                                                        hasFiles = true;
                                                        break;
                                                    }
                                                
                                            }
                                            
                                            // If no folder found, check for zip files
                                            if (!hasFiles)
                                            {
                                                foreach (var zipFile in Directory.GetFiles(documentsFolder, "*.zip"))
                                                {
                                                    string zipName = Path.GetFileNameWithoutExtension(zipFile);
                                                    // Sanitize the zip name to handle special characters
                                                    string sanitizedZipName = ArchiveHelper.SanitizeFilePath(zipName);
                                                    if (IsPersonFolderContainsApplicationId(sanitizedZipName, applicationId))
                                                    {
                                                        hasFiles = true;
                                                        break;
                                                    }
                                                }
                                            }
                                            
                                            _documentCountCache[applicationId] = hasFiles;
                                            e.Value = hasFiles ? "Docs disponibles" : "Aucun document";
                                        }
                                        catch
                                        {
                                            _documentCountCache[applicationId] = false; // Cache error
                                            e.Value = "Erreur";
                                        }
                                    }
                                    else
                                    {
                                        e.Value = "Dossier introuvable";
                                    }
                                }
                            }
                            else
                            {
                                e.Value = "";
                            }
                        }
                    }
                }

            }
            catch(Exception ex)
            {

            }
            
        }

        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column == colget_files)
            {
                // Get the current row's data
                DataRow row = gridView1.GetDataRow(e.RowHandle);
                if (row != null)
                {
                    string profileId = row["ProfileId"]?.ToString();
                    bool hasFile = false;

                    if (!string.IsNullOrEmpty(profileId))
                    {
                        hasFile = _stayDatesCache.TryGetValue(profileId, out var dates);
                    }

                    // Always enable the button - let the HandleButtonAction method handle the logic
                    repositoryItemButtonEdit1.Buttons[0].Enabled = true;
                }
            }
        }

        

        private void repositoryItemButtonEdit1_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            HandleButtonAction();
        }

        // Add Click event handler for single click support
        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            HandleButtonAction();
        }

        private void HandleButtonAction()
        {
            // Show wait form at the beginning of the import process
            _safeSplashManager.ShowWaitForm();
            _safeSplashManager.SetWaitFormCaption("Importation en cours...");
            _safeSplashManager.SetWaitFormDescription("Veuillez patienter pendant le traitement des données.");

            try
            {
                // Get the current row's data with filter-safe approach
                int focusedRow = gridView1.FocusedRowHandle;
                if (focusedRow < 0) 
                {
                    _safeSplashManager.CloseWaitForm();
                    return;
                }
                
                int dataSourceIndex = gridView1.GetDataSourceRowIndex(focusedRow);
                if (dataSourceIndex < 0) 
                {
                    _safeSplashManager.CloseWaitForm();
                    return;
                }
                
                DataRow row = bookingReadmissionDataSet1.Tables["BookingReadmission"].Rows[dataSourceIndex];

                string profileId = row["ProfileId"]?.ToString();
                string applicationId = row["ApplicationId"]?.ToString();
                
                _safeSplashManager.SetWaitFormDescription("Vérification des documents...");
                
                // Check if documents exist for this application regardless of reservation status
                bool hasDocuments = false;
                if (!string.IsNullOrEmpty(applicationId))
                {
                    if (_documentCountCache.TryGetValue(applicationId, out bool filesExist))
                    {
                        hasDocuments = filesExist;
                    }
                    else
                    {
                        var (documentsFolder, _, documentTypes) = LoadConfig();
                        if (Directory.Exists(documentsFolder))
                        {
                            try
                            {
                                // Check if a folder exists for this application ID
                                bool hasFiles = false;
                                
                                // Check for folders with matching application ID
                                foreach (var personFolder in Directory.GetDirectories(documentsFolder))
                                {
                                    string folderName = Path.GetFileName(personFolder);
                                    // Sanitize the folder name to handle special characters
                                    string sanitizedFolderName = ArchiveHelper.SanitizeFilePath(folderName);
                                        if (IsPersonFolderContainsApplicationId(sanitizedFolderName, applicationId))
                                        {
                                            hasFiles = true;
                                            break;
                                        }
                                    
                                }
                                
                                // If no folder found, check for zip files
                                if (!hasFiles)
                                {
                                    foreach (var zipFile in Directory.GetFiles(documentsFolder, "*.zip"))
                                    {
                                        string zipName = Path.GetFileNameWithoutExtension(zipFile);
                                        // Sanitize the zip name to handle special characters
                                        string sanitizedZipName = ArchiveHelper.SanitizeFilePath(zipName);
                                        if (IsPersonFolderContainsApplicationId(sanitizedZipName, applicationId))
                                        {
                                            hasFiles = true;
                                            break;
                                        }
                                    }
                                }
                                
                                hasDocuments = hasFiles;
                                _documentCountCache[applicationId] = hasFiles;
                            }
                            catch
                            {
                                _documentCountCache[applicationId] = false; // Cache error
                            }
                        }
                    }
                }

                // Check if reservation already exists
                bool reservationExists = row["NewReservation"] != DBNull.Value;
                int reservationId = 0;
                
                if (reservationExists)
                {
                    // Use existing reservation
                    reservationId = Convert.ToInt32(row["NewReservation"]);
                    
                    // If there are documents to import, open the document verification form
                    if (hasDocuments)
                    {
                        _safeSplashManager.SetWaitFormDescription("Ouverture du formulaire de vérification des documents...");
                        
                        // Close wait form before showing dialog
                        _safeSplashManager.CloseWaitForm();
                        
                        Document_verification_Form docVerifForm = new Document_verification_Form(applicationId);
                        docVerifForm.LoadDocuments();
                        docVerifForm.ShowDialog();
                        
                        // Send email for missing documents only if documents were imported
                        if (docVerifForm.WasImportButtonPressed)
                        {
                            // Show wait form again for email processing
                            _safeSplashManager.ShowWaitForm();
                            _safeSplashManager.SetWaitFormCaption("Traitement des emails...");
                            _safeSplashManager.SetWaitFormDescription("Préparation de l'email pour les documents manquants...");
                            
                            Booking.Reservation existingReservation = new Booking.Reservation(reservationId);
                            string email_adress = existingReservation.getEmail();
                            
                            // Close wait form before showing dialog
                            _safeSplashManager.CloseWaitForm();
                            
                            // Ask user if they want to send email for missing documents
                            DialogResult emailResult = MessageBox.Show(
                                "Voulez-vous envoyer un email pour les documents manquants ?",
                                "Confirmation d'envoi d'email",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);
                                
                            if (emailResult == DialogResult.Yes)
                            {
                                _safeSplashManager.ShowWaitForm();
                                _safeSplashManager.SetWaitFormCaption("Envoi d'email...");
                                _safeSplashManager.SetWaitFormDescription("Envoi de l'email pour les documents manquants...");
                                
                                sendEmailForMissingDocs(reservationId, email_adress, existingReservation);
                                
                                _safeSplashManager.CloseWaitForm();
                            }
                            
                            // Show wait form for opening reservation form
                            _safeSplashManager.ShowWaitForm();
                            _safeSplashManager.SetWaitFormCaption("Ouverture de la réservation...");
                            _safeSplashManager.SetWaitFormDescription("Ouverture du formulaire de réservation...");
                            
                            // Open the reservation form after document import
                            Reservation_update FORM = new Reservation_update(existingReservation);
                            FORM.MdiParent = this.MdiParent;
                            FORM.Show();
                            
                            _safeSplashManager.CloseWaitForm();
                        }
                        else
                        {
                            // If no import was done, just close wait form
                            _safeSplashManager.CloseWaitForm();
                        }
                        
                        // Show wait form for refresh
                        _safeSplashManager.ShowWaitForm();
                        _safeSplashManager.SetWaitFormCaption("Actualisation...");
                        _safeSplashManager.SetWaitFormDescription("Actualisation des données...");
                        
                        // Refresh the form after document import
                        ETAT_READ_Form_Activated(null, null);
                        
                        _safeSplashManager.CloseWaitForm();
                        return;
                    }
                    else
                    {
                        _safeSplashManager.CloseWaitForm();
                        MessageBox.Show("Aucun document trouvé pour cette réservation.", "Aucun document", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                
                // If not an existing reservation, proceed with XML import
                if (string.IsNullOrEmpty(profileId)) 
                {
                    _safeSplashManager.CloseWaitForm();
                    return;
                }

                _safeSplashManager.SetWaitFormDescription("Recherche du fichier XML...");

                // Check if we have cached stay dates for this profile
                if (!_stayDatesCache.TryGetValue(profileId, out var stayData))
                {
                    _safeSplashManager.CloseWaitForm();
                    MessageBox.Show("Aucun fichier XML trouvé pour ce profil.", "Fichier non trouvé", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string matchingFile = stayData.filePath;
                if (!File.Exists(matchingFile))
                {
                    _safeSplashManager.CloseWaitForm();
                    MessageBox.Show("Le fichier XML n'existe plus.", "Fichier non trouvé", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _safeSplashManager.SetWaitFormDescription("Traitement du fichier XML...");

                // Create Xml_file object using the constructor that takes a path
                Xml_file xmlFile = new Xml_file(matchingFile);

                // Create a binding list with the loaded XML file
                BindingList<Xml_file> rows = new BindingList<Xml_file>();
                rows.Add(xmlFile);

                // Process the XML file
                for (int i = 0; i < rows.Count; i++)
                {
                    if (!Booking.Reservation.isExist(rows[i].application_id))
                    {
                        _safeSplashManager.SetWaitFormDescription("Création de la réservation...");
                        
                        Booking.Reservation Reservation = rows[i].setReservation();
                        Booking.Reservation OldReservation = new Booking.Reservation(Convert.ToInt32(row["OldReservation"]));
                        Reservation.room = GetNewReservationRoom(OldReservation);
                        Reservation.category = OldReservation.category;
                        Reservation.stirred = OldReservation.stirred;
                        Reservation.residence_in = OldReservation.residence_in;
                        Reservation.residence_out = OldReservation.residence_out;
                        // Set fixed begin and end dates
                        Reservation.begin_date = new DateTime(2025, 9, 1);
                        Reservation.end_date = new DateTime(2026, 6, 30);
                        Reservation.update();
                        Reservation.validate();

                        // Close wait form before showing message box
                        _safeSplashManager.CloseWaitForm();

                        // Notify user if the room has changed
                        if (Reservation.room != OldReservation.room)
                        {
                            MessageBox.Show(
                                $"La réservation a été mise à jour avec un changement de chambre.\n" +
                                $"Ancienne chambre: {OldReservation.room}\n" +
                                $"Nouvelle chambre: {Reservation.room}",
                                "Changement de chambre",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }

                        // Show wait form again for document verification
                        _safeSplashManager.ShowWaitForm();
                        _safeSplashManager.SetWaitFormCaption("Vérification des documents...");
                        _safeSplashManager.SetWaitFormDescription("Ouverture du formulaire de vérification des documents...");
                        
                        // Close wait form before showing dialog
                        _safeSplashManager.CloseWaitForm();

                        Document_verification_Form docVerifForm = new Document_verification_Form(Reservation.application_id);
                        docVerifForm.LoadDocuments();
                        docVerifForm.ShowDialog();
                        string email_adress = Reservation.getEmail();
                        
                        // Only send emails if documents were imported
                        if (docVerifForm.WasImportButtonPressed)
                        {
                            // Ask user if they want to send email for missing documents
                            DialogResult emailResult = MessageBox.Show(
                                "Voulez-vous envoyer un email pour les documents manquants ?",
                                "Confirmation d'envoi d'email",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);
                                
                            if (emailResult == DialogResult.Yes)
                            {
                                _safeSplashManager.ShowWaitForm();
                                _safeSplashManager.SetWaitFormCaption("Envoi d'email...");
                                _safeSplashManager.SetWaitFormDescription("Envoi de l'email pour les documents manquants...");
                                
                                sendEmailForMissingDocs(Reservation.Id, email_adress, Reservation);
                                
                                _safeSplashManager.CloseWaitForm();
                            }
                        }
                        
                        if (Reservation != null)
                        {
                            if (i == rows.Count - 1)
                            {
                                _safeSplashManager.ShowWaitForm();
                                _safeSplashManager.SetWaitFormCaption("Finalisation...");
                                _safeSplashManager.SetWaitFormDescription("Finalisation de l'importation...");
                                
                                if (Reservation != null)
                                    new Thread(() => Reservation.setLogOnTransfert()).Start();
                                
                                Reservation_update FORM = new Reservation_update(Reservation);
                                FORM.MdiParent = this.MdiParent;
                                ETAT_READ_Form_Activated(null,null);
                                FORM.Show();
                                
                                _safeSplashManager.CloseWaitForm();
                            }
                        }
                    }
                    else
                    {
                        _safeSplashManager.CloseWaitForm();
                        MessageBox.Show("La Réservation de " + rows[i].fullName + " sous ID: " + rows[i].application_id + " existe déjà.", "Réservation Existante", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                _safeSplashManager.CloseWaitForm();
                MessageBox.Show($"Erreur lors du traitement du fichier: {ex.Message}" + ex.StackTrace, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Ensure wait form is closed in case of any unexpected path
                try
                {
                    _safeSplashManager.CloseWaitForm();
                }
                catch
                {
                    // Ignore errors when closing wait form
                }
            }
        }

        static public bool EditConfiguration()
        {
            using (var configForm = new ConfigurationForm())
            {
                return configForm.ShowDialog() == DialogResult.OK;
            }
        }


        
        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView View = sender as DevExpress.XtraGrid.Views.Grid.GridView;

                if (e.RowHandle >= 0)
                {
                    string state = View.GetRowCellValue(e.RowHandle, View.Columns["State"]).ToString();
                    if (e.Column.FieldName == "State")
                    {
                        switch (state)
                        {
                            case "ADMISE":
                                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                                e.Appearance.ForeColor = Color.Green;
                                break;
                            case "ADMISE-BRASSEE":
                                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                                e.Appearance.ForeColor = Color.DarkTurquoise;
                                break;
                            case "REJETEE":
                                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                                e.Appearance.ForeColor = Color.Red;
                                break;
                            default:
                                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                                break;
                        }
                    }
                    else if (e.Column == colstatus)
                    {
                        string status = View.GetRowCellDisplayText(e.RowHandle, colstatus);
                        switch (status)
                        {
                            case "Déjà importé":
                                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                                e.Appearance.ForeColor = Color.Green;
                                break;
                            case "Non déposé":
                                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                                e.Appearance.ForeColor = Color.Gray;
                                break;
                            default:
                                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                                e.Appearance.ForeColor = Color.Orange;
                                break;
                        }
                    }
                    else if (e.Column == coldocs)
                    {
                        string docStatus = View.GetRowCellDisplayText(e.RowHandle, coldocs);
                        if (docStatus == "Docs disponibles")
                        {
                            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                            e.Appearance.ForeColor = Color.Green;
                        }
                        else if (docStatus == "Aucun document")
                        {
                            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                            e.Appearance.ForeColor = Color.Gray;
                        }
                        else if (docStatus == "Erreur" || docStatus == "Dossier introuvable")
                        {
                            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                            e.Appearance.ForeColor = Color.Red;
                        }
                    }
                    else if (e.Column == colRoom2)
                    {
                        var room1Value = View.GetRowCellValue(e.RowHandle, colRoom)?.ToString();
                        var room2Value = View.GetRowCellValue(e.RowHandle, colRoom2)?.ToString();
                        
                        if (!string.IsNullOrEmpty(room2Value) && room1Value != room2Value)
                        {
                            e.Appearance.BackColor = Color.Red;
                            e.Appearance.ForeColor = Color.White;
                        }
                    }
                }
            }
            catch { }
        }

        private void ETAT_READ_Form_Activated(object sender, EventArgs e)
        {
            bookingReadmissionDataSet1 = new BookingReadmissionTableAdapter().GetData(25);
            gridControl1.DataSource = bookingReadmissionDataSet1;
            gridControl1.DataMember = "BookingReadmission";
            
            // Clear and rebuild caches
            _documentCountCache.Clear();
            PreCacheDocumentCounts();
            
            gridControl1.RefreshDataSource();
            gridView1.RefreshData();
        }

        private void EditConfigSimpleButton_Click(object sender, EventArgs e)
        {
            EditConfiguration();
        }

        public static void sendEmailForMissingDocs(int reservationId, string email_adress, object reservation)
        {
            try
            {
                DataSetETAT_READTableAdapters.atooerp_documentTableAdapter TA = new DataSetETAT_READTableAdapters.atooerp_documentTableAdapter();
                int missing_document = Convert.ToInt32(TA.GetMissingDocuments(reservationId));
                if (missing_document > 0)
                {
                    BindingList<AtooERP.Mailer.Email.Address> receivers = new BindingList<AtooERP.Mailer.Email.Address>();
                    AtooERP.Mailer.DataSetMailerTableAdapters.atooerp_mailer_email_templateTableAdapter ta = new AtooERP.Mailer.DataSetMailerTableAdapters.atooerp_mailer_email_templateTableAdapter();
                    ta.GetDataByPiece("Booking.Reservation");
                    if (email_adress != null && email_adress != "")
                    {

                        AtooERP.Mailer.Email.Address email_Address = new AtooERP.Mailer.Email.Address(email_adress);
                        receivers.Add(email_Address);
                    }
                    var templates = ta.GetDataByPiece("Booking.Reservation");
                    var row = templates.FirstOrDefault(t => t.name == "reservation_docs_request_email");
                    AtooERP.Mailer.Template template = null;
                    if (row != null)
                    {
                        if (row.Id > 0)
                        {

                            template = new AtooERP.Mailer.Template
                            {
                                Id = Convert.ToInt32(row.Id),
                                name = row.name,
                                create_date = row.create_date,
                                memo = row.IsmemoNull() ? "" : row.memo.ToString(),
                                content = row.content,
                                piece_type = row.piece_type,
                                is_default = row.is_default,
                            };

                        }

                    }
                    if (template != null)
                    {
                        AtooERP.Mailer.Email email = AtooERP.Mailer.Email.create_Email_from_template(template, reservation, receivers, null, null);
                        
                        AtooERP.Mailer.Email_Generator.Email_insert Form = new AtooERP.Mailer.Email_Generator.Email_insert(email);
                        Form.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Veuillez verifier l'existance d'un modèle d'email  ");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Un problème lors de la généation de l'email " + ex.Message);
            }
        }

        private void RefreshSimpleButton_Click(object sender, EventArgs e)
        {
            ETAT_READ_Form_Activated(null, null);
        }

        private uint? GetNewReservationRoom(Booking.Reservation Reservation)
        {
            try
            {
                Booking.DataSetRahmaTableAdapters.booking_change_room_requestTableAdapter TA = new Booking.DataSetRahmaTableAdapters.booking_change_room_requestTableAdapter();
                
                // Get all change room requests and filter for this reservation
                var allRequests = TA.GetData();
                
                // Find accepted change room requests without check-in/check-out
                var validRequests = allRequests.AsEnumerable().Where(row => 
                    Convert.ToInt32(row["reservation"]) == Reservation.Id && 
                    Convert.ToInt32(row["reservation_state"]) == 2 && // Accepted state
                    row.IsNull("check_in") && // No check-in
                    row.IsNull("check_out")   // No check-out
                ).OrderByDescending(row => Convert.ToDateTime(row["date_change_room"])); // Most recent first
                
                if (validRequests.Any())
                {
                    // Return the room_in from the most recent valid request
                    return Convert.ToUInt32(validRequests.First()["room_in"]);
                }
                else
                {
                    // No valid change room request found, return original room
                    return (uint?)Reservation.room;
                }
            }
            catch (Exception)
            {
                // In case of any error, return the original room
                return (uint?)Reservation.room;
            }
        }

        private void SendPropositionSimpleButton_Click(object sender, EventArgs e)
        {
            // Get the selected reservations from the grid
            BindingList<Booking.Reservation> ReservationList = new BindingList<Booking.Reservation>();
            var selectedIndexes = gridView1.GetSelectedRows();
            if (selectedIndexes.Length <= 0)
                return;
            foreach (int index in selectedIndexes)
            {
                if(gridView1.GetRowCellValue(index, colNewReservation) is int)
                ReservationList.Add(new Booking.Reservation(Convert.ToInt32(gridView1.GetRowCellValue(index,colNewReservation))));
            }
            if(ReservationList.Count > 0)
                sendEmailForReservationPropositions(ReservationList);
        }

        public void sendEmailForReservationPropositions(BindingList<Booking.Reservation> Reservations)
        {
            string email_adress = "";
            try
            {
                if (Reservations == null || Reservations.Count == 0)
                {
                    MessageBox.Show("La liste des réservations est vide.");
                    return;
                }

                // Préparation des destinataires
                BindingList<AtooERP.Mailer.Email.Address> receivers = new BindingList<AtooERP.Mailer.Email.Address>();

                // Récupération du modèle d'e-mail
                var ta = new AtooERP.Mailer.DataSetMailerTableAdapters.atooerp_mailer_email_templateTableAdapter();
                var templates = ta.GetDataByPiece("Booking.Reservation");
                var row = templates.FirstOrDefault(t => t.name == "reservation_proposition_email");

                if (row == null || row.Id <= 0)
                {
                    MessageBox.Show("Veuillez vérifier l'existence d'un modèle d'e-mail.");
                    return;
                }

                var template = new AtooERP.Mailer.Template
                {
                    Id = Convert.ToInt32(row.Id),
                    name = row.name,
                    create_date = row.create_date,
                    memo = row.IsmemoNull() ? "" : row.memo.ToString(),
                    content = row.content,
                    piece_type = row.piece_type,
                    is_default = row.is_default,
                };

                // Boucle sur les réservations
                foreach (Booking.Reservation reservation in Reservations)
                {
                    receivers = new BindingList<AtooERP.Mailer.Email.Address>();
                    email_adress = reservation.getEmail();
                    if (!string.IsNullOrEmpty(email_adress))
                    {
                        receivers.Add(new AtooERP.Mailer.Email.Address(email_adress));
                    }
                    var email = AtooERP.Mailer.Email.create_Email_from_template(template, reservation, receivers, null, null);
                    AddAttachmenet(email, reservation); // Pièces jointes spécifiques à la réservation
                    
                    // Close the wait form before showing the email form
                    _safeSplashManager.CloseWaitForm();
                    
                    AtooERP.Mailer.Email_Generator.Email_insert form = new AtooERP.Mailer.Email_Generator.Email_insert(email);
                    form.ShowDialog(); // Affiche la boîte de dialogue pour chaque e-mail
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Un problème est survenu lors de la génération des e-mails : " + ex.Message);
            }
        }

        public  void sendEmailForReservationProposition(int reservationId, string email_adress, Booking.Reservation reservation, BindingList<object> Reservations)
        {
            try
            {
                BindingList<AtooERP.Mailer.Email.Address> receivers = new BindingList<AtooERP.Mailer.Email.Address>();
                AtooERP.Mailer.DataSetMailerTableAdapters.atooerp_mailer_email_templateTableAdapter ta = new AtooERP.Mailer.DataSetMailerTableAdapters.atooerp_mailer_email_templateTableAdapter();
                ta.GetDataByPiece("Booking.Reservation");
                if (email_adress != null && email_adress != "")
                {
                    AtooERP.Mailer.Email.Address email_Address = new AtooERP.Mailer.Email.Address(email_adress);
                    receivers.Add(email_Address);
                }
                var templates = ta.GetDataByPiece("Booking.Reservation");
                var row = templates.FirstOrDefault(t => t.name == "reservation_proposition_email");
                AtooERP.Mailer.Template template = null;
                if (row != null)
                {
                    if (row.Id > 0)
                    {
                        template = new AtooERP.Mailer.Template
                        {
                            Id = Convert.ToInt32(row.Id),
                            name = row.name,
                            create_date = row.create_date,
                            memo = row.IsmemoNull() ? "" : row.memo.ToString(),
                            content = row.content,
                            piece_type = row.piece_type,
                            is_default = row.is_default,
                        };
                    }
                }
                if (template != null)
                {
                    AtooERP.Mailer.Email email = AtooERP.Mailer.Email.create_Email_from_template(template, reservation, receivers, null, null);
                    AddAttachmenet(email, reservation);
                    
                    // Close the wait form before showing the email form
                    _safeSplashManager.CloseWaitForm();
                    
                    AtooERP.Mailer.Email_Generator.Email_insert Form = new AtooERP.Mailer.Email_Generator.Email_insert(email);
                    Form.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Veuillez verifier l'existance d'un modèle d'email  ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Un problème lors de la généation de l'email " + ex.Message);
            }
        }
        public static void AddAttachmenet(AtooERP.Mailer.Email Email, Booking.Reservation Reservation)
        {
            Booking.Duration Duration = null;
            var attatchment_List = new BindingList<AtooERP.Mailer.Email.Attachment>();
            var addedDocumentIds = new HashSet<int>();

            if (Reservation.duration.HasValue)
                Duration = new Booking.Duration(Reservation.duration.Value);
            else
                Duration = new Booking.Duration(Booking.Duration.getIdDuration(Reservation));

            if (Reservation.duration.HasValue)
            {
                var ressourceList = Booking.Reservation.Type.Ressource
                    .getListByDurationAndType(Reservation.duration.Value, Reservation.reservation_type);

                foreach (var Rs in ressourceList)
                {
                    if (Rs.document.HasValue)
                    {
                        int docId = 0;
                        int.TryParse(Rs.document.Value.ToString(), out docId);
                        if (!addedDocumentIds.Contains(docId))
                        {
                            var attachment = new AtooERP.Mailer.Email.Attachment(Email.Id, docId);
                            attatchment_List.Add(attachment);
                            addedDocumentIds.Add(docId);
                        }
                    }
                }

                var ReservationType = new Booking.Reservation.Type(Reservation.reservation_type);
                foreach (var Doc in ReservationType.DocumentList)
                {
                    if (Doc.duration == Duration.Id)
                    {
                        bool shouldAdd =
                            (Reservation.stirred < 3 && !Doc.stirred) ||
                            (!(Reservation.stirred < 3) && Doc.stirred);

                        if (shouldAdd)
                        {
                            int docId = Convert.ToInt32(Doc.document);
                            if (!addedDocumentIds.Contains(docId))
                            {
                                var attachment = new AtooERP.Mailer.Email.Attachment(Email.Id, docId);
                                attatchment_List.Add(attachment);
                                addedDocumentIds.Add(docId);
                            }
                        }
                    }
                }
            }

            // Une seule fois à la fin
            foreach (var attachment in attatchment_List)
                attachment.insert();

            Email.attatchment_List = attatchment_List;
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column == colget_files)
            {
                // Use BeginInvoke to ensure the action happens after the click event is fully processed
                this.BeginInvoke(new Action(() => HandleButtonAction()));
            }
        }

        private void Print_SimpleButton_Click(object sender, EventArgs e)
        {
            // Check whether the GridControl can be previewed.
            if (!gridControl1.IsPrintingAvailable)
            {
                MessageBox.Show("The 'DevExpress.XtraPrinting' library is not found", "Error");
                return;
            }
            // Open the Preview window.
            AtooERP.GridReport GridReport = new AtooERP.GridReport(gridControl1, titre.Text);
            GridReport.print();
            // grid.ShowPrintPreview();
        }

        private void SendDDSimpleButton_Click(object sender, EventArgs e)
        {
            ArrayList rows = new ArrayList();

            // Create a DataTable structure for the new DataRows
            DataTable tempTable = new DataTable();
            tempTable.Columns.Add("IdReservation", typeof(int));
            tempTable.Columns.Add("Admission", typeof(int));

            // Add the selected rows to the list. 
            Int32[] selectedRowHandles = gridView1.GetSelectedRows();
            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                int selectedRowHandle = selectedRowHandles[i];
                if (selectedRowHandle >= 0)
                {
                    DataRow row = gridView1.GetDataRow(selectedRowHandle);
                    int IdReservation = Convert.ToInt32(row["OldReservation"]);
                    int admission = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(row["Counteur"]) / 10));
                    
                    // Create a new DataRow with the required columns
                    DataRow newRow = tempTable.NewRow();
                    newRow["IdReservation"] = IdReservation;
                    newRow["Admission"] = admission;
                    rows.Add(newRow);
                }
            }
            
            AtooERP_Booking.Communication.Procedure_final_departure.Procedure_final_departure_generate_type Form =
                new AtooERP_Booking.Communication.Procedure_final_departure.Procedure_final_departure_generate_type(rows);
            // Form.MdiParent = this.MdiParent;
            Form.ShowDialog();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                // Get the focused cell information
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                int focusedRowHandle = view.FocusedRowHandle;
                DevExpress.XtraGrid.Columns.GridColumn focusedColumn = view.FocusedColumn;
                
                // Check if we have a valid focused cell and if it's one of the reservation columns
                if (focusedRowHandle >= 0 && focusedColumn != null && 
                    (focusedColumn.FieldName == "OldReservation" || focusedColumn.FieldName == "NewReservation"))
                {
                    splashScreenManager2.ShowWaitForm();
                    // Get the value of the focused cell
                    object cellValue = view.GetRowCellValue(focusedRowHandle, focusedColumn);
                    
                    // Check if the cell value is not null and is a valid integer
                    if (cellValue != null && cellValue != DBNull.Value)
                    {
                        if (int.TryParse(cellValue.ToString(), out int reservationId) && reservationId > 0)
                        {
                            // Create and show the reservation form
                            Booking.Reservation reservation = new Booking.Reservation(reservationId);
                            Reservation_update reservationForm = new Reservation_update(reservation);
                            reservationForm.MdiParent = this.MdiParent;
                            reservationForm.Show();
                        }
                        else
                        {
                            MessageBox.Show("ID de réservation invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Aucune réservation trouvée pour cette cellule.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    splashScreenManager2.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'ouverture de la réservation: {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                splashScreenManager2.CloseWaitForm();
            }
        }

        private void PVSimpleButton_Click(object sender, EventArgs e)
        {
            new ReportPrintTool(new PV_Readmission()).ShowPreview();
        }
    }
}

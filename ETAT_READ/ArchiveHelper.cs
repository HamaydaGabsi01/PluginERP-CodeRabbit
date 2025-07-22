using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETAT_READ
{
    public class ArchiveHelper
    {
        public static void ExtractArchiveClean(string zipPath, string extractRoot)
        {
            string compressedRoot = zipPath;
            // Définir un modèle regex pour valider le format nombre_nom
            Regex pattern = new Regex(@"^\d+_\w+$");

            // Pour chaque fichier ZIP dans le dossier spécifié
            foreach (var zipFile in Directory.GetFiles(compressedRoot, "*.zip"))
            {
                string zipName = Path.GetFileNameWithoutExtension(zipFile);

                // Vérifier si le nom du fichier correspond au modèle nombre_nom
                if (pattern.IsMatch(zipName))
                {
                    string extractTo = Path.Combine(extractRoot, zipName);

                    if (!Directory.Exists(extractTo))
                        Directory.CreateDirectory(extractTo);

                    try
                    {
                        // Extraire avec remplacement des fichiers existants
                        ExtractWithOverwrite(zipFile, extractTo);

                        // MessageBox.Show($"Extraction réussie : {zipName}");

                        // Supprimer l'archive après extraction réussie
                        File.Delete(zipFile);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erreur lors du traitement de {zipName}: {ex.Message}");
                    }
                }
            }
        }

        // Méthode pour extraire un fichier ZIP spécifique
        public static string ExtractSingleArchive(string zipFilePath, string extractRoot, bool deleteAfterExtraction = false)
        {
            try
            {
                if (!File.Exists(zipFilePath))
                {
                    throw new FileNotFoundException("Le fichier ZIP spécifié n'existe pas.", zipFilePath);
                }

                string zipName = Path.GetFileNameWithoutExtension(zipFilePath);
                string extractTo = Path.Combine(extractRoot, zipName);

                if (!Directory.Exists(extractTo))
                    Directory.CreateDirectory(extractTo);

                // Extraire avec remplacement des fichiers existants
                ExtractWithOverwrite(zipFilePath, extractTo);

                // Supprimer l'archive après extraction si demandé
                if (deleteAfterExtraction)
                {
                    File.Delete(zipFilePath);
                }

                return extractTo;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'extraction de {Path.GetFileName(zipFilePath)}: {ex.Message}");
                return null;
            }
        }

        public static string SanitizeFilePath(string path)
        {
            // Trim leading and trailing spaces
            path = path.Trim();
            
            // Replace forward slashes with backslashes
            path = path.Replace('/', '\\');
            
            // Handle colons in file names (but preserve drive letter colon)
            if (path.Length > 1 && path[1] == ':')
            {
                // If it's a drive letter path, only sanitize the part after the drive letter
                string drivePart = path.Substring(0, 2);
                string restOfPath = path.Substring(2);
                restOfPath = restOfPath.Replace(":", "-");
                path = drivePart + restOfPath;
            }
            else
            {
                // If it's not a drive letter path, replace all colons
                path = path.Replace(":", "-");
            }
            
            // Get invalid characters for Windows file system
            char[] invalidChars = Path.GetInvalidFileNameChars().Concat(Path.GetInvalidPathChars()).Distinct().ToArray();
            
            // Replace invalid characters with hyphen, but preserve international characters
            foreach (char c in invalidChars)
            {
                if (c != ':' && c != '\\' && c != '/') // Skip already handled characters
                {
                    path = path.Replace(c, '-');
                }
            }
            
            // Normalize the string to handle composed characters
            path = path.Normalize();
            
            return path;
        }

        // Méthode pour extraire avec remplacement des fichiers existants
        public static void ExtractWithOverwrite(string zipFilePath, string extractPath)
        {
            // Extraire d'abord tous les fichiers normalement
            using (ZipArchive archive = ZipFile.OpenRead(zipFilePath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    // Obtenir le chemin complet du fichier de destination et le sanitizer
                    string destinationPath = Path.Combine(extractPath, SanitizeFilePath(entry.Name));

                    // Créer le répertoire si nécessaire
                    string directoryPath = Path.GetDirectoryName(destinationPath);
                    if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    // Si l'entrée n'a pas de nom de fichier, c'est probablement un répertoire
                    if (!string.IsNullOrEmpty(entry.Name))
                    {
                        // Supprimer le fichier existant s'il existe
                        if (File.Exists(destinationPath))
                        {
                            File.Delete(destinationPath);
                        }

                        // Extraire le fichier
                        entry.ExtractToFile(destinationPath);
                    }
                }
            }

            // Après extraction, déplacer tous les fichiers des sous-dossiers vers le dossier parent
            MoveFilesFromSubfolderToParent(extractPath);
        }

        private static void MoveFilesFromSubfolderToParent(string parentFolder)
        {
            // Obtenir tous les sous-dossiers du dossier principal
            string[] subfolders = Directory.GetDirectories(parentFolder);
            
            // S'il y a des sous-dossiers
            if (subfolders.Length > 0)
            {
                foreach (string subfolder in subfolders)
                {
                    // Déplacer tous les fichiers du sous-dossier vers le dossier parent
                    foreach (string file in Directory.GetFiles(subfolder, "*", SearchOption.AllDirectories))
                    {
                        string fileName = Path.GetFileName(file);
                        string destFile = Path.Combine(parentFolder, fileName);
                        
                        // Supprimer le fichier de destination s'il existe déjà
                        if (File.Exists(destFile))
                        {
                            File.Delete(destFile);
                        }
                        
                        // Déplacer le fichier
                        File.Move(file, destFile);
                    }
                    
                    // Supprimer le sous-dossier maintenant vide
                    Directory.Delete(subfolder, true);
                }
            }
        }

        /// <summary>
        /// Déplace un fichier vers un dossier de destination, en le plaçant dans un sous-dossier "accepted" ou "rejected"
        /// en fonction du paramètre accept. Crée les dossiers nécessaires si inexistants.
        /// </summary>
        /// <param name="filePath">Chemin complet du fichier à déplacer</param>
        /// <param name="destinationDir">Répertoire de destination</param>
        /// <param name="accept">True pour accepté, False pour rejeté</param>
        /// <returns>Le chemin complet du fichier déplacé</returns>
        public static string MoveFileToAcceptRejectFolder(string filePath, string destinationDir, bool accept)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("Le fichier spécifié n'existe pas.", filePath);
                }

                // Récupérer le nom du dossier parent du fichier
                string parentFolderName = new DirectoryInfo(Path.GetDirectoryName(filePath)).Name;
                string sourceFolderPath = Path.GetDirectoryName(filePath);
                
                // Déterminer le dossier cible en fonction du paramètre accept
                string targetSubfolder = accept ? "accepted" : "rejected";
                
                // Construire le chemin complet du dossier de destination
                string fullDestinationDir = Path.Combine(destinationDir, parentFolderName, targetSubfolder);
                
                // Créer le dossier de destination s'il n'existe pas
                if (!Directory.Exists(fullDestinationDir))
                {
                    Directory.CreateDirectory(fullDestinationDir);
                }
                
                // Obtenir le nom du fichier
                string fileName = Path.GetFileName(filePath);
                
                // Construire le chemin complet du fichier de destination
                string destinationFilePath = Path.Combine(fullDestinationDir, fileName);
                
                // Supprimer le fichier de destination s'il existe déjà
                if (File.Exists(destinationFilePath))
                {
                    File.Delete(destinationFilePath);
                }
                
                // Déplacer le fichier
                File.Move(filePath, destinationFilePath);
                
                // Vérifier si le dossier source est vide après le déplacement
                if (Directory.Exists(sourceFolderPath) && 
                    !Directory.EnumerateFileSystemEntries(sourceFolderPath).Any())
                {
                    // Supprimer le dossier source s'il est vide
                    Directory.Delete(sourceFolderPath);
                }
                
                return destinationFilePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du déplacement du fichier: {ex.Message}");
                return null;
            }
        }
    }
}

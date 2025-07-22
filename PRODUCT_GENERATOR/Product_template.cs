using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marketing;
using AtooERP;
using Commercial;
using System.ComponentModel;

namespace PRODUCT_GENERATOR
{
    public class Product_template
    {
        public string general_name { get; set; }
        public string reference { get; set; }
        public string client_abreviation { get; set; }
        public List<int> Size_list { get; set; }
        public int color { get; set; }
        public int category { get; set; }
        public decimal price { get; set; }

        public Marketing.Profile.Profile_instance Profile_instance { get; set; }

        public Dictionary<int,decimal> Coupe_components_list { get; set; }
        public Dictionary<int, decimal> Confection_components_list { get; set; }
        public Dictionary<int, decimal> Personnalisation_components_list { get; set; }
        public Dictionary<int, decimal> Finition_components_list { get; set; }

        public Product Base_product { get; set; }
        public int base_product { get; set; }

        public bool use_base_product { get; set; }
        public bool use_personnalisation { get; set; }

        // Profile management encapsulation
        private ProfileManager _profileManager;

        public Product_template()
        {
            Coupe_components_list = new Dictionary<int, decimal>();
            Confection_components_list = new Dictionary<int, decimal>();
            Personnalisation_components_list = new Dictionary<int, decimal>();
            Finition_components_list = new Dictionary<int, decimal>();
            use_base_product = true;
            
            _profileManager = new ProfileManager(this);
        }

        public void GenerateProducts()
        {
            // Initialize profile before generating products
            _profileManager.InitializeProfile();
            
            if (use_base_product)
                GenerateFromBaseProduct();
            else
                GenerateFromComponents();
        }

        string GetProductName(int size)
        {
            if (use_base_product)
                return GetProductNameByBaseProduct(size);
            else
                return GetProductNameByInformation(size);
        }

        string GetProductNameByInformation(int size)
        {
            return general_name + "-" + reference + "-" + GetColorName(color) + "[" + size + "]";
        }

        string GetProductNameByBaseProduct(int size)
        {
            return Base_product.name.Replace("WORK", client_abreviation).Split('[')[0] + "[" + size + "]";
        }

        void GenerateFromBaseProduct()
        {
            foreach (int size in Size_list)
            {
                GenerateFromBaseProduct(size);
            }
        }

        void GenerateFromComponents()
        {
            foreach(int size in Size_list)
            {
                GenerateFromComponents(size);
            }
        }

        void GenerateFromBaseProduct(int size)
        {
            Product product = new Product();
            product.name = GetProductName(size);
            product.reference = reference;
            product.IdCategory = category;
            product.price_sale = price;
            product.insert();

            // Create profile for this product using profile manager
            //_profileManager.CreateProfileForProduct(product.Id, ProfileType.Duplicated);
        }

        void GenerateFromComponents(int size)
        {
            Product product_coupe = GenerateProductCoupe(size);
            Product product_confection = GenerateProductConfection(size, product_coupe);
            if(use_personnalisation)
            {
                Product product_personnalisation = GenerateProductPersonnalisation(size, product_confection);
                Product product_finition = GenerateProductFinition(size, product_personnalisation);
            }
            else
            {
                Product product_finition = GenerateProductFinition(size, product_confection);
            }
        }

        Product GenerateProductCoupe(int size)
        {
            Product product = new Product();
            product.name = GetProductName(size) + " COUPE";
            product.is_composite = true;
            product.IdProviding_method = 2;
            foreach(var component in Coupe_components_list)
            {
                product.CompositionList.Add(new Product.Composition(0, component.Key, component.Value, "", ""));
            }
            product.insert();
            
            // Create profile for coupe product
            //_profileManager.CreateProfileForProduct(product.Id, ProfileType.Master);
            
            return product;
        }

        Product GenerateProductConfection(int size, Product product_coupe)
        {
            Product product = new Product();
            product.name = GetProductName(size) + " CONFECTION";
            product.is_composite = true;
            product.IdProviding_method = 2;
            foreach (var component in Confection_components_list)
            {
                product.CompositionList.Add(new Product.Composition(0, component.Key, component.Value, "", ""));
            }
            product.CompositionList.Add(new Product.Composition(0, product_coupe.Id, 1, "", ""));
            product.insert();
            
            // Create duplicated profile for confection
            //_profileManager.CreateProfileForProduct(product.Id, ProfileType.Duplicated);
            
            return product;
        }

        Product GenerateProductPersonnalisation(int size, Product product_confection)
        {
            Product product = new Product();
            product.name = GetProductName(size)+ " PERSONNALISE";
            product.is_composite = true;
            product.IdProviding_method = 2;
            foreach (var component in Personnalisation_components_list)
            {
                product.CompositionList.Add(new Product.Composition(0, component.Key, component.Value, "", ""));
            }
            product.CompositionList.Add(new Product.Composition(0, product_confection.Id, 1, "", ""));
            product.insert();
            
            // Create duplicated profile for personnalisation
            //_profileManager.CreateProfileForProduct(product.Id, ProfileType.Duplicated);
            
            return product;
        }

        Product GenerateProductFinition(int size, Product sub_product)
        {
            Product product = new Product();
            product.name = GetProductName(size);
            product.is_composite = true;
            product.IdProviding_method = 2;
            product.sale = true;
            foreach (var component in Finition_components_list)
            {
                product.CompositionList.Add(new Product.Composition(0, component.Key, component.Value, "", ""));
            }
            product.CompositionList.Add(new Product.Composition(0, sub_product.Id, 1, "", ""));
            product.insert();
            
            // Create duplicated profile for finition
            //_profileManager.CreateProfileForProduct(product.Id, ProfileType.Duplicated);
            
            return product;
        }

        string GetColorName(int color)
        {
            return new AtooERP.Input_type.Type.Element(color).name;
        }

        /// <summary>
        /// Legacy method - kept for compatibility, now delegates to ProfileManager
        /// </summary>
        Marketing.Profile.Profile_instance DuplicateProfile(Marketing.Profile.Profile_instance profile_instance, int productId)
        {
            return _profileManager.DuplicateProfile(profile_instance, productId);
        }
    }

    #region Profile Management Encapsulation

    /// <summary>
    /// Enum to define different types of profiles
    /// </summary>
    public enum ProfileType
    {
        Master,      // First profile created (for coupe in new profile case)
        Duplicated   // Copied from master or base product
    }

    /// <summary>
    /// Encapsulates all profile management logic for better maintainability
    /// Based on patterns from Product_insert.cs
    /// </summary>
    public class ProfileManager
    {
        private readonly Product_template _template;
        private Marketing.Profile.Profile_instance _masterProfile;
        private bool _isInitialized;

        public ProfileManager(Product_template template)
        {
            _template = template ?? throw new ArgumentNullException(nameof(template));
            _isInitialized = false;
        }

        /// <summary>
        /// Initialize profile based on use_base_product setting
        /// Similar to profileLayoutControlGroup_Shown in Product_insert.cs
        /// </summary>
        public void InitializeProfile()
        {
            if (_isInitialized) return;

            try
            {
                if (_template.use_base_product)
                {
                    InitializeProfileFromBaseProduct();
                }
                else
                {
                    InitializeNewProfile();
                }
                _isInitialized = true;
            }
            catch (Exception ex)
            {
                // Handle initialization errors gracefully
                _template.Profile_instance = new Marketing.Profile.Profile_instance();
                _masterProfile = _template.Profile_instance;
                _isInitialized = true;
            }
        }

        /// <summary>
        /// Case 1: Copy profile from base product (similar to duplication logic in Product_insert.cs)
        /// Based on lines 977-980 pattern in Product_insert.cs
        /// </summary>
        private void InitializeProfileFromBaseProduct()
        {
            if (_template.base_product > 0)
            {
                // Get the active profile from the base product
                _masterProfile = Marketing.Profile.Profile_instance.GetActiveProfile_instanceByProduct(_template.base_product);
                
                if (_masterProfile != null)
                {
                    // Mark as old to prepare for duplication (line 912 in Product_insert.cs)
                    _masterProfile.setOld();
                    _template.Profile_instance = _masterProfile;
                }
                else
                {
                    // If base product has no profile, create a new one
                    InitializeNewProfile();
                }
            }
            else
            {
                InitializeNewProfile();
            }
        }

        /// <summary>
        /// Case 2: Create new profile from scratch
        /// Based on lines 900-906 pattern in Product_insert.cs
        /// </summary>
        private void InitializeNewProfile()
        {
            // Create new profile instance
            _template.Profile_instance = new Marketing.Profile.Profile_instance("first_Instance");
            _template.Profile_instance.first_instance = true;
            _masterProfile = _template.Profile_instance;
        }

        /// <summary>
        /// Creates a profile for a specific product based on the profile type
        /// </summary>
        /// <param name="productId">The ID of the product to create profile for</param>
        /// <param name="profileType">Type of profile to create</param>
        public void CreateProfileForProduct(int productId, ProfileType profileType)
        {
            if (!_isInitialized)
            {
                InitializeProfile();
            }

            try
            {
                switch (profileType)
                {
                    case ProfileType.Master:
                        CreateMasterProfile(productId);
                        break;
                    case ProfileType.Duplicated:
                        CreateDuplicatedProfile(productId);
                        break;
                }
            }
            catch (Exception ex)
            {
                // Log error if needed, but don't break the product generation process
                System.Diagnostics.Debug.WriteLine($"Error creating profile for product {productId}: {ex.Message}");
            }
        }

        /// <summary>
        /// Creates a master profile (used for coupe in new profile case or base product case)
        /// </summary>
        private void CreateMasterProfile(int productId)
        {
            if (_template.use_base_product)
            {
                // For base product case, create a duplicated profile
                CreateDuplicatedProfile(productId);
            }
            else
            {
                // For new profile case, this is the master profile
                if (_masterProfile != null)
                {
                    _masterProfile.product = (uint)productId;
                    _masterProfile.insert();
                }
            }
        }

        /// <summary>
        /// Creates a duplicated profile from the master profile
        /// </summary>
        private void CreateDuplicatedProfile(int productId)
        {
            if (_masterProfile != null)
            {
                var duplicatedProfile = DuplicateProfile(_masterProfile, productId);
                if (duplicatedProfile != null)
                {
                    duplicatedProfile.insert();
                }
            }
        }

        /// <summary>
        /// Duplicate a profile instance for a new product
        /// Implementation based on duplication patterns from Product_insert.cs
        /// </summary>
        public Marketing.Profile.Profile_instance DuplicateProfile(Marketing.Profile.Profile_instance sourceProfile, int productId)
        {
            if (sourceProfile == null)
                return null;

            try
            {
                // Create a new profile instance copying from the source
                // Based on the duplication pattern from Product_insert.cs lines 885-894
                var newProfile = new Marketing.Profile.Profile_instance("duplicated_instance");
                
                // Copy relevant properties from source profile
                newProfile.profil = sourceProfile.profil; // Copy the profile template reference
                newProfile.product = (uint)productId; // Link to the new product
                newProfile.first_instance = false; // This is not the first instance
                
                // Copy any additional profile-specific data that may exist
                // Note: You may need to copy other properties based on your Profile_instance implementation
                
                return newProfile;
            }
            catch (Exception ex)
            {
                // Log error if needed
                System.Diagnostics.Debug.WriteLine($"Error duplicating profile: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Gets the master profile instance
        /// </summary>
        public Marketing.Profile.Profile_instance GetMasterProfile()
        {
            return _masterProfile;
        }

        /// <summary>
        /// Checks if the profile manager is initialized
        /// </summary>
        public bool IsInitialized => _isInitialized;

        /// <summary>
        /// Reset the profile manager (useful for testing or reinitialization)
        /// </summary>
        public void Reset()
        {
            _masterProfile = null;
            _isInitialized = false;
        }
    }

    #endregion
}

using System;
using System.Windows.Forms;
using Marketing;
using AtooERP_Marketing.Profile.Profile_instance;

namespace PRODUCT_GENERATOR
{
    /// <summary>
    /// Example implementation of Product_generator_form using the enhanced Product_template
    /// with encapsulated profile management
    /// </summary>
    public partial class Product_generator_form : Form
    {
        private Product_template currentTemplate;
        
        // Profile UI management (similar to Product_insert.cs)
        public Marketing.Profile.Profile_instance profile_instance;
        public ProfileInstance_insert ProfileInstance_insert;
        
        // UI Controls (you would add these in the designer)
        private Panel profilePanel;
        private CheckBox useBaseProductCheckBox;
        private ComboBox baseProductComboBox;
        private Button generateButton;
        private Button configureProfileButton;

        public Product_generator_form()
        {
            InitializeComponent();
            InitializeProductTemplate();
        }

        private void InitializeProductTemplate()
        {
            currentTemplate = new Product_template();
            
            // Set up event handlers
            generateButton.Click += GenerateProducts_Click;
            configureProfileButton.Click += ConfigureProfile_Click;
            useBaseProductCheckBox.CheckedChanged += UseBaseProduct_CheckedChanged;
        }

        #region Profile Management UI

        /// <summary>
        /// Configure profile settings - opens profile editing interface
        /// Similar to profileLayoutControlGroup_Shown in Product_insert.cs
        /// </summary>
        private void ConfigureProfile_Click(object sender, EventArgs e)
        {
            try
            {
                // Prepare the template with current settings
                UpdateTemplateFromUI();
                
                // Initialize profile if not already done
                if (currentTemplate.Profile_instance == null)
                {
                    // This will initialize the profile based on use_base_product setting
                    currentTemplate.GenerateProducts(); // This calls InitializeProfile internally
                }

                // Get the initialized profile
                profile_instance = currentTemplate.Profile_instance;
                
                if (profile_instance != null)
                {
                    // Create the profile editing UI (based on Product_insert.cs pattern)
                    ProfileInstance_insert = new ProfileInstance_insert(
                        profile_instance, 
                        currentTemplate.use_base_product ? currentTemplate.Base_product : null
                    );
                    
                    // Show profile configuration dialog or panel
                    ShowProfileConfigurationDialog();
                }
                else
                {
                    MessageBox.Show("Unable to initialize profile. Please check your base product selection.", 
                        "Profile Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error configuring profile: {ex.Message}", 
                    "Profile Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Shows profile configuration in a dialog or embedded panel
        /// </summary>
        private void ShowProfileConfigurationDialog()
        {
            // Option 1: Show in a dialog
            Form profileDialog = new Form()
            {
                Text = "Configure Product Profile",
                Size = new System.Drawing.Size(800, 600),
                StartPosition = FormStartPosition.CenterParent
            };
            
            ProfileInstance_insert.Dock = DockStyle.Fill;
            profileDialog.Controls.Add(ProfileInstance_insert);
            
            if (profileDialog.ShowDialog() == DialogResult.OK)
            {
                // Profile changes are automatically saved by ProfileInstance_insert
                MessageBox.Show("Profile configuration saved successfully!", 
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Option 2: Show in embedded panel (uncomment if preferred)
            // profilePanel.Controls.Clear();
            // profilePanel.Controls.Add(ProfileInstance_insert);
            // ProfileInstance_insert.Dock = DockStyle.Fill;
        }

        #endregion

        #region Product Generation

        /// <summary>
        /// Main product generation method
        /// </summary>
        private void GenerateProducts_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs
                if (!ValidateInputs())
                    return;

                // Update template with current UI values
                UpdateTemplateFromUI();

                // Save any profile changes before generating
                if (ProfileInstance_insert != null)
                {
                    ProfileInstance_insert.save_all();
                }

                // Generate products with profiles
                generateButton.Enabled = false;
                generateButton.Text = "Generating...";
                
                currentTemplate.GenerateProducts();
                
                MessageBox.Show("Products generated successfully with profiles!", 
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating products: {ex.Message}", 
                    "Generation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                generateButton.Enabled = true;
                generateButton.Text = "Generate Products";
            }
        }

        /// <summary>
        /// Updates the Product_template with current UI values
        /// </summary>
        private void UpdateTemplateFromUI()
        {
            currentTemplate.use_base_product = useBaseProductCheckBox.Checked;
            
            if (currentTemplate.use_base_product && baseProductComboBox.SelectedValue != null)
            {
                currentTemplate.base_product = (int)baseProductComboBox.SelectedValue;
                // Load the base product object if needed
                currentTemplate.Base_product = new Commercial.Product(currentTemplate.base_product);
            }

            // Update other template properties from UI controls
            // currentTemplate.general_name = generalNameTextBox.Text;
            // currentTemplate.reference = referenceTextBox.Text;
            // etc...
        }

        /// <summary>
        /// Validates user inputs before generation
        /// </summary>
        private bool ValidateInputs()
        {
            if (currentTemplate.use_base_product && currentTemplate.base_product <= 0)
            {
                MessageBox.Show("Please select a base product.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (currentTemplate.Size_list == null || currentTemplate.Size_list.Count == 0)
            {
                MessageBox.Show("Please specify at least one size.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handle base product checkbox changes
        /// </summary>
        private void UseBaseProduct_CheckedChanged(object sender, EventArgs e)
        {
            baseProductComboBox.Enabled = useBaseProductCheckBox.Checked;
            
            // Reset profile when switching modes
            currentTemplate.Profile_instance = null;
            if (ProfileInstance_insert != null)
            {
                profilePanel.Controls.Clear();
                ProfileInstance_insert = null;
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Load available base products into combo box
        /// </summary>
        private void LoadBaseProducts()
        {
            try
            {
                // Load products that can be used as base products
                var baseProducts = Commercial.Product.getListe(); // Adjust method name as needed
                baseProductComboBox.DataSource = baseProducts;
                baseProductComboBox.DisplayMember = "name";
                baseProductComboBox.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading base products: {ex.Message}", 
                    "Loading Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
} 
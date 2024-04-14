namespace PasswordGenerator
{
    public partial class App : Form
    {
        public App()
        {
            InitializeComponent();
        }

        private void App_Load(object sender, EventArgs e)
        {
            SetInitialSettings();
        }

        private void SetInitialSettings()
        {
            Size = new System.Drawing.Size(400, 600);
            Text = "Password Generator";
            BackColor = Color.White;
        }
    }
}

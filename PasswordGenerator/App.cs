using System.Windows.Forms;

namespace PasswordGenerator
{
    public partial class App : Form
    {
        private TextBox PasswordField;
        private Button HasDigits;
        private Button HasSpecials;
        private NumericUpDown LengthField;

        private Color ThisGray = Color.FromArgb(240, 240, 240);
        private Color ThisBlue = Color.FromArgb(138, 170, 229);
        private Color ThisRed = Color.FromArgb(205, 74, 76);
        private Color ThisGreen = Color.FromArgb(119, 221, 119);

        public App()
        {
            InitializeComponent();
            SetInitialSettings();
            CreateAppElements();
        }

        private void App_Load(object sender, EventArgs e)
        {
        }

        private void SetInitialSettings()
        {
            Size = new Size(400, 600);
            Text = "Password Generator";
            BackColor = ThisBlue;
            AutoSize = false;
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximumSize = new Size(800, 800);
            Icon = new Icon("..\\..\\..\\..\\pg-logo.ico");
        }

        private void CreateAppElements()
        {
            TableLayoutPanel row;
            TableLayoutPanel tableLayoutPanel = TLP(7);

            // "Password generator" label
            tableLayoutPanel.Controls.Add(HeaderText("Password Generator"));

            // Field for password and copy button
            row = GetRow(75, 25);
            row.Controls.Add(PasswordField = GetPasswordField(), 0, 0);
            row.Controls.Add(CopyButton(), 1, 0);
            tableLayoutPanel.Controls.Add(row);

            // Button for password generation
            tableLayoutPanel.Controls.Add(GenerateButton());

            // "Settings" label
            tableLayoutPanel.Controls.Add(HeaderText("Generation settings", 25));

            // Length option
            row = GetRow(60, 40);
            row.Controls.Add(SettingsLabel("Password length"), 0, 0);
            row.Controls.Add(LengthField = NUD(), 1, 0);
            tableLayoutPanel.Controls.Add(row);

            // HasDigits and HasSpecials labels
            tableLayoutPanel.Controls.Add(SettingsLabel("Does it have"));
                
            // Flags for digits and special characters
            row = GetRow(50, 50);
            row.Controls.Add(HasDigits = CheckBoxButton("Digits?"), 0, 0);
            row.Controls.Add(HasSpecials = CheckBoxButton("Special chars?"), 1, 0);
            tableLayoutPanel.Controls.Add(row);

            row = GetRow(0, 1);
            tableLayoutPanel.Controls.Add(row);


            Controls.Add(tableLayoutPanel);
        }

        private Label HeaderText(string text, int fontsize = 30)
        {
            var label = new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black,
            };
            label.Font = new Font("Calibri", fontsize, FontStyle.Bold);
            label.ForeColor = ThisGray;
            label.Padding = new Padding(5);
            label.AutoSize = true;

            return label;
        }

        private TextBox GetPasswordField() => new ()
        {
            Dock = DockStyle.Fill,
            Multiline = true,
            TextAlign = HorizontalAlignment.Center,
            ReadOnly = true,
            Enabled = true,
            Font = new Font("Calibri", 18),
            ForeColor = Color.Red,
            BorderStyle = BorderStyle.None,
            HideSelection = true,
            TabStop = false,
        };

        private Button CopyButton()
        {
            Bitmap img = new("..\\..\\..\\..\\copy.png");
            img = new Bitmap(img, 50, 50);
            var button = new Button()
            {
                Dock = DockStyle.Fill,
                BackColor = ThisBlue,
                Image = img,
                ImageAlign = ContentAlignment.MiddleCenter
            };
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = this.BackColor;
            button.Click += CopyButton_OnClick;

            return button;
        }

        private Button GenerateButton()
        {
            var button = new Button()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(80, 200, 120),
                Text = "Generate password",
                Font = new Font("Calibri", 20),
            };
            button.FlatStyle = FlatStyle.Standard;
            button.FlatAppearance.BorderColor = this.BackColor;
            button.Click += GenerateButton_OnClick;

            return button;
        }

        private Label SettingsLabel(string text) => new Label()
        {
            Text = text,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Calibri", 17),
            ForeColor = Color.Black,

        };

        private NumericUpDown NUD() => 
        new NumericUpDown()
        {
            Anchor = AnchorStyles.None,
            Minimum = 15,
            Maximum = 50,
            Value = 15,
            Increment = 1,
            Font = new Font("Calibri", 20),
            ForeColor = Color.Black,
            BackColor = Color.FromArgb(240, 240, 240),
            Padding = new Padding(5),
            BorderStyle = BorderStyle.FixedSingle,
            TextAlign = HorizontalAlignment.Center,
            TabStop = false,
        };

        private Button CheckBoxButton(string text)
        {
            var button = new Button()
            {
                Dock = DockStyle.Fill,
                BackColor = ThisRed,
                Text = text,
                ForeColor = Color.White,
                Font = new Font("Calibri", 12),
                FlatStyle = FlatStyle.Flat,
            };
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseDownBackColor = Color.FromArgb(41, 128, 185);
            button.Click += CBButton_OnClick;

            return button;
        }

        private void CopyButton_OnClick(object sender, EventArgs e)
        {
            if (PasswordField.Text != null && PasswordField.Text != "")
                Clipboard.SetText(PasswordField.Text);
        }

        private void GenerateButton_OnClick(object sender, EventArgs e)
        {
            bool hasDigits, hasSpecials;
            hasDigits = ColorToBool(HasDigits);
            hasSpecials = ColorToBool(HasSpecials);
            int length = (int)LengthField.Value;

            var password = PasswordGenerator.GeneratePassword(length, hasDigits, hasSpecials);
            PasswordField.Text = password;
        }

        private void CBButton_OnClick(object s, EventArgs e)
        {
            var button = s as Button;
            if (button.BackColor == ThisGreen)
            {
                button.BackColor = ThisRed;
                button.ForeColor = Color.White;
            }
            else
            {
                button.BackColor = ThisGreen;
                button.ForeColor = Color.Black;
            }
        }

        private static TableLayoutPanel TLP(int rows)
        {
            var table = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
            };

            table.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 10));

            return table;
        }

        private TableLayoutPanel GetRow(int firstPercent, int secondPercent)
        {
            var table = new TableLayoutPanel() { Anchor = AnchorStyles.Top, Dock = DockStyle.Fill, };
            table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, firstPercent));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, secondPercent));

            return table;
        }

        private bool ColorToBool(Button button) => button.BackColor == ThisGreen;
    }
}

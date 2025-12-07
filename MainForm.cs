namespace PasswordHasher
{
    public partial class MainForm : Form
    {
        private const int maxPasswordLenght = 15;

        public Dictionary<string, (string hash, string salt)> MockDatabse = new Dictionary<string, (string, string)>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            var login = LoginBox.Text;
            var password = PasswordBox.Text;

            if (!MockDatabse.TryGetValue(login, out var pair))
            {
                MessageBox.Show("Не существует пользователя с таким логином!", 
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (!PasswordHandler.VerifyPassword(password, pair.hash, pair.salt))
            {
                MessageBox.Show("Не верный логин или пароль!",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            MessageBox.Show("Вход успешен!",
                    "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SighinButton_Click(object sender, EventArgs e)
        {
            var login = LoginBox.Text;
            var password = PasswordBox.Text;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Оба поля должны быть заполнены!",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (MockDatabse.ContainsKey(login))
            {
                MessageBox.Show("Пользователь с таким логином уже существует!",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (password.Length > maxPasswordLenght)
            {
                MessageBox.Show($"Максимальная длина пароля - {maxPasswordLenght}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            (string hash, string salt) proccesedPassword = PasswordHandler.HashPassword(password);

            MockDatabse.Add(login, (proccesedPassword.hash, proccesedPassword.salt));

            DebugGrid.Rows.Add(login, password, proccesedPassword.hash, proccesedPassword.salt);
        }
    }
}

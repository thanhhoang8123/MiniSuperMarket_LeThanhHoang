using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Net.Http.Json;
using System.Text.Json;
using MiniSupermarket.WinForms.Services;

namespace MiniSupermarket.WinForms
{
    public partial class FormLogin : Form
    {

        

        public FormLogin()
        {
            InitializeComponent();
        }

        // Sự kiện khi người dùng bấm nút Đăng nhập

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPass.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(
                    "Vui lòng nhập đầy đủ tài khoản và mật khẩu!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnLogin.Enabled = false;

                bool success = await ApiClientService.LoginAsync(
                    username, password);

                if (!success)
                {
                    MessageBox.Show(
                        "Đăng nhập thất bại. Vui lòng kiểm tra tài khoản và mật khẩu.",
                        "Đăng nhập thất bại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show(
                    $"Đăng nhập thành công với quyền: {SessionManager.CurrentRole}",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                FormMainShell mainForm = new FormMainShell();

                Hide();
                mainForm.ShowDialog();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi đăng nhập hoặc kết nối đến Server:\n" + ex.Message,
                    "Lỗi hệ thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Enabled = true;
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}

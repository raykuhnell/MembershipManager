using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MembershipManager
{
    public partial class UserWindow : Window
    {
        MembershipUser currentUser;

        public UserWindow()
        {
            InitializeComponent();
        }

        public UserWindow(MembershipUser user)
        {
            InitializeComponent();

            currentUser = user;

            txtUserName.Text = user.UserName;
            txtEmail.Text = user.Email;
            txtPasswordQuestion.Text = user.PasswordQuestion;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (currentUser == null)
            {
                MembershipUser u;
                if (!String.IsNullOrEmpty(txtPasswordQuestion.Text) && !String.IsNullOrEmpty(txtPasswordAnswer.Text)) 
                {
                    MembershipCreateStatus status;
                    u = Membership.CreateUser(txtUserName.Text, txtPassword.Text, txtEmail.Text, txtPasswordQuestion.Text, txtPasswordAnswer.Text, chkIsApproved.IsChecked == true, out status);
                }
                else
                {
                    u = Membership.CreateUser(txtUserName.Text, txtPassword.Text, txtEmail.Text);
                }
            }
            else
            {
                currentUser.Email = txtEmail.Text;
            }

            this.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this user?") == MessageBoxResult.Yes)
            {
                Membership.DeleteUser(currentUser.UserName);
                this.Close();
            }
        }

        private void btnResetPassword_Click(object sender, RoutedEventArgs e)
        {
            string password = currentUser.ResetPassword();
        }
    }
}

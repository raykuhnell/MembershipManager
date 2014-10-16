using MembershipManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ObservableCollection<CheckedListItem> RoleItems { get; set; }

        public UserWindow()
        {
            InitializeComponent();

            RoleItems = new ObservableCollection<CheckedListItem>();
            foreach (string role in Roles.GetAllRoles())
            {
                RoleItems.Add(new CheckedListItem() { Text = role });
            }

            DataContext = this;
        }

        public UserWindow(MembershipUser user) : this()
        {
            currentUser = user;

            txtUserName.Text = user.UserName;
            txtEmail.Text = user.Email;
            txtPasswordQuestion.Text = user.PasswordQuestion;
            chkIsApproved.IsChecked = user.IsApproved;

            txtUserName.IsEnabled = false;

            foreach (string role in Roles.GetRolesForUser(user.UserName))
            {
                foreach (var item in RoleItems)
                {
                    if (role == item.Text)
                    {
                        item.IsChecked = true;
                    }
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentUser == null)
                {
                    if (!String.IsNullOrEmpty(txtPasswordQuestion.Text) && !String.IsNullOrEmpty(txtPasswordAnswer.Text))
                    {
                        MembershipCreateStatus status;
                        currentUser = Membership.CreateUser(txtUserName.Text, txtPassword.Password, txtEmail.Text, txtPasswordQuestion.Text, txtPasswordAnswer.Text, chkIsApproved.IsChecked == true, out status);
                    }
                    else
                    {
                        currentUser = Membership.CreateUser(txtUserName.Text, txtPassword.Password, txtEmail.Text);
                    }
                }
                else
                {
                    currentUser.Email = txtEmail.Text;
                    currentUser.IsApproved = chkIsApproved.IsChecked == true;
                    Membership.UpdateUser(currentUser);

                    if (!String.IsNullOrEmpty(txtPassword.Password) && MessageBox.Show("Entering a password for this user will change their password.  Are you sure you want to do this?", "MembershipManager", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        currentUser.ChangePassword(currentUser.ResetPassword(), txtPassword.Password);
                    }
                }

                foreach (var roleItem in RoleItems)
                {
                    if (roleItem.IsChecked && !Roles.IsUserInRole(currentUser.UserName, roleItem.Text))
                    {
                        Roles.AddUserToRole(currentUser.UserName, roleItem.Text);
                    }
                    else if (Roles.IsUserInRole(currentUser.UserName, roleItem.Text))
                    {
                        Roles.RemoveUserFromRole(currentUser.UserName, roleItem.Text);
                    }
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred saving the user information. " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this user?", "MembershipManager", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Membership.DeleteUser(currentUser.UserName);
                this.Close();
            }
        }

        private void btnResetPassword_Click(object sender, RoutedEventArgs e)
        {
            string password = currentUser.ResetPassword();
            var passwordResetWindow = new PasswordResetWindow(password);
            passwordResetWindow.ShowDialog();
        }
    }
}

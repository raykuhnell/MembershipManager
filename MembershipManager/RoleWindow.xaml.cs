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
    public partial class RoleWindow : Window
    {
        private string currentRoleName;

        public RoleWindow()
        {
            InitializeComponent();
        }

        public RoleWindow(string roleName)
        {
            InitializeComponent();

            currentRoleName = roleName;

            txtName.Text = roleName;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(currentRoleName))
                {
                    Roles.CreateRole(txtName.Text);
                }
                else if (currentRoleName != txtName.Text)
                {
                    var users = Roles.GetUsersInRole(currentRoleName);
                    Roles.DeleteRole(currentRoleName);
                    Roles.CreateRole(txtName.Text);
                    Roles.AddUsersToRole(users, txtName.Text);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred saving the role information. " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this role?", "MembershipManager", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Roles.DeleteRole(currentRoleName);
                this.Close();
            }
        }
    }
}

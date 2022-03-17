using LibraryManagement.Views.SettingManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManagement.ViewModels.SettingVM
{
    public partial class SettingViewModel : BaseViewModel
    {

        public ICommand OpenRoleSettingPageCM { get; set; }
        public ICommand OpenGeneralSettingPageCM { get; set; }
        public ICommand GeneralFirstLoadCM { get; set; }

        public SettingViewModel()
        {
            RoleList = new ObservableCollection<string>();
            RoleList.Add("ss");
            RoleList.Add("ss");
            RoleList.Add("ss");

            GeneralFirstLoadCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                GeneralFirstLoadFunc();
            });
            OpenRoleSettingPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new RoleSettingPage();
            });
            OpenGeneralSettingPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new GeneralSettingPage();
            });
            EditRoleCM = new RelayCommand<StackPanel>((p) => { return true; }, (p) =>
            {
                p.IsEnabled = true;
            });
            DeleteRoleCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManagement.ViewModels.StatisticalVM
{
    public class StatisticalViewModel : BaseViewModel
    {
        private ObservableCollection<String> statisticGenreList;
        public ObservableCollection<String> StatisticGenreList
        {
            get { return statisticGenreList; }
            set { statisticGenreList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<String> statisticLateList;
        public ObservableCollection<String> StatisticLateList
        {
            get { return statisticLateList; }
            set { statisticLateList = value; OnPropertyChanged(); }
        }


        private ComboBoxItem selectedGenrePeriod;
        public ComboBoxItem SelectedGenrePeriod
        {
            get { return selectedGenrePeriod; }
            set { selectedGenrePeriod = value; OnPropertyChanged(); }
        }

        private string selectedGenreTime;
        public string SelectedGenreTime
        {
            get { return selectedGenreTime; }
            set { selectedGenreTime = value; OnPropertyChanged(); }
        }

        private ComboBoxItem selectedLatePeriod;
        public ComboBoxItem SelectedLatePeriod
        {
            get { return selectedLatePeriod; }
            set { selectedLatePeriod = value; OnPropertyChanged(); }
        }

        private string selectedLateTime;
        public string SelectedLateTime
        {
            get { return selectedLateTime; }
            set { selectedLateTime = value; OnPropertyChanged(); }
        }



        private int selectedYear;
        public int SelectedYear
        {
            get { return selectedYear; }
            set { selectedYear = value; OnPropertyChanged(); }
        }


        public ICommand ChangeGenrePeriodCM { get; set; }
        public ICommand ChangeLatePeriodCM { get; set; }



        public StatisticalViewModel()
        {
            StatisticGenreList = new ObservableCollection<string>();
            StatisticGenreList.Add("sss");
            StatisticGenreList.Add("sss");
            StatisticGenreList.Add("sss");
            StatisticGenreList.Add("sss");
            StatisticGenreList.Add("sss");
            StatisticGenreList.Add("sss");
            StatisticGenreList.Add("sss");


            ChangeGenrePeriodCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedGenrePeriod != null)
                {
                    switch (SelectedGenrePeriod.Content.ToString())
                    {
                        case "Theo năm":
                            {
                                if (SelectedGenreTime != null)
                                {
                                    if (SelectedGenreTime.Length == 4)
                                        SelectedYear = int.Parse(SelectedGenreTime);
                                    //load by year
                                }
                                return;
                            }
                        case "Theo tháng":
                            {
                                if (SelectedGenreTime != null)
                                {
                                    //load by month
                                }
                                return;
                            }
                    }
                }
            });
            ChangeLatePeriodCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedGenrePeriod != null)
                {
                    switch (SelectedGenrePeriod.Content.ToString())
                    {
                        case "Theo năm":
                            {
                                if (SelectedGenreTime != null)
                                {
                                    if (SelectedGenreTime.Length == 4)
                                        SelectedYear = int.Parse(SelectedGenreTime);
                                    //load by year
                                }
                                return;
                            }
                        case "Theo tháng":
                            {
                                if (SelectedGenreTime != null)
                                {
                                    //load by month
                                }
                                return;
                            }
                    }
                }
            });
        }



    }
}

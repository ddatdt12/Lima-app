using LibraryManagement.DTOs;
using LibraryManagement.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace LibraryManagement.ViewModels.StatisticalVM
{
    public class StatisticalViewModel : BaseViewModel
    {
        private ObservableCollection<BorrowedGenreReportDetailDTO> statisticGenreList;
        public ObservableCollection<BorrowedGenreReportDetailDTO> StatisticGenreList
        {
            get { return statisticGenreList; }
            set { statisticGenreList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<DelayReturnBookReportDetailDTO> statisticLateList;
        public ObservableCollection<DelayReturnBookReportDetailDTO> StatisticLateList
        {
            get { return statisticLateList; }
            set { statisticLateList = value; OnPropertyChanged(); }
        }


        private string selectedGenreYear;
        public string SelectedGenreYear
        {
            get { return selectedGenreYear; }
            set { selectedGenreYear = value; OnPropertyChanged(); }
        }

        private string selectedGenreMonth;
        public string SelectedGenreMonth
        {
            get { return selectedGenreMonth; }
            set { selectedGenreMonth = value; OnPropertyChanged(); }
        }

        private DateTime? selectedLateTime;
        public DateTime? SelectedLateTime
        {
            get { return selectedLateTime; }
            set { selectedLateTime = value; OnPropertyChanged(); }
        }

        private int totalRent;
        public int TotalRent
        {
            get { return totalRent; }
            set { totalRent = value; OnPropertyChanged(); }
        }

        public ICommand GetGenreStatCM { get; set; }
        public ICommand GetLateStatCM { get; set; }

        public StatisticalViewModel()
        {
            StatisticGenreList = new ObservableCollection<BorrowedGenreReportDetailDTO>();
            StatisticLateList = new ObservableCollection<DelayReturnBookReportDetailDTO>();

            GetGenreStatCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                try
                {
                    (BorrowedGenreReportDTO list, string mes) = StatisticService.Ins.GetBookBorrowingStatisticsReportByGenre(int.Parse(SelectedGenreMonth.Remove(0, 6)), int.Parse(SelectedGenreYear));
                    if (list is null)
                    {
                        MessageBox.Show(mes);
                        return;
                    }
                    StatisticGenreList = new ObservableCollection<BorrowedGenreReportDetailDTO>(list.borrowedGenreReportDetails);
                    TotalRent = list.totalNumberOfBorrowings;

                }
                catch (Exception e)
                {

                    MessageBox.Show(e.Message);
                }
            });
            GetLateStatCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                try
                {
                    (DelayReturnBookReportDTO list, string mes) = StatisticService.Ins.GetDelayBookStatsReport((DateTime)SelectedLateTime);
                    if (list is null || mes != null)
                    {
                        MessageBox.Show(mes);
                        return;
                    }
                    StatisticLateList = new ObservableCollection<DelayReturnBookReportDetailDTO>(list.delayReturnBookReportDetails);
                }
                catch (Exception e)
                {

                    MessageBox.Show(e.Message);
                }
            });
        }



    }
}

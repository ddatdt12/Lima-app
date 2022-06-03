using LibraryManagement.DTOs;
using LibraryManagement.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Forms;
using System.Windows;

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
        public ICommand ExportGenreStatCM { get; set; }
        public ICommand ExportLateStatCM { get; set; }

        public StatisticalViewModel()
        {
            StatisticGenreList = new ObservableCollection<BorrowedGenreReportDetailDTO>();
            StatisticLateList = new ObservableCollection<DelayReturnBookReportDetailDTO>();
            selectedLateTime = DateTime.Now;

            GetGenreStatCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                try
                {
                    (BorrowedGenreReportDTO list, string mes) = StatisticService.Ins.GetBookBorrowingStatisticsReportByGenre(int.Parse(SelectedGenreMonth.Remove(0, 6)), int.Parse(SelectedGenreYear));
                    if (list is null)
                    {
                        System.Windows.MessageBox.Show(mes, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    StatisticGenreList = new ObservableCollection<BorrowedGenreReportDetailDTO>(list.borrowedGenreReportDetails);
                    TotalRent = list.totalNumberOfBorrowings;

                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show(e.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
            GetLateStatCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                try
                {
                    (DelayReturnBookReportDTO list, string mes) = StatisticService.Ins.GetDelayBookStatsReport((DateTime)SelectedLateTime);
                    if (list is null || mes != null)
                    {
                        System.Windows.MessageBox.Show(mes, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    StatisticLateList = new ObservableCollection<DelayReturnBookReportDetailDTO>(list.delayReturnBookReportDetails);
                }
                catch (Exception e)
                {

                    System.Windows.MessageBox.Show(e.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
            ExportGenreStatCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ExportGenreStatFunc();
            });
            ExportLateStatCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ExportLateStatFunc();
            });
        }


        public void ExportGenreStatFunc()
        {
            if (StatisticGenreList.Count == 0) return;
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    app.Visible = false;
                    Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(1);
                    Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[1];

                    ws.Cells[1, 1] = "Thư viện Lima";
                    ws.Cells[2, 1] = "Ngày xuất: " + DateTime.Now.ToString("dd/MM/yyyy");
                    ws.Cells[3, 1] = "Tổng số lượt mượn: " + TotalRent;

                    ws.Cells[5, 1] = "Thể loại";
                    ws.Cells[5, 2] = "Lượt mượn";
                    ws.Cells[5, 3] = "Tỉ lệ";

                    int i2 = 6;
                    foreach (var item in StatisticGenreList)
                    {

                        ws.Cells[i2, 1] = item.Genre.name;
                        ws.Cells[i2, 2] = item.NumberOfBorrowings;
                        ws.Cells[i2, 3] = item.rate;

                        i2++;
                    }
                    ws.SaveAs(sfd.FileName);
                    wb.Close();
                    app.Quit();

                    Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;

                    System.Windows.MessageBox.Show("Xuất file thành công", "Thông báo", MessageBoxButton.OK);
                }
            }
        }

        public void ExportLateStatFunc()
        {
            if (StatisticLateList.Count == 0) return;
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    app.Visible = false;
                    Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(1);
                    Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[1];

                    ws.Cells[1, 1] = "Thư viện Lima";
                    ws.Cells[2, 1] = "Ngày xuất: " + DateTime.Now.ToString("dd/MM/yyyy");

                    ws.Cells[4, 1] = "Mã sách";
                    ws.Cells[4, 2] = "Tên sách";
                    ws.Cells[4, 3] = "Ngày mượn";
                    ws.Cells[4, 4] = "Số ngày trễ";

                    int i2 = 5;
                    foreach (var item in StatisticLateList)
                    {

                        ws.Cells[i2, 1] = item.BorrowingReturnCard.bookInfo.id;
                        ws.Cells[i2, 2] = item.BorrowingReturnCard.bookInfo.Book.baseBook.name;
                        ws.Cells[i2, 3] = item.BorrowingReturnCard.borrowingDate.ToString("dd/MM/yyyy");
                        ws.Cells[i2, 4] = item.numberOfDelayReturn;

                        i2++;
                    }
                    ws.SaveAs(sfd.FileName);
                    wb.Close();
                    app.Quit();

                    Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;

                    System.Windows.MessageBox.Show("Xuất file thành công", "Thông báo", MessageBoxButton.OK);
                }
            }
        }
    }
}

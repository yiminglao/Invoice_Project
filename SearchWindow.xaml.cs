using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace CS3280_Group5_Project
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// Allows the user to view all Invoices, apply filters, 
    /// and select an Invoice to load into the Main Window.
    /// </summary>
    public partial class SearchWindow : Window
    {
        /// <summary>
        /// Invoice Search object. provides invoice list and filters results
        /// </summary>
        private InvoiceSearch InvSearch;

        public int SelectedInvoiceNumber { get; set; }

        /// <summary>
        /// SearchWindow Construtor
        /// </summary>
        public SearchWindow()
        {
            try
            {
                InitializeComponent();
                InvSearch = new InvoiceSearch();
                SelectedInvoiceNumber = 0;

                //Fill datagrid and combo boxes
                dgInvoices.ItemsSource = InvSearch.Invoices;
                FillComboBoxes();
            }
            catch (Exception ex)
            {
                //Throw a custom-trace exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Fills filter combo boxes with appropriate filters
        /// </summary>
        private void FillComboBoxes()
        {
            cbFilterNumber.Items.Clear();
            cbFilterTotal.Items.Clear();

            foreach(Invoice invoice in InvSearch.Invoices)
            {
                cbFilterNumber.Items.Add(invoice.InvoiceNum);
                cbFilterTotal.Items.Add(invoice.TotalCharge);
            }
        }

        /// <summary>
        /// Event handler for all button clicks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Check which button was clicked and perform the action

                Button btn = sender as Button;

                //Open invoice button clicked
                if (btn.Equals(btnOpenInvoice))
                {
                    //The invoiceId of the selected item should be set to
                    //a property in this window, for the main window to grab.
                    if(dgInvoices.SelectedIndex > -1)
                    {
                        SelectedInvoiceNumber = (dgInvoices.SelectedItem as Invoice).InvoiceNum;
                        DialogResult = true;
                        Close();
                    }
                }

                //cancel button clicked
                else if (btn.Equals(btnCancel))
                {
                    Close();
                }

                //Apply filter button clicked
                else if (btn.Equals(btnApplyFilter))
                {
                    //The list that is the source of the datagrid should
                    //be filtered in a separate class and refreshed as the
                    //itemssource of the datagrid
                    InvSearch.InvoiceNumFilter = cbFilterNumber.Text;
                    InvSearch.InvoiceDateFilter = dpFilterDate.Text;
                    InvSearch.TotalChargeFilter = cbFilterTotal.Text;
                    InvSearch.ApplyFilters();
                    dgInvoices.ItemsSource = InvSearch.Invoices;

                    //Refresh combo boxes
                    FillComboBoxes();

                    //Rewrite filters to combo boxes
                    cbFilterNumber.Text = InvSearch.InvoiceNumFilter;
                    cbFilterTotal.Text = InvSearch.TotalChargeFilter;
                }

                //Clear filter button clicked
                else
                {
                    //The filter combo boxes and datepicker should be reset, and 
                    //the original list be refreshed on the datagrid
                    cbFilterNumber.SelectedIndex = -1;
                    cbFilterNumber.Text = "";
                    cbFilterTotal.SelectedIndex = -1;
                    cbFilterTotal.Text = "";
                    dpFilterDate.Text = "";

                    InvSearch.ClearFilters();
                    dgInvoices.ItemsSource = InvSearch.Invoices;

                    //Refresh Combo boxes
                    FillComboBoxes();
                }
            }
            catch (Exception ex)
            {
                //Throw a custom-trace exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}

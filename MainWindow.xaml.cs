using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows;

namespace CS3280_Group5_Project
{
    /// Yi Lao-- main window class
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Initialize invoice system class
        InvoiceSystem invoiceSys;


        /// <summary>
        /// default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            //close main window
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            //disable Invoice group box
            gbInvoice.IsEnabled = false;
            //disable item group box
            gbItem.IsEnabled = false;
            // new invoice system
            invoiceSys = new InvoiceSystem();
            //load all item
            cbItem.ItemsSource = invoiceSys.ItemList;
        }

        /// <summary>
        /// Error handler to display errors
        /// </summary>
        /// <param name="sClass">class name</param>
        /// <param name="sMethod">method name</param>
        /// <param name="sMessage">error message</param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                //show message box
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                //output error log to file
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }

        /// <summary>
        /// this is the add invoice button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //lock all buttons except save invoice.
                //Allow user to enter a date and submit to get the invoice number
                //After saving, clear items list and allow items to be added
                //empty data grid
                dgItem.ItemsSource = "";
                //refresh data grid
                dgItem.Items.Refresh();
                //lock all button
                buttonEvent(false);
                //enable invoice group box 
                gbInvoice.IsEnabled = true;
                //update label to empty
                lblMoney.Content = "";
                //update label to empty
                lblPriceNum.Content = "";
                //update default selected item
                cbItem.SelectedIndex = -1;
                lblInvoiceNum.Content = "";
                dpInvoiceDate.Text = "";

            }
            catch (Exception ex)
            {
                //handle error method
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// this is the method lock or unlock button
        /// </summary>
        /// <param name="unLock">true or false</param>
        private void buttonEvent(bool unLock)
        {
            try
            {
                //disable add invoice button
                btnAddInvoice.IsEnabled = unLock;
                //disable delete invoice button
                btnDelInvoice.IsEnabled = unLock;
                //disable edit invoice button
                btnEditInvoice.IsEnabled = unLock;
                //disable Search Invoice button
                btnSearchInvoice.IsEnabled = unLock;
                //disable Update Item button
                btnUpdateItem.IsEnabled = unLock;
                //enable invoice group box 
                gbInvoice.IsEnabled = unLock;
                //disable item group box
                gbItem.IsEnabled = unLock;
            }
            catch (Exception ex)
            {
                //handle error method
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// this is the delete invoice button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lblInvoiceNum.Content != "" && lblInvoiceNum.Content != null)
                {
                    //message box result
                    MessageBoxResult result;
                    //prompt message box and hold result
                    result = MessageBox.Show("Are you suer want to delete Invoice?", "confirm", MessageBoxButton.YesNo);
                    //if user confirm to delete
                    if (result == MessageBoxResult.Yes)
                    {
                        //Delete the invoice after deleting all line items
                        invoiceSys.deleteInvoice();
                        //unlock button
                        buttonEvent(true);
                        //lock invoice group box
                        gbInvoice.IsEnabled = false;
                        //lock item group box
                        gbItem.IsEnabled = false;
                        //set invoice number to empty
                        lblInvoiceNum.Content = "";
                        //set invoice date to empty
                        dpInvoiceDate.Text = "";
                        //update label to empty
                        lblMoney.Content = "";
                        //update label to empty
                        lblPriceNum.Content = "";
                        //update default selected item
                        cbItem.SelectedIndex = -1;
                        //empty data grid
                        dgItem.ItemsSource = "";
                        //refresh data grid
                        dgItem.Items.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                //handle error method
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// this is the edit invoice button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditInvoice_Click(object sender, RoutedEventArgs e)
        {
            //Allow adding and removing items from the invoice
            try
            {

                if (lblInvoiceNum.Content != "" && lblInvoiceNum.Content != null)
                {
                    //lock button
                    buttonEvent(false);
                    //lock invoice group box
                    gbInvoice.IsEnabled = false;
                    //unlock item group box
                    gbItem.IsEnabled = true;
                    //update label to empty
                    lblMoney.Content = "";
                    //update label to empty
                    lblPriceNum.Content = "";
                    //update default selected item
                    cbItem.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                //handle error method
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// this is the update invoice button
        /// this button open item defs Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //hide main window
                Hide();
                //init new window and show update window
                new ItemDefsWindow().ShowDialog();
                invoiceSys.updateItemList();
                cbItem.ItemsSource = invoiceSys.ItemList;
                //reset invoice number label
                lblInvoiceNum.Content = "";
                //update label to empty
                lblMoney.Content = "";
                //update label to empty
                lblPriceNum.Content = "";
                //update default selected item
                cbItem.SelectedIndex = -1;
                //empty data grid
                dgItem.ItemsSource = "";
                //reset date
                dpInvoiceDate.Text = "";
                //refresh data grid
                dgItem.Items.Refresh();
                this.Show();
            }
            catch (Exception ex)
            {
                //handle error method
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                Show();
            }
        }
        /// <summary>
        /// this is the save invoice button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Create a new invoice and get back the invoice number
                if (dpInvoiceDate.Text != "")
                {
                    //add invoice to database by selected day
                    invoiceSys.addInvoice(dpInvoiceDate.Text);
                    //update invoice number label
                    lblInvoiceNum.Content = invoiceSys.currentInvoice.InvoiceNum;
                    //lock button
                    buttonEvent(false);
                    //lock invoice group box
                    gbInvoice.IsEnabled = false;
                    //unlock item group box
                    gbItem.IsEnabled = true;
                }
                else
                {
                    //prompt user to select date
                    MessageBox.Show("Please select Invoice date", "Select date");
                }
            }
            catch (Exception ex)
            {
                //call handle Error method
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }


        /// <summary>
        /// this is the add item button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbItem.SelectedIndex != -1)
                {
                    //Add the item selected from the combo box to the invoice line items
                    //Update the total cost
                    double itemCost = 0.0;
                    //call get max line item number method
                    invoiceSys.getMaxLineNum(invoiceSys.currentInvoice.InvoiceNum);
                    //add item to database
                    invoiceSys.addInvoiceItem(invoiceSys.maxLineNum + 1, (cbItem.SelectedItem as ItemDesc).ItemCode);
                    //update data grid source
                    dgItem.ItemsSource = invoiceSys.invoiceItem;
                    //add up total cost
                    itemCost = (cbItem.SelectedItem as ItemDesc).Cost;
                    //update total charge
                    invoiceSys.currentInvoice.TotalCharge += itemCost;
                    //update total cost to database
                    invoiceSys.updateTotal(invoiceSys.currentInvoice.TotalCharge);
                    // update total cost label
                    lblPriceNum.Content = invoiceSys.currentInvoice.TotalCharge;
                }
            }
            catch (Exception ex)
            {
                //handle error method
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// this is the search invoice button
        /// open search invoice window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //init new search window
                SearchWindow search = new SearchWindow();
                //hide main window
                Hide();
                //show search window dialog
                search.ShowDialog();
                //empty data grid
                dgItem.ItemsSource = "";
                //refresh data grid
                dgItem.Items.Refresh();
                //if true
                if ((bool)search.DialogResult)
                {
                    //The invoice number in the search window's property should be loaded
                    //and set to display only mode.
                    invoiceSys.updateInvoice(search.SelectedInvoiceNumber);
                    //update invoice number label
                    lblInvoiceNum.Content = invoiceSys.currentInvoice.InvoiceNum.ToString();
                    //update invoice date label
                    dpInvoiceDate.Text = invoiceSys.currentInvoice.InvoiceDate;
                    //update data grid source
                    dgItem.ItemsSource = invoiceSys.invoiceItem;
                    //set item selected to -1
                    cbItem.SelectedIndex = -1;
                    //update item cost to empty
                    lblMoney.Content = "";
                    //update total charge to empty
                    lblPriceNum.Content = invoiceSys.currentInvoice.TotalCharge;
                }
            }
            catch (Exception ex)
            {
                //handle error method
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                Show();
            }


        }

        /// <summary>
        /// this is the method to cancel invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelInvoice_Click(object sender, RoutedEventArgs e)
        {
            //unlock button
            buttonEvent(true);
            //lock invoice group box
            gbInvoice.IsEnabled = false;
            //lock item group box
            gbItem.IsEnabled = false;
        }

        /// <summary>
        /// this is the select item selection change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbItem_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                //if user has selected item
                if (cbItem.SelectedIndex != -1)
                {
                    //update label cost
                    lblMoney.Content = (cbItem.SelectedItem as ItemDesc).Cost;
                }
            }
            catch (Exception ex)
            {
                //handle error method
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// save item method to enable button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //unlock button
                buttonEvent(true);
                //lock invoice group box
                gbInvoice.IsEnabled = false;
                //lock item group box
                gbItem.IsEnabled = false;
                //set default index
                cbItem.SelectedIndex = -1;
                //update label
                lblMoney.Content = "";
            }
            catch (Exception ex)
            {
                //handle error method
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }
        /// <summary>
        /// delete single item from data grid box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                //message box result
                MessageBoxResult result;
                //prompt user to confirm delete item
                result = MessageBox.Show("Are you suer want to delete this item", "confirm", MessageBoxButton.YesNo);
                //if user has selected item
                if (dgItem.SelectedItem != null && result == MessageBoxResult.Yes)
                {
                    //temp cost to hold item cost
                    double tempCost = (dgItem.SelectedItem as LineItem).Cost;
                    //hold invoice number
                    int invoiceNum = (dgItem.SelectedItem as LineItem).InvoiceNum;
                    //hold line item number
                    int lineItemNum = (dgItem.SelectedItem as LineItem).LineItemNum;
                    //call delete invoice item by pass in invoice number and line item number
                    invoiceSys.deleteInvoiceItem(invoiceNum, lineItemNum);
                    //update data grid box
                    dgItem.ItemsSource = invoiceSys.invoiceItem;
                    //calculate total charger
                    invoiceSys.currentInvoice.TotalCharge = invoiceSys.currentInvoice.TotalCharge - tempCost;
                    //update total charger to database
                    invoiceSys.updateTotal(invoiceSys.currentInvoice.TotalCharge);
                    //update label
                    lblPriceNum.Content = invoiceSys.currentInvoice.TotalCharge;
                }
                else
                {
                    //
                    MessageBox.Show("Please select your item", "Select Item");
                }

            }
            catch (Exception ex)
            {
                //handle error method
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}

using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CS3280_Group5_Project
{
    /// <summary>
    /// Interaction logic for ItemDefsWindow.xaml.
    /// Allows the user to view, add, edit, and delete Item definitions
    /// Code by Jeffrey
    /// </summary>
    public partial class ItemDefsWindow : Window
    {
        /// <summary>
        /// Data master class for manipulating database information
        /// </summary>
        private InvoiceDataMaster dataMaster;

        /// <summary>
        /// Set to true if adding a new itemdesc
        /// </summary>
        private bool isNewItem;

        public ItemDefsWindow()
        {
            InitializeComponent();

            dataMaster = new InvoiceDataMaster();
            dgItemDescs.ItemsSource = dataMaster.MakeItemDescList();

            DisableItemEditMode();
        }

        /// <summary>
        /// Enables textboxes and save/cancel buttons.
        /// Disables datagrid and Item buttons
        /// Clears textboxes if parameter is true
        /// </summary>
        /// <param name="isNewItem"></param>
        private void EnableItemEditMode()
        {
            try
            {
                btnSetAddEditDelete.IsEnabled = false;
                btnSetSaveCancel.IsEnabled = true;
                dgItemDescs.IsEnabled = false;
                txtDescription.IsEnabled = true;
                txtCost.IsEnabled = true;
                txtItemCode.Focus();

                if (isNewItem)
                {
                    txtItemCode.IsEnabled = true;
                    txtItemCode.Clear();
                    txtDescription.Clear();
                    txtCost.Clear();
                }
            }
            catch (Exception ex)
            {
                //Throw a custom-trace exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Disables item edit mode.
        /// Enables textbox and item buttons.
        /// Disable textboxes and clears values.
        /// </summary>
        private void DisableItemEditMode()
        {
            try
            {
                btnSetAddEditDelete.IsEnabled = true;
                btnSetSaveCancel.IsEnabled = false;
                dgItemDescs.IsEnabled = true;
                dgItemDescs.SelectedIndex = -1;
                txtItemCode.IsEnabled = false;
                txtDescription.IsEnabled = false;
                txtCost.IsEnabled = false;

                txtItemCode.Clear();
                txtDescription.Clear();
                txtCost.Clear();
            }
            catch (Exception ex)
            {
                //Throw a custom-trace exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Only allow numbers to be entered, with optional period and up 
        /// to 2 additiional numbers. Money.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCost_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                e.Handled = Regex.IsMatch(e.Text, @"[^0-9.]") || !Regex.IsMatch(txtCost.Text, @"^\$?\s?\d*[.]?\d{0,1}$");
            }
            catch (Exception ex)
            {
                //Throw a custom-trace exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
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
                //Get the button that was pressed
                Button btn = sender as Button;

                //Execute action based on the button pressed

                //Save button
                if (sender.Equals(btnSave))
                {
                    ItemDesc item;
                    string oldItemCode;

                    //Check if it's an add or edit
                    if (isNewItem)
                    {
                        item = new ItemDesc();
                        oldItemCode = "NULL";
                    }
                    else
                    {
                        item = dgItemDescs.SelectedItem as ItemDesc;
                        oldItemCode = item.ItemCode;
                    }

                    //Set the information from the text boxes to the item
                    if (!txtItemCode.Text.Equals(""))
                    {
                        item.ItemCode = txtItemCode.Text;
                        txtItemCode.BorderBrush = Brushes.DarkBlue;
                    }
                    else
                    {
                        txtItemCode.BorderBrush = Brushes.Red;
                        return;
                    }

                    if (!txtDescription.Text.Equals(""))
                    {
                        item.Description = txtDescription.Text;
                        txtDescription.BorderBrush = Brushes.DarkBlue;
                    }
                    else
                    {
                        txtDescription.BorderBrush = Brushes.Red;
                        return;
                    }

                    if (double.TryParse(txtCost.Text, out double result))
                    {
                        item.Cost = result;
                        txtCost.BorderBrush = Brushes.DarkBlue;
                    }
                    else
                    {
                        txtCost.BorderBrush = Brushes.Red;
                        return;
                    }

                    //Add or update to the database
                    if (isNewItem)
                    {
                        if (!dataMaster.AddNewItemDesc(item))
                        {
                            MessageBox.Show("Error: Duplicate Item Code.", "Duplicate Item Code", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    else
                        dataMaster.UpdateItemDesc(item);



                    //Refresh items list
                    dgItemDescs.ItemsSource = dataMaster.MakeItemDescList();

                    DisableItemEditMode();
                }
                //Cancel button
                else if (sender.Equals(btnCancel))
                {
                    //refresh item list and disable edit mode
                    dgItemDescs.ItemsSource = dataMaster.MakeItemDescList();
                    DisableItemEditMode();
                }
                //Edit Item button
                else if (sender.Equals(btnEdit))
                {
                    //The datagrid, addeditdelete buttons should be locked,
                    //Unlock savecancel buttons,
                    //Set textboxes Readonly to false

                    if (dgItemDescs.SelectedIndex != -1)
                    {
                        isNewItem = false;
                        EnableItemEditMode();
                    }
                }
                //Add New Item button
                else if (sender.Equals(btnAdd))
                {
                    //enable edit mode for new item     
                    isNewItem = true;
                    EnableItemEditMode();
                }
                //Delete Item button
                else if (sender.Equals(btnDelete))
                {
                    //Confirm deletion of the item
                    if (dgItemDescs.SelectedIndex != -1)
                    {
                        MessageBoxResult result = MessageBox.Show("Delete this item?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            dataMaster.DeleteItemDesc(dgItemDescs.SelectedItem as ItemDesc);
                            dgItemDescs.ItemsSource = dataMaster.MakeItemDescList();
                        }
                    }
                }
                //Close button
                else
                {
                    Close();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("related records"))
                {
                    MessageBox.Show("Cannot Delete Item, because it is actively used in an invoice.", "Delete Item Denied", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                    //Throw a custom-trace exception
                    throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                        MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Event handler for datagrid selection changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgItemDefs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //Clears textboxes or loads them with information for the selection
                if (dgItemDescs.SelectedIndex == -1)
                {
                    txtCost.Clear();
                    txtDescription.Clear();
                    txtItemCode.Clear();
                }
                else
                {
                    ItemDesc item = dgItemDescs.SelectedItem as ItemDesc;

                    txtDescription.Text = item.Description;
                    txtItemCode.Text = item.ItemCode;
                    txtCost.Text = item.Cost.ToString();
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

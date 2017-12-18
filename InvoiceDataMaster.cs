using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace CS3280_Group5_Project
{
    /// <summary>
    /// This class gets dataset information from the database and 
    /// converts it into useable objects (lists, invoices, etc.),
    /// and saves data to the database from objects and lists.
    /// </summary>
    class InvoiceDataMaster
    {
        /// <summary>
        /// clsDataAccess object for db connection
        /// </summary>
        private clsDataAccess DataAccess;

        /// <summary>
        /// InvoiceSQL object, for SQL statements generation
        /// </summary>
        private InvoiceSQL DataSQL;

        /// <summary>
        /// Constructor for InvoiceDataMaster. 
        /// </summary>
        public InvoiceDataMaster()
        {
            DataAccess = new clsDataAccess();
            DataSQL = new InvoiceSQL();
        }

        /// <summary>
        /// Returns a single invoice
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public Invoice GetInvoice(int invoiceNum)
        {
            try
            {
                int rows = 0;
                DataSet ds = DataAccess.ExecuteSQLStatement(DataSQL.SelectInvoice(invoiceNum), ref rows);

                Invoice temp = new Invoice();
                temp.InvoiceNum = invoiceNum;
                if (DateTime.TryParse(ds.Tables[0].Rows[0].ItemArray[1].ToString(), out DateTime date))
                    temp.InvoiceDate = date.ToShortDateString();
                if (double.TryParse(ds.Tables[0].Rows[0].ItemArray[2].ToString(), out double total))
                    temp.TotalCharge = total;

                return temp;
            }
            catch (Exception ex)
            {
                //Throw a custom-trace exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Adds a new invoice to the database, 
        /// and sets the generated invoiceNum
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public void AddNewInvoice(Invoice inv)
        {
            try
            {
                DataAccess.ExecuteNonQuery(DataSQL.InsertInvoice(inv.InvoiceDate, 0));

                //gets the invoice num and sets the property on the object
                if (int.TryParse(DataAccess.ExecuteScalarSQL(DataSQL.SelectLatestInvoiceNum()), out int result))
                    inv.InvoiceNum = result;
            }
            catch (Exception ex)
            {
                //Throw a custom-trace exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes the passed invoice from the database
        /// </summary>
        /// <param name="inv"></param>
        public void DeleteInvoice(Invoice inv)
        {
            try
            {
                DataAccess.ExecuteNonQuery(DataSQL.DeleteInvoice(inv.InvoiceNum));
            }
            catch (Exception ex)
            {
                //Throw a custom-trace exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updates the invoice information on the database for the passed invoice
        /// </summary>
        /// <param name="inv"></param>
        public void UpdateInvoiceTotal(Invoice inv)
        {
            try
            {
                DataAccess.ExecuteNonQuery(DataSQL.UpdateInvoice(inv.InvoiceNum, inv.TotalCharge));
            }
            catch (Exception ex)
            {
                //Throw a custom-trace exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Adds the line item passed in to the database.
        /// </summary>
        /// <param name="item"></param>
        public void AddNewLineItem(LineItem item)
        {
            try
            {
                DataAccess.ExecuteNonQuery(DataSQL.InsertLineItem(item.InvoiceNum, item.LineItemNum, item.ItemCode));
            }
            catch (Exception ex)
            {
                //Throw a custom-trace exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes the line item passed from the database
        /// </summary>
        /// <param name="item"></param>
        public void DeleteLineItem(LineItem item)
        {
            try
            {
                DataAccess.ExecuteNonQuery(DataSQL.DeleteLineItem(item.InvoiceNum, item.LineItemNum));
            }
            catch (Exception ex)
            {
                //Throw a custom-trace exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes the line item passed from the database
        /// </summary>
        /// <param name="invoiceNum">invoice number</param>
        public void DeleteLineItembyInv(int invoiceNum)
        {
            try
            {
                DataAccess.ExecuteNonQuery(DataSQL.DeleteLineItemByInvoice(invoiceNum));
            }
            catch (Exception ex)
            {
                //Throw a custom-trace exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Adds a new itemdesc to the database.
        /// Returns false if the item errors due to duplicate primary key
        /// </summary>
        /// <param name="item"></param>
        public bool AddNewItemDesc(ItemDesc item)
        {
            try
            {
                DataAccess.ExecuteNonQuery(DataSQL.InsertItemDesc(item.ItemCode, item.Description, item.Cost));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Updates the current itemdesc in the database.
        /// Old item code accesses the item if the itemcode was changed
        /// </summary>
        /// <param name="item"></param>
        public void UpdateItemDesc(ItemDesc item)
        {
            try
            {
                DataAccess.ExecuteNonQuery(DataSQL.UpdateItemDesc(item.ItemCode, item.Description, item.Cost));
            }
            catch (Exception ex)
            {
                //Throw a custom-trace exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes the passed itemdesc from the database
        /// </summary>
        /// <param name="item"></param>
        public void DeleteItemDesc(ItemDesc item)
        {
            try
            {
                DataAccess.ExecuteNonQuery(DataSQL.DeleteItemDesc(item.ItemCode));
            }
            catch (Exception ex)
            {
                //Throw a custom-trace exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns a List of all invoices
        /// </summary>
        /// <returns></returns>
        public List<Invoice> MakeInvoiceList()
        {
            try
            {
                //grab all invoices from the database
                int retVal = 0;
                DataSet ds = DataAccess.ExecuteSQLStatement(DataSQL.SelectAllInvoices(), ref retVal);

                List<Invoice> retList = new List<Invoice>();

                for (int i = 0; i < retVal; i++)
                {
                    Invoice temp = new Invoice();

                    if (int.TryParse(ds.Tables[0].Rows[i].ItemArray[0].ToString(), out int num))
                        temp.InvoiceNum = num;

                    if (DateTime.TryParse(ds.Tables[0].Rows[i].ItemArray[1].ToString(), out DateTime date))
                        temp.InvoiceDate = date.ToShortDateString();

                    if (double.TryParse(ds.Tables[0].Rows[i].ItemArray[2].ToString(), out double total))
                        temp.TotalCharge = total;

                    retList.Add(temp);
                }

                return retList;
            }
            catch (Exception ex)
            {
                //Throw a custom-trace exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns a list of line items tied to the selected invoice
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public List<LineItem> MakeLineItemList(int invoiceNum)
        {
            try
            {
                //grab all line items from db
                int retVal = 0;
                DataSet ds = DataAccess.ExecuteSQLStatement(DataSQL.SelectInvoiceItems(invoiceNum), ref retVal);

                List<LineItem> retList = new List<LineItem>();

                LineItem temp;

                for (int i = 0; i < retVal; i++)
                {
                    temp = new LineItem();
                    if (int.TryParse(ds.Tables[0].Rows[i].ItemArray[0].ToString(), out int iNum))
                        temp.InvoiceNum = iNum;
                    if (int.TryParse(ds.Tables[0].Rows[i].ItemArray[1].ToString(), out int lNum))
                        temp.LineItemNum = lNum;
                    temp.ItemCode = ds.Tables[0].Rows[i].ItemArray[2].ToString();
                    temp.ItemDesc = ds.Tables[0].Rows[i].ItemArray[3].ToString();
                    if (double.TryParse(ds.Tables[0].Rows[i].ItemArray[4].ToString(), out double cost))
                        temp.Cost = cost;

                    retList.Add(temp);
                }

                return retList;

            }
            catch (Exception ex)
            {
                //Throw a custom-trace exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns a list of all Item Definitions
        /// </summary>
        /// <returns></returns>
        public List<ItemDesc> MakeItemDescList()
        {
            try
            {
                //grab all itemdescs from the database
                int retVal = 0;
                DataSet ds = DataAccess.ExecuteSQLStatement(DataSQL.SelectAllItemDesc(), ref retVal);

                List<ItemDesc> retList = new List<ItemDesc>();

                for (int i = 0; i < retVal; i++)
                {
                    ItemDesc temp = new ItemDesc();

                    temp.ItemCode = ds.Tables[0].Rows[i].ItemArray[0].ToString();
                    temp.Description = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                    if (double.TryParse(ds.Tables[0].Rows[i].ItemArray[2].ToString(), out double cost))
                        temp.Cost = cost;

                    retList.Add(temp);
                }

                return retList;
            }
            catch (Exception ex)
            {
                //Throw a custom-trace exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// add item to invoice to database
        /// </summary>
        /// <param name="item">Line item table</param>
        public void addItemInvoice(LineItem item)
        {
            try
            {
                int iRetVal = 0;
                DataAccess.ExecuteSQLStatement(DataSQL.InsertLineItem(item.InvoiceNum, item.LineItemNum, item.ItemCode), ref iRetVal);
            }
            catch (Exception ex)
            {

                //Throw a custom-trace exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// delete single line item by invoice number line item number
        /// </summary>
        /// <param name="invoiceNum">invoice number</param>
        /// <param name="lineItemNum">line item number</param>
        public void DeleteLineItemByInvoiceNum(int invoiceNum, int lineItemNum)
        {
            try
            {

                DataAccess.ExecuteNonQuery(DataSQL.DeleteLineItemByInvoice(invoiceNum, lineItemNum));
            }
            catch (Exception ex)
            {

                //Throw a custom-trace exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// get max line number
        /// </summary>
        /// <param name="invoiceNum">invoice number</param>
        /// <returns></returns>
        public int getMaxLineNumber(int invoiceNum)
        {
            try
            {
                int retVal = 0;
                DataSet ds = DataAccess.ExecuteSQLStatement(DataSQL.SelectLatestLineNum(invoiceNum), ref retVal);
                string MaxLineNumber = ds.Tables[0].Rows[0][0].ToString();
                int max = 0;
                Int32.TryParse(MaxLineNumber, out max);
                return max;
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

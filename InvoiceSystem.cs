using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CS3280_Group5_Project
{

    public class InvoiceSystem
    {
        //hold current invoice info
        public Invoice currentInvoice;
        //hold list of item list
        public List<ItemDesc> ItemList;
        //hold list of invoice item
        public List<LineItem> invoiceItem;
        //invoice data master class
        private InvoiceDataMaster dataMaster;

        /// <summary>
        /// the max line number
        /// </summary>
        public int maxLineNum { get; set; }

        /// <summary>
        /// get max line number method
        /// </summary>
        /// <param name="invoiceNum"></param>
        public void getMaxLineNum(int invoiceNum)
        {
            //assign max number
            maxLineNum = dataMaster.getMaxLineNumber(invoiceNum);
        }


        /// <summary>
        /// delete invoice 
        /// </summary>
        public void deleteInvoice()
        {
            try
            {
                //delete invoice by pass in current invoice number
                dataMaster.DeleteLineItembyInv(currentInvoice.InvoiceNum);
                //delete invoice
                dataMaster.DeleteInvoice(currentInvoice);

            }
            catch (Exception ex)
            {
                //Throw a custom-trace exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// delete item from select invoice
        /// </summary>
        /// <param name="invoiceNum">invoice number</param>
        /// <param name="lineItemNum">line item number</param>
        public void deleteInvoiceItem(int invoiceNum, int lineItemNum)
        {
            try
            {
                //delete invoice by pass in current invoice number
                dataMaster.DeleteLineItemByInvoiceNum(invoiceNum, lineItemNum);
                //delete invoice
                dataMaster.MakeLineItemList(currentInvoice.InvoiceNum);
                //update invoice 
                updateInvoice(invoiceNum);
            }
            catch (Exception ex)
            {
                //Throw a custom-trace exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// update total
        /// </summary>
        /// <param name="total">total money</param>
        public void updateTotal(double total)
        {
            try
            {
                //update total charge
                currentInvoice.TotalCharge = total;
                //update total charge to database
                dataMaster.UpdateInvoiceTotal(currentInvoice);
            }
            catch (Exception ex)
            {
                //Throw a custom-trace exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// add new invoice method passing by date
        /// </summary>
        /// <param name="date">date</param>
        public void addInvoice(string date)
        {
            try
            {
                //make new invoice
                Invoice temp = new Invoice();
                //hold date
                temp.InvoiceDate = date;
                //update to database
                dataMaster.AddNewInvoice(temp);
                //update to invoice 
                updateInvoice(temp.InvoiceNum);
            }
            catch (Exception ex)
            {
                //Throw a custom-trace exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// add line item to invoice
        /// </summary>
        /// <param name="LineNum"></param>
        /// <param name="itemCode"></param>
        /// <param name="cost"></param>
        public void addInvoiceItem(int LineNum, string itemCode)
        {
            try
            {
                //make new line item
                LineItem temp = new LineItem();
                //get current invoice number
                temp.InvoiceNum = currentInvoice.InvoiceNum;
                //get item code
                temp.ItemCode = itemCode;
                //get line item number
                temp.LineItemNum = LineNum;
                //insert to database
                dataMaster.AddNewLineItem(temp);
                //make new list from database
                dataMaster.MakeLineItemList(currentInvoice.InvoiceNum);
                //update invoice
                updateInvoice(currentInvoice.InvoiceNum);
            }
            catch (Exception ex)
            {
                //Throw a custom-trace exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// update invoice by pass in invoice number
        /// </summary>
        /// <param name="invoiceNum"></param>
        public void updateInvoice(int invoiceNum)
        {
            try
            {
                //update invoice by passing in invoice number
                currentInvoice = dataMaster.GetInvoice(invoiceNum);
                //make line item list
                invoiceItem = dataMaster.MakeLineItemList(invoiceNum);
            }
            catch (Exception ex)
            {
                //Throw a custom-trace exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
           
        }

        /// <summary>
        /// update item list
        /// </summary>
        public void updateItemList()
        {
            try
            {
                //update item list
                ItemList = dataMaster.MakeItemDescList();
            }
            catch (Exception ex)
            {
                //Throw a custom-trace exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }


        }
        /// <summary>
        /// default constructor
        /// </summary>
        public InvoiceSystem()
        {
            //new invoice data master
            dataMaster = new InvoiceDataMaster();
            //call update item list method
            updateItemList();
        }
    }



    /// <summary>
    /// This class holds the data for a single invoice.
    /// Contains a list of its line items
    /// </summary>
    public class Invoice
    {
        /// <summary>
        /// The ID Number of this invoice
        /// </summary>
        public int InvoiceNum { get; set; }

        /// <summary>
        /// The Date of this invoice
        /// </summary>
        public string InvoiceDate { get; set; }

        /// <summary>
        /// The total charge of all line items
        /// </summary>
        public double TotalCharge { get; set; }

        //Constructor for Invoice
        public Invoice()
        {
            InvoiceNum = 0;
            InvoiceDate = "1/1/2000";
            TotalCharge = 0.00;
        }


    }

    /// <summary>
    /// This class holds data for a single LineItem
    /// </summary>
    public class LineItem
    {
        /// <summary>
        /// The invoice number to which this line item is a component
        /// </summary>
        public int InvoiceNum { get; set; }

        /// <summary>
        /// The line number for this item in the invoice
        /// </summary>
        public int LineItemNum { get; set; }

        /// <summary>
        /// The Item Code of this line item
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// The description of this line item
        /// </summary>
        public string ItemDesc { get; set; }

        /// <summary>
        /// The cost of this line item
        /// </summary>
        public double Cost { get; set; }

        

        /// <summary>
        /// Constructor for LineItem
        /// </summary>
        public LineItem()
        {
            InvoiceNum = 0;
            LineItemNum = 0;
            ItemCode = "NONE";
            ItemDesc = "UNKNOWN";
            Cost = 0;
           
        }
    }

    /// <summary>
    /// This class holds data for an ItemDesc.
    /// Aggregated into LineItem objects
    /// </summary>
    public class ItemDesc
    {
        /// <summary>
        /// This item's ItemCode
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// This item's description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The cost of this item
        /// </summary>
        public double Cost { get; set; }

        /// <summary>
        /// Constructor for ItemDesc
        /// </summary>
        public ItemDesc()
        {
            ItemCode = "A";
            Description = "UNKNOWN";
            Cost = 0;
        }

        /// <summary>
        /// ToString override for display in combo boxes
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ItemCode + " - " + Description;
        }

    }
}

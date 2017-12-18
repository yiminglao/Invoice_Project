/* InvoiceSQL.cs
 * Contains the InvoiceSQL Class to return SQL statements for the Invoice System
 * Jeffrey Hart CS3280 ONL   
 */

namespace CS3280_Group5_Project
{
    /// <summary>
    /// This class contains methods to return SQL statements for the Invoice System
    /// </summary>
    class InvoiceSQL
    {
        /// <summary>
        /// Returns a SQL string to select all invoices
        /// </summary>
        /// <returns></returns>
        public string SelectAllInvoices()
        {
            return "SELECT InvoiceNum, InvoiceDate, TotalCharge FROM Invoices";
        }

        /// <summary>
        /// Returns a SQL string to select a single invoice by Invoice Number
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public string SelectInvoice(int invoiceNum)
        {
            return "SELECT * FROM Invoices WHERE InvoiceNum = " + invoiceNum;
        }

        /// <summary>
        /// Returns a SQL string to select the InvoiceNum of the last added invoice
        /// </summary>
        /// <returns></returns>
        public string SelectLatestInvoiceNum()
        {
            return "SELECT MAX(InvoiceNum) FROM Invoices";
            
        }

        /// <summary>
        /// Returns a SQL string to select the LineItemNum of the invoice
        /// </summary>
        /// <returns></returns>
        public string SelectLatestLineNum(int invoiceNum)
        {
            return "SELECT MAX(LineItemNum) FROM LineItems WHERE InvoiceNum = " + invoiceNum;
        }

        /// <summary>
        /// Returns a SQL string to Insert a new invoice in the the Invoices table
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <param name="totalCharge"></param>
        /// <returns></returns>
        public string InsertInvoice(string invoiceDate, double totalCharge)
        {
            return "INSERT INTO Invoices(InvoiceDate, TotalCharge)" +
                "Values(#" + invoiceDate + "#, " + totalCharge + ")";
        }

        /// <summary>
        /// Returns a SQL string to Update an Invoice's total charge in the Invoices table
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="totalCharge"></param>
        /// <returns></returns>
        public string UpdateInvoice(int invoiceNum, double totalCharge)
        {
            return "UPDATE Invoices SET TotalCharge = " + totalCharge +
                " WHERE InvoiceNum = " + invoiceNum;
        }

        /// <summary>
        /// Returns a SQL string to Delete an invoice from the Invoices table
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public string DeleteInvoice(int invoiceNum)
        {
            return "DELETE FROM Invoices WHERE InvoiceNum = " + invoiceNum;
        }

        /// <summary>
        /// Returns a SQL string to Select all items for the invoiceNum passed in,
        /// Includes ItemDesc information for each line item.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public string SelectInvoiceItems(int invoiceNum)
        {
            return "SELECT LineItems.InvoiceNum, LineItems.LineItemNum, LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost" +
                " FROM LineItems, ItemDesc" +
                " WHERE LineItems.InvoiceNum = " + invoiceNum.ToString() +
                " AND LineItems.ItemCode = ItemDesc.ItemCode";
        }

        /// <summary>
        /// Returns a SQL string to add a new item to the selected invoice
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="lineItemNum"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string InsertLineItem(int invoiceNum, int lineItemNum, string itemCode)
        {
            return "INSERT INTO LineItems(InvoiceNum, LineItemNum, ItemCode) " +
                "VALUES(" + invoiceNum + ", " + lineItemNum + ", '" + itemCode + "')";
        }

        /// <summary>
        /// Returns a SQL string to delete the selected item from the selected invoice
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="lineItemNum"></param>
        /// <returns></returns>
        public string DeleteLineItem(int invoiceNum, int lineItemNum)
        {
            return "DELETE FROM Invoices " +
                "WHERE InvoiceNum = " + invoiceNum + " AND LineItemNum = " + lineItemNum;
        }

        /// <summary>
        /// delete line item for table by passing invoice number
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public string DeleteLineItemByInvoice(int invoiceNum)
        {
            return "DELETE FROM LineItems " +
                "WHERE InvoiceNum = " + invoiceNum;
        }

        /// <summary>
        /// delete line item for table by passing invoice number and lineItemNum
        /// </summary>
        /// <param name="invoiceNum">invoice Number</param>
        /// <param name="lineItemNum">line Item Number</param>
        /// <returns></returns>
        public string DeleteLineItemByInvoice(int invoiceNum, int lineItemNum)
        {
            return "DELETE FROM LineItems " +
                "WHERE InvoiceNum = " + invoiceNum + " AND LineItemNum = " + lineItemNum;
        }


        /// <summary>
        /// Returns a SQL string to select all items from the ItemDesc table
        /// </summary>
        /// <returns></returns>
        public string SelectAllItemDesc()
        {
            return "SELECT * FROM ItemDesc";
        }

        /// <summary>
        /// Returns a SQL string to select a single itemDesc by itemCode
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string SelectItemDesc(string itemCode)
        {
            return "SELECT * FROM ItemDesc WHERE ItemCode = '" + itemCode + "'";
        }

        /// <summary>
        /// Returns a SQL string to insert a new item into the ItemDesc table
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="itemDesc"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        public string InsertItemDesc(string itemCode, string itemDesc, double cost)
        {
            return "INSERT INTO ItemDesc(ItemCode, ItemDesc, Cost) " +
                "VALUES('" + itemCode + "', '" + itemDesc + "', " + cost + ")";
        }

        /// <summary>
        /// Returns a SQL string to update an item in the ItemDesc table
        /// </summary>
        /// <param name="OldItemCode"></param>
        /// <param name="ItemCode"></param>
        /// <param name="itemDesc"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        public string UpdateItemDesc(string ItemCode, string itemDesc, double cost)
        {
            return "UPDATE ItemDesc SET ItemDesc = '" + itemDesc +
                "', Cost = " + cost + " WHERE ItemCode = '" + ItemCode + "'"; 
        }

        /// <summary>
        /// Returns a SQL string to delete an item from the ItemDesc table
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string DeleteItemDesc(string itemCode)
        {
            return "DELETE FROM ItemDesc WHERE ItemCode = '" + itemCode + "'";
        }

    }
}

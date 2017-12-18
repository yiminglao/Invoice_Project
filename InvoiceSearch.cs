using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CS3280_Group5_Project
{
    /// <summary>
    /// This class holds a list of all invoices, and adds filters to a filtered list.
    /// </summary>
    class InvoiceSearch
    {
        /// <summary>
        /// Generates Invoice Lists
        /// </summary>
        private InvoiceDataMaster dataMaker;

        /// <summary>
        /// list of invoices
        /// </summary>
        public List<Invoice> Invoices { get; set; }

        /// <summary>
        /// Filter for invoice number
        /// </summary>
        public string InvoiceNumFilter { get; set; }

        /// <summary>
        /// Filter for invoice date
        /// </summary>
        public string InvoiceDateFilter { get; set; }

        /// <summary>
        /// Filter for total cost
        /// </summary>
        public string TotalChargeFilter { get; set; }

        /// <summary>
        /// Constructor for InvoiceSearch
        /// </summary>
        public InvoiceSearch()
        {
            try
            {          
                dataMaker = new InvoiceDataMaster();
                ClearFilters(); //call clearfilters to get initial invoice list
            }
            catch (Exception ex)
            {
                //Throw a custom-trace exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Generates a fresh list of Invoices, unfiltered
        /// </summary>
        private void RefreshInvoices()
        {
            try
            {
                Invoices = dataMaker.MakeInvoiceList();
            }
            catch (Exception ex)
            {
                //Throw a custom-trace exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Clears filters and calls RefreshInvoices
        /// </summary>
        public void ClearFilters()
        {
            try
            {
                InvoiceNumFilter = "";
                InvoiceDateFilter = "";
                TotalChargeFilter = "";

                RefreshInvoices();
            }
            catch (Exception ex)
            {
                //Throw a custom-trace exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Applies filters from properties to a fresh list
        /// </summary>
        public void ApplyFilters()
        {
            try
            {
                //Get a fresh set of data
                RefreshInvoices();

                //Check which filters are not blank and filter accordingly
                if(!InvoiceNumFilter.Equals(""))
                {
                    for(int i = 0; i < Invoices.Count; i++)
                    {
                        if (!Invoices[i].InvoiceNum.ToString().Contains(InvoiceNumFilter))
                            Invoices.RemoveAt(i--);
                    }
                }

                if(!InvoiceDateFilter.Equals(""))
                {
                    for (int i = 0; i < Invoices.Count; i++)
                    {
                        if (!Invoices[i].InvoiceDate.Contains(InvoiceDateFilter))
                            Invoices.RemoveAt(i--);
                    }
                }

                if (!TotalChargeFilter.Equals(""))
                {
                    for (int i = 0; i < Invoices.Count; i++)
                    {
                        if (!Invoices[i].TotalCharge.ToString().Contains(TotalChargeFilter))
                            Invoices.RemoveAt(i--);
                    }
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

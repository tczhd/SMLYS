using SMLYS.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.DTOs.SearchIndex
{
    public class SearchIndexFields
    {
        public static List<string> GetSelectFields(IndexNameType indexNameType)
        {
            List<string> fields = new List<string>();
            switch (indexNameType)
            {
                case IndexNameType.Patient:
                    fields = new List<String>() {"Id", "FirstName", "LastName", "Title", "Gender",
                        "Age", "Phone", "Email", "Address1","City", "Region", "PostalCode", "DoctorFirstName", "DoctorLastName"};
                    break;
                case IndexNameType.Invoice:
                    fields = new List<String>() {"Id", "InvoiceDate","UserFirstName","UserLastName",  "FirstName", "LastName",
                        "Phone", "Email","Address1","City","Region", "PostalCode", "DoctorFirstName", "DoctorLastName"};
                    break;
                case IndexNameType.Service:
                    fields = new List<String>() {"Id", "Name", "Description", "Cost"};
                    break;
            }

            return fields;
        }

        public static List<string> GetHighlightFields(IndexNameType indexNameType)
        {
            List<string> fields = new List<string>();
            switch (indexNameType)
            {
                case IndexNameType.Patient:
                    fields = new List<String>() {"FirstName", "LastName"};
                    break;
                case IndexNameType.Invoice:
                    fields = new List<String>() { "FirstName", "LastName"};
                    break;
                case IndexNameType.Service:
                    fields = new List<String>() { "Name"};
                    break;
            }

            return fields;
        }
    }
}
